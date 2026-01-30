using ApplicationLayer.Bookings.Queries.GetBookingDetails;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Bookings.Queries
{
    [TestFixture]
    public class GetBookingDetailsQueryHandlerTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private GetBookingDetailsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _handler = new GetBookingDetailsQueryHandler(_mockBookingRepository.Object);
        }

        [Test]
        public async Task Handle_WithValidBookingIdAndMatchingBranch_ReturnsBookingDetails()
        {
            // Arrange
            var bookingId = 1;
            var userBranchId = 1;
            var booking = new Booking
            {
                BookingId = bookingId,
                VehiclePlateNumber = "ABC123",
                ServiceType = "Oil Change",
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending",
                CustomerName = "John Doe",
                CustomerPhone = "555-1234",
                CustomerEmail = "john@example.com",
                Notes = "Customer prefers morning appointments",
                BranchId = userBranchId,
                Branch = new Branch { BranchId = userBranchId, Name = "Main Branch" },
                CreatedAt = DateTime.UtcNow
            };

            _mockBookingRepository
                .Setup(r => r.GetBookingByIdAsync(bookingId))
                .ReturnsAsync(OperationResult<Booking>.Success(booking));

            var query = new GetBookingDetailsQuery(bookingId, userBranchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.BookingId, Is.EqualTo(bookingId));
            Assert.That(result.Data.VehiclePlateNumber, Is.EqualTo("ABC123"));
            Assert.That(result.Data.CustomerName, Is.EqualTo("John Doe"));
            Assert.That(result.Data.BranchName, Is.EqualTo("Main Branch"));
        }

        [Test]
        public async Task Handle_WithMismatchedBranchId_ReturnsUnauthorizedFailure()
        {
            // Arrange
            var bookingId = 1;
            var userBranchId = 1;
            var bookingBranchId = 2; // Different branch

            var booking = new Booking
            {
                BookingId = bookingId,
                VehiclePlateNumber = "ABC123",
                ServiceType = "Oil Change",
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending",
                BranchId = bookingBranchId,
                Branch = new Branch { BranchId = bookingBranchId, Name = "Other Branch" }
            };

            _mockBookingRepository
                .Setup(r => r.GetBookingByIdAsync(bookingId))
                .ReturnsAsync(OperationResult<Booking>.Success(booking));

            var query = new GetBookingDetailsQuery(bookingId, userBranchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("do not have access"));
        }

        [Test]
        public async Task Handle_WithNonExistentBooking_ReturnsNotFoundFailure()
        {
            // Arrange
            var bookingId = 999;
            var userBranchId = 1;

            _mockBookingRepository
                .Setup(r => r.GetBookingByIdAsync(bookingId))
                .ReturnsAsync(OperationResult<Booking>.Failure("Booking not found."));

            var query = new GetBookingDetailsQuery(bookingId, userBranchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("Booking not found"));
        }

        [Test]
        public async Task Handle_WithNullBookingData_ReturnsFailure()
        {
            // Arrange
            var bookingId = 1;
            var userBranchId = 1;

            _mockBookingRepository
                .Setup(r => r.GetBookingByIdAsync(bookingId))
                .ReturnsAsync(OperationResult<Booking>.Success(null!));

            var query = new GetBookingDetailsQuery(bookingId, userBranchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
        }

        [Test]
        public async Task Handle_WithRepositoryError_ReturnsFailure()
        {
            // Arrange
            var bookingId = 1;
            var userBranchId = 1;
            var errorMessage = "Database connection error";

            _mockBookingRepository
                .Setup(r => r.GetBookingByIdAsync(bookingId))
                .ReturnsAsync(OperationResult<Booking>.Failure(errorMessage));

            var query = new GetBookingDetailsQuery(bookingId, userBranchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain(errorMessage));
        }
    }
}
