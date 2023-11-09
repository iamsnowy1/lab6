using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class FunctionCache<TKey, TResult>
    {
        private readonly Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();
        private readonly Func<TKey, TResult> function;

        public FunctionCache(Func<TKey, TResult> function)
        {
            this.function = function ?? throw new ArgumentNullException(nameof(function));
        }

        public TResult GetResult(TKey key)
        {
            if (cache.TryGetValue(key, out var cacheItem))
            {
                if (IsCacheItemValid(cacheItem))
                {
                    Console.WriteLine($"Using cached result for key: {key}");
                    return cacheItem.Result;
                }
            }

            
            TResult result = function(key);

            
            CacheItem newCacheItem = new CacheItem(result);
            cache[key] = newCacheItem;

            Console.WriteLine($"Calculating and caching result for key: {key}");

            return result;
        }

        public void SetCacheExpiration(TKey key, TimeSpan expirationTime)
        {
            if (cache.TryGetValue(key, out var cacheItem))
            {
                cacheItem.SetExpiration(expirationTime);
            }
        }

        private bool IsCacheItemValid(CacheItem cacheItem)
        {
            return cacheItem.ExpirationTime > DateTime.Now;
        }

        private class CacheItem
        {
            public TResult Result { get; }
            public DateTime ExpirationTime { get; private set; }

            public CacheItem(TResult result)
            {
                Result = result;
                SetExpiration(TimeSpan.MaxValue); 
            }

            public void SetExpiration(TimeSpan expirationTime)
            {
                DateTime newExpiration = DateTime.MaxValue;

               
                if (expirationTime != TimeSpan.MaxValue)
                {
                    newExpiration = DateTime.Now.Add(expirationTime);
                }

                ExpirationTime = newExpiration;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            
            Func<string, int> expensiveFunction = key =>
            {
                Console.WriteLine($"Executing expensive operation for key: {key}");
                return key.Length;
            };

            FunctionCache<string, int> cache = new FunctionCache<string, int>(expensiveFunction);

            
            int result1 = cache.GetResult("hello");
            Console.WriteLine($"Result 1: {result1}");

            
            int result2 = cache.GetResult("hello");
            Console.WriteLine($"Result 2: {result2}");

           
            cache.SetCacheExpiration("hello", TimeSpan.FromSeconds(2));

            
            System.Threading.Thread.Sleep(3000);

            
            int result3 = cache.GetResult("hello");
            Console.WriteLine($"Result 3: {result3}");
        }
    }
}
