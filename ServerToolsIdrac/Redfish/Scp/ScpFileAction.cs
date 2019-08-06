﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
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
          
            string jobUri = await CreateExportJobAsync(target, exportUse);
            return await GetFileContentAsync(jobUri);   
        }

        public async Task<string> ImportScpFileAsync(string path, string target, string shutdownType, string powerStatus)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Servidor {0} inacessivel", host));

            var request = new RestRequest(ImportSystemConfiguration, Method.POST, DataFormat.Json);
            var body = BuildRequestBody(path, target, shutdownType, powerStatus);
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

        private object BuildRequestBody(string path, string target, string shutdownType, string powerStatus)
        {
            string file = File.ReadAllText(path);
            return  new
            {
                ImportBuffer = file,
                ShareParameters = new
                {
                    Target = target
                },
                ShutdownType = shutdownType,
                HostPowerState = powerStatus
            };
        }

        private async Task<string> GetFileContentAsync(string jobUri)
        {
            var request = new RestRequest(jobUri);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to retrive SCP File, Error Code {0}", 
                    response.StatusCode));

            return response.Content;
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
                throw new RedfishException(string.Format("Fail to create Export Job, Error Code {0}",
                    response.StatusCode));
            
            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }
    }
}