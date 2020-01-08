namespace AnonymousTypes.Examples
{
    class AnonymousTypesArePartOfTheCommonTypeSystem
    {
        public void AnonymousTypesInheritFromObject()
        {
            var a    = new {Name = "John Doe"};
            object b = a;
        }
    }
}
