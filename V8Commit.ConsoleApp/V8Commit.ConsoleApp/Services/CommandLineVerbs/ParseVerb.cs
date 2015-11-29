using System;
using System.IO;
using CommandLine;
using V8Commit.Services.FileV8Services;

namespace V8Commit.ConsoleApp
{
    [Verb("parse", HelpText = "Disassemble «1C:Enterprise 8» file on the source text files.")]
    public sealed class ParseVerb : IVerb
    {
        [Option('i', "input", Required = true, HelpText = "Used to specify the file for parsing.")]
        public string Input { get; set; }

        [Option('o', "output", Required = true, HelpText = "Used to specify the output directory.")]
        public string Output { get; set; }

        [Option('p', "plugin", Required = true, HelpText = "The plugin will be used to output the source files.")]
        public string Plugin { get; set; }

        public int Invoke()
        {
            // Common check input file
            try
            {
                if (!File.Exists(Input))
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
                Output += "\\";
                if (!Directory.Exists(Output))
                {
                    Directory.CreateDirectory(Output);
                }
            }
            catch
            {
                Console.WriteLine("Unexpected error. Invalid directory.");
                return 1;
            }

            // Todo: checking plugin

            //try
            //{
            //string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            //string[] files = System.IO.Directory.GetFiles(folder, "*.dll");
            //foreach (string file in files)
            //{
            //    IPlugin plugin = IsPlugin(file);
            //    if (plugin != null)
            //        sPlugins.Add(plugin);
            //}
            //
            //catch (Exception e)
            //{
            //    ToLog(e.Message);
            //}

            using (FileV8Reader v8Commit = new FileV8Reader(Input))
            {
                var fileSystem = v8Commit.ReadV8FileSystem();
                v8Commit.WriteToOutputDirectory(v8Commit, fileSystem, Output);
            }
            
            return 0;
        }
    }
}
