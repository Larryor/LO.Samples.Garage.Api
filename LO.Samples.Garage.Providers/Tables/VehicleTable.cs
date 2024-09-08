using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LO.Samples.Garage.Providers.Enums;
using Microsoft.EntityFrameworkCore;

namespace LO.Samples.Garage.Providers.Tables
{
    [Table("Vehicles")]
    [Index(nameof(Name), IsUnique = true)]
    public class VehicleTable : BaseTable
    {
        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }

        /// <summary>
        /// If the car is in Rented state then it must have a customer/id assigned to it
        /// </summary>
        [Required]
        public CarStateDto State { get; set; }

        public BranchTable Branch { get; set; }
        public Guid BranchId { get; set; }

        /// <summary>
        /// If this value is not null then it means the car is rented, but this should always be consistent with the "State" above
        /// </summary>
        public CustomerTable? Customer { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
