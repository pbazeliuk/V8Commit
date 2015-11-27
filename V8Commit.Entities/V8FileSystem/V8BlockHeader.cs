using System;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8BlockHeader : ICloneable
    {
        private Int32 _dataSize;
        private Int32 _pageSize;
        private Int32 _refToNextPage;

        public Int32 DataSize
        {
            get
            {
                return _dataSize;
            }
            set
            {
                _dataSize = value;
            }
        }  
        public Int32 PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        } 
        public Int32 RefToNextPage
        {
            get
            {
                return _refToNextPage;
            }
            set
            {
                _refToNextPage = value;
            }
        }

        public static Int32 Size()
        {
            return 31; // 1 + 1 + 8 + 1 + 8 + 1 + 8 + 1 + 1 + 1
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
