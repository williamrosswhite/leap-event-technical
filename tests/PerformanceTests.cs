using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeapEventTechnical.Tests
{
    [TestClass]
    public class PerformanceTests : BaseTicketServiceTests
    {
        [TestMethod]
        public void GetTop5EventsByTicketCount_LargeDataset_PerformsEfficiently()
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
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count); // Only top 5 events should be returned
        }

        [TestMethod]
        public void GetTop5EventsByTotalSales_LargeDataset_PerformsEfficiently()
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
    }
}