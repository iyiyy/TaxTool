using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaxTool.Model;
using TaxTool.Model.Entity;

namespace UnitTests;

/**
 * This class mocks the DB context.
 * It seems to work, however, because all access functions
 * in the repositories are async
 * (looks like moq doesn't have support for async calls)
 * I couldn't confirm it.
 */
public class TaxContextMock
{
    public static TaxContext GetMock(
        List<TaxRecord> taxRecordsLst,
        List<Municipality> municipalitiesLst,
        Expression<Func<TaxContext, DbSet<TaxRecord>>> dbTaxRecordsSetSelectionExpr,
        Expression<Func<TaxContext, DbSet<Municipality>>> dbMunicipalitiesSelectionExpr)
    {
        IQueryable<TaxRecord> taxRecordsQueryable = taxRecordsLst.AsQueryable();
        Mock<DbSet<TaxRecord>> taxRecordsMock = new Mock<DbSet<TaxRecord>>();

        IQueryable<Municipality> municipalitiesQueryable = municipalitiesLst.AsQueryable();
        Mock<DbSet<Municipality>> municipalitiesMock = new Mock<DbSet<Municipality>>();
        Mock<TaxContext> dbContext = new Mock<TaxContext>();


        taxRecordsMock.As<IQueryable<TaxRecord>>()
            .Setup(s => s.Provider)
            .Returns(taxRecordsQueryable.Provider);
        taxRecordsMock.As<IQueryable<TaxRecord>>()
            .Setup(s => s.Expression)
            .Returns(taxRecordsQueryable.Expression);
        taxRecordsMock.As<IQueryable<TaxRecord>>()
            .Setup(s => s.ElementType)
            .Returns(taxRecordsQueryable.ElementType);
        taxRecordsMock.As<IQueryable<TaxRecord>>()
            .Setup(s => s.GetEnumerator())
            .Returns(() => taxRecordsQueryable.GetEnumerator());

        taxRecordsMock.Setup(x => x.Add(It.IsAny<TaxRecord>()))
            .Callback<TaxRecord>(taxRecordsLst.Add);
        taxRecordsMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TaxRecord>>()))
            .Callback<IEnumerable<TaxRecord>>(taxRecordsLst.AddRange);
        taxRecordsMock.Setup(x => x.Remove(It.IsAny<TaxRecord>()))
            .Callback<TaxRecord>(t => taxRecordsLst.Remove(t));
        taxRecordsMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TaxRecord>>()))
            .Callback<IEnumerable<TaxRecord>>(ts =>
            {
                foreach (var t in ts)
                {
                    taxRecordsLst.Remove(t);
                }
            });

        municipalitiesMock.As<IQueryable<Municipality>>()
            .Setup(s => s.Provider)
            .Returns(municipalitiesQueryable.Provider);
        municipalitiesMock.As<IQueryable<Municipality>>()
            .Setup(s => s.Expression)
            .Returns(municipalitiesQueryable.Expression);
        municipalitiesMock.As<IQueryable<Municipality>>()
            .Setup(s => s.ElementType)
            .Returns(municipalitiesQueryable.ElementType);
        municipalitiesMock.As<IQueryable<Municipality>>()
            .Setup(s => s.GetEnumerator())
            .Returns(() => municipalitiesQueryable.GetEnumerator());

        municipalitiesMock.Setup(x => x.Add(It.IsAny<Municipality>()))
            .Callback<Municipality>(municipalitiesLst.Add);
        municipalitiesMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<Municipality>>()))
            .Callback<IEnumerable<Municipality>>(municipalitiesLst.AddRange);
        municipalitiesMock.Setup(x => x.Remove(It.IsAny<Municipality>()))
            .Callback<Municipality>(m => municipalitiesLst.Remove(m));
        municipalitiesMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<Municipality>>()))
            .Callback<IEnumerable<Municipality>>(ms =>
            {
                foreach (var m in ms)
                {
                    municipalitiesLst.Remove(m);
                }
            });

        dbContext.Setup(dbTaxRecordsSetSelectionExpr).Returns(taxRecordsMock.Object);
        dbContext.Setup(dbMunicipalitiesSelectionExpr).Returns(municipalitiesMock.Object);
        return dbContext.Object;
    }
}