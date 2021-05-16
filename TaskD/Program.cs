using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        var bfr = new BinaryFileReader("input.bin");
        Console.WriteLine(bfr.GetDifference());
    }
}