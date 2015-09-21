namespace DbVersioning
{
    public class DbUpdateSourceDescriptor
    {
        public DbUpdateSourceDescriptor(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}