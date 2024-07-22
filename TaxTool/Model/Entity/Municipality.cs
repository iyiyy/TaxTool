using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaxTool.Model.Entity;

[Table("Municipality")]
public class Municipality
{
    public Guid Id { get; set; }
    [Required] public String Name { get; set; }

    public List<TaxRecord> TaxRecords { get; } = new();
}