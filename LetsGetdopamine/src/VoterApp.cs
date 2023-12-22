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
            int[,] teamVoteBank = new int[numberofTeams, 1];
            foreach (var userVote in votes)
            {
                int weight = numberofTeams;
                foreach (var teamVote in userVote)
                {
                    teamVoteBank[teamVote - 'A', 0] = teamVoteBank[teamVote - 'A', 0] + weight;
                    weight--;
                }
            }
            var teams = votes[0].ToCharArray();

            //Sorting
            Array.Sort(teams, (x,y) =>
            {
                if (teamVoteBank[x - 'A', 0] != teamVoteBank[y - 'A', 0])
                {

                    return teamVoteBank[y - 'A', 0] - teamVoteBank[x - 'A', 0];
                }
                return y - x;
            });
            return new string(teams);
        }

    }
}
