using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Internet
{
    public class HpeInternet
    {
        private readonly IRestClient client;
        
        public HpeInternet(string serialNumber)
        {
            client = new RestClient(string.Format("https://partsurfer.hpe.com/Search.aspx?searchText={0}", serialNumber));
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErros) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
        }

        public async Task<string> GetProcessorAsync()
        {
            var request = new RestRequest();
            request.AddHeader("Accept", "*/*");
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new Exception(string.Format("Fail to get Processor, Error Code {0}",
                    response.StatusCode));

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response.Content);
            string processorName = "Não encontrado";

            foreach(var element in document.DocumentNode.SelectNodes("//span"))
            {
                if (element.InnerText.ToLower().Contains("ghz"))
                {
                    processorName = element.InnerText;
                    break;
                }
            }
            Thread.Sleep(6000);
            return processorName;
        }
    }
}
