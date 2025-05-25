using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeapEventTechnical.Tests
{
    [TestClass]
    public class GetTop5EventsByTotalSalesTests : BaseTicketServiceTests
    {
        [TestMethod]
        public void ReturnsTop5EventsSortedByTotalSales()
        {
            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count); // There are 3 events in the dummy data

            // Verify the order and total sales
            Assert.AreEqual("Event 1", result[0].EventName); // Event 1 has the highest total sales
            Assert.AreEqual(4500, result[0].TotalSales); // Event 1 total sales: 1000 + 1500 + 2000

            Assert.AreEqual("Event 2", result[1].EventName); // Event 2 is second
            Assert.AreEqual(5200, result[1].TotalSales); // Event 2 total sales: 1200 + 1800 + 2200

            Assert.AreEqual("Event 3", result[2].EventName); // Event 3 is third
            Assert.AreEqual(5100, result[2].TotalSales); // Event 3 total sales: 1300 + 1700 + 2100
        }

        [TestMethod]
        public void FewerThan5Events()
        {
            // Arrange
            SetEvents(Events.Take(2).ToList()); // Only 2 events
            SetTicketSales(TicketSales.Where(ts => ts.Event.Id == "E1" || ts.Event.Id == "E2").ToList());

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Only 2 events exist
            Assert.AreEqual("Event 1", result[0].EventName);
            Assert.AreEqual("Event 2", result[1].EventName);
        }

        [TestMethod]
        public void NoEvents()
        {
            // Arrange
            SetEvents(new List<Event>()); // No events
            SetTicketSales(new List<TicketSales>()); // No ticket sales

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count); // No events should be returned
        }

        [TestMethod]
        public void TicketSalesQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<TicketSales>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTotalSales());
            Assert.AreEqual("An error occurred while fetching the top 5 events by total sales.", exception.Message);
        }

        [TestMethod]
        public void EventQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<Event>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTotalSales());
            Assert.AreEqual("An error occurred while fetching the top 5 events by total sales.", exception.Message);
        }

        [TestMethod]
        public void LargeDataset_PerformsEfficiently()
        {
            // Arrange
            var events = Enumerable.Range(1, 1000).Select(i => new Event
            {
                Id = $"E{i}",
                Name = $"Event {i}",
                StartsOn = DateTime.Now.AddDays(i),
                EndsOn = DateTime.Now.AddDays(i + 1),
                Location = $"Location {i}"
            }).ToList();

            var ticketSales = Enumerable.Range(1, 10000).Select(i => new TicketSales
            {
                Id = $"T{i}",
                UserId = $"U{i}",
                PurchaseDate = DateTime.Now.AddDays(-i),
                PriceInCents = 1000 + i,
                Event = events[i % events.Count]
            }).ToList();

            SetEvents(events);
            SetTicketSales(ticketSales);

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count); // Only top 5 events should be returned
        }

        [TestMethod]
        public void EventsWithZeroTickets_AreExcluded()
        {
            // Arrange
            SetEvents(Events.Take(2).ToList()); // Only 2 events
            SetTicketSales(TicketSales.Where(ts => ts.Event.Id == "E1").ToList()); // Only Event 1 has tickets

            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());

            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count); // Only Event 1 should be returned
            Assert.AreEqual("Event 1", result[0].EventName);
        }

        [TestMethod]
        public void QueriesTicketSalesAndEvent()
        {
            // Act
            _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            _sessionMock!.Verify(s => s.Query<TicketSales>(), Times.Once); // Ensure TicketSales is queried
            _sessionMock.Verify(s => s.Query<Event>(), Times.Once); // Ensure Event is queried
        }
    }
}