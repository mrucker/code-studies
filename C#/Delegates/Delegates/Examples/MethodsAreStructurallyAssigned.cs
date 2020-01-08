namespace Delegates.Examples
{
    public class MethodsAreStructurallyAssigned
    {
        private delegate int Add(int left, int right);
        private delegate int Sum(int left, int right);
 
        private int Total1(int left, int right)
        {
            return left + right;
        }

        private int Total2(int left, int right)
        {
            return left + right;
        }

        private int InvokeAdd(Add add, int left, int right)
        {
            return add.Invoke(left, right);
        }

        public void MethodsCanBeAssignedToAnyDelegateWithMatchingSignature()
        {
            Add add = Total1;
            Sum sum = Total1;
        }

        public void MethodsCanBeAssignedToAnyParameterWithMatchingSignature()
        {
            InvokeAdd(Total1, 1, 1);
            InvokeAdd(Total2, 2, 2);
        }

        public void NamedAndAnonymousMethodsCanBeAssignedToDelegates()
        {
            Add add1 = Total1;
            Add add2 = (left, right) => left + right;            
        }
    }
}