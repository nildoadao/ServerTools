using RestSharp;
using RestSharp.Authenticators;
using ServerToolsIdrac.Redfish.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private readonly IRestClient restClient;
        private readonly string host;
        private readonly NetworkCredential credentials;

        public FirmwareAction(string host, NetworkCredential credentials)
        {
            this.host = host;
            restClient = new RestClient(string.Format("https://{0}", host))
            {
                Authenticator = new NtlmAuthenticator(credentials)
            };
            this.credentials = credentials;
            // Ignore SSL Certificate
            restClient.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Upload the firmware file to Idrac asynchronously
        /// </summary>
        /// <param name="path">File Path</param>
        /// <returns>Location for the resource created</returns>
        private async Task<string> UploadFileAsync(string path)
        {
            var boundary = Guid.NewGuid().ToString();
            using (var request = new HttpRequestMessage(HttpMethod.Post, string.Format("https://{0}{1}", host, FirmwareInventory)))
            using (var multipartContent = new MultipartFormDataContent(boundary))
            using (var fileContent = new StreamContent(File.Open(path, FileMode.Open)))
            {
                var credentialsHeader = string.Format("{0}:{1}", credentials.UserName, credentials.Password);
                request.Headers.Authorization = 
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(credentialsHeader))); 

                var etag = await GetEtagHeaderAsync();
                request.Headers.TryAddWithoutValidation("If-Match", etag);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "\"file\"",
                    FileName = string.Format("\"{0}\"", Path.GetFileName(path)),
                };
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                // Idrac 7/8 don't accept boundary in quotes, so we have to manualy add the header
                multipartContent.Headers.Remove("Content-Type");
                multipartContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
                multipartContent.Add(fileContent);
                request.Content = multipartContent;

                using (HttpResponseMessage response = await HttpUtil.Client.SendAsync(request))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        throw new UnauthorizedAccessException("Access denied, check user/password");

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException("Fail to Upload Firmware to Idrac: " + response.ReasonPhrase);

                    return response.Headers.Location.ToString();
                }
            }
        }

        /// <summary>
        /// Returns the Etag Header
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetEtagHeaderAsync()
        {
            var request = new RestRequest(FirmwareInventory);
            request.AddHeader("Accept", "*/*");
            var response = await restClient.ExecuteTaskAsync(request);

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
            var response = await restClient.ExecuteTaskAsync(request);

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
