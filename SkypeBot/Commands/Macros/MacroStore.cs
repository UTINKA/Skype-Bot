using System.Collections.Generic;

namespace SkypeBot.Commands.Macros
{
    public class MacroStore
    {
        readonly List<Macro> macros = new List<Macro>();

        public IEnumerable<Macro> All
        {
            get { return macros; }
        }

        public void Add(Macro macro)
        {
            macros.Add(macro);
        }

        public void Remove(Macro macro)
        {
            macros.Remove(macro);
        }
    }
}
