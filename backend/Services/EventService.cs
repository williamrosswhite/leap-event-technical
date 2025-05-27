using Models;
using NHibernate;

public class EventService : IEventService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly ILogger<EventService> _logger;

    public EventService(
        ISessionFactory sessionFactory,
        ILogger<EventService> logger)
    {
        _sessionFactory = sessionFactory ??
            throw new ArgumentNullException(nameof(sessionFactory));
        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public IList<Event> GetEventsWithinDays(int days)
    {
        try
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var targetDate = DateTime.Now.AddDays(days);

                _logger.LogInformation("Fetching events within the next {Days} days. Target date: {TargetDate}", days, targetDate);

                var events = session.Query<Event>()
                                    .Where(e => e.StartsOn >= DateTime.Now && e.StartsOn <= targetDate)
                                    .OrderBy(e => e.StartsOn)
                                    .ToList();

                _logger.LogInformation("Successfully fetched {EventCount} events.", events.Count);

                return events;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching events within the next {Days} days.", days);
            throw;
        }
    }
}