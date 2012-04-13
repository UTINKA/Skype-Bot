using System.Reflection;
using Ninject;
using SKYPE4COMLib;
using System.Linq;
using SkypeBot.Commands.Macros;
using SkypeBot.Commands.Nicks;

namespace SkypeBot
{
    class Program
    {
        readonly SkypeInterface skypeInterface;
        readonly CommandInvoker commandInvoker;
        readonly QuitSignal quitSignal;

        static void Main()
        {
            var kernel = new StandardKernel();

            kernel.Bind<QuitSignal>().ToSelf().InSingletonScope();
            kernel.Bind<MacroStore>().ToSelf().InSingletonScope();
            kernel.Bind<NickStore>().ToSelf().InSingletonScope();

            var commands = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof (ICommand).IsAssignableFrom(x))
                .Where(x => !x.IsAbstract && !x.IsInterface);

            foreach (var command in commands)
                kernel.Bind<ICommand>().To(command);

            kernel.Get<Program>().Run();
        }

        public Program(SkypeInterface skypeInterface, CommandInvoker commandInvoker, QuitSignal quitSignal)
        {
            this.skypeInterface = skypeInterface;
            this.commandInvoker = commandInvoker;
            this.quitSignal = quitSignal;
        }

        void Run()
        {
            skypeInterface.Skype.MessageStatus += ChatMessageStatus;

            skypeInterface.Chat.SendMessage("> Online.");

            /*
            skypeInterface.Chat.SendMessage(
                "> Connection established...\n" +
                "> Biological lifeforms detected...\n" +
                "> Initiating communication protocol 'HUMAN'.");
             */

            quitSignal.Wait();
        }

        void ChatMessageStatus(ChatMessage message, TChatMessageStatus status)
        {
            if (status != TChatMessageStatus.cmsSent && status != TChatMessageStatus.cmsReceived)
                return;

            if (string.CompareOrdinal(message.ChatName, skypeInterface.Chat.Name) != 0)
                return;

            if (!message.Body.StartsWith("!"))
                return;
            
            commandInvoker.Process(new Message
            {
                Body = message.Body,
                SenderId = message.Sender.Handle,
                SenderDisplayName = message.Sender.FullName
            });
        }
    }
}
