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

namespace ServerToolsIdrac.Redfish.Chassis
{
    public class ChassisAction
    {
        // Uris to Chassis actions
        public const string ChassisRoot = @"/redfish/v1/Chassis/System.Embedded.1";

        private IRestClient client;
        private string host;

        public ChassisAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host));
            client.Authenticator = new NtlmAuthenticator(credentials);
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<string> GetServiceTagAsync()
        {
            if(!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Servidor {0} inacessivel", host));

            var request = new RestRequest(ChassisRoot);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to get Service Tag, Error code {0}",
                    response.StatusCode));

            JObject responseBody = JObject.Parse(response.Content);
            return responseBody["SKU"].ToString();
        }
    }
}
