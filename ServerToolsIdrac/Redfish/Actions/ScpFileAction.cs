using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Models;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Actions
{
    public class ScpFileAction
    {
        // Uris to SCP Actions
        public const string ExportSystemConfiguration = @"/redfish/v1/Managers/iDRAC.Embedded.1/Actions/Oem/EID_674_Manager.ExportSystemConfiguration";
        public const string ImportSystemConfiguration = @"/redfish/v1/Managers/iDRAC.Embedded.1/Actions/Oem/EID_674_Manager.ImportSystemConfiguration";

        private readonly IRestClient client;
        private readonly string host;
        private readonly NetworkCredential credentials;

        /// <summary>
        /// Creates a new SCP file action
        /// </summary>
        /// <param name="host">Host to perform SCP actions</param>
        /// <param name="credentials">Idrac credentials</param>
        public ScpFileAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            this.credentials = credentials;
            client = new RestClient(string.Format("https://{0}", host))
            {
                Authenticator = new NtlmAuthenticator(credentials)
            };

            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Exports an SCP file
        /// </summary>
        /// <param name="target">SCP target</param>
        /// <param name="exportUse">Scp use strategy</param>
        /// <returns></returns>
        public async Task<string> ExportScpFileAsync(string target, string exportUse)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            string jobUri = await CreateExportJobAsync(target, exportUse);
            return await GetFileContentAsync(jobUri);   
        }

        /// <summary>
        /// Performs an SCP File Import
        /// </summary>
        /// <param name="path">Local SCP file path</param>
        /// <param name="target">SCP Target</param>
        /// <param name="shutdownType">Shutdown strategy</param>
        /// <param name="powerStatus">Host power status</param>
        /// <returns></returns>
        public async Task<string> ImportScpFileAsync(string path, string target, string shutdownType, string powerStatus)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            var request = new RestRequest(ImportSystemConfiguration, Method.POST, DataFormat.Json);
            request.AddHeader("Accept", "*/*");

            string file = File.ReadAllText(path);
            var body = new
            {
                ImportBuffer = file,
                ShareParameters = new
                {
                    Target = target
                },
                ShutdownType = shutdownType,
                HostPowerState = powerStatus
            };

            request.AddJsonBody(body);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to import the file, Error Code {0}", 
                    response.StatusCode));

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        /// <summary>
        /// Returns the SCP File Content
        /// </summary>
        /// <param name="jobUri">Scp Task Uri</param>
        /// <returns>File Text</returns>
        private async Task<string> GetFileContentAsync(string jobUri)
        {
            var request = new RestRequest(jobUri);
            request.AddHeader("Accept", "*/*");
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to retrive SCP File, Error Code {0}",
                    response.StatusCode));

            TaskAction task = new TaskAction(host, credentials);
            string jobId = await task.GetTaskIdAsync(jobUri);
            await WaitJobAsync(jobId);
            response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to retrive SCP File, Error Code {0}",
                    response.StatusCode));

            return response.Content;
        }

        /// <summary>
        /// Waits until the Job is completed
        /// </summary>
        /// <param name="jobId">Job Id for watch</param>
        /// <returns>True when the Job is finished</returns>
        private async Task<bool> WaitJobAsync(string jobId)
        {
            JobAction action = new JobAction(host, credentials);
            DateTime start = DateTime.Now;

            Job job = new Job()
            {
                JobState = ""
            };

            while (!job.JobState.Equals("Completed"))
            {
                job = await action.GetJobAsync(jobId);

                if (job.JobState.Contains("Failed"))
                    throw new RedfishException(string.Format("{0} Has failed", jobId));

                if (DateTime.Now > start.AddSeconds(JobAction.JobTimeout))
                    throw new TimeoutException(string.Format("{0} Time exceeded", jobId));
            }
            return true;
        }

        private async Task<string> CreateExportJobAsync(string target, string exportUse)
        {
            var request = new RestRequest(ExportSystemConfiguration, Method.POST);
            request.AddHeader("Accept", "*/*");

            var body = new
            {
                ExportFormat = "XML",
                ShareParameters = new
                {
                    Target = target
                },
                ExportUse = exportUse
            };

            request.AddJsonBody(body);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to create Export Job, Error Code {0}",
                    response.StatusCode));
            
            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }
    }
}
