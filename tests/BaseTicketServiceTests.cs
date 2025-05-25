using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using DTOs;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeapEventTechnical.Tests
{
    public abstract class BaseTicketServiceTests
    {
        protected Mock<ISessionFactory>? _sessionFactoryMock;
        protected Mock<ISession>? _sessionMock;
        protected TicketService? _ticketService;

        // Keep the set accessor private
        protected List<Event> Events { get; set; } = new();
        protected List<TicketSales> TicketSales { get; set; } = new();

        [TestInitialize]
        public void BaseSetup()
        {
            // Initialize mocks
            _sessionFactoryMock = new Mock<ISessionFactory>();
            _sessionMock = new Mock<ISession>();

            // Set up the session factory to return the mocked session
            _sessionFactoryMock.Setup(f => f.OpenSession()).Returns(_sessionMock.Object);

            // Initialize the TicketService with the mocked session factory
            _ticketService = new TicketService(_sessionFactoryMock.Object);

            // Initialize dummy data
            InitializeDummyData();

            // Mock the session to return the dummy data
            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());
        }

        private void InitializeDummyData()
        {
            // Initialize events
            Events = new List<Event>
            {
                new Event
                {
                    Id = "E1",
                    Name = "Event 1",
                    StartsOn = DateTime.Now.AddDays(1),
                    EndsOn = DateTime.Now.AddDays(2),
                    Location = "Location 1"
                },
                new Event
                {
                    Id = "E2",
                    Name = "Event 2",
                    StartsOn = DateTime.Now.AddDays(3),
                    EndsOn = DateTime.Now.AddDays(4),
                    Location = "Location 2"
                },
                new Event
                {
                    Id = "E3",
                    Name = "Event 3",
                    StartsOn = DateTime.Now.AddDays(5),
                    EndsOn = DateTime.Now.AddDays(6),
                    Location = "Location 3"
                }
            };

            // Initialize ticket sales
            TicketSales = new List<TicketSales>
            {
                // Tickets for Event 1
                new TicketSales
                {
                    Id = "T1",
                    UserId = "U1",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1000,
                    Event = Events[0]
                },
                new TicketSales
                {
                    Id = "T2",
                    UserId = "U2",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1500,
                    Event = Events[0]
                },
                new TicketSales
                {
                    Id = "T3",
                    UserId = "U3",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2000,
                    Event = Events[0]
                },

                // Tickets for Event 2
                new TicketSales
                {
                    Id = "T4",
                    UserId = "U4",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1200,
                    Event = Events[1]
                },
                new TicketSales
                {
                    Id = "T5",
                    UserId = "U5",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1800,
                    Event = Events[1]
                },
                new TicketSales
                {
                    Id = "T6",
                    UserId = "U6",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2200,
                    Event = Events[1]
                },

                // Tickets for Event 3
                new TicketSales
                {
                    Id = "T7",
                    UserId = "U7",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1300,
                    Event = Events[2]
                },
                new TicketSales
                {
                    Id = "T8",
                    UserId = "U8",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1700,
                    Event = Events[2]
                },
                new TicketSales
                {
                    Id = "T9",
                    UserId = "U9",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2100,
                    Event = Events[2]
                }
            };
        }

        // Add protected methods to modify Events and TicketSales
        protected void SetEvents(List<Event> events)
        {
            Events = events;
        }

        protected void SetTicketSales(List<TicketSales> ticketSales)
        {
            TicketSales = ticketSales;
        }
    }
}