using System;
using System.Collections.Generic;
using System.IO;

public class BinaryFileReader
{
    private string path;

    public BinaryFileReader(string path)
    {
        this.path = path;
    }

    public int GetDifference()
    {
        Int16 n1 = 0;
        Int32 n2 = 0;
        using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read) ,System.Text.Encoding.Unicode))
        {
            if (binaryReader.PeekChar() > -1)
            {
                n1 += binaryReader.ReadInt16();
            }
        }
        using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read) ,System.Text.Encoding.Unicode))
        {
            while (binaryReader.PeekChar() > -1)
            {
                n2 += binaryReader.ReadInt32();
            }
        }
        return Math.Abs(n1 - n2);
    }
}