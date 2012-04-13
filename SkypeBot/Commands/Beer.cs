namespace SkypeBot.Commands
{
    public class Beer : BasicCommand
    {
        readonly Writer writer;

        public Beer(Writer writer) : base("!beer")
        {
            this.writer = writer;
        }

        public override void Process(Message message)
        {
            writer.Write("> Cheers! (beer)");
        }
    }
}
