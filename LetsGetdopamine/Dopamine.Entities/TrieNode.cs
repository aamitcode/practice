using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine.Entities
{
    internal class TrieNode
    {
        public string Val { get; set; }
        public Dictionary<string, TrieNode> Children { get; set; }
        public bool IsEnd { get; set; } = false;

        public TrieNode(string val)
        {
            this.Val = val;
            Children = new Dictionary<string, TrieNode>();
        }
    }
}
