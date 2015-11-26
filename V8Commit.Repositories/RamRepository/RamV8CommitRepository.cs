using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Entities.V8FileSystem;

namespace V8Commit.Repositories.RamRepository
{
    public class RamV8CommitRepository : IV8CommitRepository
    {
        public V8BlockHeader ReadBlockHeader()
        {
            throw new NotImplementedException();
        }

        public V8ContainerHeader ReadContainerHeader()
        {
            throw new NotImplementedException();
        }

        public V8FileHeader ReadFileHeader()
        {
            throw new NotImplementedException();
        }

        public V8FileSystemReference ReadFileSystemReference()
        {
            throw new NotImplementedException();
        }
    }
}
