using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(
      ITicketService ticketService,
      ILogger<TicketsController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves a list of tickets for a given event ID.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>A list of tickets for the specified event.</returns>
    [HttpGet("{eventId}")]
    public IActionResult GetTicketsByEventId(string eventId)
    {
        try
        {
            var tickets = _ticketService.GetTicketsByEventId(eventId);
            
            if (tickets == null || !tickets.Any())
            {
              _logger.LogInformation("No tickets found for event ID: {EventId}", eventId);
              return NotFound(new { Message = "No tickets found for the specified event." });
            }

            _logger.LogInformation("Successfully retrieved {Count} tickets for event ID: {EventId}", tickets.Count(), eventId);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving tickets for event ID: {EventId}", eventId);
            return StatusCode(500, new { Error = "An error occurred while processing your request." });
        }
    }

    /// <summary>
    /// Retrieves the top 5 events by ticket count.
    /// </summary>
    /// <returns>A list of the top 5 events by ticket count.</returns>
    [HttpGet("top-sales")]
    public IActionResult GetTop5EventsByTicketCount()
    {
        try
        {
            var topEvents = _ticketService.GetTop5EventsByTicketCount();

            if (topEvents == null || !topEvents.Any())
            {
              _logger.LogInformation("No events found for top 5 by ticket count.");
              return NotFound(new { Message = "No events found for top 5 by ticket count." });
            }

            _logger.LogInformation("Successfully retrieved top 5 events by ticket count.");
            return Ok(topEvents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the top 5 events by ticket count.");
            return StatusCode(500, new { Error = "An error occurred while processing your request." });
        }
    }

    /// <summary>
    /// Retrieves the top 5 events by total revenue.
    /// </summary>
    /// <returns>A list of the top 5 events by total revenue.</returns>
    [HttpGet("top-revenue")]
    public IActionResult GetTop5EventsByRevenue()
    {
        try
        {
            var topEvents = _ticketService.GetTop5EventsByTotalSales();
            
            if (topEvents == null || !topEvents.Any())
            {
              _logger.LogInformation("No events found for top 5 by total revenue.");
              return NotFound(new { Message = "No events found for top 5 by total revenue." });
            }

            _logger.LogInformation("Successfully retrieved top 5 events by total revenue.");
            return Ok(topEvents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the top 5 events by total revenue.");
            return StatusCode(500, new { Error = "An error occurred while processing your request." });
        }
    }
}