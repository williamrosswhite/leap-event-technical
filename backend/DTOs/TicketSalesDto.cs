namespace DTOs
{
    public class TicketSalesDto
    {
        public required string TicketId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PriceInCents { get; set; }
    }
}