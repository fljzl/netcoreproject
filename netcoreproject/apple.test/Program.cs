using apple.test.BaseTest;
using System;

namespace apple.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = TestFactory.GetMyTest(0);
            test.Log();
            Console.ReadKey();
        }
    }
}
