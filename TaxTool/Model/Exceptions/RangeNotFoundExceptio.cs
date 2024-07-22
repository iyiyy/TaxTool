namespace TaxTool.Model.Exceptions;

public class RangeNotFoundException : Exception
{
    public RangeNotFoundException() {}
    public RangeNotFoundException(string message): base(message) {}
}