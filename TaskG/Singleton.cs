using System;
using System.Collections.Generic;

class Singleton<T> where T : new()
{
    private static T parametr = new T();
    private List<T> n = new List<T>();
    public static T Instance
    {
        get
        {
            return parametr;
        }
        set
        {
            throw new NotSupportedException("This operation is not supported");
        }
    }
}
