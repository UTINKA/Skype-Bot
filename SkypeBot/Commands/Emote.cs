using SkypeBot.Commands.Nicks;

namespace SkypeBot.Commands
{
    public class Emote : BasicCommand
    {
        const string Token = "!me";

        readonly Writer writer;
        readonly NickStore nickStore;

        public Emote(Writer writer, NickStore nickStore) : base(Token)
        {
            this.writer = writer;
            this.nickStore = nickStore;
        }

        public override void Process(Message message)
        {
            var command = message.Body.Substring(Token.Length).Trim();
            var nick = nickStore.Nicks.ContainsKey(message.SenderId)
                ? nickStore.Nicks[message.SenderId]
                : message.SenderDisplayName;

            writer.Write("> " + nick + " " + command);
        }
    }
}
