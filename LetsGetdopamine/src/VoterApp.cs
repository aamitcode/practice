using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public partial class Solution
    {

        public string RankTeams(string[] votes)
        {
            if (votes.Length < 1)
            {
                return string.Empty;
            }
            int numberofVoters = votes.Length;
            int numberofTeams = votes[0].Length;
            Dictionary<char, int> teamVoteBank = new Dictionary<char, int>();
            foreach (var userVote in votes)
            {
                int weight = numberofTeams;
                foreach (var teamVote in userVote)
                {
                    if (!teamVoteBank.ContainsKey(teamVote))
                    {
                        teamVoteBank.Add(teamVote, 0);
                    }
                    teamVoteBank[teamVote] = teamVoteBank[teamVote] + weight;
                    weight--;
                }
            }
            var teams = votes[0].ToCharArray();

            //Sorting
            Array.Sort(teams, (x, y) =>
            {
                if (teamVoteBank[x] != teamVoteBank[y])
                {

                    return teamVoteBank[y] - teamVoteBank[x];
                }
                return x - y;
            });
            return new string(teams);
        }
    }
}
