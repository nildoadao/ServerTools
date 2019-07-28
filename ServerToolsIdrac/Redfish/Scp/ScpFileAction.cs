using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Job;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
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

        private RestClient client;
        private string host;

        public ScpFileAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<string> ExportScpFileAsync(string target, string exportUse)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("servidor {0} inacessivel", host));

            string exportJob = await CreateExportJobAsync(target, exportUse);
            var jobAction = new JobAction(host, client);
            DateTime start = DateTime.Now;
            IdracJob job = await jobAction.GetJobAsync(exportJob);

            while (true)
            {
                job = await jobAction.GetJobAsync(exportJob);

                if (job.JobState.Contains("Completed"))
                    break;

                else if (job.JobState.Contains("Failed"))
                    throw new RedfishException(string.Format("Fail to execute the Job: {0}", job.Message));

                else if (DateTime.Now >= start.AddMinutes(JobAction.JobTimeout))
                    throw new TimeoutException("Job Time exceeded");
            }

            return await jobAction.GetJobResultAsync(job.Id);
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
