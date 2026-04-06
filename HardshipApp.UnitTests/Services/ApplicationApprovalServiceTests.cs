using HardshipApp.Models.Entities;
using HardshipApp.Models.Enum;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Approval;
using HardshipApp.Service.Services;
using Moq;

namespace HardshipApp.UnitTests.Services;

public class ApplicationApprovalServiceTests
{
    private readonly Mock<IApplicationApprovalRepository> _mockApprovalRepo;
    private readonly Mock<IHardshipApplicationRepository> _mockApplicationRepo;
    private readonly ApplicationApprovalService _service;

    public ApplicationApprovalServiceTests()
    {
        _mockApprovalRepo = new Mock<IApplicationApprovalRepository>();
        _mockApplicationRepo = new Mock<IHardshipApplicationRepository>();
        _service = new ApplicationApprovalService(
            _mockApprovalRepo.Object,
            _mockApplicationRepo.Object);
    }

    [Fact]
    public async Task CreateAsync_ApplicationNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockApplicationRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((HardshipApplication?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.CreateAsync(Guid.NewGuid(), new CreateApprovalDto
            {
                Decision = ApplicationStatus.Approved,
                Notes = "Approved"
            }));
    }

    [Fact]
    public async Task GetByApplicationIdAsync_NotFound_ReturnsNull()
    {
        // Arrange
        _mockApprovalRepo
            .Setup(r => r.GetByApplicationIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ApplicationApproval?)null);

        // Act
        var result = await _service.GetByApplicationIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}