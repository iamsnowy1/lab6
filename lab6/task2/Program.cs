using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public delegate bool Criteria<T>(T item);

   
    public class Repository<T>
    {
        private List<T> items;

        public Repository(List<T> initialItems)
        {
            items = initialItems;
        }

       
        public List<T> Find(Criteria<T> criteria)
        {
            List<T> result = new List<T>();
            foreach (var item in items)
            {
                if (criteria(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Repository<int> intRepository = new Repository<int>(numbers);
            Console.WriteLine("Even numbers:");
            List<int> evenNumbers = intRepository.Find(x => x % 2 == 0);
            PrintList(evenNumbers);
            Console.WriteLine("Numbers greater than 5:");
            List<int> greaterThanFive = intRepository.Find(x => x > 5);
            PrintList(greaterThanFive);
            List<string> words = new List<string> { "apple", "banana", "orange", "grape", "kiwi" };
            Repository<string> stringRepository = new Repository<string>(words);
            Console.WriteLine("Words starting with 'a':");
            List<string> aWords = stringRepository.Find(s => s.StartsWith("a"));
            PrintList(aWords);
        }

       
        static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
