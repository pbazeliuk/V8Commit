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
        private string _value;
        private FileV8Tree _parent;

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
        public string Value
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
        public FileV8Tree Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public FileV8Tree(string key, string value, FileV8Tree parent = null)
        {
            _key = key;
            _value = value;
            _parent = parent;
        }
        ~FileV8Tree()
        {
            if(_leaves != null)
            {
                _leaves.Clear();
            }         
        }

        public FileV8Tree GetLeaf(params int[] values)
        {
            FileV8Tree current = this;
            foreach (var value in values)
            {
                current = current._leaves[value];
            }
            return current;
        }
        public FileV8Tree GetNode(int index)
        {
            return _leaves[index];
        }
        public FileV8Tree AddLeaf(string key, string value, FileV8Tree parent = null)
        {
            if(_leaves == null)
            {
                _leaves = new List<FileV8Tree>();
            }
            FileV8Tree newLeaf = new FileV8Tree(key, value, parent);
            _leaves.Add(newLeaf);
            return newLeaf;
        }
    }
}
