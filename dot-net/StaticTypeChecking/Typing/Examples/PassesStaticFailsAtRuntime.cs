
using System;

namespace StaticTypeChecking.Examples
{
    public class PassesStaticFailsAtRuntime
    {
        /// <summary>
        /// This passes static type-checking but would fail at runtime throwing a System.FormatException
        /// </summary>
        public void PassesStaticTypeCheckFailsAtRunTime()
        {
            int result = 1 + Convert.ToInt32("a");
        }
    }
}
