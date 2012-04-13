using System;
using System.Text;
using System.Linq;

namespace SkypeBot.Commands.DiceRolling
{
    public class DiceRoller : BasicCommand
    {
        const string Token = "!roll";

        readonly Writer writer;
        readonly DiceParser parser;
        readonly Random random = new Random();

        public DiceRoller(Writer writer, DiceParser parser)
            : base(Token)
        {
            this.writer = writer;
            this.parser = parser;
        }

        public override void Process(Message message)
        {
            DiceRoll[] rolls;

            try
            {
                rolls = parser
                    .Parse(message.Body.Substring(Token.Length))
                    .ToArray();
            }
            catch (ArgumentException)
            {
                writer.Write("> Sorry I don't understand. Try something like '!roll d20 2d4 2d8+7'.");
                return;
            }

            var output = new StringBuilder();

            foreach (var roll in rolls)
            {
                if (roll.Times < 1)
                {
                    writer.Write("> I can't roll less than 1 die.");
                    return;
                }

                if (roll.Times > 100)
                {
                    writer.Write("> I don't have that many dice on hand sorry!");
                    return;
                }

                if (roll.Sides < 2)
                {
                    writer.Write("> Dice with less than two sides confuse me.");
                    return;
                }

                if (roll.Sides > 1000)
                {
                    writer.Write("> Those dice have so many sides I can't read them any more!");
                    return;
                }

                if (roll.Bonus > 1000)
                {
                    writer.Write("> Not sure if cheating or incompetent.");
                    return;
                }

                var total = roll.Bonus;
                var results = new int[roll.Times];

                for (var i = 0; i < roll.Times; i++)
                {
                    var result = random.Next(1, roll.Sides + 1);

                    results[i] = result;
                    total += result;
                }

                output.Append("> ");
                output.Append(roll.Original);
                output.Append(": ");

                if (roll.Times == 1 && roll.Bonus == 0)
                {
                    output.Append(total);
                    output.Append("\n");
                }
                else
                {
                    output.Append(string.Join(" + ", results));

                    if (roll.Bonus > 0)
                    {
                        output.Append(" + ");
                        output.Append(roll.Bonus);
                    }

                    output.Append(" = ");
                    output.Append(total);
                    output.Append("\n");
                }
            }

            output.Remove(output.Length - 1, 1);

            writer.Write(output.ToString());
        }
    }
}
