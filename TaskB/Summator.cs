using System;
using System.IO;

public class Summator
{
    private int sum = 0;
    public Summator(string path)
    {
        using (StreamReader st = new StreamReader(path))
        {
            while (!st.EndOfStream)
            {
                string line = st.ReadLine();
                string[] numbers = line.Split("_");
                for (int i = 0 ; i < numbers.Length; i ++ )
                {
                    int number = int.Parse(numbers[i]);
                    sum += number;
                }
            }
            st.Close();
        }
    }

    public int Sum
    {
        get => sum;

    }
}