using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using Assessment_test;

namespace Contractor_NUnit_testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            dataManager = new DataManager();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}