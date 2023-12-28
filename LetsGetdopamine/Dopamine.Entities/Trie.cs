using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine.Entities
{
    public class Trie
    {
        private TrieNode root = new TrieNode(string.Empty);
        public void Register(string[] components, string data)
        {
            TrieNode pCrawl = root;
            foreach (string component in components)
            {
                if (!pCrawl.Children.TryGetValue(component, out TrieNode? value))
                {
                    value = new TrieNode(string.Empty);
                    pCrawl.Children.Add(component, value);
                }
                pCrawl = value;
            }
            pCrawl.IsEnd = true;
            pCrawl.Val = data;
        }
        public string Find(string[] components)
        {
            TrieNode pCrawl = root;
            string result = SearchTrie(components, 0, root);
            return result == null ? "notFound!" : result;
        }

        private string SearchTrie(string[] components, int index, TrieNode node)
        {
            int size = components.Length;

            if (index == size)// Last Item in the list
            {
                return node != null && node.IsEnd ? node.Val : string.Empty; // return node or empty string 
            }
            
            TrieNode pCrawl = node;
            string component = components[index];

            if ("*".Equals(component))
            {
                string result = string.Empty;
                foreach (var entry in pCrawl.Children)
                {
                    TrieNode temp = entry.Value;
                    result = SearchTrie(components, index + 1, temp);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
                return result;
            }
            // current node is not null and children contain component, continue the search
            else if (pCrawl != null && pCrawl.Children != null && pCrawl.Children.ContainsKey(component))
            {
                return SearchTrie(components, index + 1, pCrawl.Children[component]);
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
