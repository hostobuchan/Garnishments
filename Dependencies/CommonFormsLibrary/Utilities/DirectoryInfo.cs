using System.Collections.Generic;

namespace System
{
    public static class Utilities
    {
        public static void CreateDirectoryStructure(IO.DirectoryInfo directory)
        {
            var curDir = directory;
            Stack<System.IO.DirectoryInfo> dirsToCreate = new Stack<System.IO.DirectoryInfo>();
            dirsToCreate.Push(directory);
            while (curDir != null
                && !string.IsNullOrEmpty(curDir.FullName)
                && curDir.Parent != null
                && !curDir.Parent.Exists)
            {
                dirsToCreate.Push(curDir.Parent);
                curDir = curDir.Parent;
            }
            if (string.Equals(curDir.FullName, directory.Root.FullName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Drive or Resource Not Available\r\n\r\n{directory.Root.FullName}");
            }
            foreach (var dirToCreate in dirsToCreate)
            {
                dirToCreate.Create();
            }
        }
    }
}
