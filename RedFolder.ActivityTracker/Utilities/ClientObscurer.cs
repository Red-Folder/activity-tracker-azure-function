using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Utilities
{
    public class ClientObscurer
    {
        private Dictionary<string, string> _mappings = new Dictionary<string, string>();
        private int _counter = 0;

        public string Obscure(string client)
        {
            if (client == "RFC") return "Own Projects";

            if (_mappings.ContainsKey(client)) return _mappings[client];

            _counter++;
            var newName = $"Client {NumberToString(_counter, true)}";
            _mappings.Add(client, newName);
            return newName;
        }

        private String NumberToString(int number, bool isCaps)

        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));

            return c.ToString();
        }
    }
}
