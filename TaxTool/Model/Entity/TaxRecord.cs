using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxTool.Model.Entity;

[Table("TaxRecord")]
public class TaxRecord
{
    public Guid Id { get; set; }
    
    [Required]public DateTimeOffset FromDate { get; set; }
    [Required]public DateTimeOffset ToDate { get; set; }
    [Required]public double TaxRate { get; set; }
    [Required]public PeriodType Type { get; set; }
    
    public Guid MunicipalityId { get; set; }
    public Municipality Municipality { get; set; }
}

public enum PeriodType
{
    Monthly,
    Daily,
    Yearly
}

