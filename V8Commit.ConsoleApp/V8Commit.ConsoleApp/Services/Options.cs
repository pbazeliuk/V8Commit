/* https://github.com/gsscoder/commandline */

using CommandLine;

namespace V8Commit.ConsoleApp
{
    [Verb("parse", HelpText = "Disassemble «1C:Enterprise 8» file on the source text files.")]
    public sealed class ParseVerb
    {
        [Option('i', "input", Required = true, HelpText = "Used to specify the file for parsing.")]
        public string Input { get; set; }

        [Option('o', "output", Required = true, HelpText = "Used to specify the output directory.")]
        public string Output { get; set; }

        [Option('p', "plugin", Required = true, HelpText = "The plugin will be used to output the source files.")]
        public string Plugin { get; set; }
    }
}
