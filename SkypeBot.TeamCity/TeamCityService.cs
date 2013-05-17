using System;
using System.Collections.Generic;
using System.Text;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace SkypeBot.TeamCity
{
    public class TeamCityService
    {
        private readonly TeamCityClient _client;

        public TeamCityService(string url, string login, string password)
        {
            _client = new TeamCityClient(url);
            _client.Connect(login, password);
        }

        public string GetBuildStatuses()
        {
            List<BuildConfig> buildConfigs = _client.AllBuildConfigs();
            var sb = new StringBuilder();
            foreach (BuildConfig buildConfig in buildConfigs)
            {
                Build build = _client.LastBuildByBuildConfigId(buildConfig.Id);
                string status = build.Status == "FAILURE" ? "(devil)" : "";
                sb.AppendLine(buildConfig.Name + " " + build.Status + " " + status);
            }
            return sb.ToString();
        }

        public void Add2Queue(string buildName)
        {
            throw new NotImplementedException();
        }
    }
}