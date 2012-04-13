using System.Text;

namespace SkypeBot.Commands.Nicks
{
    public class WhoCommand : BasicCommand
    {
        readonly Writer writer;
        readonly NickStore nickStore;

        public WhoCommand(Writer writer, NickStore nickStore) : base("!who")
        {
            this.writer = writer;
            this.nickStore = nickStore;
        }

        public override void Process(Message message)
        {
            var output = new StringBuilder();
            output.Append("> I currently know the following nicks:\n");

            foreach (var nick in nickStore.Nicks)
            {
                output.Append(nick.Key);
                output.Append(": ");
                output.Append(nick.Value);
                output.Append("\n");
            }

            output.Remove(output.Length - 1, 1);

            writer.Write(output.ToString());
        }
    }
}
