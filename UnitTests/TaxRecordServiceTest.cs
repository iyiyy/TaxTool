using Moq;
using TaxTool.Model.DTO;
using TaxTool.Model.Repository;
using TaxTool.Service;

namespace UnitTests;

/**
 * Started off with this suite but quickly realized there is not much
 * to test here as the service only calls repository functions.
 * Had it had some data validation logic or something like that then
 * it woul've made more sense to continue.
 */
public class TaxRecordServiceTest
{
    [Fact]
    public async Task TestAddNewTaxRecordAsyncOk()
    {
        var mockMunicipalityRepo = new Mock<IMunicipalityRepository>();
        var municipalityFound = new MunicipalityDto { Name = "Copenhagen", Id = new Guid() };
        mockMunicipalityRepo.Setup(
                x =>
                    x.GetMunicipality(It.IsAny<string>()))
            .ReturnsAsync(municipalityFound);

        var mockTaxRecordRepo = new Mock<ITaxRecordRepository>();
        mockTaxRecordRepo.Setup(
            x =>
                x.AddAsync(It.IsAny<TaxRecordDto>()));

        var taxService = new TaxService(
            mockMunicipalityRepo.Object,
            mockTaxRecordRepo.Object);
        await taxService.AddNewTaxRecordAsync(It.IsAny<TaxRecordDto>());

        mockTaxRecordRepo.Verify(
            x =>
                x.AddAsync(It.IsAny<TaxRecordDto>()), Times.Once());
    }
}