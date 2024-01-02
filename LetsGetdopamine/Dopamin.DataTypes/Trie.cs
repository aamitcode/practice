using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dopamin.DataTypes
{
    public class Trie<T> : ITrie<T> where T : IComparable
    {
        private readonly TrieNode<T> root;
        private readonly T special;

        public Trie(T special)
        {
            root = new TrieNode<T>();
            this.special = special;
        }
        public T Get(T[] components)
        {
            return SeachTrie(components, root, 0);
        }

        public void Register(T[] components,T data)
        {
            var pCrawler = root;

            foreach(var component in components)
            {
                if (!pCrawler.Children.TryGetValue(component, out var child))
                {
                    child = new TrieNode<T>();
                    pCrawler.Children.Add(component, child);
                }
                pCrawler = child;
            }
            pCrawler.IsLeaf = true;
            pCrawler.Value = data;
        }

        private T SeachTrie(T[] prefixes,TrieNode<T> node, int index)
        {
            if(prefixes.Length == index)
            {
                return node != null && node.IsLeaf ? node.Value : default(T);
            }

            var pCrawler = node;
            var pComponent = prefixes[index];
            if(special.CompareTo(pComponent) == 0)
            {
                T result = default(T);
                foreach (var entry in pCrawler.Children)
                {
                    var temp = entry.Value;
                    result = SeachTrie(prefixes, temp, index + 1);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return result;
            }
            else if (pCrawler != null && pCrawler.Children != null && pCrawler.Children.ContainsKey(pComponent))
            {
                return SeachTrie(prefixes, pCrawler.Children[pComponent], index + 1);
            }
            else
            {
                return default(T);
            }


        }
    }
}
