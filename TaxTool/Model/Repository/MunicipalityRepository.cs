using Microsoft.EntityFrameworkCore;
using TaxTool.Model.DTO;
using TaxTool.Model.Entity;
using TaxTool.Model.Exceptions;

namespace TaxTool.Model.Repository;

public class MunicipalityRepository(TaxContext db) : IMunicipalityRepository
{
    public async Task<MunicipalityDto> GetMunicipality(string name)
    {
        var municipality = await db.Municipalities
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync();
        if (municipality == null)
        {
            throw new MissingMunicipalityException();
        }

        return new MunicipalityDto
        {
            Name = municipality.Name,
            Id = municipality.Id
        };
    }
}