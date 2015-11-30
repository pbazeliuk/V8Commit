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

using System.Collections.Generic;

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
