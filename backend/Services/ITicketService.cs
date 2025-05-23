using System.Collections.Generic;
using Models;

public interface ITicketService
{
    /// <summary>
    /// Fetches the top 5 highest-selling events by ticket count.
    /// </summary>
    /// <returns>A list of the top 5 events by ticket count.</returns>
    IList<Event> GetTop5EventsByTicketCount();

    /// <summary>
    /// Fetches the top 5 highest-selling events by total sales amount.
    /// </summary>
    /// <returns>A list of the top 5 events by total sales amount.</returns>
    IList<Event> GetTop5EventsByTotalSales();
}