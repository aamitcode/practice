using Dopamin.DataTypes;
using Dopamine.Entities;

namespace MiddleWireRouter
{
    public class MiddlewireRouter : IMiddlewireRouter
    {
        private readonly Trie<string> trie;

        public MiddlewireRouter()
        {
            trie = new Trie<string>("*");
        }
        public string route(string path)
        {
            return trie.Get(path.Split("/"));
        }

        public void withRoute(string path, string result)
        {
            trie.Register(path.Split("/"), result);
        }
    }
}
