using System.ComponentModel.DataAnnotations;

namespace LO.Samples.Garage.Shared.External.Requests.POST
{
    public class VehiclePostRequest
    {
        [Required]
        [MaxLength]
        public string Name { get; set; }
    }
}
