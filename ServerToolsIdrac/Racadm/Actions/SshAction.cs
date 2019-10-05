using Renci.SshNet;
using ServerToolsIdrac.Racadm.Model;
using ServerToolsIdrac.Racadm.Util;
using ServerToolsIdrac.Redfish.Util;
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

        public async Task<SshResponse> RunCommandAsync(string command)
        {
            SshResponse result = null;

            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            await Task.Run(() =>
            {
                using (SshClient client = new SshClient(host, credential.UserName, credential.Password))
                {
                    try
                    {
                        client.Connect();
                        var commandResult = client.RunCommand(command);
                        result = new SshResponse(commandResult.Result, commandResult.ExitStatus);
                    }
                    catch(Exception e)
                    {
                        result = new SshResponse(e.Message, -1);
                    }
                }
            });

            return result;
        }

        public async Task<SshResponse> RunScriptAsync(IEnumerable<string> commands)
        {
            SshResponse result = null;

            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            await Task.Run(() =>
            {
                using (SshClient client = new SshClient(host, credential.UserName, credential.Password))
                {
                    try
                    {
                        client.Connect();

                        foreach (var command in commands)
                        {
                            var commandResult = client.RunCommand(command);
                            result = new SshResponse(commandResult.Result, commandResult.ExitStatus);

                            if (commandResult.ExitStatus != 0 | commandResult.Result.Contains("ERROR"))
                                break;
                        }
                    }
                    catch(Exception e)
                    {
                        result = new SshResponse(e.Message, -1);
                    }
                }
            });
            return result;
        }
    }
}
