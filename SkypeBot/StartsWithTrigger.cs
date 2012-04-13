using System;

namespace SkypeBot
{
    public class StartsWithTrigger : ITrigger
    {
        readonly string token;

        public StartsWithTrigger(string token)
        {
            this.token = token;
        }

        public string Name
        {
            get { return token; }
        }

        public MatchType Match(Message message)
        {
            if (message.Body.StartsWith(token, StringComparison.CurrentCultureIgnoreCase))
                return MatchType.Strong;

            return MatchType.None;
        }
    }
}