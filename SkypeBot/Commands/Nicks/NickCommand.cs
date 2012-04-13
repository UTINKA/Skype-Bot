namespace SkypeBot.Commands.Nicks
{
    public class NickCommand : BasicCommand
    {
        const string Token = "!nick";

        readonly Writer writer;
        readonly NickStore nickStore;

        public NickCommand(Writer writer, NickStore nickStore)
            : base(Token)
        {
            this.writer = writer;
            this.nickStore = nickStore;
        }

        public override void Process(Message message)
        {
            var nick = message.Body.Substring(Token.Length).Trim();

            if (string.IsNullOrWhiteSpace(nick))
            {
                if (nickStore.Nicks.ContainsKey(message.SenderId))
                    nickStore.Nicks.Remove(message.SenderId);

                writer.Write("> Nick cleared.");
            }
            else
            {
                nickStore.Nicks[message.SenderId] = nick;
                writer.Write("> Nick stored.");
            }
        }
    }
}
