using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SvnClean.Tests
{
    [TestFixture]
    internal class CleanUpFixture
    {
        [TestCase(@"C:\svn\chinook2")]
        public void GetNotVersionedItemsIsNotEmptyTest(string rootDirectory)
        {
            List<string> notVersioned = CleanUp.GetNotVersionedItems(rootDirectory);

            Assert.That(notVersioned == null, Is.False);

            foreach (string path in notVersioned)
            {
                Console.WriteLine(path);
            }
        }

        [TestCase(@"C:\svn\chinook2")]
        public void RunTest(string rootDirectory)
        {
            CleanUp.Run(rootDirectory);
        }
    }
}
