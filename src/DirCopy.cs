using System.Collections.Generic;
using System.IO;

public static class DirCopy
{
    /// <summary>
    /// Copy a directory recursively.
    /// </summary>
    /// <param name="src">absolute path of source directory </param>
    /// <param name="dest">absolute path of destination directory</param>
    /// <param name="ignored">list of absolute path of ignored directories from source directory path</param>
    /// <exception cref="DirectoryNotFoundException">Throws when source directory doesn't exist.</exception>
    public static void DirectoryCopy(string src, string dest, IList<string> ignored = null)
    {
        if (ignored != null && ignored.Contains(src))
        {
            return;
        }

        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(src);
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + src);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(dest))
        {
            Directory.CreateDirectory(dest);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string t = Path.Combine(dest, file.Name);
            file.CopyTo(t, true);
        }

        // Copy subdirectories and their contents to new location.
        foreach (DirectoryInfo subdir in dirs)
        {
            string t = Path.Combine(dest, subdir.Name);
            DirectoryCopy(subdir.FullName, t, ignored);
        }
        
    }
}