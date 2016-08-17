﻿/**
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

namespace V8Commit.Infrastructure
{
    public static class UInt64Extensions
    {
        public static DateTime ConvertToDate(this UInt64 source)
        {
            DateTime start = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(source / 1000 * 100);
        }
    }
}
