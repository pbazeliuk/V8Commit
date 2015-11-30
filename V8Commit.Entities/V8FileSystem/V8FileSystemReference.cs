/**
 * Copyright © 2015 Petro Bazeliuk 
 *
 * The contents of this file are subject to the terms of one of the following
 * open source licenses: Apache 2.0 or or EPL 1.0 (the "Licenses"). You can
 * select the license that you prefer but you may not use this file except in
 * compliance with one of these Licenses.
 * 
 * You can obtain a copy of the Apache 2.0 license at
 * http://www.opensource.org/licenses/apache-2.0
 * 
 * You can obtain a copy of the EPL 1.0 license at
 * http://www.opensource.org/licenses/eclipse-1.0
 * 
 * See the Licenses for the specific language governing permissions and
 * limitations under the Licenses.
 *
 */

using System;

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
