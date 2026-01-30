using DomainLayer.Models;
using InfrastructureLayer.Data;
using InfrastructureLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Bookings.Repositories
{
    [TestFixture]
    public class BookingRepositoryTests
    {
        private WebShopSystemDbContext _context;
        private BookingRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WebShopSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;

            _context = new WebShopSystemDbContext(options);
            _repository = new BookingRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetBookingsByBranchIdAsync_WithValidBranchId_ReturnsOnlyBookingsForThatBranch()
        {
            // Arrange
            var branch1 = new Branch { BranchId = 1, Name = "Branch 1" };
            var branch2 = new Branch { BranchId = 2, Name = "Branch 2" };

            var booking1 = new Booking
            {
                BookingId = 1,
                VehiclePlateNumber = "ABC123",
                ServiceType = "Oil Change",
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending",
                BranchId = 1
            };

            var booking2 = new Booking
            {
                BookingId = 2,
                VehiclePlateNumber = "XYZ789",
                ServiceType = "Tire Replacement",
                AppointmentDate = DateTime.UtcNow.AddDays(2),
                Status = "Pending",
                BranchId = 1
            };

            var booking3 = new Booking
            {
                BookingId = 3,
                VehiclePlateNumber = "DEF456",
                ServiceType = "Brake Inspection",
                AppointmentDate = DateTime.UtcNow.AddDays(3),
                Status = "Pending",
                BranchId = 2 // Different branch
            };

            _context.Branches.AddRange(branch1, branch2);
            _context.Bookings.AddRange(booking1, booking2, booking3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBookingsByBranchIdAsync(1);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);

            var bookings = result.Data.ToList();
            Assert.That(bookings, Has.Count.EqualTo(2));
            Assert.That(bookings.All(b => b.BranchId == 1), Is.True);
            Assert.That(bookings.Any(b => b.BookingId == 3), Is.False); // Should not include booking from other branch
        }

        [Test]
        public async Task GetBookingsByBranchIdAsync_WithNonExistentBranch_ReturnsEmptyList()
        {
            // Arrange
            var nonExistentBranchId = 999;

            // Act
            var result = await _repository.GetBookingsByBranchIdAsync(nonExistentBranchId);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Is.Empty);
        }

        [Test]
        public async Task GetBookingByIdAsync_WithValidBookingId_ReturnsBookingWithBranchDetails()
        {
            // Arrange
            var branch = new Branch { BranchId = 1, Name = "Main Branch" };
            var booking = new Booking
            {
                BookingId = 1,
                VehiclePlateNumber = "ABC123",
                ServiceType = "Oil Change",
                AppointmentDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending",
                BranchId = 1
            };

            _context.Branches.Add(branch);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBookingByIdAsync(1);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.BookingId, Is.EqualTo(1));
            Assert.That(result.Data.VehiclePlateNumber, Is.EqualTo("ABC123"));
            Assert.That(result.Data.Branch, Is.Not.Null);
            Assert.That(result.Data.Branch.Name, Is.EqualTo("Main Branch"));
        }

        [Test]
        public async Task GetBookingByIdAsync_WithNonExistentId_ReturnsFailure()
        {
            // Arrange
            var nonExistentBookingId = 999;

            // Act
            var result = await _repository.GetBookingByIdAsync(nonExistentBookingId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("Booking not found"));
        }
    }
}
