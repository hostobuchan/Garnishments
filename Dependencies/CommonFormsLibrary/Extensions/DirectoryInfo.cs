namespace System
{
    public static partial class Extensions
    {
        public static void CreateDirectoryStructure(this IO.DirectoryInfo directory)
        {
            Utilities.CreateDirectoryStructure(directory);
        }
    }
}
