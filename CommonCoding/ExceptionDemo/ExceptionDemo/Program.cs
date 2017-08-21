using System;

namespace ExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            var start = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                string test = "test " + i;

                try
                {
                    int.Parse(test);
                }
                catch (Exception)
                {
                    // error handling
                }
            }

            Console.WriteLine("With excpetions: {0}ms", (DateTime.Now - start).TotalMilliseconds);

            start = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                string test = "test " + i;

                int x;
                var result = int.TryParse(test, out x);
                if (!result)
                {
                    // error handling

                }
            }

            Console.WriteLine("Without excpetions: {0}ms", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine("Press key");
            Console.ReadKey();
        }
    }
}
