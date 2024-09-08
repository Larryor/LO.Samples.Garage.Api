namespace LO.Samples.Garage.Shared.Internal.Entities
{
    public class Branch
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime LastModifiedAtUTC { get; set; }
    }
}
