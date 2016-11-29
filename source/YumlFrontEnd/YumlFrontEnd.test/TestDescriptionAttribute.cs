using NUnit.Framework;

namespace UmlSketch.Test
{
    /// <summary>
    /// extension of NUnit test attribute
    /// let's the user enter a description directly 
    /// as attribute argument
    /// </summary>
    public class TestDescriptionAttribute : TestAttribute
    {
        public TestDescriptionAttribute(string description = "")
        {
            Description = description;
        }
    }
}
