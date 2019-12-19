using System;
using System.Threading;

namespace Threads_001
{
    class Program
    {
        enum words
        {
            cool = 1,
            super = 2,
            the_best = 3, 
            clever = 4,
            beautiful = 5,
            kind = 6,
        }
        static void Main(string[] args)
        {
            Thread thread = new Thread(Method);
            thread.Start();

        }

        static void Method()
        {
            Random random = new Random();
            Console.WriteLine("You are "+(words)random.Next(1, 6));
            Thread.Sleep(200);
            Thread thread = new Thread(Method);
            thread.Start();
        } 
    }
}
