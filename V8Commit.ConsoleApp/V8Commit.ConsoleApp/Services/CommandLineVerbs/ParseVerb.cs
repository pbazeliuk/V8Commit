using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CommandLine;
using V8Commit.Plugins;
using V8Commit.Services.FileV8Services;
using System.ComponentModel.Composition.Hosting;

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

        [Option('t', "threads", Required = false, Default = 1, HelpText = "Currently not used.")]
        public int Threads { get; set; }

        [ImportMany(typeof(IParsePlugin), AllowRecomposition = true)]
        private IEnumerable<Lazy<IParsePlugin, IPluginMetadata>> _plugins;

        public int Invoke()
        {
            // Common check input file
            try
            {
                if (!File.Exists(Input))
                {
                    Console.WriteLine("File does not exist.");
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

            /* Try to parse with plugin (lazy loading .dll) */
            try
            {
                /* Checking plugins directory */ 
                string pluginDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\";
                if (!Directory.Exists(pluginDirectory))
                {
                    Console.WriteLine("Loading plugin error. Plugins folder does not exist.");
                    return 1;
                }

                /* Initializing DirectoryCatalog for CompositionContainer  */
                using (var directory = new DirectoryCatalog(pluginDirectory))
                {
                    /* Initializing CompositionContainer for composing parts. */
                    using (var container = new CompositionContainer(directory))
                    {
                        /* Lazy loading plugins in _plugins */
                        container.ComposeParts(this);

                        /* Initialize FileV8Reader, V8FileSystem */
                        using (FileV8Reader v8Reader = new FileV8Reader(Input))
                        {
                            var fileSystem = v8Reader.ReadV8FileSystem();
                            foreach (var plugin in _plugins)
                            {
                                if (String.Equals(plugin.Metadata.Name, Plugin, StringComparison.OrdinalIgnoreCase))
                                {
                                    /* Lazy loading matched plugin and try to parse input file */
                                    plugin.Value.Parse(v8Reader, fileSystem, Output, Threads);
                                    return 0;
                                }
                            }
                        }
                    }
                }   
            }
            catch
            {
                Console.WriteLine("Unexpected error.");
                return 1;
            }

            Console.WriteLine("Plugin with name: {0} not found.", Plugin);
            return 1;
        }
    }
}
