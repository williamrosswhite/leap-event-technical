using Models;
using NHibernate;
using DTOs;

public class TicketService : ITicketService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly ILogger<TicketService> _logger;

    public TicketService(
        ISessionFactory sessionFactory,
        ILogger<TicketService> logger)
    {
        _sessionFactory = sessionFactory ??
            throw new ArgumentNullException(nameof(sessionFactory));
        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public IList<TicketSalesDto> GetTicketsByEventId(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
        {
            _logger.LogWarning("Invalid event ID provided: {EventId}", eventId);
            throw new ArgumentException("Event ID cannot be null or empty.", nameof(eventId));
        }

        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                _logger.LogInformation("Fetching tickets for event ID: {EventId}", eventId);

                var tickets = session.Query<TicketSales>()
                                     .Where(t => t.Event.Id == eventId)
                                     .Select(t => new TicketSalesDto
                                     {
                                         TicketId = t.Id,
                                         PurchaseDate = t.PurchaseDate,
                                         PriceInCents = t.PriceInCents
                                     })
                                     .ToList();

                _logger.LogInformation("Successfully fetched {TicketCount} tickets for event ID: {EventId}", tickets.Count, eventId);
                return tickets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching tickets for event ID: {EventId}", eventId);
                throw new ApplicationException($"An error occurred while fetching tickets for event ID {eventId}.", ex);
            }
        }
    }

    public IList<EventDto> GetTop5EventsByTicketCount()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                _logger.LogInformation("Fetching the top 5 events by ticket count.");

                var ticketSalesQuery = session.Query<TicketSales>();
                if (ticketSalesQuery == null)
                {
                    _logger.LogWarning("No ticket sales data found.");
                    return new List<EventDto>();
                }

                // Group ticket sales by Event ID and count tickets
                var results = ticketSalesQuery
                    .GroupBy(t => t.Event.Id)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        TicketCount = g.Count()
                    })
                    .OrderByDescending(g => g.TicketCount)
                    .Take(5)
                    .ToList();

                var eventIds = results.Select(r => r.EventId).ToList();
                var eventQuery = session.Query<Event>();
                if (eventQuery == null)
                {
                    _logger.LogWarning("No event data found.");
                    return new List<EventDto>();
                }

                // Fetch event details for the top 5 events
                var events = eventQuery
                    .Where(e => eventIds.Contains(e.Id))
                    .ToList();

                // Map to EventDto
                var eventDtos = events.Select(e => new EventDto
                {
                    EventId = e.Id,
                    EventName = e.Name,
                    EventStartDate = e.StartsOn,
                    EventEndDate = e.EndsOn,
                    TicketCount = results.First(r => r.EventId == e.Id).TicketCount
                }).ToList();

                _logger.LogInformation("Successfully fetched the top 5 events by ticket count.");
                return eventDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the top 5 events by ticket count.");
                throw new ApplicationException("An error occurred while fetching the top 5 events by ticket count.", ex);
            }
        }
    }

    public IList<EventDto> GetTop5EventsByTotalSales()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                _logger.LogInformation("Fetching the top 5 events by total sales.");

                // Fetch ticket sales and event data
                var ticketSalesQuery = session.Query<TicketSales>();
                var eventQuery = session.Query<Event>();

                if (ticketSalesQuery == null || !ticketSalesQuery.Any())
                {
                    _logger.LogWarning("No ticket sales data found.");
                    return new List<EventDto>();
                }

                if (eventQuery == null || !eventQuery.Any())
                {
                    _logger.LogWarning("No event data found.");
                    return new List<EventDto>();
                }

                // Group ticket sales by Event ID and calculate total sales
                var results = ticketSalesQuery
                    .GroupBy(t => t.Event.Id)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        TotalSales = g.Sum(t => t.PriceInCents)
                    })
                    .OrderByDescending(g => g.TotalSales)
                    .Take(5)
                    .ToList();

                // Fetch event details for the top 5 events
                var eventIds = results.Select(r => r.EventId).ToList();
                var events = eventQuery
                    .Where(e => eventIds.Contains(e.Id))
                    .ToList();

                // Map to EventDto
                var eventDtos = events.Select(e => new EventDto
                {
                    EventId = e.Id,
                    EventName = e.Name,
                    EventStartDate = e.StartsOn,
                    EventEndDate = e.EndsOn,
                    TotalSales = results.First(r => r.EventId == e.Id).TotalSales
                }).ToList();

                _logger.LogInformation("Successfully fetched the top 5 events by total sales.");
                return eventDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the top 5 events by total sales.");
                throw new ApplicationException("An error occurred while fetching the top 5 events by total sales.", ex);
            }
        }
    }
}