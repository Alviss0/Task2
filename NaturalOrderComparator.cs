namespace Lab3Sort_Tests
{
    public class NaturalOrderComparator : IComparator<int>
    {
        public int Compare(int first, int second)
        {
            return first - second;
        }
    }
}