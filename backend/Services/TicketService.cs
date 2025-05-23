using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

public class TicketService : ITicketService
{
    private readonly ISessionFactory _sessionFactory;

    public TicketService(ISessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }

    public IList<Event> GetTop5EventsByTicketCount()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            return session.Query<TicketSales>()
                          .GroupBy(t => t.Event)
                          .OrderByDescending(g => g.Count())
                          .Take(5)
                          .Select(g => g.Key)
                          .ToList();
        }
    }

    public IList<Event> GetTop5EventsByTotalSales()
    {
        using (var session = _sessionFactory.OpenSession())
        {
            return session.Query<TicketSales>()
                          .GroupBy(t => t.Event)
                          .OrderByDescending(g => g.Sum(t => t.PriceInCents))
                          .Take(5)
                          .Select(g => g.Key)
                          .ToList();
        }
    }
}