using System;

namespace SimaticLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                SimaticString ss = new SimaticString();
                Console.WriteLine($"max length: {ss.MaxLength} - length: {ss.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Caught! - {ex.Message}");
            }
        }
    }
}
