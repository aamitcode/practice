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
            
            var teamVoteBank = buildVoteBankTracker(votes);
            Dictionary<char, int> score = new Dictionary<char, int>();
            
            foreach(var teamWithVotes in teamVoteBank)
            {
                int weight = votes[0].Length;
                score.Add(teamWithVotes.Key, 0);
                foreach(var sc in teamWithVotes.Value)
                {
                    score[teamWithVotes.Key] += sc * weight;
                    weight--;
                }
            }

            
            var teams = votes[0].ToCharArray();

            //Sorting
            Array.Sort(teams, (x, y) => sort(x, y, teamVoteBank));

            return new string(teams);
        }

        public int sort(char x, char y, Dictionary<char, int> teamVoteBank)
        {
            if (teamVoteBank[x] != teamVoteBank[y])
            {

                return teamVoteBank[y] - teamVoteBank[x];
            }
            return x - y;
        }

        public int sort(char x, char y, Dictionary<char, int[]> teamVoteBank)
        {
            for (int i = 0; i < teamVoteBank.Count; i++)
            {
                if (teamVoteBank[x][i] != teamVoteBank[y][i])
                {

                    return teamVoteBank[y][i] - teamVoteBank[x][i];
                }
            }
            return x - y;
        }

        public Dictionary<char, int> buildVoteBank(string[] votes)
        {
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
            return teamVoteBank;
        }
        public Dictionary<char, int[]> buildVoteBankTracker(string[] votes)
        {
            int numberofVoters = votes.Length;
            int numberofTeams = votes[0].Length;
            Dictionary<char, int[]> teamVoteBank = new Dictionary<char, int[]>();
            foreach (var userVote in votes)
            {
                int pos = 0;
                foreach (var teamVote in userVote)
                {
                    if (!teamVoteBank.ContainsKey(teamVote))
                    {
                        teamVoteBank.Add(teamVote, new int[userVote.Length]);
                    }
                    teamVoteBank[teamVote][pos]++;
                    pos++;
                }
            }
            return teamVoteBank;
        }
    }
}
