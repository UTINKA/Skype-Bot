using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace SkypeBot.Commands
{
    public class TeamCityComamnd : BasicCommand
    {
        private const string Token = "!tc";
        private readonly TeamCityClient _client;
        private readonly Writer _writer;

        public TeamCityComamnd(Writer writer) : base(Token)
        {
            _writer = writer;
            _client = new TeamCityClient(ConfigurationManager.AppSettings["teamcity-url"]);
            _client.Connect(ConfigurationManager.AppSettings["teamcity-login"],
                            ConfigurationManager.AppSettings["teamcity-password"]);
        }

        public override void Process(Message message)
        {
            Console.WriteLine(message.Body);
            if (message.Body.Contains("status"))
            {
                List<BuildConfig> buildConfigs = _client.AllBuildConfigs();
                var sb = new StringBuilder();
                foreach (BuildConfig buildConfig in buildConfigs)
                {
                    Build build = _client.LastBuildByBuildConfigId(buildConfig.Id);
                    string status = build.Status == "FAILURE" ? "(devil)" : "";
                    sb.AppendLine(buildConfig.Name + " " + build.Status + " " + status);
                }
                _writer.Write(sb.ToString());
            }
        }
    }
}