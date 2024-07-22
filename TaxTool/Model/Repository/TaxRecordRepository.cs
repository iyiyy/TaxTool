using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaxTool.Model.DTO;
using TaxTool.Model.Entity;
using TaxTool.Model.Exceptions;

namespace TaxTool.Model.Repository;

public class TaxRecordRepository(
    TaxContext db,
    IMunicipalityRepository municipalityRepository)
    : ITaxRecordRepository
{
    public async Task AddAsync(TaxRecordDto taxRecordDto)
    {
        var mun = await municipalityRepository
            .GetMunicipality(taxRecordDto.municipality);

        var tr = await db.TaxRecords.Where(
            x =>
                x.MunicipalityId == mun.Id
                && x.FromDate == taxRecordDto.from
                && x.ToDate == taxRecordDto.to).ToListAsync();

        if (tr.Count != 0)
        {
            throw new TaxRecordAlreadyExistsException();
        }

        var pt = GetPeriodType(taxRecordDto.from, taxRecordDto.to);
        await db.TaxRecords.AddAsync(new TaxRecord
        {
            FromDate = taxRecordDto.from,
            ToDate = taxRecordDto.to,
            TaxRate = taxRecordDto.rate,
            Type = pt
        });
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaxRecordDto current, TaxRecordDto updated)
    {
        var mun = await municipalityRepository
            .GetMunicipality(current.municipality);

        var hit = await db.TaxRecords.Where(
            x =>
                mun.Id == x.MunicipalityId
                && current.from == x.FromDate
                && current.to == x.ToDate
                && current.rate == x.TaxRate).FirstOrDefaultAsync();
        if (hit == null)
        {
            throw new NoTaxRecordFoundException();
        }

        hit.FromDate = updated.from;
        hit.ToDate = updated.to;
        hit.TaxRate = updated.rate;
        hit.Type = GetPeriodType(updated.from, updated.to);
        await db.SaveChangesAsync();
    }

    public async Task<TaxRecordDto> GetAsync(TaxRecordDto target)
    {
        var mun = await municipalityRepository
            .GetMunicipality(target.municipality);

        var recordsWithDateInRange =
            await db.TaxRecords.Where(
                x =>
                    x.MunicipalityId == mun.Id
                    && x.FromDate <= target.from
                    && x.ToDate >= target.from).ToListAsync();
        if (recordsWithDateInRange.IsNullOrEmpty())
        {
            throw new RangeNotFoundException();
        }

        recordsWithDateInRange.Sort(
            (a, b) => a.Type.CompareTo(b.Type));
        var tr = recordsWithDateInRange[0];
        var newRecord = new TaxRecordDto
        {
            municipality = mun.Name,
            rate = tr.TaxRate,
            from = tr.FromDate,
            to = tr.ToDate
        };
        return newRecord;
    }

    private PeriodType GetPeriodType(DateTimeOffset from, DateTimeOffset to)
    {
        var interval = to - from;
        return interval.Days switch
        {
            > 31 => PeriodType.Yearly,
            > 27 => PeriodType.Monthly,
            0 => PeriodType.Daily,
            _ => throw new RangeNotSupportedException()
        };
    }
}