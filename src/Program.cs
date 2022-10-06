using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using CommandLine.Text;

public class Options
{
    [Option('s', "Src", Required = true)]
    public string SrcDir { get; set; }
    
    [Option('d', "Dest", Required = true)]
    public string DestDir { get; set; }

    [Option('i', "Ignore", Required = false)]
    public string IgnoreDir { get; set; }
}

class Program
{
    private static void DirectoryCopy(string sourceDirName, string destDirName, string ignoredDirPath)
    {
        if (sourceDirName == ignoredDirPath)
        {
            return;
        }

        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        // Copy subdirectories and their contents to new location.
        foreach (DirectoryInfo subdir in dirs)
        {
            string temppath = Path.Combine(destDirName, subdir.Name);
            DirectoryCopy(subdir.FullName, temppath, ignoredDirPath);
        }
        
    }

    public static void Main(string[] args)
    {
        var parser = new Parser(with => with.HelpWriter = null);
        var parserResult = parser.ParseArguments<Options>(args);
        parserResult.WithParsed(o =>
            {
                //TrimEnding '/' and '\' because DirectoryInfo.FullName never ends with '/' and '\'.
                var srcDir = Path.GetFullPath(o.SrcDir).TrimEnd('/', '\\');
                var destDir = Path.GetFullPath(o.DestDir).TrimEnd('/', '\\');
                var ignoreDir = Path.GetFullPath(o.IgnoreDir).TrimEnd('/', '\\');

                Console.WriteLine($@"Copying
from
    {srcDir}
to
    {destDir}
and ignore
    {ignoreDir}");
                DirectoryCopy(srcDir, destDir, ignoreDir);

                Console.WriteLine("Copying finished.");
            })
            .WithNotParsed(errs =>
            {
                DisplayHelp(parserResult, errs);
                Environment.ExitCode = -1;
            });
    }

    static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
    {
        HelpText helpText = null;
        if (errs.IsVersion())
        {
            helpText = HelpText.AutoBuild(result);
        }
        else
        {
            helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
        }
        Console.WriteLine(helpText);
    }
}
