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
using Service.Loader;
using CommandLine;

namespace V8Commit.ConsoleApp
{
    class V8CommitEntry
    {
        static void Main(string[] args)
        {
            //AssemblyLoader.ResolveAssemblies<Assemblies>(AppDomain.CurrentDomain);

            var help = new StringWriter();
            new Parser(with => with.HelpWriter = help)
                .ParseArguments(args, typeof(ParseVerb))
                    .MapResult(
                        (ParseVerb opts) => opts.Invoke(),
                        errors => {
                            Console.WriteLine(help.ToString());
                            return 1;
                        });
        }
    }
}
