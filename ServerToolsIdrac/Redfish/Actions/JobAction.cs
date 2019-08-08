using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Models;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Actions
{
    public class JobAction
    {
        // Uris to Job Actions
        public const string JobStatus = @"/redfish/v1/Managers/iDRAC.Embedded.1/Jobs/";
        public const double JobTimeout = 600;

        private IRestClient client;
        private string host;

        public JobAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<Job> GetJobAsync(string jobId)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            var request = new RestRequest(JobStatus + jobId, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to read Job Status, Error code {0}", 
                    response.StatusCode));

            try
            {
                return JsonConvert.DeserializeObject<Job>(response.Content);
            }
            catch(Exception ex)
            {
                throw new RedfishException(string.Format("Fail to read Job: {0}", ex.Message));
            }
        }

    }
}
