using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Entities.V8FileSystem;

namespace V8Commit.Repositories.RamRepository
{
    public class RamV8CommitRepository : IV8CommitRepository
    {
        private readonly string _fileName;

        public V8FileSystem ReadHeadersV8FileSystem()
        {
            throw new NotImplementedException();
        }

        public V8FileSystem ReadHeadersV8FileSystem(bool isInflated = true)
        {
            throw new NotImplementedException();
        }

        public V8FileSystem ReadV8FileSystem(bool isInflated = true)
        {
            throw new NotImplementedException();
        }

        public void WriteToOutputDirectory(V8FileSystem fileSystem, string outputDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
