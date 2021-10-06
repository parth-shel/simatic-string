using System;

namespace SimaticLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            bool done = false;
            do
            {
                try
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.Write("enter Simatic string (or empty string to give up): ");
                    string s = Console.ReadLine();

                    if (String.IsNullOrEmpty(s))
                    {
                        done = true;
                    }

                    Console.WriteLine("creating simatic string...");

                    SimaticString ss = new SimaticString(s.Length);

                    Console.WriteLine($"max length: {ss.MaxLength} - length: {ss.Length}");

                    Console.WriteLine("populating simatic string...");

                    ss.Set(s);

                    Console.WriteLine($"max length: {ss.MaxLength} - length: {ss.Length}");
                    Console.WriteLine($"string representation: '{ss.ToString()}'");
                    Console.WriteLine($"byte representation: {BitConverter.ToString(ss.GetBytes())}");

                    Console.WriteLine("reversing simatic string...");

                    ss.Reverse();

                    Console.WriteLine($"max length: {ss.MaxLength} - length: {ss.Length}");
                    Console.WriteLine($"string representation: '{ss.ToString()}'");
                    Console.WriteLine($"byte representation: {BitConverter.ToString(ss.GetBytes())}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception Caught! - {ex.Message}");
                }
            }
            while (!done);
        }
    }
}
