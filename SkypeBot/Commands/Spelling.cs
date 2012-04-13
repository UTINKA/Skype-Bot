namespace SkypeBot.Commands
{
    public class Spelling : BasicCommand
    {
        readonly Writer writer;

        public Spelling(Writer writer) : base("!spell")
        {
            this.writer = writer;
        }

        public override void Process(Message message)
        {
            writer.Write("> Friend. Guard. Tuesday.");
        }
    }
}