using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
  private readonly ITicketService _ticketService;

  public TicketsController(ITicketService ticketService)
  {
    _ticketService = ticketService;
  }

  /// <summary>
  /// Retrieves a list of tickets for a given event ID.
  /// </summary>
  /// <param name="eventId">The ID of the event.</param>
  /// <returns>A list of tickets for the specified event.</returns>
  [HttpGet("{eventId}")]
  public IActionResult GetTicketsByEventId(string eventId)
  {
    var tickets = _ticketService.GetTicketsByEventId(eventId);
    return Ok(tickets);
  }

  /// <summary>
  /// Retrieves the top 5 events by ticket count.
  /// </summary>
  /// <returns>A list of the top 5 events by ticket count.</returns>
  [HttpGet("top-sales")]
  public IActionResult GetTop5EventsByTicketCount()
  {
    var topEvents = _ticketService.GetTop5EventsByTicketCount();
    return Ok(topEvents);
  }

  /// <summary>
  /// Retrieves the top 5 events by total revenue.
  /// </summary>
  /// <returns>A list of the top 5 events by total revenue.</returns>
  [HttpGet("top-revenue")]
  public IActionResult GetTop5EventsByRevenue()
  {
    var topEvents = _ticketService.GetTop5EventsByTotalSales();
    return Ok(topEvents);
  }
}