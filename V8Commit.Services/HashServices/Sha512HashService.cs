﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace V8Commit.Services.HashServices
{
    public class Sha512HashService : IHashService
    {
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