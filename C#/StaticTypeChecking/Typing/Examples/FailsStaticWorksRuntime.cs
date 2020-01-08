using NUnit.Framework;

namespace StaticTypeChecking.Examples
{
    class FailsStaticWorksRuntime
    {
        /// <summary>
        /// In theory this would work at runtime because the code that fails static type-checking would never execute.
        /// </summary>
        public void UnreachableCodeFailsStaticTypeCheck()
        {
            if (false)
            {
                int result = 1 + "a";
            }
            else
            {
                int result = 1 + 1;
            }
        }

        /// <summary>
        /// In memory both char and byte types are stored as 8 bits. Even so they can't be assigned to eachother implicitly.
        /// </summary>
        public void IdenticalBytesIncompatibleTypes()
        {
            byte aAsByte = 97;
            char aAsChar = 'a';

            Assert.IsTrue(aAsChar == aAsByte);
            
            aAsByte = aAsChar;
        }
    }
}
