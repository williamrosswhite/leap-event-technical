namespace Models
{
    public class Event
    {
        public virtual required string Id { get; set; }
        public virtual required string Name { get; set; }
        public virtual required DateTime StartsOn { get; set; }
        public virtual required DateTime EndsOn { get; set; }
        public virtual required string Location { get; set; }
    }
}