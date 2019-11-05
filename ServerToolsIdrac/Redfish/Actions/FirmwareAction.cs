﻿using RestSharp;
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
    public class FirmwareAction
    {
        // Uris to Firmware actions
        public const string FirmwareInventory = @"/redfish/v1/UpdateService/FirmwareInventory";
        public const string DellUpdateService = @"/redfish/v1/UpdateService/Actions/Oem/DellUpdateService.Install";
        public const string SimpleUpdate = @"/redfish/v1/UpdateService/Actions/UpdateService.SimpleUpdate";

        private readonly IRestClient client;
        private readonly string host;

        public FirmwareAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            client = new RestClient(string.Format("https://{0}", host))
            {
                Authenticator = new NtlmAuthenticator(credentials)
            };
            // Ignore SSL Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Upload the firmware file to Idrac asynchronously
        /// </summary>
        /// <param name="path">File Path</param>
        /// <returns>Location for the resource created</returns>
        private async Task<string> UploadFileAsync(string path)
        {
            var request = new RestRequest(FirmwareInventory, Method.POST);
            request.AddHeader("Accept", "*/*");
            request.AddFile("firmware", path);
            string etag = await GetEtagHeaderAsync();
            request.AddHeader("If-Match", etag);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to upload the file, Error Code {0}",
                    response.StatusCode));

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        /// <summary>
        /// Returns the Etag Header
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetEtagHeaderAsync()
        {
            var request = new RestRequest(FirmwareInventory);
            request.AddHeader("Accept", "*/*");
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to get Etag Header, Error Code {0}",
                    response.StatusCode));

            return response.Headers
                .Where(x => x.Name == "ETag")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        /// <summary>
        /// Performs a firmware update from a local file 
        /// </summary>
        /// <param name="path">Fila Path</param>
        /// <param name="option">Update mode</param>
        /// <returns>Location for Update Job</returns>
        public async Task<string> UpdateFirmwareAsync(string path, string option)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            string location = await UploadFileAsync(path);

            List<string> uris = new List<string>()
            {
                location
            };

            var request = new RestRequest(DellUpdateService, Method.POST, DataFormat.Json);
            request.AddHeader("Accept", "*/*");

            var body = new
            {
                SoftwareIdentityURIs = uris,
                InstallUpon = option
            };

            request.AddJsonBody(body);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException(string.Format("Fail to create update Job, Error Code {0}", 
                    response.StatusCode));

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }
    }
}
