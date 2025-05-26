namespace DTOs
{
    public class TicketSalesWithEventDto
    {
        public required string TicketId { get; set; }
        public required string UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PriceInCents { get; set; }
        public required string EventId { get; set; }
        public required string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
    }
}