namespace TaxTool.Model.Exceptions;

public class MissingMunicipalityException : Exception
{
    public MissingMunicipalityException() {}
    public MissingMunicipalityException(string message): base(message) {}
}