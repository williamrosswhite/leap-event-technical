using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public IActionResult GetEvents([FromQuery] int days)
    {
        if (days != 30 && days != 60 && days != 180)
        {
            return BadRequest("Invalid value for 'days'. Allowed values are 30, 60, or 180.");
        }

        var events = _eventService.GetEventsWithinDays(days);
        return Ok(events);
    }
}