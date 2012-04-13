using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SkypeBot.Commands.DiceRolling
{
    public class DiceParser
    {
        readonly Regex pattern = new Regex(@"^(\d*)d(\d+)(?:\+(\d+))?$");

        public IEnumerable<DiceRoll> Parse(string spec)
        {
            var rollSpecs = spec
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            if (rollSpecs.Length == 0)
                throw new ArgumentException("No rolls specified.");

            foreach (var rollSpec in rollSpecs)
            {
                var match = pattern.Match(rollSpec);

                if (!match.Success)
                    throw new ArgumentException("Failed to parse roll '" + rollSpec + "'.");

                var roll = new DiceRoll
                {
                    Original = rollSpec
                };

                try
                {
                    if (!string.IsNullOrWhiteSpace(match.Groups[1].Value))
                        roll.Times = int.Parse(match.Groups[1].Value);
                    else
                        roll.Times = 1;

                    roll.Sides = int.Parse(match.Groups[2].Value);

                    if (!string.IsNullOrWhiteSpace(match.Groups[3].Value))
                        roll.Bonus = int.Parse(match.Groups[3].Value);
                    else
                        roll.Bonus = 0;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Failed to parse roll '" + rollSpec + "'.", ex);
                }

                yield return roll;
            }
        }
    }
}