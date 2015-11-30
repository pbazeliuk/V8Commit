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
