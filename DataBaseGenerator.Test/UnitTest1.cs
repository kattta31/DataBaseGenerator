using System;
using DataBaseGenerator.Core.GeneratorRules.WorkList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseGenerator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RandomNameGenerate()
        {
            var name = new RandomFirstNameRule();
            var result = name.Generate();

            Console.WriteLine(result);

        }

        [TestMethod]
        public void RandomLastNameGenerate()
        {
            var lastName = new RandomLastNameRule();
            var result = lastName.Generate();

            Console.WriteLine(result);

        }
    }
}
