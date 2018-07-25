namespace Delegates.Examples
{
    public class DelegatesAreNominallyAssigned
    {
        private delegate int Add(int left, int right);
        private delegate int Sum(int left, int right);

        public void DelegatesWithMatchingStructureButDifferentTypeNamesNotAssignableToEachother()
        {
            Add add = (left, right) => left + right;
            Sum sum = (left, right) => left + right;

            //not allowed
            //sum = add;
        }

    }
}