using System.Collections.Generic;
using DTOs;

public interface ITicketService
{
    /// <summary>
    /// Fetches the top 5 highest-selling events by ticket count.
    /// </summary>
    /// <returns>A list of the top 5 events by ticket count.</returns>
    IList<EventDto> GetTop5EventsByTicketCount();

    /// <summary>
    /// Fetches the top 5 highest-selling events by total sales amount.
    /// </summary>
    /// <returns>A list of the top 5 events by total sales amount.</returns>
    IList<EventDto> GetTop5EventsByTotalSales();

    /// <summary>
    /// Fetches a list of tickets associated with a specific event ID.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>A list of tickets for the specified event.</returns>
    IList<TicketSalesDto> GetTicketsByEventId(string eventId);

    /// <summary>
    /// Fetches the top N tickets, including event details, ordered by purchase date.
    /// </summary>
    /// <param name="limit">The maximum number of tickets to fetch.</param>
    /// <returns>A list of the top N tickets with event details.</returns>
    IList<TicketSalesWithEventDto> GetTopTickets(int limit = 100);
}