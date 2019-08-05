using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.Util
{
    /// <summary>
    /// Mediator class to handle messages between views
    /// </summary>
    public class NavigationUtil
    {
        public static IDictionary<string, List<Action<object>>> messages =
            new Dictionary<string, List<Action<object>>>();
         
        public static void Register(string token, Action<object> action)
        {
            if (!messages.ContainsKey(token))
            {
                var list = new List<Action<object>>()
                {
                    action
                };
                messages.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach(var item in messages[token])
                {
                    if (item.Method.ToString().Equals(action.Method.ToString()))
                        found = true;

                    if (!found)
                        messages[token].Add(action);
                }
            }
        }

        public static void UnRegister(string token, Action<object> action)
        {
            if (messages.ContainsKey(token))
                messages[token].Remove(action);
        }

        public static void NotifyColleagues(string token, object args)
        {
            if (messages.ContainsKey(token))
                foreach (var callback in messages[token])
                    callback(args);
        }
    }
}
