using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Entities.V8FileSystem;

namespace V8Commit.Repositories
{
    public interface IV8CommitRepository
    {
        V8ContainerHeader ReadContainerHeader();
        V8BlockHeader ReadBlockHeader();
        V8FileSystemReference ReadFileSystemReference();
        V8FileHeader ReadFileHeader();
    }
}
