using System;
using System.Linq;
using Ninject;

namespace SkypeBot
{
    public class CommandInvoker
    {
        readonly IKernel kernel;
        readonly Writer writer;

        public CommandInvoker(IKernel kernel, Writer writer)
        {
            this.kernel = kernel;
            this.writer = writer;
        }

        public void Process(Message message)
        {
            var commands = kernel.GetAll<ICommand>().ToList();

            var command =
                commands.FirstOrDefault(x => x.Trigger.Match(message) == MatchType.Strong) ??
                commands.FirstOrDefault(x => x.Trigger.Match(message) == MatchType.Weak);

            if (command == null)
            {
                writer.Write("> I don't know that command sorry.");
                return;
            }

            try
            {
                command.Process(message);
            }
            catch (Exception ex)
            {
                writer.Write("> Something went horribly wrong: " + ex.Message);
            }
        }
    }
}