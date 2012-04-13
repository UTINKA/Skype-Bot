namespace SkypeBot.Commands
{
    public class Quit : BasicCommand
    {
        readonly Writer writer;
        readonly QuitSignal quitSignal;

        public Quit(Writer writer, QuitSignal quitSignal) : base("!quit")
        {
            this.writer = writer;
            this.quitSignal = quitSignal;
        }

        public override void Process(Message message)
        {
            if(message.SenderId != "barry.dahlberg")
            {
                writer.Write(
                    "> Authenticating command...\n" +
                    "> Request denied...\n" +
                    "> Report filed against user '" + message.SenderDisplayName + "'.");
                return;
            }

            writer.Write("> Bye.");

            /*
            writer.Write(
                "> Authenticating command...\n" +
                "> Greetings, Master.");

            writer.Write(
                "> Suspending sentience simulation...\n" +
                "> Deallocating transient neural networks...\n" +
                "> Protocol termination in progress...\n" +
                "> EOF.");
            */

            quitSignal.Quit();
        }
    }
}
