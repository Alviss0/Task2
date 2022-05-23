using NUnit.Framework;
using System;

namespace Lab3Sort_Tests
{
    public class KeyComparatorTest
    {
        [Test]
        public void TestComparePersonsByName()
        {
            Person firstPerson = new Person("Boba");
            Person secondPerson = new Person("Biba");
            Person thirdPerson = new Person("Boba");

            IComparator<Person> nameComparator = new KeyComparator<Person, string>(person => person.Name);
            
            int actualResult = nameComparator.Compare(firstPerson, secondPerson);
            Assert.Greater(actualResult, 0);
            
            actualResult = nameComparator.Compare(secondPerson, firstPerson);
            Assert.Less(actualResult, 0);

            actualResult = nameComparator.Compare(thirdPerson, firstPerson);
            Assert.AreEqual(0, actualResult);
        }

        private class Person
        {
            public string Name { get; }

            public Person(string name)
            { 
                Name = name;
            }
        }
    }

    public class KeyComparator<T, K> : IComparator<T> where K : IComparable
    {
        public delegate K KeyExtractor(T obj);

        //делегат извлекает ключи из сравниваемых объектов
        private readonly KeyExtractor _keyExtractor;

        public KeyComparator(KeyExtractor keyExtractor)
        {
            _keyExtractor = keyExtractor;
        }

        public int Compare(T first, T second)
        {
            K firstKey = _keyExtractor.Invoke(first);
            K secondKey = _keyExtractor.Invoke(second);

            return firstKey.CompareTo(secondKey);
        }
    }
}