using NUnit.Framework;
using System;
using System.Linq;

namespace Yuml.Test
{

    /// <summary>
    /// base class that contains common helper functions
    /// used by other test cases
    /// </summary>
    [TestFixture]
    public class SimpleTestBase
    {
        private const int RandomStringLength = 10;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomString(int? seed = null) =>
            new string(
                Enumerable.Repeat(Alphabet, RandomStringLength)
                .Select(s => s[new Random(seed ?? (int)DateTime.Now.Ticks).Next(s.Length)])
                .ToArray());

        protected virtual void Init() { /* default implementation does not have initialization */}

        protected ClassifierDictionary CreateDictionaryWithoutSystemTypes()
         => NSubstitute.Substitute.For<ClassifierDictionary>(false);


        [SetUp]
        public void Setup() => Init();
    }
}