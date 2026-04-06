using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Applicant;
using HardshipApp.Service.Services;
using Moq;

namespace HardshipApp.UnitTests.Services;

public class ApplicantServiceTests
{
    private readonly Mock<IApplicantRepository> _mockApplicantRepo;
    private readonly ApplicantService _service;

    public ApplicantServiceTests()
    {
        _mockApplicantRepo = new Mock<IApplicantRepository>();
        _service = new ApplicantService(_mockApplicantRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_NotFound_ReturnsNull()
    {
        // Arrange
        _mockApplicantRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Applicant?)null);

        // Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsResponseDto()
    {
        // Arrange
        var dto = new CreateApplicantDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1995, 5, 5),
            Email = "jane@example.com",
        };

        _mockApplicantRepo
            .Setup(r => r.CreateAsync(It.IsAny<Applicant>()))
            .ReturnsAsync((Applicant a) => a);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("jane@example.com", result.Email);
    }

    [Fact]
    public async Task UpdateAsync_NotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockApplicantRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Applicant?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.UpdateAsync(Guid.NewGuid(), new UpdateApplicantDto
            {
                FirstName = "Updated",
                LastName = "Name",
                DateOfBirth = new DateOnly(1995, 5, 5),
                Email = "updated@example.com"
            }));
    }
}