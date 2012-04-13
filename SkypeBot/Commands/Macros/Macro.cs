namespace SkypeBot.Commands.Macros
{
    public class Macro
    {
        public string UserId { get; private set; }
        public string Alias { get; private set; }
        public string Command { get; private set; }

        public Macro(string userId, string alias, string command)
        {
            UserId = userId;
            Alias = alias;
            Command = command;
        }
    }
}