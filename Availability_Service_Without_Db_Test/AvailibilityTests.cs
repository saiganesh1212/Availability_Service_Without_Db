using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Availability_Service_Without_DB.Repository;
using Availability_Service_Without_DB.Models;
using System.Collections.Generic;
using Availability_Service_Without_DB.Controllers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Availability_Service_Without_Db_Test
{
    public class Tests
    {
        Mock<IConfiguration> config;
        List<Flight> flights;
        Mock<IAvailabilityRepo> mock;
        [SetUp]
        public void Setup()
        {
            config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("AuthServiceSampleKeyIssuedByGanesh");
            flights = new List<Flight>() { new Flight() { FlightId = 1, Company_Name = "Indigo", Starting_Location = "Delhi", Destination = "Pune", Available_Seats = 24 },
            new Flight() { FlightId=2,Company_Name="AirIndia",Starting_Location="Pune",Destination="Agra",Available_Seats=35},
            new Flight() { FlightId=3,Company_Name="King Fisher",Starting_Location="Agra",Destination="Mumbai",Available_Seats=6}
            };
            mock = new Mock<IAvailabilityRepo>();
        }

        [Test]
        public void Get_All_Flights_Test()
        {

            
            mock.Setup(x => x.GetFlights()).Returns(flights);
            var controller = new AvailabilityController(mock.Object);
            var result = controller.Get_AllFlights() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<List<Flight>>(result.Value);
            
        }
        
        [Test]
        public void Get_Availability_For_Flight_Success_Test()
        {
            mock.Setup(x => x.AvailableCount(It.IsAny<int>())).Returns(It.IsAny<int>());
            var controller = new AvailabilityController(mock.Object);
            var result = controller.Get_Availability_For_Flight(1) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]
        public void Get_Availability_For_Flight_Failure_Test()
        {

            mock.Setup(x => x.AvailableCount(It.IsAny<int>())).Returns(null);
            var controller = new AvailabilityController(mock.Object);
            var result = controller.Get_Availability_For_Flight(1) as StatusCodeResult;
            Assert.IsNull(result);

        }

        [Test]
        public void Reduce_Availability_Success_Test()
        {
            mock.Setup(x => x.Reduce(It.IsAny<int>())).Returns(true);
            var controller = new AvailabilityController(mock.Object);
            var result = controller.Reduce_Availability(1) as OkResult;
            Assert.AreEqual(200, result.StatusCode);

        }
        [Test]
        public void Reduce_Availability_Failure_Test()
        {
            mock.Setup(x => x.Reduce(It.IsAny<int>())).Returns(false);
            var controller = new AvailabilityController(mock.Object);
            var result = controller.Reduce_Availability(1) as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);

        }
    }
}