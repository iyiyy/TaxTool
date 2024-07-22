using TaxTool.Model.DTO;

namespace TaxTool.Model.Repository;

public interface ITaxRecordRepository
{
    Task AddAsync(TaxRecordDto taxRecordDto);
    Task UpdateAsync(TaxRecordDto current, TaxRecordDto updated);
    Task<TaxRecordDto> GetAsync(TaxRecordDto target);
}