using System.Globalization;
using TaxTool.Model.DTO;

namespace UnitTests;

/**
 * This is what proved to be an overstretch.
 * The idea was to mock the DB context and test the repositories.
 * 
 */
public class TaxRecordRepositoryTest
{
    [Fact]
    public async void TestGet()
    {
        //arrange
        var repo = TaxRecordRepositoryMock.GetMock();
        //act
        var res = await repo.GetAsync(new TaxRecordDto
        {
            municipality = "Copenhagen",
            from = DateTimeOffset.ParseExact(
                "2024.01.01 00:00:00 UTC",
                "yyyy.MM.dd HH:mm:ss 'UTC'",
                new CultureInfo("en-US"))
        });
        Console.WriteLine("debugpoint");
        //assert
    }
}