using System;
using System.IO;
using CommandLine;

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
                        (ParseVerb opts) => opts.Invoke(),
                        errors => {
                            Console.WriteLine(help.ToString());
                            return 1;
                        });
        }
    }
}
