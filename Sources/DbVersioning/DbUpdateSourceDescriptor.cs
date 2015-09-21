namespace DbVersioning
{
    public class DbUpdateSourceDescriptor
    {
        public DbUpdateSourceDescriptor(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals((DbUpdateSourceDescriptor) obj);
        }

        protected bool Equals(DbUpdateSourceDescriptor other)
        {
            return string.Equals(Path, other.Path);
        }

        public override int GetHashCode()
        {
            return (Path != null ? Path.GetHashCode() : 0);
        }
    }
}