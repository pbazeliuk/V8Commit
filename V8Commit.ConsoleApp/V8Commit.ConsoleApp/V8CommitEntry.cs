using System;
using V8Commit.Repositories;
using V8Commit.Services;

namespace V8Commit.ConsoleApp
{
    class V8CommitEntry
    {
        static void Main(string[] args)
        {
            string output = @"D:\V8Commit\service\output\";

            IV8CommitRepository testContainer = new FileV8CommitRepository(@"D:\V8Commit\service\data\Обработка1.epf");
            var fileSystem = testContainer.ReadV8FileSystem();

            foreach (var c in fileSystem.References)
            {
                Console.WriteLine("\nSystem reference:");
                Console.WriteLine(ConversionService.Uint64ToDate(c.FileHeader.CreationDate));
                Console.WriteLine(ConversionService.Uint64ToDate(c.FileHeader.ModificationDate));
                Console.WriteLine(c.FileHeader.ReservedField);
                Console.WriteLine(c.FileHeader.FileName);
            }

            testContainer.WriteToOutputDirectory(fileSystem, output);

        }
    }
}
