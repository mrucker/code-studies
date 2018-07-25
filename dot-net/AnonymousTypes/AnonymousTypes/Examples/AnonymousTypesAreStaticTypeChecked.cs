using NUnit.Framework;

namespace AnonymousTypes.Examples
{
    class AnonymousTypesAreStaticTypeChecked
    {
        public void UnableToReadFromNewPropertyOnAnonymousType()
        {
            var a = new {FirstName = "John", LastName = "Doe"};

            Assert.AreEqual("John", a.FirstName);

            Assert.AreEqual("The", a.MiddleName);
        }
    }
}
