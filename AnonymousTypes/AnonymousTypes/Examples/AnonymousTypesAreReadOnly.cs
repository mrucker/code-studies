namespace AnonymousTypes.Examples
{
    class AnonymousTypesAreReadOnly
    {
        public void AnonymousTypePropertiesAreReadOnly()
        {
            var anon = new {FirstName = "John", LastName = "Doe"};

            anon.FirstName = "Steve";
        }
    }
}
