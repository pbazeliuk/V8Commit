﻿using System;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8BlockHeader
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
                _dataSize = DataSize;
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
                PageSize = _pageSize;
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
                _refToNextPage = RefToNextPage;
            }
        }

        public static Int32 Size()
        {
            return 12; // 4 + 4 + 4
        }
    }
}