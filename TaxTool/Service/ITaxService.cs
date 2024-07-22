using TaxTool.Model.DTO;

namespace TaxTool.Service;

public interface ITaxService
{
    Task AddNewTaxRecordAsync(TaxRecordDto taxRecordDto);
    Task UpdateTaxRecordAsync(TaxRecordDto current, TaxRecordDto update);
    Task<TaxRecordDto> GetTaxRecordAsync(TaxRecordDto target);
}