using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using V8Commit.Repositories;
using V8Commit.Services;
using V8Commit.Services.HashServices;

namespace V8Commit.ConsoleApp
{
    class V8CommitEntry
    {
        static void Main(string[] args)
        {
            IV8CommitRepository testContainer = new FileV8CommitRepository(@"D:\V8Commit\V8Commit.TestData\btc-e.cf");
            var container = testContainer.ReadContainerHeader();
            Console.WriteLine(container.RefToNextPage);
            Console.WriteLine(container.PageSize);
            Console.WriteLine(container.PagesCount);
            Console.WriteLine(container.ReservedField);

            var blockHeader = testContainer.ReadBlockHeader();
            Console.WriteLine(blockHeader.DataSize);
            Console.WriteLine(blockHeader.PageSize);
            Console.WriteLine(blockHeader.RefToNextPage);

        }
    }
}
