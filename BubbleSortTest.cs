using NUnit.Framework;

namespace Lab3Sort_Tests
{
    public class BubbleSortTest
    {
        [Test]
        public void TestSortArrInNaturalOrder()
        {
            int[] arr = {1, 6, -9, 5, 1, 3};

            int[] expectedResult = {-9, 1, 1, 3, 5, 6};

            new BubbleSort<int>().sortBy(arr, new NaturalOrderComparator());
            
            Assert.AreEqual(expectedResult, arr);
        }
    }

    #region need
    public class BubbleSort<T> : ISorting<T>
    {
        public void sortBy(T[] arr, IComparator<T> elementComparator)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (elementComparator.Compare(arr[i], arr[j]) > 0)
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }
            }
        }
    }

    public interface IComparator<T>
    {

        /**
         * Сравнивает два объекта и возвращает:
         *  - положительное число, если первый объект больше второго;
         *  - 0, если числа равны;
         *  - отрицательное число, если первый объект меньше второго.
         */
        public int Compare(T first, T second);
    }

    public interface ISorting<T>
    {
        public void sortBy(T[] array, IComparator<T> elementComparator);
    }
    #endregion
}