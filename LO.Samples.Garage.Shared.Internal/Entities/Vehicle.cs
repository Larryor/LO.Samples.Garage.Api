using LO.Samples.Garage.Shared.Internal.Enums;

namespace LO.Samples.Garage.Shared.Internal.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CarState State { get; set; }

        public Guid BranchId { get; set; }

        public Guid? CustomerId { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime LastModifiedAtUTC { get; set; }
    }
}
