/**
 * Copyright © 2015 Petro Bazeliuk 
 *
 * The contents of this file are subject to the terms of one of the following
 * open source licenses: Apache 2.0 or or EPL 1.0 (the "Licenses"). You can
 * select the license that you prefer but you may not use this file except in
 * compliance with one of these Licenses.
 * 
 * You can obtain a copy of the Apache 2.0 license at
 * http://www.opensource.org/licenses/apache-2.0
 * 
 * You can obtain a copy of the EPL 1.0 license at
 * http://www.opensource.org/licenses/eclipse-1.0
 * 
 * See the Licenses for the specific language governing permissions and
 * limitations under the Licenses.
 *
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CommandLine;
using V8Commit.Plugins;
using V8Commit.Services.FileV8Services;
using Microsoft.Practices.ServiceLocation;

// Debug
using V8Commit.Services.ConversionServices;
using Plugin.V8Commit20;

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
                                    //plugin.Value.Parse(v8Reader, fileSystem, Output, Threads);

                                    // Debug
                                    var tmp = new V8Commit20(ServiceLocator.Current.GetInstance<IConversionService<UInt64, DateTime>>());
                                    tmp.Parse(v8Reader, fileSystem, Output, Threads);

                                    return 0;
                                }
                            }
                        }
                    }
                }   
            }
            catch(Exception exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
                return 1;
            }

            Console.WriteLine("Plugin with name: {0} not found.", Plugin);
            return 1;
        }
    }
}
