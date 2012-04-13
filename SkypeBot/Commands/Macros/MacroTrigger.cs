using System.Linq;

namespace SkypeBot.Commands.Macros
{
    public class MacroTrigger : ITrigger
    {
        readonly MacroStore macroStore;

        public MacroTrigger(MacroStore macroStore)
        {
            this.macroStore = macroStore;
        }

        public string Name { get { return "User Macros"; } }

        public MatchType Match(Message message)
        {
            var usersMacros = macroStore.All
                .Where(x => x.UserId == message.SenderId)
                .ToList();

            return (usersMacros.Any(x => x.Alias == message.Body))
                       ? MatchType.Weak
                       : MatchType.None;
        }
    }
}