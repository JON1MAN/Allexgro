
public class Response<T>
{
    public bool isSuccessful { get; set; }
    public T Body { get; set; }
    public string Message { get; set; }
}