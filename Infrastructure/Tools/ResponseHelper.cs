namespace Infrastructure.Tools;

public abstract class ResponseHelperBase
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}

public class ResponseHelper : ResponseHelperBase
{
    public ResponseHelper()
    {
        IsSuccess = false;
    }
}

public class ResponseHelper<T> : ResponseHelperBase
{
    public T? Result { get; set; }

}