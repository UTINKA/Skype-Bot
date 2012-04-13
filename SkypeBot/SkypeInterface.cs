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

            Chat = Skype.Chat["#isomnicron/$c09ef07e4f1de141"];
        }
    }
}