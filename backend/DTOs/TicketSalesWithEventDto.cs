namespace DTOs
{
    /// <summary>
    /// Represents a ticket sale with associated event details.
    /// </summary>
    public class TicketSalesWithEventDto
    {
        /// <summary>
        /// The unique identifier for the ticket.
        /// </summary>
        public required string TicketId { get; set; }

        /// <summary>
        /// The unique identifier for the user who purchased the ticket.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// The date and time when the ticket was purchased.
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// The price of the ticket in cents.
        /// </summary>
        public int PriceInCents { get; set; }

        /// <summary>
        /// The unique identifier for the associated event.
        /// </summary>
        public required string EventId { get; set; }

        /// <summary>
        /// The name of the associated event.
        /// </summary>
        public required string EventName { get; set; }

        /// <summary>
        /// The start date and time of the associated event.
        /// </summary>
        public DateTime EventStartDate { get; set; }

        /// <summary>
        /// The end date and time of the associated event.
        /// </summary>
        public DateTime EventEndDate { get; set; }
    }
}