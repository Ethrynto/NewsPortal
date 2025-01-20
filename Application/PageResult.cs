namespace Application;

public class PageResult<T>
{
    public T[] Data { get; }

    public PageResult(T[] data, int total)
    {
        Data = data;
    }
}