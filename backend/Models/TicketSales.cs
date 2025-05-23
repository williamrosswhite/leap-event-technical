public class TicketSales
{
    public virtual string Id { get; set; }
    public virtual string EventId { get; set; }
    public virtual string UserId { get; set; }
    public virtual DateTime PurchaseDate { get; set; } 
    public virtual int PriceInCents { get; set; }

    // Navigation Property References Related Event
    public virtual Event Event { get; set; }
}