using Ninject;
using System.Linq;

namespace SkypeBot.Commands
{
    public class Help : BasicCommand
    {
        readonly Writer writer;
        readonly IKernel kernel;

        public Help(Writer writer, IKernel kernel) : base("!help")
        {
            this.writer = writer;
            this.kernel = kernel;
        }

        public override void Process(Message message)
        {
            var triggers = kernel.GetAll<ICommand>()
                .Select(x => x.Trigger.Name)
                .OrderBy(x => x);

            writer.Write(
                "> I currently understand the following commands:\n" +
                "> " + string.Join(", ", triggers));
        }
    }
}
