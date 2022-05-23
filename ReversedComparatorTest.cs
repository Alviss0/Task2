using NUnit.Framework;

namespace Lab3Sort_Tests
{
    public class ReversedComparatorTest
    {
        [Test]
        public void TestReversedComparatorByTwoNumbers()
        {
            IComparator<int> comparator = new NaturalOrderComparator();
            IComparator<int> reversedComparator = new ReversCmprtor<int>(comparator);

            int actualResult = reversedComparator.Compare(-2, 6);
            Assert.Greater(actualResult, 0);
            
            actualResult = reversedComparator.Compare(8, 8);
            Assert.AreEqual(0, actualResult);
            
            actualResult = reversedComparator.Compare(6, -2);
            Assert.Less(actualResult, 0);
        }
    }

    public class ReversCmprtor<T> : IComparator<T>
    {
        private IComparator<T> del;

        public ReversCmprtor(IComparator<T> del)
        {
            this.del = del;
        }

        public int Compare(T first, T second)
        {
            return -del.Compare(first, second);
        }

        public static IComparator<T> wrap(IComparator<T> original, bool asc)
        {
            if (asc)
            {
                return original;
            }

            return new ReversCmprtor<T>(original);
        }
    }
}