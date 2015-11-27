using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8FileSystem
    {
        private V8ContainerHeader _container;
        private V8BlockHeader _blockHeader;
        private List<V8FileSystemReference> _references;

        public V8ContainerHeader Container
        {
            get
            {
                return _container;
            }

            set
            {
                _container = value;
            }
        }
        public V8BlockHeader BlockHeader
        {
            get
            {
                return _blockHeader;
            }

            set
            {
                _blockHeader = value;
            }
        }
        public List<V8FileSystemReference> References
        {
            get
            {
                return _references;
            }

            set
            {
                _references = value;
            }
        }
    }
}
