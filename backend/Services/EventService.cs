using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

public class EventService : IEventService
{
    private readonly ISessionFactory _sessionFactory;

    public EventService(ISessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }

    public IList<Event> GetEventsWithinDays(int days)
    {
        using (var session = _sessionFactory.OpenSession())
        {
            var targetDate = DateTime.Now.AddDays(days);
            return session.Query<Event>()
                          .Where(e => e.StartsOn >= DateTime.Now && e.StartsOn <= targetDate)
                          .OrderBy(e => e.StartsOn)
                          .ToList();
        }
    }
}