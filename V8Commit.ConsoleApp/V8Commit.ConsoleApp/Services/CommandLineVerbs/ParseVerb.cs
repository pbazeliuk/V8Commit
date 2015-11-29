using System;
using System.IO;
using CommandLine;
using V8Commit.Services.FileV8Services;
using V8Commit.Entities.V8FileSystem;
using System.IO.Compression;
using System.Text;

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
                WriteToOutputDirectory(v8Commit, fileSystem, Output);
            }
            
            return 0;
        }

        // TODO : make as plugins
        public void WriteToOutputDirectory(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string outputDirectory)
        {
            foreach (var reference in fileSystem.References)
            {
                fileV8Reader.Seek(reference.RefToData, SeekOrigin.Begin);
                string path = outputDirectory + reference.FileHeader.FileName;

                using (MemoryStream memStream = new MemoryStream())
                {
                    using (MemoryStream memReader = new MemoryStream(fileV8Reader.ReadBytes(fileV8Reader.ReadBlockHeader())))
                    {
                        if (reference.IsInFlated)
                        {
                            using (DeflateStream deflateStream = new DeflateStream(memReader, CompressionMode.Decompress))
                            {
                                deflateStream.CopyTo(memStream);
                            }
                        }
                        else
                        {
                            memReader.CopyTo(memStream);
                        }
                    }

                    if (fileV8Reader.IsV8FileSystem(memStream, memStream.Capacity))
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        memStream.Seek(0, SeekOrigin.Begin);
                        using (FileV8Reader tmpV8Reader = new FileV8Reader(new BinaryReader(memStream, Encoding.Default, true)))
                        {
                            reference.Folder = tmpV8Reader.ReadV8FileSystem(false);
                            WriteToOutputDirectory(tmpV8Reader, reference.Folder, path + "\\");
                        }
                    }
                    else
                    {
                        using (FileStream fileStream = File.Create(path))
                        {
                            memStream.Seek(0, SeekOrigin.Begin);
                            memStream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }
    }
}
