using System;
using System.Collections;
using System.Collections.Generic;

class Methods 
{
    public static T Max<T>(T a, T b)
    {
        if (a is double value1 && b is double value2)
        {
            if (value1 > value2)
                return a;
            else if (value1 < value2)
                return b;
        }
        return b;
    }
}

