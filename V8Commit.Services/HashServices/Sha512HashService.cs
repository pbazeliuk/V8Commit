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
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace V8Commit.Services.HashServices
{
    public class Sha512HashService : IHashService
    {
        public string HashFile(string path)
        {
            if (!File.Exists(path))
            {
                return String.Empty;
            }

            using (var sha512Hash = new SHA512Managed())
            {
                using (var stream = File.OpenRead(path))
                {
                    byte[] rawData = sha512Hash.ComputeHash(stream);
                    return BitConverter.ToString(rawData).Replace("-", String.Empty);
                }
            }
        }
        public string HashString(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return String.Empty;
            }

            byte[] rawData = Encoding.UTF8.GetBytes(source);
            return BitConverter.ToString(MakeHash(rawData)).Replace("-", String.Empty);
        }
        public string HashSecure(SecureString source)
        {
            if (source.Length == 0)
            {
                return String.Empty;
            }

            IntPtr bstr = Marshal.SecureStringToBSTR(source);
            try
            {
                byte[] rawData = new byte[source.Length];
                Marshal.Copy(bstr, rawData, 0, rawData.Length);
                return BitConverter.ToString(MakeHash(rawData)).Replace("-", String.Empty);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(bstr);
            }
        }

        private byte[] MakeHash(byte[] data)
        {
            using (var sha512Hash = new SHA512Managed())
            {
                return sha512Hash.ComputeHash(data);
            }
        }
    }
}
