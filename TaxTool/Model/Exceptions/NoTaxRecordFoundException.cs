namespace TaxTool.Model.Exceptions;

public class NoTaxRecordFoundException : Exception
{
    public NoTaxRecordFoundException() {}
    public NoTaxRecordFoundException(string message): base(message) {}
}