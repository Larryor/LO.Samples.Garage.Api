namespace LO.Samples.Garage.Shared.External.Responses
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime LastModifiedAtUTC { get; set; }
    }
}
