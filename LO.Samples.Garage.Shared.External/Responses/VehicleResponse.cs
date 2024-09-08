using LO.Samples.Garage.Shared.External.Enums;
using System.Text.Json.Serialization;

namespace LO.Samples.Garage.Shared.External.Responses
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CarStateResponse State { get; set; }

        public Guid BranchId { get; set; }

        public Guid? CustomerId { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime LastModifiedAtUTC { get; set; }
    }
}
