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

using _1CV8Adapters;
using V8Commit.Entities.V8FileSystem;
using V8Commit.Plugins;
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
                RaiseFileNotExistsException(@"root");
            }

            FileV8Tree rootTree = fileV8Reader.ReadV8File(root);
            string rootGUID = rootTree.GetLeaf(1).Value;
            var rootDescription = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, rootGUID);
            if (rootDescription == null)
            {
                RaiseFileNotExistsException(rootGUID);
            }

            FileV8Tree descriptionTree = fileV8Reader.ReadV8File(rootDescription);
            FileV8Tree objectModule = descriptionTree.GetLeaf(3, 1, 1, 3, 1, 1, 2);
            FileV8Tree forms = descriptionTree.GetLeaf(3, 1, 5);
            FileV8Tree models = descriptionTree.GetLeaf(3, 1, 4);

            // There is forms guid? 
            if (forms.GetNode(0).Value.Equals("d5b0e5ed-256d-401c-9c36-f630cafd8a62"))
            {
                int count = Convert.ToInt32(forms.GetNode(1).Value);
                for(int i = 2; i < count + 2; i++)
                {
                    string formGUID = forms.GetNode(i).Value;
                    var formDescription = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, formGUID);
                    if (formDescription == null)
                    {
                        RaiseFileNotExistsException(formGUID);
                    }

                    FileV8Tree formDescriptionTree = fileV8Reader.ReadV8File(formDescription);
                    string formName = formDescriptionTree.GetLeaf(1, 1, 1, 1, 2).Value;


                    var form = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, formGUID + ".0");
                    if (form == null)
                    {
                        RaiseFileNotExistsException(formGUID + ".0");
                    }

                    FileV8Tree formTree = fileV8Reader.ReadV8File(form);

                }
            }
        }

        private void RaiseDescriptionNotFoundException(string fileName)
        {
            // TODO:
            Console.WriteLine("{0} description not found. Choose correct «1C:Enterprise 8» file.", fileName);
            throw new NotImplementedException();
        }
        private void RaiseFileNotExistsException(string fileName)
        {
            // TODO:
            Console.WriteLine("File {0} does not exist. Choose correct «1C:Enterprise 8» file.", fileName);
            throw new NotImplementedException();
        }

    }
}
