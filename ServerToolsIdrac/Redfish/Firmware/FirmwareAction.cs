using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Firmware
{
    public class FirmwareAction
    {
        // Uris to Firmware actions
        public const string FirmwareInventory = @"/redfish/v1/UpdateService/FirmwareInventory";
        public const string DellUpdateService = @"/redfish/v1/UpdateService/Actions/Oem/DellUpdateService.Install";
        public const string SimpleUpdate = @"/redfish/v1/UpdateService/Actions/UpdateService.SimpleUpdate";

        private ConnectionInfo connection;
        private RestClient client;

        /// <summary>
        /// Creates a new Firmware action instance using Basic Authentication
        /// </summary>
        /// <param name="connection">Server Connection information</param>
        public FirmwareAction(ConnectionInfo connection)
        {
            this.connection = connection;
            client = new RestClient(string.Format("https://{0}", connection.Host));
            client.Authenticator = new HttpBasicAuthenticator(connection.User, connection.Password);
            // Ignore Certificate
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Upload the firmware file to Idrac asynchronously
        /// </summary>
        /// <param name="path">File Path</param>
        /// <returns>Location for the resource created</returns>
        private async Task<string> UploadFileAsync(string path)
        {
            var request = new RestRequest(FirmwareInventory, Method.POST);
            request.AddFile("firmware", path);
            string etag = await GetEtagHeaderAsync();
            request.AddHeader("If-Match", etag);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to upload the file");

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
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to get Etag Header");

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
            if (!await ConnectionUtil.CheckConnectionAsync(connection.Host))
                throw new Exception(string.Format("servidor {0} inacessivel", connection.Host));

            string location = await UploadFileAsync(path);

            List<string> uris = new List<string>()
            {
                location
            };

            var request = new RestRequest(DellUpdateService, Method.POST, DataFormat.Json);

            var body = new
            {
                SoftwareIdentityURIs = uris,
                InstallUpon = option
            };

            request.AddJsonBody(body);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("fail to create update Job");

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }
    }
}
