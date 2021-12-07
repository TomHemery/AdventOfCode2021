public class FixedIndexedQueue<T>
{
    T[] array;
    int start;
    int len;

    public FixedIndexedQueue(int initialBufferSize)
    {
        array = new T[initialBufferSize];
        start = 0;
        len = 0;
    }

    public void Enqueue(T t)
    {          
        array[(start + len) % array.Length] = t;
        ++len;
    }

    public T Dequeue()
    {
        var result = array[start];
        start = (start + 1) % array.Length;
        --len;
        return result;
    }

    public int Count { get { return len; } }

    public T this[int index]
    {
        get 
        { 
            return array[(start + index) % array.Length]; 
        }

        set
        {
            array[(start + index) % array.Length] = value;
        }
    }        

    public System.Collections.IEnumerator GetEnumerator () 
    {
        return array.GetEnumerator();
    }

    public override string ToString()
    {
        string res = "[";
        res += this[0];
        for(int i = 1; i < array.Length; i++) {
            res += ", " + this[i];
        }
        res += "]";
        return res;
    }
}