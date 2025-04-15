using aspnetcoremvc2.Models;
using aspnetcoremvc2.Repositories;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace unittest.Repositories;

public class InMemoryRookieRepositoryTests
{
    private Mock<IMemoryCache> _cache;
    private InMemoryRookieRepository _repository;
    private List<Person> _rookies;

    [SetUp]
    public void Setup()
    {
        _rookies = new List<Person>() {
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
        _cache = new Mock<IMemoryCache>();
        _cache.Setup(cache => cache.TryGetValue(It.IsAny<object>(), out It.Ref<object?>.IsAny)).Returns(false);
        // _cache.Setup(cache => cache.Set(It.IsAny<object>(), It.IsAny<object?>()));
        _cache.Setup(x => x.CreateEntry("rookies")).Returns(Mock.Of<ICacheEntry>);

        _repository = new InMemoryRookieRepository(_cache.Object);

        _cache.Invocations.Clear();
    }

    [Test]
    public void GetRookiesBornInYear_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        int year = 2004;
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetRookiesBornInYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetRookiesBornInYear_WithRookieListInCache_ReturnsListWithOneRookie()
    {
        // Arrange
        int year = 2004;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetRookiesBornInYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetFullNames_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetFullNames();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<string>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetFullNames_WithRookieListInCache_ReturnsListOfStrings()
    {
        // Arrange
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetFullNames();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<string>>());
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetMales_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetMales();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetMales_WithRookieListInCache_ReturnsListOfMaleRookies()
    {
        // Arrange
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetMales();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetOldest_WithoutRookieListInCache_ReturnsNull()
    {
        // Arrange
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetOldest();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetOldest_WithoutEmptyRookieListInCache_ReturnsNull()
    {
        // Arrange
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = new List<Person>(); })
            .Returns(true);

        // Act
        var result = _repository.GetOldest();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetOldest_WithRookieListInCache_ReturnsListOfMaleRookies()
    {
        // Arrange
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetOldest();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[1]));
    }

    [Test]
    public void GetRookiesBornAfterYear_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        int year = 2002;
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetRookiesBornAfterYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetRookiesBornAfterYear_WithRookieListInCache_ReturnsListWithOneRookie()
    {
        // Arrange
        int year = 2002;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetRookiesBornAfterYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetRookiesBornBeforeYear_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        int year = 2004;
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetRookiesBornBeforeYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetRookiesBornBeforeYear_WithRookieListInCache_ReturnsListWithOneRookie()
    {
        // Arrange
        int year = 2004;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetRookiesBornBeforeYear(year);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetAll_WithoutRookieListInCache_ReturnsEmptyList()
    {
        // Arrange
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetAll();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void GetAll_WithRookieListInCache_ReturnsListOfMaleRookies()
    {
        // Arrange
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetAll();

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.TypeOf<List<Person>>());
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetById_WithoutRookieListInCache_ReturnsNull()
    {
        // Arrange
        int id = 1;
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.GetById(id);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetById_WithExistingRookieId_ReturnsRookie()
    {
        // Arrange
        int existingId = 1;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetById(existingId);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.EqualTo(_rookies[0]));
    }

    [Test]
    public void GetById_WithNonexistentRookieId_ReturnsNull()
    {
        // Arrange
        int nonexistentId = 999;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.GetById(nonexistentId);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Create_WithoutRookieListInCache_ReturnsNull()
    {
        // Arrange
        Person newRookie = new Person()
        {
            FirstName = "Quang Huy",
            LastName = "Nguyen",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 12, 7),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.Create(newRookie);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Create_WithRookieListInCache_ReturnsNewlyCreatedRookie()
    {
        // Arrange
        Person newRookie = new Person()
        {
            FirstName = "Quang Huy",
            LastName = "Nguyen",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 12, 7),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.Create(newRookie);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.EqualTo(newRookie));
    }

    [Test]
    public void Update_WithoutRookieListInCache_ReturnsFalse()
    {
        // Arrange
        Person updatedRookie = new Person()
        {
            Id = 3,
            FirstName = "Quang Huy",
            LastName = "Nguyen",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 12, 7),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.Update(updatedRookie);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Update_WithNonexistentRookie_ReturnsFalse()
    {
        // Arrange
        Person updatedRookie = new Person()
        {
            Id = 999,
            FirstName = "Quang Huy",
            LastName = "Nguyen",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 12, 7),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.Update(updatedRookie);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Update_WithExistingRookie_ReturnsTrue()
    {
        // Arrange
        Person updatedRookie = new Person()
        {
            Id = 1,
            FirstName = "Quang Huy",
            LastName = "Nguyen",
            Gender = Gender.Male,
            DateOfBirth = new DateOnly(2004, 12, 7),
            PhoneNumber = "0123456789",
            BirthPlace = "Hanoi",
            IsGraduated = false
        };
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.Update(updatedRookie);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.True);
    }

    [Test]
    public void Delete_WithoutRookieListInCache_ReturnsNull()
    {
        // Arrange
        int id = 1;
        _cache.Setup(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny)).Returns(false);

        // Act
        var result = _repository.Delete(id);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out It.Ref<object?>.IsAny), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_WithNonexistentRookieId_ReturnsNull()
    {
        // Arrange
        int nonexistentId = 999;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var result = _repository.Delete(nonexistentId);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_WithExistingRookieId_ReturnsDeletedRookie()
    {
        // Arrange
        int existingId = 1;
        object? outValue;
        _cache.Setup(cache => cache.TryGetValue("rookies", out outValue))
            .Callback((object key, out object? val) => { val = _rookies; })
            .Returns(true);

        // Act
        var expectedResult = _rookies[0];
        var result = _repository.Delete(existingId);

        // Assert
        _cache.Verify(cache => cache.TryGetValue("rookies", out outValue), Times.Once);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}