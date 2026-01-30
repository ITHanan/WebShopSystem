using ApplicationLayer.Bookings.Queries.GetBookingsByBranch;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Bookings.Queries
{
    [TestFixture]
    public class GetBookingsByBranchQueryHandlerTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private GetBookingsByBranchQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _handler = new GetBookingsByBranchQueryHandler(_mockBookingRepository.Object);
        }

        [Test]
        public async Task Handle_WithValidBranchId_ReturnsBookingsSortedByAppointmentDate()
        {
            // Arrange
            var branchId = 1;
            var bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 1,
                    VehiclePlateNumber = "ABC123",
                    ServiceType = "Oil Change",
                    AppointmentDate = DateTime.UtcNow.AddDays(3),
                    Status = "Pending",
                    BranchId = branchId
                },
                new Booking
                {
                    BookingId = 2,
                    VehiclePlateNumber = "XYZ789",
                    ServiceType = "Tire Replacement",
                    AppointmentDate = DateTime.UtcNow.AddDays(1),
                    Status = "Pending",
                    BranchId = branchId
                },
                new Booking
                {
                    BookingId = 3,
                    VehiclePlateNumber = "DEF456",
                    ServiceType = "Brake Inspection",
                    AppointmentDate = DateTime.UtcNow.AddDays(2),
                    Status = "Pending",
                    BranchId = branchId
                }
            };

            _mockBookingRepository
                .Setup(r => r.GetBookingsByBranchIdAsync(branchId))
                .ReturnsAsync(OperationResult<IEnumerable<Booking>>.Success(bookings));

            var query = new GetBookingsByBranchQuery(branchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);

            var resultList = result.Data.ToList();
            Assert.That(resultList, Has.Count.EqualTo(3));

            // Verify sorted by appointment date ascending
            Assert.That(resultList[0].BookingId, Is.EqualTo(2)); // Earliest date
            Assert.That(resultList[1].BookingId, Is.EqualTo(3)); // Middle date
            Assert.That(resultList[2].BookingId, Is.EqualTo(1)); // Latest date
        }

        [Test]
        public async Task Handle_WithEmptyBookingList_ReturnsEmptyResult()
        {
            // Arrange
            var branchId = 1;
            var emptyBookings = new List<Booking>();

            _mockBookingRepository
                .Setup(r => r.GetBookingsByBranchIdAsync(branchId))
                .ReturnsAsync(OperationResult<IEnumerable<Booking>>.Success(emptyBookings));

            var query = new GetBookingsByBranchQuery(branchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Is.Empty);
        }

        [Test]
        public async Task Handle_WhenRepositoryFails_ReturnsFailureResult()
        {
            // Arrange
            var branchId = 1;
            var errorMessage = "Database connection failed";

            _mockBookingRepository
                .Setup(r => r.GetBookingsByBranchIdAsync(branchId))
                .ReturnsAsync(OperationResult<IEnumerable<Booking>>.Failure(errorMessage));

            var query = new GetBookingsByBranchQuery(branchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ErrorMessage, Does.Contain(errorMessage));
        }

        [Test]
        public async Task Handle_WithNullBookingData_ReturnsEmptyResult()
        {
            // Arrange
            var branchId = 1;

            _mockBookingRepository
                .Setup(r => r.GetBookingsByBranchIdAsync(branchId))
                .ReturnsAsync(OperationResult<IEnumerable<Booking>>.Success(null!));

            var query = new GetBookingsByBranchQuery(branchId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Is.Empty);
        }
    }
}
