namespace TaxTool.Model.DTO;

public class TaxRecordDto
{
    public string municipality { get; set; }
    public double rate { get; set; }
    public DateTimeOffset from { get; set; }
    public DateTimeOffset to { get; set; }
}