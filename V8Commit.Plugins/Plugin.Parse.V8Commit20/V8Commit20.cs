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
using System.IO.Compression;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using V8Commit.Entities.V8FileSystem;
using V8Commit.Plugins;
using V8Commit.Services.FileV8Services;
using V8Commit.Services.ConversionServices;

namespace Plugin.V8Commit20
{
    [Export(typeof(IParsePlugin)),
        ExportMetadata("Name", "V8Commit20"),
        ExportMetadata("Author", "Copyright © Petro Bazeliuk 2015"),
        ExportMetadata("Version", "1.0.0.0")]
    public class V8Commit20 : IParsePlugin
    {
        private readonly IConversionService<UInt64, DateTime> _convertionService;

        public V8Commit20(IConversionService<UInt64, DateTime> covertionService)
        {
            this._convertionService = covertionService;
        }

        public void Parse(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string output, int threads = 1)
        {
            var root = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, @"root");
            if (root == null)
            {
                Console.WriteLine("File root does not exist. Choose correct «1C:Enterprise 8» file.");
                throw new NotImplementedException();
            }

            

            fileV8Reader.Seek(root.RefToData, SeekOrigin.Begin);
            using (var stream = new MemoryStream(fileV8Reader.ReadBytes(fileV8Reader.ReadBlockHeader())))
            {
                StringBuilder sb = new StringBuilder(stream.Capacity);
                if (root.IsInFlated)
                {
                    using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        using (var reader = new StreamReader(deflateStream))
                        {
                            sb.Append(reader.ReadToEnd());
                        }
                    }
                }
                else
                {
                    using (var reader = new StreamReader(stream))
                    {
                        sb.Append(reader.ReadToEnd());
                    }
                }

                string str = sb.ToString();
                //String result = new String(buffer, "UTF-8");
                //Console.WriteLine("{0}", sb.ToString());
                Console.WriteLine(sb.ToString());
                //string str = Encoding.Default.GetString(buf);
            }

        }
    }
}
