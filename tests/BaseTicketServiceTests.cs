using Moq;
using Models;
using NHibernate;
using Microsoft.Extensions.Logging;

namespace LeapEventTechnical.Tests
{
    public abstract class BaseTicketServiceTests
    {
        protected TicketService? _ticketService;

        // Mocks
        protected Mock<ISessionFactory>? _sessionFactoryMock;
        protected Mock<ISession>? _sessionMock;
        protected Mock<ILogger<TicketService>>? _loggerMock;

        // Placeholders for dummy data
        protected List<Event> Events { get; set; } = new();
        protected List<TicketSales> TicketSales { get; set; } = new();

        [TestInitialize]
        public void BaseSetup()
        {
            // Arrange
            _sessionFactoryMock = new Mock<ISessionFactory>();
            _sessionMock = new Mock<ISession>();
            _loggerMock = new Mock<ILogger<TicketService>>();

            // Set up session factory to return the mocked session
            _sessionFactoryMock.Setup(f => f.OpenSession()).Returns(_sessionMock.Object);

            // Initialize TicketService
            _ticketService = new TicketService(_sessionFactoryMock.Object, _loggerMock.Object);

            InitializeDummyData();

            // Mock the session to return the dummy data
            _sessionMock!.Setup(s => s.Query<Event>()).Returns(Events.AsQueryable());
            _sessionMock.Setup(s => s.Query<TicketSales>()).Returns(TicketSales.AsQueryable());
        }

        private void InitializeDummyData()
        {
            // Arrange: Initialize dummy data for this test
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

            TicketSales = new List<TicketSales>
            {
                new TicketSales
                {
                    Id = "T1",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1000,
                    Event = Events[0]
                },
                new TicketSales
                {
                    Id = "T2",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1500,
                    Event = Events[0]
                },
                new TicketSales
                {
                    Id = "T3",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2000,
                    Event = Events[0]
                },
                new TicketSales
                {
                    Id = "T4",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1200,
                    Event = Events[1]
                },
                new TicketSales
                {
                    Id = "T5",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1800,
                    Event = Events[1]
                },
                new TicketSales
                {
                    Id = "T6",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2200,
                    Event = Events[1]
                },

                new TicketSales
                {
                    Id = "T7",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    PriceInCents = 1300,
                    Event = Events[2]
                },
                new TicketSales
                {
                    Id = "T8",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    PriceInCents = 1700,
                    Event = Events[2]
                },
                new TicketSales
                {
                    Id = "T9",
                    PurchaseDate = DateTime.Now.AddDays(-3),
                    PriceInCents = 2100,
                    Event = Events[2]
                }
            };
        }

        // Protected methods to modify Events and TicketSales
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