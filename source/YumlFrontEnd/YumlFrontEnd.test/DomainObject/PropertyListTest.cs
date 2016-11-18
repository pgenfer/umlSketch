using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using static NSubstitute.Substitute;

namespace Yuml.Test
{
    public class PropertyListTest : TestBase
    {
        private ClassifierDictionary _classifiers;

        protected override void Init()
        {
            _classifiers = CreateDictionaryWithoutSystemTypes();
            _classifiers.String.Returns(String);
        }


        [TestDescription("Check that auto generation of property names work correctly")]
        public void CreateNewPropertyWithBestInitialValues_NameTest()
        {
            var properties = new PropertyList();
            properties.CreateProperty("New Property 1", String);
            properties.CreateProperty("New Property 2", String);

            var newProperty = properties.CreateNew(_classifiers);

            Assert.AreEqual("New Property 3", newProperty.Name);
            Assert.AreEqual(String, newProperty.Type);
        }

        [TestDescription("Check that auto generation of property names work correctly with different names")]
        public void CreateNewPropertyWithBestInitialValues_DifferentNameTest()
        {
            var properties = new PropertyList();
            properties.CreateProperty("New Property 1", String);
            properties.CreateProperty("Name", String);

            var newProperty = properties.CreateNew(_classifiers);

            Assert.AreEqual("New Property 2", newProperty.Name);
            Assert.AreEqual(String, newProperty.Type);
        }

        [TestDescription("Check that auto generation of property uses commonly used type")]
        public void CreateNewPropertyWithBestInitialValues_TypeTest()
        {
            var properties = new PropertyList();
            properties.CreateProperty("New Property 1", Integer);
            properties.CreateProperty("New Property 2", Integer);
            properties.CreateProperty("New Property 3", String);

            var newProperty = properties.CreateNew(_classifiers);

            Assert.AreEqual("New Property 4", newProperty.Name);
            Assert.AreEqual(Integer, newProperty.Type);
        }

        [TestDescription("Check that auto generation of property when creating on empty list")]
        public void CreateNewPropertyWithBestInitialValues_InitialListTest()
        {
            var properties = new PropertyList();

            var newProperty = properties.CreateNew(_classifiers);

            Assert.AreEqual("New Property 1", newProperty.Name);
            Assert.AreEqual(String, newProperty.Type);
        }
    }
}