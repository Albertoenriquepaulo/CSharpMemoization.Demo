using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Memoization.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            Func<long, long> factorialFunc = null;
            factorialFunc = (n) => n > 1 ? n * factorialFunc(n - 1) : 1;

            var stopWatch = Stopwatch.StartNew();
            for (var i = 0; i < 20000000; i++)
            {
                factorialFunc(9);
            }
            Console.WriteLine(stopWatch.ElapsedMilliseconds);


            var stopWatch1 = Stopwatch.StartNew();
            var factorial = factorialFunc.Memoize(); // factorial is type of factorialFunc, and at the same time send the function itself to the Memoize method
            for (var i = 0; i < 20000000; i++)
            {
                factorial(9);
            }
            Console.WriteLine(stopWatch1.ElapsedMilliseconds);
        }


        // Program class MUST BE static 

        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func)
        {   // This means, that this method will be available (or Added) to all function who has one input parameter and one output parameter
            // and this is because of "this"
            var cache = new ConcurrentDictionary<T, TResult>();  // It is a concurrent dictionary
                                                                 // TResult will be a function

            return a => cache.GetOrAdd(a, func);                // we return a lambda function, which is a representation of the function
                                                                // "a" is the number, if doesn't exist it is added if not it is calculated
                                                                // that's why the method is called "GetOrAdd"

            //This is a dictionary of functions
        }
    }
}
