using System.Globalization;
using Moq;
using TaxTool.Model;
using TaxTool.Model.Entity;
using TaxTool.Model.Repository;

namespace UnitTests;

/**
 * Could have used a better name for this.
 * Not as much a mock for the repository as a repository
 * that uses a mocked DB context.
 */

public class TaxRecordRepositoryMock
{
    public static ITaxRecordRepository GetMock()
    {
        var munId = new Guid();
        List<Municipality> municipalities = new();
        municipalities.Add(new Municipality { Id = munId, Name = "Copenhagen" });

        List<TaxRecord> taxRecords = new();

        taxRecords.Add(new TaxRecord
        {
            Id = Guid.NewGuid(),
            MunicipalityId = munId,
            FromDate = DateTimeOffset.ParseExact("2024.01.01 00:00:00 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            ToDate = DateTimeOffset.ParseExact("2024.12.31 23:59:59 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            TaxRate = 0.2,
            Type = PeriodType.Yearly
        });

        taxRecords.Add(new TaxRecord
        {
            Id = Guid.NewGuid(),
            MunicipalityId = munId,
            FromDate = DateTimeOffset.ParseExact("2024.05.01 00:00:00 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            ToDate = DateTimeOffset.ParseExact("2024.05.31 23:59:59 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            TaxRate = 0.4,
            Type = PeriodType.Monthly
        });

        taxRecords.Add(new TaxRecord
        {
            Id = Guid.NewGuid(),
            MunicipalityId = munId,
            FromDate = DateTimeOffset.ParseExact("2024.01.01 00:00:00 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            ToDate = DateTimeOffset.ParseExact("2024.01.01 23:59:59 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            TaxRate = 0.1,
            Type = PeriodType.Daily
        });

        taxRecords.Add(new TaxRecord
        {
            Id = Guid.NewGuid(),
            MunicipalityId = munId,
            FromDate = DateTimeOffset.ParseExact("2024.12.25 00:00:00 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            ToDate = DateTimeOffset.ParseExact("2024.12.25 23:59:59 UTC", "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US")),
            TaxRate = 0.1,
            Type = PeriodType.Daily
        });
        TaxContext db = TaxContextMock
            .GetMock(taxRecords,
                municipalities,
                x => x.TaxRecords,
                y => y.Municipalities);

        Mock<MunicipalityRepository> municipalityRepoMock = new Mock<MunicipalityRepository>(db);
        return new TaxRecordRepository(db, municipalityRepoMock.Object);
    }
}