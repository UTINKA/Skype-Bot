using System.Configuration;
using SkypeBot.TeamCity;

namespace SkypeBot.Commands
{
    public class TeamCityComamnd : BasicCommand
    {
        private const string Token = "!tc";

        private readonly TeamCityService _teamCityService;
        private readonly Writer _writer;

        public TeamCityComamnd(Writer writer) : base(Token)
        {
            _writer = writer;
            _teamCityService = new TeamCityService(ConfigurationManager.AppSettings["teamcity-url"],
                                                   ConfigurationManager.AppSettings["teamcity-login"],
                                                   ConfigurationManager.AppSettings["teamcity-password"]);
        }

        public override void Process(Message message)
        {
            if (message.Body.Contains("status"))
            {
                string buildStatuses = _teamCityService.GetBuildStatuses();
                _writer.Write(buildStatuses);
            }

            if (message.Body.Contains("add2queue"))
            {
                string buildName = message.Body.Replace(Token, string.Empty).Replace("add2queue", string.Empty).Trim();
                _teamCityService.Add2Queue(buildName);
                _writer.Write(buildName + "added to queue");
            }
        }
    }
}