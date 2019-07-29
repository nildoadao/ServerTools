using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Job;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Scp
{
    public class ScpFileAction
    {
        // Uris to SCP Actions
        public const string ExportSystemConfiguration = @"/redfish/v1/Managers/iDRAC.Embedded.1/Actions/Oem/EID_674_Manager.ExportSystemConfiguration";
        public const string ImportSystemConfiguration = @"/redfish/v1/Managers/iDRAC.Embedded.1/Actions/Oem/EID_674_Manager.ImportSystemConfiguration";

        private IRestClient client;
        private string host;
        private NetworkCredential credentials;

        public ScpFileAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            this.credentials = credentials;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<string> ExportScpFileAsync(string target, string exportUse)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("servidor {0} inacessivel", host));
          
            string jobUri = await CreateExportJobAsync(target, exportUse);           
            DateTime start = DateTime.Now;

            while (true)
            {
                JobAction action = new JobAction(host, credentials);
                IdracJob job = await action.GetJobAsync(jobUri);

                if (job.JobState.Contains("Completed"))
                    return await action.GetJobResultAsync(job.Id); 

                else if (job.JobState.Contains("Failed"))
                    throw new RedfishException(string.Format("Fail to execute the Job: {0}", job.Message));

                else if (DateTime.Now >= start.AddMinutes(JobAction.JobTimeout))
                    throw new TimeoutException("Job Time exceeded");
            }         
        }

        public async Task<string> ImportScpFileAsync(string path, string target, string shutdownType, string powerStatus)
        {
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

            var request = new RestRequest(ImportSystemConfiguration, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to import the file");

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        private async Task<string> CreateExportJobAsync(string target, string exportUse)
        {
            var request = new RestRequest(ExportSystemConfiguration, Method.POST);
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
                throw new RedfishException("Fail to create Export Job");
            
            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }
    }
}
