using System;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8ContainerHeader
    {

        private Int32 _refToNextPage;
        private Int32 _pageSize;
        private Int32 _pagesCount;
        private Int32 _reservedField;

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
        public Int32 PagesCount
        {
            get
            {
                return _pagesCount;
            }

            set
            {
                _pagesCount = value;
            }
        }
        public Int32 ReservedField
        {
            get
            {
                return _reservedField;
            }

            set
            {
                _reservedField = value;
            }
        }

        public V8ContainerHeader()
        {
            _refToNextPage = 0x7FFFFFFF;
        }

        public static Int32 Size()
        {
            return 16; // 4 + 4 + 4 + 4
        }
    }
}
