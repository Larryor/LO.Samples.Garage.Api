namespace LO.Samples.Garage.Shared.Internal.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime LastModifiedAtUTC { get; set; }
    }
}
