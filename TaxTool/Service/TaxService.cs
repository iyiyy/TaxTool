using TaxTool.Model.DTO;
using TaxTool.Model.Exceptions;
using TaxTool.Model.Repository;

namespace TaxTool.Service;

public class TaxService(IMunicipalityRepository mr, ITaxRecordRepository tr) : ITaxService
{
    public async Task AddNewTaxRecordAsync(TaxRecordDto taxRecordDto)
    {
        // TODO: Validations
        await tr.AddAsync(taxRecordDto);
    }

    public async Task UpdateTaxRecordAsync(TaxRecordDto current, TaxRecordDto updated)
    {
        await tr.UpdateAsync(current, updated);
    }


    public async Task<TaxRecordDto> GetTaxRecordAsync(TaxRecordDto target)
    {
        return await tr.GetAsync(target);
    }
}