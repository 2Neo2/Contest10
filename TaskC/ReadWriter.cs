using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

public class ReadWriter
{
    public static Tuple<char, char> GetMostAndLeastCommonLetters(string path)
    {
        var line = File.ReadAllText(path).ToLower();
        int letter = 0;
        char mostCommonLetter = '\u0000';
        char leastCommonLetter = '\u0000';
        Dictionary<char, int> letters = new Dictionary<char, int>();

        for (int i = 0; i < line.Length; i++)
        {
            int count = 1;
            if (Char.IsLetter(line[i]))
            {
                letter += 1;
                if (letters.ContainsKey(line[i]))
                    letters[line[i]] += 1;
                else
                    letters.Add(line[i], count);
            }
        }
        
        if (letter == 0)
            return new Tuple<char, char>(' ',' ');
        
        int maxValue = letters.Values.Max();
        int minValue = letters.Values.Min();
        bool flag = true;
        foreach (var key in letters)
        {
            if (key.Value == maxValue)
            {
                if (flag)
                {
                    mostCommonLetter = key.Key;
                    flag = false;
                }
            }
            if (key.Value == minValue)
            {
                leastCommonLetter = key.Key;
                break;
            }
        }
        return new Tuple<char, char>(leastCommonLetter, mostCommonLetter);
    }

    public static void ReplaceMostRareLetter(Tuple<char, char> leastAndMostCommon, string inputPath, string outputPath)
    {
        var line = File.ReadAllText(inputPath);
        string replaceText = "";
        for (int i = 0; i < line.Length; i++)
        {
            if (Char.IsLetter(line[i]))
            {
                if (line[i].ToString().ToLower() == leastAndMostCommon.Item1.ToString())
                {
                    if (line[i] >= 65 && line[i] <= 90)
                        replaceText += leastAndMostCommon.Item2.ToString().ToUpper();
                    else if (line[i] >= 97 && line[i] <= 122)
                        replaceText += leastAndMostCommon.Item2.ToString().ToLower();
                }
                else        
                    replaceText += line[i].ToString();
            }
            else
                replaceText += line[i].ToString();
        }
        File.WriteAllText(outputPath, replaceText);
    }
}