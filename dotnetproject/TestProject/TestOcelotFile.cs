using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Ocelot.Tests
{
    [TestFixture]
    public class OcelotJsonTests
    {
        private JObject ocelotJson;

        [SetUp]
        public void Setup()
        {
        //     // Load the Ocelot.json file for testing (replace with your actual file path)
        //     ///home/coder/project/workspace/dotnetproject/dotnetapigateway/Ocelot.json
        // string jsonFilePath = "/home/coder/workspace/dotnetproject/dotnetapigateway/Ocelot.json";
        //     string jsonText = System.IO.File.ReadAllText(jsonFilePath);
        //     ocelotJson = JObject.Parse(jsonText);
string jsonFilePath = "/home/coder/workspace/dotnetproject/dotnetapigateway/Ocelot.json";

if (System.IO.File.Exists(jsonFilePath))
{
    string jsonText = System.IO.File.ReadAllText(jsonFilePath);
   ocelotJson = JObject.Parse(jsonText);
}
else
{
    Console.WriteLine("The file does not exist at the specified path.");
}

        }

       [Test]
        public void VerifyFlightRoute()
        {
            var FlightRoute = ocelotJson["Routes"][0];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Flight"));
            Assert.That(FlightRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(FlightRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(FlightRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(7214));
        }

        [Test]
        public void VerifyFlightIDRoute()
        {
            var AirportRoute = ocelotJson["Routes"][1];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Flight/{id}"));
            Assert.That(AirportRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(7214));
        }
[Test]
        public void VerifyAirportRoute()
        {
            var AirportRoute = ocelotJson["Routes"][2];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Airport"));
            Assert.That(AirportRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(6200));
        }
                [Test]
        public void VerifyAirportNamesRoute()
        {
            var AirportRoute = ocelotJson["Routes"][3];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Airport/AirportNames"));
            Assert.That(AirportRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(6200));
        }
        [Test]
        public void VerifyAirportIDRoute()
        {
            var AirportRoute = ocelotJson["Routes"][4];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Airport/{id}"));
            Assert.That(AirportRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("https"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(AirportRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(6200));
        }
        [Test]
        public void VerifyFlightRouteUpstreamPath()
        {
            var FlightRoute = ocelotJson["Routes"][0];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Flight"));
        }
        [Test]
        public void VerifyFlightIDRouteUpstreamPath()
        {
            var FlightRoute = ocelotJson["Routes"][1];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Flight/{id}"));
        }
        [Test]
        public void VerifyAirportRouteUpstreamPath()
        {
            var FlightRoute = ocelotJson["Routes"][2];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Airport"));
        }
        [Test]
        public void VerifyAirportNamesRouteUpstreamPath()
        {
            var FlightRoute = ocelotJson["Routes"][3];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Airport/AirportNames"));
        }

        [Test]
        public void VerifyFlightRouteHttpMethods()
        {
            var FlightRoute = ocelotJson["Routes"][0];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "POST", "GET" }));
        }
        [Test]
        public void VerifyFlightIDRouteHttpMethods()
        {
            var FlightRoute = ocelotJson["Routes"][1];

            Assert.That(FlightRoute, Is.Not.Null);
            Assert.That(FlightRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "DELETE", "GET" }));
        }
        [Test]
        public void VerifyAirportRouteHttpMethods()
        {
            var AirportRoute = ocelotJson["Routes"][2];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "POST", "GET" }));
        }
        [Test]
        public void VerifyAirportNamesRouteHttpMethods()
        {
            var AirportRoute = ocelotJson["Routes"][3];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "GET" }));
        }
        [Test]
        public void VerifyAirportDeleteIDRouteHttpMethods()
        {
            var AirportRoute = ocelotJson["Routes"][4];

            Assert.That(AirportRoute, Is.Not.Null);
            Assert.That(AirportRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "DELETE" }));
        }
    }
}
