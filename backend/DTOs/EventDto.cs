namespace DTOs
{
    public class EventDto
    {
        public required string EventId { get; set; }
        public required string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int? TicketCount { get; set; }
        public int? TotalSales { get; set; }
    }
}