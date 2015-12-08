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

namespace _1CV8Adapters
{
    public class FileV8Tree
    {
        private List<FileV8Tree> _leaves;
        private string _key;
        private object _value;

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public FileV8Tree(string key, object value)
        {
            _key = key;
            _value = value;
        }
        ~FileV8Tree()
        {
            if(_leaves != null)
            {
                _leaves.Clear();
            }         
        }

        public FileV8Tree AddLeaf(string key, object value)
        {
            if(_leaves == null)
            {
                _leaves = new List<FileV8Tree>();
            }
            FileV8Tree newLeaf = new FileV8Tree(key, value);
            _leaves.Add(newLeaf);
            return newLeaf;
        }
    }
}
