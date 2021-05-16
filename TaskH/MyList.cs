using System;
using System.Linq;
using System.Text;

public class MyList<T>
{
    private T[] _items;

    public MyList()
    {
        _items = new T[0];
    }

    public MyList(int capacity)
    {
        _items = new T[capacity];
    }

    public int Count => _items.Count(x => x!= null);

    public int Capacity => _items.Length;


    public void Add(T element)
    {
        if (Capacity == 0)
        {
            _items = new T[4];
        }
        else if (Count == Capacity)
        {
            T[] resizeArr = new T[Capacity * 2];
            for (int i = 0; i < Count; i++)
                resizeArr[i] = _items[i];
            _items = resizeArr;
        }
        _items[Count] = element;
    }

    public T this[int index]
    {
        get
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            if (index < 0)
                throw new IndexOutOfRangeException();
            return _items[index];
        }
    }

    public void Clear()
    {
        _items = new T[Capacity];
    }

    public void RemoveLast()
    {
        if (Count <= 0 )
            throw new IndexOutOfRangeException();
        else
        {
            T[] arr = new T[Count - 1];
            for (int i = 0; i < Count - 1 ;i++)
            {
                arr[i] = _items[i];
            }
            _items = arr;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < Count && index >= 0)
        {
            var arr = new T[_items.Length];
            for (int i = 0, j = 0; i < Count - 1; i++, j++)
            {
                if (i == index)
                    j++;
                arr[i] = _items[j];
            }

            _items = arr;
        }
        else 
            throw new IndexOutOfRangeException();
    }

    public T Max()
    {
        T max = _items[0];
        if (Count == 0 )
            throw new IndexOutOfRangeException();
        if (!(max is IComparable comparable))
            throw new NotSupportedException("This operation is not supported for this type");
        for (int i = 1; i < Count; i++)
            if (((IComparable) _items[i]).CompareTo(comparable) >= 0)
                max = _items[i];
        return max;
    }

    public override string ToString()
    {
        string result = "";
        foreach (var item in _items)
        {
            result += item + " ";
        }
        return result;
    }
}