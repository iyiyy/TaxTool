namespace TaxTool.Model.Exceptions;

public class TaxRecordAlreadyExistsException : Exception
{
    public TaxRecordAlreadyExistsException() {}
    public TaxRecordAlreadyExistsException(string message): base(message) {}
}