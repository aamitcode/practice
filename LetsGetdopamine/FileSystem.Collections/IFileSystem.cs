namespace FileSystem.Collections
{
    public interface IFileSystem
    {
        public void AddFileToDirectory(string fileName, int fileSize, string collectionName = "");
        public int GetTotalSizeProcessed();
        public List<Collection> GetTopNCollectionBySize(int n);
    }
}
