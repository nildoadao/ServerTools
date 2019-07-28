using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Job
{
    public class JobAction
    {
        // Uris to Job Actions
        public const string JobStatus = @"/redfish/v1/Managers/iDRAC.Embedded.1/Jobs/";
        public const string JobResult = @"/redfish/v1/TaskService/Tasks/";
        public const double JobTimeout = 10;

        private RestClient client;
        private string host;

        public JobAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public JobAction(string host, RestClient client)
        {
            this.host = host;
            this.client = client;
        }

        public async Task<IdracJob> GetJobAsync(string jobUri)
        {
            var request = new RestRequest(jobUri, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to read Job Status");

            try
            {
                IdracJob job = JsonConvert.DeserializeObject<IdracJob>(response.Content);
                return job;
            }
            catch(Exception ex)
            {
                throw new RedfishException(string.Format("Fail to read Job: {0}", ex.Message));
            }
        }

        public async Task<string> GetJobResultAsync(string jobId)
        {
            var request = new RestRequest(JobResult + jobId, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to read Job Result");

            return response.Content;
        }
    }
}
