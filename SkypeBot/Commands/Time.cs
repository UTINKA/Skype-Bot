using System;

namespace SkypeBot.Commands
{
    public class Time : BasicCommand
    {
        readonly Writer writer;
        readonly TimeZoneInfo nz = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
        readonly TimeZoneInfo eu = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

        public Time(Writer writer) : base("!time")
        {
            this.writer = writer;
            nz = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            eu = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        }

        public override void Process(Message message)
        {
            var utcNow = DateTime.UtcNow;
            var nzNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, nz);
            var euNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, eu);

            writer.Write(string.Format("> It is currently {0}, {1} in NZ and {2}, {3} in Switzerland.",
                nzNow.DayOfWeek.ToString(),
                nzNow.ToShortTimeString(),
                euNow.DayOfWeek.ToString(),
                euNow.ToShortTimeString()));
        }
    }
}