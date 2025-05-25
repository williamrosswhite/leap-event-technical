using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using System;

namespace LeapEventTechnical.Tests
{
    [TestClass]
    public class ErrorHandlingTests : BaseTicketServiceTests
    {
        [TestMethod]
        public void GetTop5EventsByTicketCount_TicketSalesQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<TicketSales>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTicketCount());
            Assert.AreEqual("An error occurred while fetching the top 5 events by ticket count.", exception.Message);
        }

        [TestMethod]
        public void GetTop5EventsByTicketCount_EventQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<Event>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTicketCount());
            Assert.AreEqual("An error occurred while fetching the top 5 events by ticket count.", exception.Message);
        }

        [TestMethod]
        public void GetTop5EventsByTotalSales_TicketSalesQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<TicketSales>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTotalSales());
            Assert.AreEqual("An error occurred while fetching the top 5 events by total sales.", exception.Message);
        }

        [TestMethod]
        public void GetTop5EventsByTotalSales_EventQueryFails_ThrowsException()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<Event>()).Throws(new ApplicationException("Database query failed"));

            // Act & Assert
            var exception = Assert.ThrowsException<ApplicationException>(() => _ticketService!.GetTop5EventsByTotalSales());
            Assert.AreEqual("An error occurred while fetching the top 5 events by total sales.", exception.Message);
        }

        [TestMethod]
        public void GetTop5EventsByTicketCount_NullOrInvalidData_HandlesGracefully()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<Event>()).Returns((IQueryable<Event>)null!);
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns((IQueryable<TicketSales>)null!);

            // Act
            var result = _ticketService!.GetTop5EventsByTicketCount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count); // Should return an empty list
        }

        [TestMethod]
        public void GetTop5EventsByTotalSales_NullOrInvalidData_HandlesGracefully()
        {
            // Arrange
            _sessionMock!.Setup(s => s.Query<Event>()).Returns((IQueryable<Event>)null!);
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns((IQueryable<TicketSales>)null!);

            // Act
            var result = _ticketService!.GetTop5EventsByTotalSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count); // Should return an empty list
        }
    }
}