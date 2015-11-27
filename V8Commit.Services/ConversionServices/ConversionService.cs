using System;
using System.Security;

namespace V8Commit.Services
{
    public static class ConversionService
    {
        public static SecureString ToSecureString(string source)
        {
            SecureString result = new SecureString();
            if (string.IsNullOrEmpty(source))
            {
                return result;
            }

            foreach (char c in source.ToCharArray())
            {
                result.AppendChar(c);
            }

            return result;
        }

        public static DateTime Uint64ToDate(UInt64 source)
        {
            DateTime start = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);       
            return start.AddMilliseconds(source / 1000 * 100);
        }

    }
}
