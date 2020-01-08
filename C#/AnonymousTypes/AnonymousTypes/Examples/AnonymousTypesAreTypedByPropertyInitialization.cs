using NUnit.Framework;

namespace AnonymousTypes.Examples
{
    class AnonymousTypesAreTypedByPropertyInitialization
    {
        public void TwoAnonymousTypesOfSameTypeBecauseIdenticalPropertiesInitizializedInSameOrder()
        {
            var anon1 = new {FirstName = "John", LastName = "Doe"};
            var anon2 = new {FirstName = "Jane", LastName = "Doe" };

            Assert.AreNotSame(anon1, anon2);

            anon1 = anon2;
            
            Assert.AreSame(anon1, anon2);

        }

        public void TwoAnonymousTypesOfDifferentTypeBecausePropertiesInitializedInDifferentOrder()
        {
            var anon1 = new { FirstName = "John", LastName = "Doe" };
            var anon2 = new { LastName = "Doe", FirstName = "Jane"};

            Assert.AreNotSame(anon1, anon2);

            anon1 = anon2;

            Assert.AreSame(anon1, anon2);
        }
    }
}
