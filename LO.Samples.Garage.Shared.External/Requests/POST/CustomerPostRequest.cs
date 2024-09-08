using System.ComponentModel.DataAnnotations;

namespace LO.Samples.Garage.Shared.External.Requests.POST
{
    public class CustomerPostRequest
    {
        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }
    }
}
