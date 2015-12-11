/**
* Copyright © 2015 Petro Bazeliuk 
*
* The contents of this file are subject to the terms of commercial license.
* Content usage without proper license is prohibited.
* To obtain it contact pbazeliuk@gmail.com
*
*/

using System;
using System.ComponentModel.Composition;

using _1CV8Adapters;
using V8Commit.Entities.V8FileSystem;
using V8Commit.Plugins;

namespace Plugin.V8Commit30
{
    [Export(typeof(IParsePlugin)),
     Export(typeof(IBuildPlugin)),
        ExportMetadata("Name", "V8Commit30"),
        ExportMetadata("Author", "Copyright © Petro Bazeliuk 2015"),
        ExportMetadata("Version", "1.0.0.0")]
    public class V8Commit30 : IParsePlugin, IBuildPlugin
    {
        public void Build()
        {
            throw new NotImplementedException();
        }

        public void Parse(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string output, int threads = 1)
        {
            throw new NotImplementedException();
        }
    }
}
