using aspnetcoremvc2.Controllers;
using aspnetcoremvc2.Models;
using aspnetcoremvc2.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace unittest.Controllers;

public class RookieControllerTests
{
    private List<Person> _rookies = new List<Person>() {
        new Person() {
            Id = 1,
            FirstName = "Quang Tung",
            LastName = "Ta",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 9, 21),
            PhoneNumber = "0921426803",
            BirthPlace = "Hanoi",
            IsGraduated = false
        },
        new Person() {
            Id = 2,
            FirstName = "Duy Bach",
            LastName = "Dang",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2002, 9, 8),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        }
    };

    private Mock<IRookieService> _mockRookieService;
    private Mock<IExcelExportService<Person>> _mockExcelService;
    private RookieController _controller;

    [SetUp]
    public void Setup()
    {
        _mockRookieService = new Mock<IRookieService>();
        _mockExcelService = new Mock<IExcelExportService<Person>>();
        _controller = new RookieController(_mockRookieService.Object, _mockExcelService.Object);
    }

    [TearDown]
    public void Teardown()
    {
        _controller.Dispose();
    }

    [Test]
    public void Index_WithAListOfRookies_ReturnsAViewResult()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetAll()).Returns(_rookies);

        // Act
        var result = _controller.Index();

        // Assert
        _mockRookieService.Verify(s => s.GetAll(), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<List<Person>>());

        var model = (List<Person>)viewResult.ViewData.Model;
        Assert.That(model, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetDetails_WithOneRookie_ReturnsAViewResult()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetById(1)).Returns(_rookies[0]);

        // Act
        var result = _controller.GetDetails(1);

        // Assert
        _mockRookieService.Verify(s => s.GetById(1), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;

        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<Person>());
    }

    [Test]
    public void GetDetails_WithNullRookie_ReturnsNotFoundResult()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetById(999)).Returns(() => null);

        // Act
        var result = _controller.GetDetails(999);

        // Assert
        _mockRookieService.Verify(s => s.GetById(999), Times.Once);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Create_InvokedByGetRequestWithoutArguments_ReturnsAViewResult()
    {
        // Arrange
        // None.

        // Act
        var result = _controller.Create();

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
    }

    [Test]
    public void Create_InvokedByPostRequestWithValidModel_ReturnsARedirectToIndex()
    {
        // Arrange
        _mockRookieService.Setup(service => service.Create(_rookies[0])).Returns(_rookies[0]);

        // Act
        var result = _controller.Create(_rookies[0]);

        // Assert
        _mockRookieService.Verify(service => service.Create(_rookies[0]), Times.Once);

        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

        var redirectResult = (RedirectToActionResult)result;
        Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
    }

    [Test]
    public void Create_InvokedByPostRequestWithInvalidModel_ReturnsAViewResultWithCurrentModel()
    {
        // Arrange
        _controller.ModelState.AddModelError("rookie", "Fields are missing.");
        var newRookie = new Person();

        // Act
        var result = _controller.Create(newRookie);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.InstanceOf<Person>());
    }

    [Test]
    public void Edit_CalledWithANonexistentId_ReturnsANotFoundResult()
    {
        // Arrange
        int nonexistentId = 999;
        _mockRookieService.Setup(service => service.GetById(nonexistentId)).Returns(() => null);

        // Act
        var result = _controller.Edit(nonexistentId);

        // Assert
        _mockRookieService.Verify(service => service.GetById(nonexistentId), Times.Once);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Edit_CalledWithAnExistingId_ReturnsAViewResultWithRookieModel()
    {
        // Arrange
        int existingId = 1;
        _mockRookieService.Setup(service => service.GetById(existingId)).Returns(_rookies[0]);

        // Act
        var result = _controller.Edit(existingId);

        // Assert
        _mockRookieService.Verify(service => service.GetById(existingId), Times.Once);
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void Edit_CalledWithMismatchingIdsInArguments_ReturnsANotFoundResult()
    {
        // Arrange
        int id = 1;
        int modelId = 2;
        var rookieModel = new Person()
        {
            Id = modelId,
            FirstName = "Quang Tung",
            LastName = "Ta",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 9, 21),
            PhoneNumber = "0921426803",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };

        // Act
        var result = _controller.Edit(id, rookieModel);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Edit_CalledWithValidModel_ReturnsARedirectToIndex()
    {
        // Arrange
        int id = 1;
        var rookieModel = new Person()
        {
            Id = id,
            FirstName = "Quang Tung",
            LastName = "Ta",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 9, 21),
            PhoneNumber = "0921426803",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        _mockRookieService.Setup(service => service.Update(rookieModel)).Returns(true);

        // Act
        var result = _controller.Edit(id, rookieModel);

        // Assert
        _mockRookieService.Verify(service => service.Update(rookieModel), Times.Once);
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

        var redirectResult = (RedirectToActionResult)result;
        Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
    }

    [Test]
    public void Edit_CalledWithInvalidModel_ReturnsAViewResultWithCurrentModel()
    {
        // Arrange
        int id = 1;
        var invalidModel = new Person()
        {
            Id = id,
        };
        _controller.ModelState.AddModelError("rookie", "Fields are missing.");

        // Act
        var result = _controller.Edit(id, invalidModel);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.EqualTo(invalidModel));
    }

    [Test]
    public void Delete_CalledWithNonexistentId_ReturnsADeleteErrorViewResult()
    {
        // Arrange
        int nonexistentId = 999;
        _mockRookieService.Setup(service => service.Delete(nonexistentId)).Returns(() => null);

        // Act
        var result = _controller.Delete(nonexistentId);

        // Assert
        _mockRookieService.Verify(service => service.Delete(nonexistentId), Times.Once);
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewName, Is.EqualTo("DeleteError"));
    }

    [Test]
    public void Delete_CalledWithExistingId_ReturnsADeleteSuccessViewResultWithDeletedRookieModel()
    {
        // Arrange
        int existingId = 1;
        _mockRookieService.Setup(service => service.Delete(existingId)).Returns(_rookies[0]);

        // Act
        var result = _controller.Delete(existingId);

        // Assert
        _mockRookieService.Verify(service => service.Delete(existingId), Times.Once);
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewName, Is.EqualTo("DeleteSuccess"));
        Assert.That(viewResult.Model, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void Male_WithAListOfMaleRookies_ReturnsAViewResult()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetMales()).Returns(_rookies);

        // Act
        var result = _controller.Male();

        // Assert
        _mockRookieService.Verify(s => s.GetMales(), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<List<Person>>());

        var model = (List<Person>)viewResult.ViewData.Model;
        Assert.That(model, Has.Count.EqualTo(2));
    }

    [Test]
    public void Oldest_WithAnOldestRookie_ReturnsAViewResultWithListOfOneRookieAsModel()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetOldest()).Returns(_rookies[1]);

        // Act
        var result = _controller.Oldest();

        // Assert
        _mockRookieService.Verify(s => s.GetOldest(), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<List<Person>>());

        var model = (List<Person>)viewResult.ViewData.Model;
        Assert.That(model, Has.Count.EqualTo(1));
    }

    [Test]
    public void Oldest_WithNoOldestRookie_ReturnsAViewResultWithEmptyListOfRookieAsModel()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetOldest()).Returns(() => null);

        // Act
        var result = _controller.Oldest();

        // Assert
        _mockRookieService.Verify(s => s.GetOldest(), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<List<Person>>());

        var model = (List<Person>)viewResult.ViewData.Model;
        Assert.That(model, Has.Count.EqualTo(0));
    }

    [Test]
    public void FullNames_WithAListOfRookieNames_ReturnsAViewResult()
    {
        // Arrange
        var mockServiceResult = new List<string>() { "Quang Tung Ta", "Duy Bach Dang" };
        _mockRookieService.Setup(service => service.GetFullNames()).Returns(mockServiceResult);

        // Act
        var result = _controller.FullNames();

        // Assert
        _mockRookieService.Verify(s => s.GetFullNames(), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<List<string>>());

        var model = (List<string>)viewResult.ViewData.Model;
        Assert.That(model, Has.Count.EqualTo(2));
    }

    [Test]
    public void BornIn_WithAListOfRookies_ReturnsAViewResult()
    {
        // Arrange
        int year = 2004;
        var mockServiceResult = new List<Person>() { _rookies[0] };
        _mockRookieService.Setup(service => service.GetRookiesBornInYear(year)).Returns(mockServiceResult);

        // Act
        var result = _controller.BornIn(year);

        // Assert
        _mockRookieService.Verify(s => s.GetRookiesBornInYear(year), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<GetRookiesByYearViewModel>());

        var model = (GetRookiesByYearViewModel)viewResult.ViewData.Model;
        Assert.That(model.Rookies, Has.Count.EqualTo(1));
        Assert.That(model.Year, Is.EqualTo(year));
    }

    [Test]
    public void BornBefore_WithAListOfRookies_ReturnsAViewResult()
    {
        // Arrange
        int year = 2004;
        var mockServiceResult = new List<Person>() { _rookies[1] };
        _mockRookieService.Setup(service => service.GetRookiesBornBeforeYear(year)).Returns(mockServiceResult);

        // Act
        var result = _controller.BornBefore(year);

        // Assert
        _mockRookieService.Verify(s => s.GetRookiesBornBeforeYear(year), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<GetRookiesByYearViewModel>());

        var model = (GetRookiesByYearViewModel)viewResult.ViewData.Model;
        Assert.That(model.Rookies, Has.Count.EqualTo(1));
        Assert.That(model.Year, Is.EqualTo(year));
    }

    [Test]
    public void BornAfter_WithAListOfRookies_ReturnsAViewResult()
    {
        // Arrange
        int year = 2002;
        var mockServiceResult = new List<Person>() { _rookies[0] };
        _mockRookieService.Setup(service => service.GetRookiesBornAfterYear(year)).Returns(mockServiceResult);

        // Act
        var result = _controller.BornAfter(year);

        // Assert
        _mockRookieService.Verify(s => s.GetRookiesBornAfterYear(year), Times.Once);

        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.ViewData.Model, Is.InstanceOf<GetRookiesByYearViewModel>());

        var model = (GetRookiesByYearViewModel)viewResult.ViewData.Model;
        Assert.That(model.Rookies, Has.Count.EqualTo(1));
        Assert.That(model.Year, Is.EqualTo(year));
    }

    [Test]
    public void Excel_WithAListOfRookies_ReturnsAFileResult()
    {
        // Arrange
        _mockRookieService.Setup(service => service.GetAll()).Returns(_rookies);
        _mockExcelService.Setup(service => service.ExportToExcel(_rookies)).Returns(new MemoryStream());

        // Act
        var result = _controller.Excel();

        // Assert
        _mockRookieService.Verify(service => service.GetAll(), Times.Once);
        _mockExcelService.Verify(service => service.ExportToExcel(_rookies), Times.Once);
        Assert.That(result, Is.InstanceOf<FileResult>());

        var fileResult = (FileResult)result;
        Assert.That(fileResult.ContentType, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        Assert.That(fileResult.FileDownloadName, Is.EqualTo("Rookies.xlsx"));
    }
}