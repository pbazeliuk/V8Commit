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

        public Int32 RefToHeader
        {
            get
            {
                return _refToHeader;
            }
            set
            {
                _refToHeader = RefToHeader;
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
                _refToData = RefToData;
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
                _reservedField = ReservedField;
            }
        }

        public static Int32 Size()
        {
            return 12; // 4 + 4 + 4
        }

    }
}
