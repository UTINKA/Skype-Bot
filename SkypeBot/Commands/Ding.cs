namespace SkypeBot.Commands
{
    public class Ding : BasicCommand
    {
        readonly Writer writer;

        public Ding(Writer writer) : base("!ding")
        {
            this.writer = writer;
        }

        public override void Process(Message message)
        {
            writer.Write("> Dong... the witch is dead!");
        }
    }
}
