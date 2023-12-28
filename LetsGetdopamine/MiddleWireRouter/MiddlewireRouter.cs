using Dopamine.Entities;

namespace MiddleWireRouter
{
    public class MiddlewireRouter : IMiddlewireRouter
    {
        private readonly Trie trie;

        public MiddlewireRouter()
        {
            trie = new Trie();
        }
        public string route(string path)
        {
            return trie.Find(path.Split("/"));
        }

        public void withRoute(string path, string result)
        {
            trie.Register(path.Split("/"), result);
        }
    }
}
