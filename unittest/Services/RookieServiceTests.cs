using aspnetcoremvc2.Models;
using aspnetcoremvc2.Repositories;
using aspnetcoremvc2.Services;
using Moq;

namespace unittest.Services;

public class RookieServiceTests
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
    private Mock<IRookieRepository> _repository;
    private RookieService _service;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IRookieRepository>();
        _service = new RookieService(_repository.Object);
    }

    [Test]
    public void GetById_CalledWithExistingId_ReturnsARookie()
    {
        // Arrange
        int existingId = 1;
        _repository.Setup(repo => repo.GetById(existingId)).Returns(_rookies[0]);

        // Act
        var result = _service.GetById(existingId);

        // Assert
        _repository.Verify(repo => repo.GetById(existingId), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void GetById_CalledWithNonExistentId_ReturnsNull()
    {
        // Arrange
        int nonexistentId = 999;
        _repository.Setup(repo => repo.GetById(nonexistentId)).Returns(() => null);

        // Act
        var result = _service.GetById(nonexistentId);

        // Assert
        _repository.Verify(repo => repo.GetById(nonexistentId), Times.Once);
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void GetRookiesBornInYear_CalledWithYear_ReturnsAListOfRookies()
    {
        // Arrange
        int year = 2004;
        var mockRepoResult = new List<Person>() { _rookies[0] };
        _repository.Setup(repo => repo.GetRookiesBornInYear(year)).Returns(mockRepoResult);

        // Act
        var result = _service.GetRookiesBornInYear(year);

        // Assert
        _repository.Verify(repo => repo.GetRookiesBornInYear(year), Times.Once);
        Assert.That(result, Is.EqualTo(mockRepoResult));
    }

    [Test]
    public void GetFullNames_Called_ReturnsAListOfNames()
    {
        // Arrange
        List<string> names = new List<string>() { "Quang Tung Ta", "Duy Bach Dang" };
        _repository.Setup(repo => repo.GetFullNames()).Returns(names);

        // Act
        var result = _service.GetFullNames();

        // Assert
        _repository.Verify(repo => repo.GetFullNames(), Times.Once);
        Assert.That(result, Is.EqualTo(names));
    }

    [Test]
    public void GetMales_Called_ReturnsAListOfMaleRookies()
    {
        // Arrange
        _repository.Setup(repo => repo.GetMales()).Returns(_rookies);

        // Act
        var result = _service.GetMales();

        // Assert
        _repository.Verify(repo => repo.GetMales(), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies));
    }

    [Test]
    public void GetOldest_CalledWhenThereIsAnOldestRookie_ReturnsOldestRookie()
    {
        // Arrange
        _repository.Setup(repo => repo.GetOldest()).Returns(_rookies[1]);

        // Act
        var result = _service.GetOldest();

        // Assert
        _repository.Verify(repo => repo.GetOldest(), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[1]));
    }

    [Test]
    public void GetOldest_CalledWhenThereIsNoOldestRookie_ReturnsNull()
    {
        // Arrange
        _repository.Setup(repo => repo.GetOldest()).Returns(() => null);

        // Act
        var result = _service.GetOldest();

        // Assert
        _repository.Verify(repo => repo.GetOldest(), Times.Once);
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void GetRookiesBornBeforeYear_CalledWithYear_ReturnsAListOfRookies()
    {
        // Arrange
        int year = 2004;
        var mockRepoResult = new List<Person>() { _rookies[1] };
        _repository.Setup(repo => repo.GetRookiesBornBeforeYear(year)).Returns(mockRepoResult);

        // Act
        var result = _service.GetRookiesBornBeforeYear(year);

        // Assert
        _repository.Verify(repo => repo.GetRookiesBornBeforeYear(year), Times.Once);
        Assert.That(result, Is.EqualTo(mockRepoResult));
    }

    [Test]
    public void GetRookiesBornAfterYear_CalledWithYear_ReturnsAListOfRookies()
    {
        // Arrange
        int year = 2002;
        var mockRepoResult = new List<Person>() { _rookies[0] };
        _repository.Setup(repo => repo.GetRookiesBornAfterYear(year)).Returns(mockRepoResult);

        // Act
        var result = _service.GetRookiesBornAfterYear(year);

        // Assert
        _repository.Verify(repo => repo.GetRookiesBornAfterYear(year), Times.Once);
        Assert.That(result, Is.EqualTo(mockRepoResult));
    }

    [Test]
    public void GetAll_Called_ReturnsAListOfRookies()
    {
        // Arrange
        _repository.Setup(repo => repo.GetAll()).Returns(_rookies);

        // Act
        var result = _service.GetAll();

        // Assert
        _repository.Verify(repo => repo.GetAll(), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies));
    }

    [Test]
    public void Create_CalledWithNewRookie_ReturnsTheCreatedRookie()
    {
        // Arrange
        _repository.Setup(repo => repo.Create(_rookies[0])).Returns(_rookies[0]);

        // Act
        var result = _service.Create(_rookies[0]);

        // Assert
        _repository.Verify(repo => repo.Create(_rookies[0]), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void Update_RookieSuccessfullyUpdated_ReturnsTrue()
    {
        // Arrange
        _repository.Setup(repo => repo.Update(_rookies[0])).Returns(true);

        // Act
        var result = _service.Update(_rookies[0]);

        // Assert
        _repository.Verify(repo => repo.Update(_rookies[0]), Times.Once);
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public void Update_RookieUnsuccessfullyUpdated_ReturnsFalse()
    {
        // Arrange
        _repository.Setup(repo => repo.Update(_rookies[0])).Returns(false);

        // Act
        var result = _service.Update(_rookies[0]);

        // Assert
        _repository.Verify(repo => repo.Update(_rookies[0]), Times.Once);
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void Delete_CalledWithExistingId_ReturnsDeletedRookie()
    {
        // Arrange
        int existingId = 1;
        _repository.Setup(repo => repo.Delete(existingId)).Returns(_rookies[0]);

        // Act
        var result = _service.Delete(existingId);

        // Assert
        _repository.Verify(repo => repo.Delete(existingId), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void Delete_CalledWithNonexistentId_ReturnsNull()
    {
        // Arrange
        int nonexistentId = 999;
        _repository.Setup(repo => repo.Delete(nonexistentId)).Returns(() => null);

        // Act
        var result = _service.Delete(nonexistentId);

        // Assert
        _repository.Verify(repo => repo.Delete(nonexistentId), Times.Once);
        Assert.That(result, Is.EqualTo(null));
    }
}