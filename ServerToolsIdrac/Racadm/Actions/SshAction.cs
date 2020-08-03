using Renci.SshNet;
using Renci.SshNet.Common;
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
        private readonly string host;
        private readonly NetworkCredential credential;

        public SshAction(string host, NetworkCredential credential)
        {
            this.host = host;
            this.credential = credential;
        }

        private void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    prompt.Response = credential.Password;
                }
            }
        }

        public async Task<SshResponse> RunCommandAsync(string command)
        {
            SshResponse result = null;

            if (!await ConnectionUtil.CheckConnectionAsync(host))
                throw new Exception(string.Format("Server {0} unreachable", host));

            await Task.Run(() =>
            {
                KeyboardInteractiveAuthenticationMethod keyboardbAuthentication = new KeyboardInteractiveAuthenticationMethod(credential.UserName);
                PasswordAuthenticationMethod pauth = new PasswordAuthenticationMethod(credential.UserName, credential.Password);
                keyboardbAuthentication.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                ConnectionInfo connectionInfo = new ConnectionInfo(host, 22, credential.UserName, pauth, keyboardbAuthentication);

                using (SshClient client = new SshClient(connectionInfo))
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
                KeyboardInteractiveAuthenticationMethod keyboardbAuthentication = new KeyboardInteractiveAuthenticationMethod(credential.UserName);
                PasswordAuthenticationMethod pauth = new PasswordAuthenticationMethod(credential.UserName, credential.Password);
                keyboardbAuthentication.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                ConnectionInfo connectionInfo = new ConnectionInfo(host, 22, credential.UserName, pauth, keyboardbAuthentication);

                using (SshClient client = new SshClient(connectionInfo))
                {
                    try
                    {
                        client.Connect();

                        foreach (var command in commands)
                        {
                            var commandResult = client.RunCommand(command);
                            result = new SshResponse(commandResult.Result, commandResult.ExitStatus);
                        }
                    }
                    catch (Exception e)
                    {
                        result = new SshResponse(e.Message, -1);
                    }
                }
            });
            return result;
        }
    }
}
