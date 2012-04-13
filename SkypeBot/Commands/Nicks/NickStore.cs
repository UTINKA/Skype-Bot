using System.Collections.Generic;

namespace SkypeBot.Commands.Nicks
{
    public class NickStore
    {
        public IDictionary<string, string> Nicks { get; private set; }

        public NickStore()
        {
            Nicks = new Dictionary<string, string>();
        }
    }
}
