namespace FileSystem.Collections
{
    public class Collection : IComparable<Collection>
    {
        private readonly string directoryName;
        private readonly Dictionary<string, File> files;
        private int collectionSize;
        public Collection(string directoryName)
        {
            this.directoryName = directoryName;
            this.files = new Dictionary<string, File>();
            this.collectionSize = 0;
        }

        public void AddFile(string fileName, int fileSize)
        {
            this.collectionSize += fileSize;
            if (files.ContainsKey(fileName))
            {
                files[fileName] = new File(fileName, fileSize);
            }
            else
            {
                files.Add(fileName, new File(fileName, fileSize));
            }
        }

        public int CompareTo(Collection? other)
        {
            return other.collectionSize.CompareTo(this.collectionSize);
        }

        public int Size { get { return collectionSize; } }
        public string Name { get { return directoryName; } }
    }
}
