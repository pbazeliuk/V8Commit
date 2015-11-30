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
