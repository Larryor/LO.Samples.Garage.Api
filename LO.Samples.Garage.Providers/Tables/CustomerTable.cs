using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LO.Samples.Garage.Providers.Tables
{
    [Table("Customers")]
    [Index(nameof(Name), IsUnique = true)]
    public class CustomerTable : BaseTable
    {
        [Required]
        [MaxLength(2048)]
        
        public string Name { get; set; }
    }
}
