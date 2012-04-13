using System.Linq;

namespace SkypeBot.Commands.Macros
{
    public class MacroCommand : BasicCommand
    {
        const string Token = "!macro";

        readonly Writer writer;
        readonly MacroStore macroStore;

        public MacroCommand(Writer writer, MacroStore macroStore) : base(Token)
        {
            this.writer = writer;
            this.macroStore = macroStore;
        }

        public override void Process(Message message)
        {
            var usersMacros = macroStore.All
                .Where(x => x.UserId == message.SenderId)
                .ToList();

            var request = message.Body.Substring(Token.Length).Trim();

            if (string.IsNullOrWhiteSpace(request))
            {
                if (usersMacros.Count == 0)
                {
                    writer.Write("> User has no macros stored. Try '!macro !spall !spell' to create one and '!spall' to use it.");
                    return;
                }

                writer.Write("> User currently has the following macros stored:\n");

                foreach (var macro in usersMacros)
                    writer.Write("> " + macro.Alias + " " + macro.Command);
                
                return;
            }

            var mid = request.IndexOf(' ');

            if (mid == -1)
            {
                var macro = usersMacros.SingleOrDefault(x => x.Alias == request);

                if (macro == null)
                {
                    writer.Write("> Tried to delete a macro but none with that alias was found.");
                    return;
                }

                macroStore.Remove(macro);
                writer.Write("> Macro removed.");
                return;
            }

            var alias = request.Substring(0, mid);

            if (!alias.StartsWith("!"))
            {
                InvalidFormat();
                return;
            }

            var existing = usersMacros.SingleOrDefault(x => x.Alias == alias);

            if (existing != null)
            {
                macroStore.Remove(existing);
                writer.Write("> Old macro removed.");
            }

            var command = request.Substring(mid + 1).Trim();

            macroStore.Add(new Macro(
                message.SenderId,
                alias,
                command));

            writer.Write("> Macro stored. Use '" + alias + "' to run it or '!macro " + alias + "' to delete it.");
        }

        void InvalidFormat()
        {
            writer.Write(
                "> Tried to store a macro but the format of the macro is incorrect.\n" +
                "> Please specify a single word alias beginning with a ! followed by the command which should run instead.\n" +
                "> In the following example '!sword' is the alias and '!roll d20+7 2d8+3' is the command.\n" +
                "> !macro !sword !roll d20+7 2d8+3");
        }
    }
}
