namespace Models
{
    public class TicketSales
    {
        public virtual required string Id { get; set; }
        public virtual required string UserId { get; set; }
        public virtual required DateTime PurchaseDate { get; set; }
        public virtual required int PriceInCents { get; set; }

        // Navigation Property References Related Event
        public virtual required Event Event { get; set; }
    }
}