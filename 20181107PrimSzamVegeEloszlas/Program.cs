using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181107PrimSzamVegeEloszlas
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] hist = new int[10];
            const int epochLength = 1000000;
            var file = File.CreateText("primes.csv");
            for(int epoch = 0; epoch<20; epoch++)
            {
                int start = epoch * epochLength;
                int end = (epoch + 1) * epochLength - 1;
                for (int j = 0; j < 10; j++)
                    hist[j] = 0;
                for (int i = start; i <= end; i++)
                    if (IsPrime(i))
                        hist[i % 10]++;
                Console.WriteLine($"--- Interval: {start}-{end} ---");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write($"#{j}:{hist[j]} ");
                    file.Write($"{hist[j]};");
                }
                Console.WriteLine("");
                file.WriteLine("");
            }
            file.Close();
        }

        private static bool IsPrime(int n)
        {
            for (int i = 2; i <= (int)Math.Ceiling(Math.Sqrt(n))+1; i++)
                if (n % i == 0)
                    return false;
            return true;
        }
    }
}
