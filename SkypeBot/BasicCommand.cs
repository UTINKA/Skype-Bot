namespace SkypeBot
{
    public abstract class BasicCommand : ICommand
    {
        protected BasicCommand(string token)
        {
            Trigger = new StartsWithTrigger(token);
        }

        public ITrigger Trigger { get; private set; }

        public abstract void Process(Message message);
    }
}