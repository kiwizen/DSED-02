using System;


namespace FilePathConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            Console.Read();
        }
    }
}