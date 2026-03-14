namespace Calc202603;

public class Stack

{
    private const int Capacity = 50;
    private string[] _array = new string[Capacity];
    private int _pointer = 0;

    public void Push(string value)
    {
        if (_pointer == _array.Length)
        {
            var extendedArray = new string[_array.Length * 2];
            for (var i = 0; i < _array.Length; i++)
            {
                extendedArray[i] = _array[i];
            }

            _array = extendedArray;
        }

        _array[_pointer] = value;
        _pointer++;
    }

    public string Pull()
    {
        if (_pointer == 0)
        {
            return null;
        }

        _pointer--;
        var value = _array[_pointer];
        return value;
    }

    public int Count()
    {
        return _pointer;
    }

    public string GetTop()
    {
        if (_pointer == 0)
        {
            return null;
        }

        var value = _array[_pointer - 1];

        return value;
    }
    
    public string AsString()
    {
        string _value = "";

        for (int _idx = 0; _idx < _pointer; _idx++)
        {
            _value += _array[_idx];
        }

        return _value;
    }
}