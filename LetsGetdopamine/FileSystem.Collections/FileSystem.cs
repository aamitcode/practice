/**********************
 * 
 * */
namespace FileSystem.Collections
{
    public class FileSystem : IFileSystem
    {
        private readonly Dictionary<string, Collection> collections;
        private readonly Dictionary<string, File> filesWithoutCollections;
        private readonly PriorityQueue<string, Collection> collectionMaxHeap;
        private int totalSize;
        public FileSystem()
        {
            collections = new Dictionary<string, Collection> ();
            filesWithoutCollections = new Dictionary<string, File>();
            collectionMaxHeap = new PriorityQueue<string, Collection>();
            totalSize = 0;
        }

        public void AddFileToDirectory(string fileName, int fileSize, string collectionName = "")
        {
            this.totalSize += fileSize;
            if (!string.IsNullOrWhiteSpace(collectionName))
            {
                if (collections.TryGetValue(collectionName, out Collection? value))
                {
                    value.AddFile(fileName, fileSize);
                    /// As C# priority queue is not updateable, doing reheapify any time colleciton size is changing
                    var unorderMap = this.collectionMaxHeap.UnorderedItems.ToList();
                    this.collectionMaxHeap.Clear();
                    this.collectionMaxHeap.EnqueueRange(unorderMap);
                }
                else
                {
                    collections.Add(collectionName, new Collection(collectionName));
                    collections[collectionName].AddFile(fileName, fileSize);
                    collectionMaxHeap.Enqueue(collectionName, collections[collectionName]);
                }
                
            }
            else
            {
                filesWithoutCollections.Add(fileName, new File(fileName, fileSize));
            }
        }

        public List<Collection> GetTopNCollectionBySize(int n)
        {
            var topNcollection = new List<Collection>();
            for(int i = 0; i < n; i++)
            {
                string topCollection = this.collectionMaxHeap.Dequeue();
                topNcollection.Add(this.collections[topCollection]);
            }
            foreach(var item in topNcollection)
            {
                this.collectionMaxHeap.Enqueue(item.Name, item);
            }
            return topNcollection;
        }

        public int GetTotalSizeProcessed()
        {
            return this.totalSize;
        }
    }
}

