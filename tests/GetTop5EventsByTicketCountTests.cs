using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeapEventTechnical.Tests;
using System.Linq;
using Models;

namespace LeapEventTechnical.Tests
{
    [TestClass]
    public class GetTop5EventsByTicketCountTests : BaseTicketServiceTests
    {
        [TestMethod]
        public void ReturnsTop5EventsSortedByTicketCount()
        {
            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count); // There are 3 events in the dummy data

            // Verify the order and ticket counts
            Assert.AreEqual("Event 1", result[0].EventName); // Event 1 has the highest ticket count
            Assert.AreEqual(3, result[0].TicketCount); // Event 1 has 3 tickets

            Assert.AreEqual("Event 2", result[1].EventName); // Event 2 is second
            Assert.AreEqual(3, result[1].TicketCount); // Event 2 also has 3 tickets

            Assert.AreEqual("Event 3", result[2].EventName); // Event 3 is third
            Assert.AreEqual(3, result[2].TicketCount); // Event 3 also has 3 tickets
        }

        [TestMethod]
        public void FewerThan5Events()
        {
            // Arrange
            Events = Events.Take(2).ToList(); // Only 2 events
            TicketSales = TicketSales.Where(ts => ts.Event.Id == "E1" || ts.Event.Id == "E2").ToList();

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Only 2 events exist
            Assert.AreEqual("Event 1", result[0].EventName);
            Assert.AreEqual("Event 2", result[1].EventName);
        }

        [TestMethod]
        public void Exactly5Events()
        {
            // Arrange
            Events.Add(new Models.Event
            {
                Id = "E4",
                Name = "Event 4",
                StartsOn = System.DateTime.Now.AddDays(7),
                EndsOn = System.DateTime.Now.AddDays(8),
                Location = "Location 4"
            });
            Events.Add(new Models.Event
            {
                Id = "E5",
                Name = "Event 5",
                StartsOn = System.DateTime.Now.AddDays(9),
                EndsOn = System.DateTime.Now.AddDays(10),
                Location = "Location 5"
            });

            TicketSales.Add(new Models.TicketSales
            {
                Id = "T10",
                UserId = "U10",
                PurchaseDate = System.DateTime.Now.AddDays(-1),
                PriceInCents = 1000,
                Event = Events[3]
            });
            TicketSales.Add(new Models.TicketSales
            {
                Id = "T11",
                UserId = "U11",
                PurchaseDate = System.DateTime.Now.AddDays(-2),
                PriceInCents = 1500,
                Event = Events[4]
            });

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count); // Exactly 5 events exist
            Assert.AreEqual("Event 1", result[0].EventName);
            Assert.AreEqual("Event 2", result[1].EventName);
            Assert.AreEqual("Event 3", result[2].EventName);
            Assert.AreEqual("Event 4", result[3].EventName);
            Assert.AreEqual("Event 5", result[4].EventName);
        }

        [TestMethod]
        public void NoEvents()
        {
            // Arrange
            Events.Clear();
            TicketSales.Clear();

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count); // No events should be returned
        }

        [TestMethod]
        public void HandlesTies()
        {
            // Arrange
            TicketSales.Add(new Models.TicketSales
            {
                Id = "T10",
                UserId = "U10",
                PurchaseDate = System.DateTime.Now.AddDays(-1),
                PriceInCents = 1000,
                Event = Events[0] // Add another ticket to Event 1 to create a tie
            });

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count); // Only 3 events exist

            // Verify EventDto properties for Event 1
            Assert.AreEqual("E1", result[0].EventId);
            Assert.AreEqual("Event 1", result[0].EventName);
            Assert.AreEqual(4, result[0].TicketCount); // Event 1 now has 4 tickets

            // Verify EventDto properties for Event 2
            Assert.AreEqual("E2", result[1].EventId);
            Assert.AreEqual("Event 2", result[1].EventName);
            Assert.AreEqual(3, result[1].TicketCount);

            // Verify EventDto properties for Event 3
            Assert.AreEqual("E3", result[2].EventId);
            Assert.AreEqual("Event 3", result[2].EventName);
            Assert.AreEqual(3, result[2].TicketCount);
        }
    }
}