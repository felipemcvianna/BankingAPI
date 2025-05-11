namespace Banking.Exceptions.ExceptionBase;

public interface ICustomHttpException
{
    object ToResult();
    int StatusCodes { get; }
}