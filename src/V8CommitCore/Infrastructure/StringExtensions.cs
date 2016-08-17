/**
 * Copyright © 2015-2016 Petro Bazeliuk 
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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace V8Commit.Infrastructure
{
    public static class StringExtensions
    {
        public static string ComputeStringMD5(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            byte[] data = Encoding.UTF8.GetBytes(source);
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(data))
                    .Replace("-", string.Empty);
            }
        }

        public static string ComputeFileMD5(this string path)
        {
            if (!File.Exists(path))
            {
                return String.Empty;
            }

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    byte[] data = md5.ComputeHash(stream);
                    return BitConverter.ToString(data)
                        .Replace("-", string.Empty);
                }
            }
        }
    }
}
