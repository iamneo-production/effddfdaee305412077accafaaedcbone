using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroservicetwo.Controllers;
using dotnetmicroservicetwo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroservicetwo.Tests
{
    [TestFixture]
    public class AirportControllerTests
    {
        private AirportController _AirportController;
        private AirportDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<AirportDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AirportDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Airports.AddRange(new List<Airport>
            {
                new Airport { AirportID = 1, AirportName = "Airport One",Location="One Location",Country="One Country",Website="oneairportwebsite.com",ContactNumber="80808080808080",Email="oneairportservice@airport.com",Description="One Airport Description" },
                new Airport { AirportID = 2, AirportName = "Airport Two",Location="Two Location",Country="Two Country",Website="twoairportwebsite.com",ContactNumber="80808080808080",Email="twoairportservice@airport.com",Description="Two Airport Description" },
                new Airport { AirportID = 3, AirportName = "Airport Three",Location="Three Location",Country="Three Country",Website="threeairportwebsite.com",ContactNumber="80808080808080",Email="threeairportservice@airport.com",Description="Three Airport Description" }
            });
            _context.SaveChanges();

            _AirportController = new AirportController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }
        [Test]
        public void AirportClassExists()
        {
            // Arrange
            Type AirportType = typeof(Airport);

            // Act & Assert
            Assert.IsNotNull(AirportType, "Airport class not found.");
        }
        [Test]
        public void Airport_Properties_AirportName_ReturnExpectedDataTypes()
        {
            // Arrange
            Airport airport = new Airport();
            PropertyInfo propertyInfo = airport.GetType().GetProperty("AirportName");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "AirportName property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "AirportName property type is not string.");
        }
[Test]
        public void Airport_Properties_Country_ReturnExpectedDataTypes()
        {
            // Arrange
            Airport airport = new Airport();
            PropertyInfo propertyInfo = airport.GetType().GetProperty("Country");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Country property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Country property type is not string.");
        }

        [Test]
        public async Task GetAllAirports_ReturnsOkResult()
        {
            // Act
            var result = await _AirportController.GetAllAirports();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllAirports_ReturnsAllAirports()
        {
            // Act
            var result = await _AirportController.GetAllAirports();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Airport>>(okResult.Value);
            var airports = okResult.Value as IEnumerable<Airport>;

            var AirportCount = airports.Count();
            Assert.AreEqual(3, AirportCount); // Assuming you have 3 Airports in the seeded data
        }


        [Test]
        public async Task AddAirport_ValidData_ReturnsOkResult()
        {
            // Arrange
            var newAirport = new Airport
            {
 AirportName = "Airport New",Location="New Location",Country="New Country",Website="newirportwebsite.com",ContactNumber="80808080808080",Email="newairportservice@airport.com",Description="New Airport Description"
            };

            // Act
            var result = await _AirportController.AddAirport(newAirport);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task DeleteAirport_ValidId_ReturnsNoContent()
        {
            // Arrange
              // var controller = new AirportsController(context);

                // Act
                var result = await _AirportController.DeleteAirport(1) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteAirport_InvalidId_ReturnsBadRequest()
        {
                   // Act
                var result = await _AirportController.DeleteAirport(0) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Not a valid Airport id", result.Value);
        }
    }
}
