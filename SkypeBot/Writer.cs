namespace SkypeBot
{
    public class Writer
    {
        readonly SkypeInterface skypeInterface;

        public Writer(SkypeInterface skypeInterface)
        {
            this.skypeInterface = skypeInterface;
        }

        public void Write(string message)
        {
            skypeInterface.Chat.SendMessage(message);
        }
    }
}