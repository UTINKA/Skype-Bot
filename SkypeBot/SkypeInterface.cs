using System.Configuration;
using SKYPE4COMLib;

namespace SkypeBot
{
    public class SkypeInterface
    {
        public Skype Skype { get; private set; }
        public Chat Chat { get; private set; }

        public SkypeInterface()
        {
            Skype = new Skype();
            Skype.Attach();

            Chat = Skype.Chat[ConfigurationManager.AppSettings["chatname"]];
        }
    }
}