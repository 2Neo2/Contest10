using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        var N = ushort.Parse(File.ReadAllText("input.txt").Split("\n")[0]);
        var text = File.ReadAllText("input.txt").Split("\n");
        ushort[] numbers = new ushort[N];
        int index = 0;
        for (int i = 1; i < text.Length - 1; i++)
        {
            numbers[index] = ushort.Parse(text[i]);
            index += 1;
        }

        using (BinaryWriter binaryWriter = new BinaryWriter(File.Open("output.bin", FileMode.Open, FileAccess.Write)))
        {
            binaryWriter.Write(N);
            foreach (var item in numbers)
            {
                binaryWriter.Write(item);
            }
        }
    }
}