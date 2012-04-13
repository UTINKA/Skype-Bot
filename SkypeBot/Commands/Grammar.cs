namespace SkypeBot.Commands
{
    public class Grammar : BasicCommand
    {
        readonly Writer writer;

        public Grammar(Writer writer) : base("!grammar")
        {
            this.writer = writer;
        }

        public override void Process(Message message)
        {
            writer.Write(
                "> Use 'your' or 'their' only if it belongs to them.\n" +
                "> They're and you're are for when the 'are' something.\n" +
                "> Apostrophes are never for plurals. Use them for posession and contraction.");
        }
    }
}