using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using DTOs;

public class TicketService : ITicketService
{
    private readonly ISessionFactory _sessionFactory;

    public TicketService(ISessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }

        public IList<TicketSalesDto> GetTicketsByEventId(string eventId)
    {
        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                return session.Query<TicketSales>()
                            .Where(t => t.Event.Id == eventId)
                            .Select(t => new TicketSalesDto
                            {
                                TicketId = t.Id,
                                UserId = t.UserId,
                                PurchaseDate = t.PurchaseDate,
                                PriceInCents = t.PriceInCents
                            })
                            .ToList();
            }
            catch (Exception ex)
            {
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
                // Fetch the TicketSales data
                var ticketSalesQuery = session.Query<TicketSales>();
                if (ticketSalesQuery == null)
                {
                    return new List<EventDto>(); // Return an empty list if TicketSales is null
                }

                // Group by Event.Id and calculate ticket counts
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

                // Fetch the corresponding Event objects
                var eventIds = results.Select(r => r.EventId).ToList();
                var eventQuery = session.Query<Event>();
                if (eventQuery == null)
                {
                    return new List<EventDto>(); // Return an empty list if Event is null
                }

                var events = eventQuery
                    .Where(e => eventIds.Contains(e.Id))
                    .ToList();

                // Map the results to EventDto
                return events.Select(e => new EventDto
                {
                    EventId = e.Id,
                    EventName = e.Name,
                    EventStartDate = e.StartsOn,
                    EventEndDate = e.EndsOn,
                    TicketCount = results.First(r => r.EventId == e.Id).TicketCount
                }).ToList();
            }
            catch (Exception ex)
            {
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
                // Ensure the source is not null
                var ticketSalesQuery = session.Query<TicketSales>() ?? Enumerable.Empty<TicketSales>().AsQueryable();
                var eventQuery = session.Query<Event>() ?? Enumerable.Empty<Event>().AsQueryable();

                // Group by Event.Id and calculate total sales
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

                // Fetch the corresponding Event objects
                var eventIds = results.Select(r => r.EventId).ToList();
                var events = eventQuery
                    .Where(e => eventIds.Contains(e.Id))
                    .ToList();

                // Map the results to EventDto
                return events.Select(e => new EventDto
                {
                    EventId = e.Id,
                    EventName = e.Name,
                    EventStartDate = e.StartsOn,
                    EventEndDate = e.EndsOn,
                    TotalSales = results.First(r => r.EventId == e.Id).TotalSales
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the top 5 events by total sales.", ex);
            }
        }
    }
}