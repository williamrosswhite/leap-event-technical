using Models;

public interface IEventService
{
    /// <summary>
    /// Fetches events occurring within the next n days.
    /// </summary>
    /// <returns>A list of events within the next n days.</returns>
    IList<Event> GetEventsWithinDays(int days);
}