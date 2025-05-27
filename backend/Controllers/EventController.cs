using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(
        IEventService eventService,
        ILogger<EventsController> logger)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves a list of events that occurred within the specified number of days.
    /// </summary>
    /// <param name="days">Can be 30, 60, or 180 days.</param>
    /// <returns>A list of events.</returns>
    [HttpGet]
    public IActionResult GetEvents([FromQuery] int days)
    {
        try
        {
            if (days != 30 && days != 60 && days != 180)
            {
                _logger.LogWarning("Invalid value for 'days': {Days}", days);
                return BadRequest(new { Error = "Invalid value for 'days'. Allowed values are 30, 60, or 180." });
            }

            var events = _eventService.GetEventsWithinDays(days);

            if (events == null || !events.Any())
            {
                _logger.LogInformation("No events found for the specified time range: {Days} days", days);
                return NotFound(new { Message = "No events found for the specified time range." });
            }

            _logger.LogInformation("Successfully fetched {Count} events for the past {Days} days", events.Count(), days);
            return Ok(events);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Argument exception occurred while fetching events for {Days} days", days);
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching events for {Days} days", days);
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}