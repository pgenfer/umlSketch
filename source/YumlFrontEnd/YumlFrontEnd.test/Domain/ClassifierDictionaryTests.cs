using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    [TestFixture]
    public class ClassifierDictionaryTests
    {
        [Test]
        public void CreateNewClassTest()
        {
            var classifierDictionary = new ClassifierDictionary();
            var newClass = classifierDictionary.CreateNewClass("Class");

            Assert.AreEqual("Class", newClass.Name);
        }
    }
}