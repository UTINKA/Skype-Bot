using System;
using System.Linq;

namespace SkypeBot.Commands.Macros
{
    public class MacroRunner : ICommand
    {
        readonly MacroStore macroStore;
        readonly MacroTrigger trigger;
        readonly CommandInvoker commandInvoker;

        public ITrigger Trigger { get { return trigger; } }

        public MacroRunner(MacroStore macroStore, MacroTrigger trigger, CommandInvoker commandInvoker)
        {
            this.macroStore = macroStore;
            this.trigger = trigger;
            this.commandInvoker = commandInvoker;
        }

        public void Process(Message message)
        {
            var macro = macroStore.All.Single(x => x.UserId == message.SenderId && x.Alias == message.Body);

            // TODO: Smarter detection of recursive macros
            if (string.Equals(message.Body, macro.Command, StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("Infinite macro definition.");

            message.Body = macro.Command;
            commandInvoker.Process(message);
        }
    }
}
