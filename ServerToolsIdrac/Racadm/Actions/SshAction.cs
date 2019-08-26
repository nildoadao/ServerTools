using Renci.SshNet;
using ServerToolsIdrac.Racadm.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Racadm.Actions
{
    public class SshAction
    {
        private string host;
        private NetworkCredential credential;

        public SshAction(string host, NetworkCredential credential)
        {
            this.host = host;
            this.credential = credential;
        }

        public async Task<string> RunCommandAsync(string command)
        {
            string result = "";

            await Task.Run(() =>
            {
                using (SshClient client = new SshClient(host, credential.UserName, credential.Password))
                {
                    client.Connect();
                    var commandResult = client.RunCommand(command);

                    if (commandResult.ExitStatus != 0 | commandResult.Result.Contains("ERROR"))
                        throw new RacadmException(commandResult.Result);

                    result = commandResult.Result;
                }
            });

            return result;
        }

        public Task RunScriptAsync(IEnumerable<string> commands)
        {
            return Task.Run(() =>
            {
                using (SshClient client = new SshClient(host, credential.UserName, credential.Password))
                {
                    client.Connect();

                    foreach (var command in commands)
                    {
                        var commandResult = client.RunCommand(command);

                        if (commandResult.ExitStatus != 0 | commandResult.Result.Contains("ERROR"))
                            throw new RacadmException(commandResult.Result);
                    }
                }
            });
        }
    }
}
