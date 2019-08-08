using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Actions
{
    public class TaskAction
    {
        // Uris to Task Actions
        public const string TaskStatus = @"/redfish/v1/TaskService/Tasks/";

        private IRestClient client;
        private string host;

        public TaskAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<string> GetTaskIdAsync(string jobUri)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            var request = new RestRequest(jobUri);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to retrive Task, Error Code {0}",
                    response.StatusCode));

            JObject responseJson = JObject.Parse(response.Content);
            return responseJson["Id"].ToString();
        }

    }
}
