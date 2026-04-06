using HardshipApp.Models.Entities;
using HardshipApp.Models.Enum;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Service.Services;
using Moq;

namespace HardshipApp.UnitTests.Services;

public class HardshipApplicationServiceTests
{
    private readonly Mock<IHardshipApplicationRepository> _mockApplicationRepo;
    private readonly Mock<IApplicantRepository> _mockApplicantRepo;
    private readonly HardshipApplicationService _service;

    public HardshipApplicationServiceTests()
    {
        _mockApplicationRepo = new Mock<IHardshipApplicationRepository>();
        _mockApplicantRepo = new Mock<IApplicantRepository>();
        _service = new HardshipApplicationService(
            _mockApplicationRepo.Object,
            _mockApplicantRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_NotFound_ReturnsNull()
    {
        // Arrange
        _mockApplicationRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((HardshipApplication?)null);

        // Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_NotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockApplicationRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((HardshipApplication?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.UpdateAsync(Guid.NewGuid(), new UpdateHardshipApplicationDto
            {
                Income = 50000,
                Expenses = 30000,
                Status = ApplicationStatus.UnderReview
            }));
    }

    [Fact]
    public async Task DeleteAsync_NotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockApplicationRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((HardshipApplication?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.DeleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsResponseDto()
    {
        // Arrange
        var dto = new CreateHardshipApplicationDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Email = "john@example.com",
            Income = 50000,
            Expenses = 30000,
            HardshipReason = "Lost job"
        };

        _mockApplicantRepo
            .Setup(r => r.CreateAsync(It.IsAny<Applicant>()))
            .ReturnsAsync((Applicant a) => a);

        _mockApplicationRepo
            .Setup(r => r.CreateAsync(It.IsAny<HardshipApplication>()))
            .ReturnsAsync((HardshipApplication a) => a);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal(ApplicationStatus.Pending, result.Status);
    }
}
