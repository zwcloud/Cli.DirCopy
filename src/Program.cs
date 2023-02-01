using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using CommandLine.Text;

public class Options
{
    [Option('s', "Src", Required = true)]
    public string SrcDir { get; set; }
    
    [Option('d', "Dest", Required = true)]
    public string DestDir { get; set; }

    [Option('i', "Ignore", Required = false)]
    public IEnumerable<string> IgnoreDirs { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var parser = new Parser(with => with.HelpWriter = null);
        var parserResult = parser.ParseArguments<Options>(args);
        parserResult.WithParsed(o =>
            {
                //TrimEnding '/' and '\' because DirectoryInfo.FullName never ends with '/' and '\'.
                var srcDir = Path.GetFullPath(o.SrcDir).TrimEnd('/', '\\');
                var destDir = Path.GetFullPath(o.DestDir).TrimEnd('/', '\\');
                var ignoreDirList = o.IgnoreDirs
                    .Select(ignoreDir => Path.GetFullPath(ignoreDir).TrimEnd('/', '\\'))
                    .ToList();

                Console.WriteLine($@"Copying
from
	{srcDir}
to
	{destDir}
and ignore
	{string.Join("\n\t", ignoreDirList)}");
                DirCopy.DirectoryCopy(srcDir, destDir, ignoreDirList);

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
