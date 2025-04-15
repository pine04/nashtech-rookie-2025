using aspnetcoremvc2.Models;
using aspnetcoremvc2.Services;

namespace unittest.Services;

public class RookieExcelExportServiceTests
{
    private RookieExcelExportService _service = new RookieExcelExportService();

    [Test]
    public void ExportToExcel_CalledWithNull_ReturnsNull()
    {
        // Arrange
        IEnumerable<Person>? data = null;

        // Act
        var result = _service.ExportToExcel(data);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ExportToExcel_CalledWithEmptyData_ReturnsNull()
    {
        // Arrange
        IEnumerable<Person> data = new List<Person>();

        // Act
        var result = _service.ExportToExcel(data);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ExportToExcel_CalledWithNonemptyListOfRookies_ReturnsStream()
    {
        // Arrange
        IEnumerable<Person> data = new List<Person>() {
            new Person() {
                Id = 1,
                FirstName = "Quang Tung",
                LastName = "Ta",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(2004, 9, 21),
                PhoneNumber = "0921426803",
                BirthPlace = "Hanoi",
                IsGraduated = false
            }
        };

        // Act
        var result = _service.ExportToExcel(data);

        // Assert
        Assert.That(result, Is.InstanceOf<Stream>());
        var stream = (Stream)result;
        Assert.That(stream.Position, Is.EqualTo(0));
    }
}