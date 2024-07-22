using TaxTool.Model.DTO;

namespace TaxTool.Model.Repository;

public interface IMunicipalityRepository
{
    Task<MunicipalityDto> GetMunicipality(string name);
}