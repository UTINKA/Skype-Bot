namespace SkypeBot.Commands
{
    public class Ping : BasicCommand
    {
        readonly Writer writer;

        public Ping(Writer writer) : base("!ping")
        {
            this.writer = writer;
        }

        public override void Process(Message message)
        {
            writer.Write("> Pong!");
        }
    }
}
