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
using System.Text;

using _1CV8Adapters;
using V8Commit.Entities.V8FileSystem;
using V8Commit.Plugins;
using V8Commit.Services.ConversionServices;
using V8Commit.Services.HashServices;

namespace Plugin.V8Commit20
{
    [Export(typeof(IParsePlugin)),
        ExportMetadata("Name", "V8Commit20"),
        ExportMetadata("Author", "Copyright © Petro Bazeliuk 2015"),
        ExportMetadata("Version", "1.0.0.0")]
    public class V8Commit20 : IParsePlugin
    {
        private readonly IConversionService<UInt64, DateTime> _convertionService;
        private readonly IHashService _hashService;

        [ImportingConstructor]
        public V8Commit20([Import("HashService")] IHashService hashService,
                          [Import("CovertionService")] IConversionService<UInt64, DateTime> covertionService)
        {
            this._convertionService = covertionService;
            this._hashService = hashService;
        }

        public void Parse(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string output, int threads = 1)
        {
            FileV8Tree rootTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, @"root");
            FileV8Tree rootPropertiesTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, rootTree.GetLeaf(1).Value);
           
            FileV8Tree objectModule = rootPropertiesTree.GetLeaf(3, 1, 1, 3, 1, 1, 2);
            FileV8Tree forms = rootPropertiesTree.GetLeaf(3, 1, 5);
            FileV8Tree models = rootPropertiesTree.GetLeaf(3, 1, 4);

            FileV8Tree objectModuleTree = GetDescriptionTree(fileV8Reader, fileSystem, objectModule.Value + ".0", @"text");
            {
                string path = output + "\\МодульОбъекта\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string module = '\uFEFF' + objectModuleTree.GetLeaf(0).Value;

                string filePath = path + "МодульОбъекта.txt";
                string hashFile = _hashService.HashFile(filePath);
                string hashModule = _hashService.HashString(module);

                if (!hashModule.Equals(hashFile))
                {
                    using (StreamWriter fileStream = new StreamWriter(filePath))
                    {
                        fileStream.Write(module);
                    }
                }
            }

               
            if(!Directory.Exists(output + "Form"))
            {
                Directory.CreateDirectory(output + "Form");
            }

            if (!Directory.Exists(output + "СКД"))
            {
                Directory.CreateDirectory(output + "СКД");
            }
            /* Directories that are mighty to be deleted */
            List<string> directories = new List<string>();
            directories.AddRange(Directory.GetDirectories(output + "Form"));
            directories.AddRange(Directory.GetDirectories(output + "СКД"));

            /* There is forms guid? */
            if (forms.GetNode(0).Value.Equals("d5b0e5ed-256d-401c-9c36-f630cafd8a62"))
            {
                int count = Convert.ToInt32(forms.GetNode(1).Value);
                for(int i = 2; i < count + 2; i++)
                {
                    FileV8Tree formTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, forms.GetNode(i).Value + ".0");
                    FileV8Tree formPropertiesTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, forms.GetNode(i).Value);

                    string formName = formPropertiesTree.GetLeaf(1, 1, 1, 1, 2).Value.Replace("\"", "");
                    string path = output + "Form\\" + formName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // This directory is no needed to be deleted
                    directories.Remove(path);
                
                    string tmpSheet = formTree.GetLeaf(2).Value;
                    string formModule = '\uFEFF' + tmpSheet.Substring(1, tmpSheet.Length - 2).Replace("\"\"", "\"");

                    string filePath = path + "\\" + formName + ".txt";
                    string hashFile = _hashService.HashFile(filePath);
                    string hashForm = _hashService.HashString(formModule);

                    if (!hashForm.Equals(hashFile))
                    {
                        using (StreamWriter fileStream = new StreamWriter(filePath))
                        {
                            fileStream.Write(formModule);
                        }
                    }
                }
            }

            /* There is models guid? */
            if (models.GetNode(0).Value.Equals("3daea016-69b7-4ed4-9453-127911372fe6"))
            {
                int count = Convert.ToInt32(models.GetNode(1).Value);
                for (int i = 2; i < count + 2; i++)
                {
                    FileV8Tree modelTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, models.GetNode(i).Value + ".0");
                    FileV8Tree modelPropertiesTree = GetDescriptionTree(fileV8Reader, fileSystem, String.Empty, models.GetNode(i).Value);

                    string modelName = modelPropertiesTree.GetLeaf(1, 2, 2).Value.Replace("\"", "");
                    string path = output + "СКД\\" + modelName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    /* This directory is no needed to be deleted */
                    directories.Remove(path);

                    string modelModule = modelTree.GetLeaf(0).Value.Replace("\0\0\0\0\u0001\0\0\0`&\0\0\0\0\0\0w\u0011\0\0\0\0\0\0﻿", "");

                    string filePath = path + "\\" + modelName + ".txt";
                    string hashFile = _hashService.HashFile(filePath);
                    string hashModel = _hashService.HashString(modelModule);

                    if (!hashModel.Equals(hashFile))
                    {
                        using (StreamWriter fileStream = new StreamWriter(filePath))
                        {
                            fileStream.Write(modelModule);
                        }
                    }
                }
            }

            // This directories are need to be deleted
            DeleteDirectories(directories);
        }
        private FileV8Tree GetDescriptionTree(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string folderName, string fileName)
        {
            if (String.IsNullOrEmpty(folderName))
            {
                var file = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, fileName);
                if (file == null)
                {
                    RaiseFileNotExistsException(fileName);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    fileV8Reader.ReadV8FileRawData(stream, file);
                    return fileV8Reader.ParseV8File(stream, fileName);
                }
            }
            else
            {
                var folder = fileV8Reader.FindFileSystemReferenceByFileHeaderName(fileSystem.References, folderName);
                if (folder == null)
                {
                    RaiseFileNotExistsException(folderName);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    fileV8Reader.ReadV8FileRawData(stream, folder);
                    if (fileV8Reader.IsV8FileSystem(stream))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        using (FileV8Reader tmpV8Reader = new FileV8Reader(new BinaryReader(stream, Encoding.Default, true)))
                        {
                            folder.Folder = tmpV8Reader.ReadV8FileSystem(false);
                            return GetDescriptionTree(tmpV8Reader, folder.Folder, String.Empty, fileName);
                        }
                    }
                    else
                    {
                        RaiseItIsNotV8FolderException(folderName);
                    }
                }
            }

            return null; 
        }
        private void DeleteDirectories(List<string> directories)
        {
            foreach (string directory in directories)
            {
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
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
        private void RaiseItIsNotV8FolderException(string fileName)
        {
            // TODO:
            Console.WriteLine("File {0} it is not 1CV8 folder. Choose correct «1C:Enterprise 8» file.", fileName);
            throw new NotImplementedException();
        }
    }
}
