
namespace AnonymousTypes.Examples
{
    class AnonymousTypesCanUseHanldeName
    {
        public void AnonymousTypesWillUseHandleNameWhenNothingElseGiven()
        {
            var fullname = new {FirstName = "John", LastName = "Doe"};
            
            var a = new {fullname};
            fullname = a.fullname;

            var fullname2 = new {fullname.FirstName, fullname.LastName};
            fullname = fullname2;
        }
    }
}
