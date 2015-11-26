using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Entities.V8FileSystem;

namespace V8Commit.Repositories
{
    public class FileV8CommitRepository : IV8CommitRepository
    {
        private readonly string _fileName;
        private BinaryReader _reader;

        public FileV8CommitRepository(string fileName)
        {
            if (File.Exists(fileName))
            {
                this._fileName = fileName;
                _reader = new BinaryReader(File.Open(_fileName, FileMode.Open));
            }
            else
            {
                throw new NotImplementedException();
            }    
        }

        ~FileV8CommitRepository()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }

        public V8ContainerHeader ReadContainerHeader()
        {
            V8ContainerHeader container = new V8ContainerHeader();
            container.RefToNextPage = _reader.ReadInt32();
            container.PageSize = _reader.ReadInt32();
            container.PagesCount = _reader.ReadInt32();
            container.ReservedField = _reader.ReadInt32();

            return container;
        }

        public V8BlockHeader ReadBlockHeader()
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
