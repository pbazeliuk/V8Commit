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
                return _reservedField;
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
