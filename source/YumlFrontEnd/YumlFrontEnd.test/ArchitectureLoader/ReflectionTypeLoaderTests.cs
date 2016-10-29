//using NUnit.Framework;
//using Yuml.TypeLoaders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Yuml.Test
//{
//    [TestFixture]
//    public class ReflectionTypeLoaderTests
//    {
//        [TestDescription(
//            @"Loads a type via reflection and stores it information
//             in the classifier dictionary")]
//        public void LoadReflectionTypeLoader_LoadType()
//        {
//            var classDictionary = new ClassifierDictionary();
//            var loader = new ReflectionTypeLoader(classDictionary);

//            loader.LoadType(typeof(string));

//            Assert.IsNotNull(classDictionary.FindByName(typeof(string).Name));
//        }
//    }
//}