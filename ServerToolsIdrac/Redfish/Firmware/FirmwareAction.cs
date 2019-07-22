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

        public FirmwareAction(ConnectionInfo connection)
        {
            this.connection = connection;
            client = new RestClient(string.Format("https://{0}", connection.Host));
            client.Authenticator = new HttpBasicAuthenticator(connection.User, connection.Password);
        }

        private async Task<string> UploadFileAsync(string path)
        {
            var request = new RestRequest(FirmwareInventory, Method.POST, DataFormat.Json);
            request.AddFile("firmware", path);
            string etag = await GetEtagHeaderAsync();
            request.AddHeader("Etag", etag);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to upload the file");

            return response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        private async Task<string> GetEtagHeaderAsync()
        {
            var request = new RestRequest(FirmwareInventory);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new RedfishException("Fail to get Etag Header");

            return response.Headers
                .Where(x => x.Name == "Etag")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
        }

        public async Task<string> UpdateFirmwareAsync(string path, string option)
        {
            if (!await ConnectionUtil.CheckConnectionAsync(connection.Host))
                throw new Exception(string.Format("servidor {0} inacessivel", connection.Host));

            string location = await UploadFileAsync(path);
            List<string> uris = new List<string>()
            {
                location
            };

            var request = new RestRequest(DellUpdateService, Method.POST);
            request.Parameters.Add(new Parameter("SoftwareIdentityURIs", uris, ParameterType.RequestBody));
            request.Parameters.Add(new Parameter("InstallUpon", option, ParameterType.RequestBody));
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
