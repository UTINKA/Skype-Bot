namespace SkypeBot
{
    public interface ITrigger
    {
        MatchType Match(Message message);

        string Name { get; }
    }
}