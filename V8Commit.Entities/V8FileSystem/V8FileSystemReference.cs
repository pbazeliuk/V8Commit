﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8FileSystemReference
    {
        private Int32 _refToHeader;
        private Int32 _refToData;
        private Int32 _reservedField;

        private V8FileHeader _fileHeader;
        private byte[] _fileData;

        private bool _isInflated;

        private V8FileSystem _folder;

        public Int32 RefToHeader
        {
            get
            {
                return _refToHeader;
            }
            set
            {
                _refToHeader = value;
            }
        } 
        public Int32 RefToData
        {
            get
            {
                return _refToData;
            }
            set
            {
                _refToData = value;
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

        public V8FileHeader FileHeader
        {
            get
            {
                return _fileHeader;
            }
            set
            {
                _fileHeader = value;
            }
        }
        public byte[] FileData
        {
            get
            {
                return _fileData;
            }
            set
            {
                _fileData = value;
            }
        }

        public bool IsInFlated
        {
            get
            {
                return _isInflated;
            }
            set
            {
                _isInflated = value;
            }
        }

        public V8FileSystem Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }

        public static Int32 Size()
        {
            return 12; // 4 + 4 + 4
        }
    }
}