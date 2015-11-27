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


        public V8FileSystem ReadV8FileSystem()
        {
            V8FileSystem fileSystem = new V8FileSystem();
            
            // Reading container 16 bytes
            fileSystem.Container = ReadContainerHeader();
            
            // Reading block header 31 bytes
            fileSystem.BlockHeader = ReadBlockHeader();

            // Reading container references
            fileSystem.References = ReadFileSystemReferences((V8BlockHeader)fileSystem.BlockHeader.Clone());

            return fileSystem;
        }

        private V8ContainerHeader ReadContainerHeader()
        {
            V8ContainerHeader container = new V8ContainerHeader();
            container.RefToNextPage = _reader.ReadInt32();
            container.PageSize = _reader.ReadInt32();
            container.PagesCount = _reader.ReadInt32();
            container.ReservedField = _reader.ReadInt32();

            return container;
        }
        private V8BlockHeader ReadBlockHeader()
        {
            char[] Block = _reader.ReadChars(V8BlockHeader.Size());
            if (Block[0]  != 0x0d || Block[1]  != 0x0a ||
                Block[10] != 0x20 || Block[19] != 0x20 || 
                Block[28] != 0x20 || Block[29] != 0x0d || Block[30] != 0x0a)
            {
                throw new NotImplementedException();
            }

            string HexDataSize = new string(Block, 2, 8);
            string HexPageSize = new string(Block, 11, 8);
            string HexNextPage = new string(Block, 20, 8);

            V8BlockHeader header = new V8BlockHeader();
            header.DataSize = Convert.ToInt32(HexDataSize, 16);
            header.PageSize = Convert.ToInt32(HexPageSize, 16);
            header.RefToNextPage = Convert.ToInt32(HexNextPage, 16);

            return header;
        }
        private V8FileSystemReference ReadFileSystemReference(byte[] buffer, int position)
        {
            V8FileSystemReference reference = new V8FileSystemReference();
            reference.RefToHeader = BitConverter.ToInt32(buffer, position);
            reference.RefToData = BitConverter.ToInt32(buffer, position + 4);
            reference.ReservedField = BitConverter.ToInt32(buffer, position + 8);

            return reference;
        }
        private List<V8FileSystemReference> ReadFileSystemReferences(V8BlockHeader blockHeader)
        {
            List<V8FileSystemReference> references = new List<V8FileSystemReference>();
            references.Capacity = (int)(blockHeader.DataSize / V8FileSystemReference.Size());

            Int32 bytesReaded = 0;
            Int32 bytesToRead = 0;
            Int32 dataSize = blockHeader.DataSize;
            byte[] bytes = new byte[blockHeader.DataSize];

            while (dataSize > bytesReaded)
            {
                bytesToRead = Math.Min(blockHeader.PageSize, dataSize - bytesReaded);
                _reader.Read(bytes, bytesReaded, bytesToRead);
                bytesReaded += bytesToRead;
                if (blockHeader.RefToNextPage != 0x7FFFFFFF)
                {
                    _reader.BaseStream.Seek(blockHeader.RefToNextPage, SeekOrigin.Begin);
                    blockHeader = ReadBlockHeader();
                }
            }

            bytesReaded = 0;
            while (dataSize > bytesReaded)
            {
                references.Add(ReadFileSystemReference(bytes, bytesReaded));
                bytesReaded += V8FileSystemReference.Size();
            }

            return references;
        }

        public V8FileHeader ReadFileHeader()
        {
            throw new NotImplementedException();
        }

 

       
    }
}
