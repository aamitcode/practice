using FileSystem.Collections;

namespace FileSystem.Test
{
    [TestClass]
    public class FileSystemTest
    {
        [TestMethod]
        public void BasicTest()
        {
            IFileSystem fileSystem = new Collections.FileSystem();
            fileSystem.AddFileToDirectory("file1.txt", 100, "");
            Assert.AreEqual(100, fileSystem.GetTotalSizeProcessed());
            fileSystem.AddFileToDirectory("file2.txt", 200, "collection1");
            Assert.AreEqual(300, fileSystem.GetTotalSizeProcessed());
            var topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(1);
            Assert.AreEqual(1, topDirectoryCollectionBasedOnSize.Count());
            fileSystem.AddFileToDirectory("file3.txt", 300, "collection2");
            Assert.AreEqual(600, fileSystem.GetTotalSizeProcessed());
            topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(1);
            Assert.AreEqual(1, topDirectoryCollectionBasedOnSize.Count());
            Assert.AreEqual(300, topDirectoryCollectionBasedOnSize[0].Size);
        }
        [TestMethod]
        public void AdvanceTest()
        {
            IFileSystem fileSystem = new Collections.FileSystem();
            fileSystem.AddFileToDirectory("file1.txt", 100, "");
            fileSystem.AddFileToDirectory("file2.txt", 200, "collection1");
            fileSystem.AddFileToDirectory("file3.txt", 200, "collection1");
            fileSystem.AddFileToDirectory("file4.txt", 300, "collection2");
            var topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(1);
            Assert.AreEqual("collection1", topDirectoryCollectionBasedOnSize[0].Name);
            fileSystem.AddFileToDirectory("file5.txt", 300, "collection2");
            fileSystem.AddFileToDirectory("file6.txt", 300, "collection3");
            topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(2);
            Assert.AreEqual("collection2", topDirectoryCollectionBasedOnSize[0].Name);
            Assert.AreEqual("collection1", topDirectoryCollectionBasedOnSize[1].Name);
            fileSystem.AddFileToDirectory("file7.txt", 900, "collection4");
            topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(3);
            Assert.AreEqual(3, topDirectoryCollectionBasedOnSize.Count);
            Assert.AreEqual("collection4", topDirectoryCollectionBasedOnSize[0].Name);
            topDirectoryCollectionBasedOnSize = fileSystem.GetTopNCollectionBySize(1);
            Assert.AreEqual(1, topDirectoryCollectionBasedOnSize.Count);
            Assert.AreEqual("collection4", topDirectoryCollectionBasedOnSize[0].Name);
        }
    }
}