namespace FileSystem.Collections
{
    public class File
    {
        private readonly string name;
        private readonly int size;
        public File(string name, int size)
        {
            this.name = name;
            this.size = size;
        }
    }
}
