using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroserviceone.Controllers;
using dotnetmicroserviceone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroserviceone.Tests
{
    [TestFixture]
    public class FlightControllerTests
    {
        private FlightController _FlightController;
        private FlightDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<FlightDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FlightDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Flights.AddRange(new List<Flight>
            {
                new Flight { FlightID = 1, FlightNumber = "12399Flight",TicketPrice=12000,TotalSeats=200,Airline="One AirLine",Pilot="Pilot1",Status="Ready",Gate="3",Terminal="One" },
                new Flight { FlightID = 2, FlightNumber = "12945Flight",TicketPrice=8000,TotalSeats=250,Airline="Two AirLine",Pilot="Pilot2",Status="Ready",Gate="4",Terminal="Two" },
                new Flight { FlightID = 3, FlightNumber = "12388Flight",TicketPrice=40000,TotalSeats=400,Airline="Thee AirLine",Pilot="Pilot3",Status="Ready",Gate="7",Terminal="One" },
            });
            _context.SaveChanges();

            _FlightController = new FlightController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }
        [Test]
        public void FlightClassExists()
        {
            // Arrange
            Type FlightType = typeof(Flight);

            // Act & Assert
            Assert.IsNotNull(FlightType, "Flight class not found.");
        }
        [Test]
        public void Flight_Properties_FlightNumber_ReturnExpectedDataTypes()
        {
            // Arrange
            Flight flight = new Flight();
            PropertyInfo propertyInfo = flight.GetType().GetProperty("FlightNumber");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "FlightNumber property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "FlightNumber property type is not string.");
        }
[Test]
        public void Flight_Properties_Pilot_ReturnExpectedDataTypes()
        {
            // Arrange
            Flight flight = new Flight();
            PropertyInfo propertyInfo = flight.GetType().GetProperty("Pilot");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Pilot property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Pilot property type is not string.");
        }
        [Test]
        public void Flight_Properties_Status_ReturnExpectedDataTypes()
        {
            // Arrange
            Flight flight = new Flight();
            PropertyInfo propertyInfo = flight.GetType().GetProperty("Status");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Status property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Status property type is not string.");
        }

        [Test]
        public async Task GetAllFlights_ReturnsOkResult()
        {
            // Act
            var result = await _FlightController.GetAllFlights();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllFlights_ReturnsAllFlights()
        {
            // Act
            var result = await _FlightController.GetAllFlights();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Flight>>(okResult.Value);
            var flights = okResult.Value as IEnumerable<Flight>;

            var FlightCount = flights.Count();
            Assert.AreEqual(3, FlightCount); // Assuming you have 3 Flights in the seeded data
        }

        [Test]
        public async Task GetFlightById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _FlightController.GetFlightById(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetFlightById_ExistingId_ReturnsFlight()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _FlightController.GetFlightById(existingId);

            // Assert
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            var flight = okResult.Value as Flight;
            Assert.IsNotNull(flight);
            Assert.AreEqual(existingId, flight.FlightID);
        }

        [Test]
        public async Task GetFlightById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 99; // Assuming this ID does not exist in the seeded data

            // Act
            var result = await _FlightController.GetFlightById(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task AddFlight_ValidData_ReturnsOkResult()
        {
            // Arrange
            var newFlight = new Flight
            {
FlightNumber = "12345Flight",TicketPrice=12000,TotalSeats=200,Airline="New AirLine",Pilot="Rageena",Status="Ready",Gate="3",Terminal="One"
            };

            // Act
            var result = await _FlightController.AddFlight(newFlight);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task DeleteFlight_ValidId_ReturnsNoContent()
        {
            // Arrange
              // var controller = new FlightsController(context);

                // Act
                var result = await _FlightController.DeleteFlight(1) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteFlight_InvalidId_ReturnsBadRequest()
        {
                   // Act
                var result = await _FlightController.DeleteFlight(0) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Not a valid Flight id", result.Value);
        }
    }
}
