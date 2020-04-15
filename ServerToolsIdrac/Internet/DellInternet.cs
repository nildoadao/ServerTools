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
    public class DellInternet
    {
        private readonly IRestClient client;

        public DellInternet(string serviceTag)
        {
            client = new RestClient(string.Format("https://www.dell.com/support/components/dashboard/br/pt/brbstd1/configuration/export?serviceTag={0}", serviceTag));
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErros) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
        }

        public async Task<string> GetDellProcessorAsync()
        {
            var request = new RestRequest();
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful)
                throw new Exception(string.Format("Fail to get Processor, error {0}", 
                    response.ErrorMessage));

            var lines = response.Content.Split('\n');
            string processor = "Não encontrado";

            foreach(var line in lines)
            {
                if (line.ToLower().Contains("ghz"))
                {
                    processor = line.Substring(11);
                    processor = processor.Split(',')[0];
                    break;
                }
            }
            Thread.Sleep(6000);
            return processor;
        }
    }
}
