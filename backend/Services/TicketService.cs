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

    public IList<EventDto> GetTop5EventsByTicketCount()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                // Fetch the top 5 events by ticket count
                var results = session.Query<TicketSales>()
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
                var events = session.Query<Event>()
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

    public IList<EventDto> GetTop5EventsByTotalSales()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            try
            {
                // Group by Event.Id and calculate total sales
                var results = session.Query<TicketSales>()
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
                var events = session.Query<Event>()
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

    ////////////////////////////////////////////////////
    /// TODO: Create Event and 
    ////////////////////////////////////////////////////

    public IList<TicketSalesWithEventDto> GetTopTickets(int limit = 100)
    {
        using (var session = _sessionFactory.OpenSession())
        {
            return session.Query<TicketSales>()
                        .OrderByDescending(t => t.PurchaseDate)
                        .Take(limit)
                        .Select(t => new TicketSalesWithEventDto
                        {
                            TicketId = t.Id,
                            UserId = t.UserId,
                            PurchaseDate = t.PurchaseDate,
                            PriceInCents = t.PriceInCents,
                            EventId = t.Event.Id,
                            EventName = t.Event.Name,
                            EventStartDate = t.Event.StartsOn,
                            EventEndDate = t.Event.EndsOn
                        })
                        .ToList();
        }
    }

}