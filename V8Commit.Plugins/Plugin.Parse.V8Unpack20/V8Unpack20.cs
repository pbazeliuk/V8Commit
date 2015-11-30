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

using System.ComponentModel.Composition;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Entities.V8FileSystem;
using V8Commit.Plugins;
using V8Commit.Services.FileV8Services;

namespace Plugin.V8Unpack20
{
    [Export(typeof(IParsePlugin)),
        ExportMetadata("Name", "V8Unpack20"),
        ExportMetadata("Author", "Copyright © Petro Bazeliuk 2015"),
        ExportMetadata("Version", "1.0.0.0")]
    public class V8Unpack20 : IParsePlugin
    {
        public void Parse(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string output, int threads = 1)
        {
            foreach (var reference in fileSystem.References)
            {
                fileV8Reader.Seek(reference.RefToData, SeekOrigin.Begin);
                string path = output + reference.FileHeader.FileName;

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
                            Parse(tmpV8Reader, reference.Folder, path + "\\");
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
