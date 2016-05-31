using System;

namespace HelloWorld.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var helloWorld = new ApplicationHelloWorld();
            Console.WriteLine(helloWorld.SayHelloWorld());
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
