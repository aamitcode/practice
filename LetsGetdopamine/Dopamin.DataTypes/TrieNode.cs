namespace Dopamin.DataTypes
{
    internal class TrieNode<T>
    {
        public T Value { get; set; }
        public Dictionary<T, TrieNode<T>> Children { get; set;} = new Dictionary<T, TrieNode<T>>();
        public bool IsLeaf { get; set; }
    }
}
