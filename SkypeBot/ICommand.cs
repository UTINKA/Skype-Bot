namespace SkypeBot
{
    public interface ICommand
    {
        ITrigger Trigger { get; }

        void Process(Message message);
    }
}
