namespace Delegates.Examples
{
    public class DelegatesArePartOfTheCommonTypeSystem
    {
        private delegate int Add(int left, int right);

        private int Total(int left, int right)
        {
            return left + right;
        }

        public void DelegateCanBeAssignedToObject()
        {
            Add add = (left, right) => left + right;
            
            object obj = new Add(Total);
        }
    }
}