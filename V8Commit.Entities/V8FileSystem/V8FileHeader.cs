﻿using System;

namespace V8Commit.Entities.V8FileSystem
{
    public class V8FileHeader
    {
        private UInt64 _creationDate;
        private UInt64 _modificationDate;
        private Int32 _reservedField;
        private string _fileName;

        public UInt64 CreationDate
        {
            get
            {
                return _creationDate;
            }

            set
            {
                _creationDate = value;
            }
        }
        public UInt64 ModificationDate
        {
            get
            {
                return _modificationDate;
            }

            set
            {
                _modificationDate = value;
            }
        }
        public Int32 ReservedField
        {
            get
            {
                return ReservedField;
            }

            set
            {
                _reservedField = value;
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public static Int32 Size()
        {
            return 20; // 8 + 8 + 4
        }
    }
}
