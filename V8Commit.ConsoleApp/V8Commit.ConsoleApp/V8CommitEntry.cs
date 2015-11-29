using System;
using System.IO;
using CommandLine;
using V8Commit.Repositories;

namespace V8Commit.ConsoleApp
{
    class V8CommitEntry
    {
        static void Main(string[] args)
        {
            var help = new StringWriter();
            new Parser(with => with.HelpWriter = help)
                .ParseArguments(args, typeof(ParseVerb))
                    .MapResult(
                        (ParseVerb opts) => ParseVerb(opts),
                        errors => {
                            Console.WriteLine(help.ToString());
                            return 1;
                        });
        }

        static int ParseVerb(ParseVerb opts)
        {
            // Common check input file
            try
            {
                if (!File.Exists(opts.Input))
                {
                    Console.WriteLine("File does not exists.");
                    return 1;
                }
            }
            catch
            {
                Console.WriteLine("Unexpected error. Invalid file.");
                return 1;
            }

            // Common check output directory
            try
            {
                opts.Output += "\\";
                if (!Directory.Exists(opts.Output))
                {
                    Directory.CreateDirectory(opts.Output);
                }
            }
            catch
            {
                Console.WriteLine("Unexpected error. Invalid directory.");
                return 1;
            }

            // Todo: checking plugin

            IV8CommitRepository container = new FileV8CommitRepository(opts.Input);
            var fileSystem = container.ReadV8FileSystem();
            container.WriteToOutputDirectory(fileSystem, opts.Output);

            return 0;
        }
    }
}
