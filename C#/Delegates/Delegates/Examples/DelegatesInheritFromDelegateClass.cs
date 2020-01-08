using System;

namespace Delegates.Examples
{    
    public class DelegatesInheritFromDelegateClass
    {
        private delegate int Add(int left, int right);
        private delegate int Sum(int left, int right);
         
        public void DelegatesCanBeAssignedToDelegateTypedVariable()
        {
            var add = new Add((left, right) => left + right);
            var sum = new Sum((left, right) => left + right);

            add += (left, right) => left + right;

            int[] a;
            int[] b;

            add.Invoke(1,1);

            Delegate dAdd = add;
            Delegate dSum = sum;
        }
    }
}