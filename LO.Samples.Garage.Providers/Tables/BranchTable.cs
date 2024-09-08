using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LO.Samples.Garage.Providers.Tables
{
    [Table("Branches")]
    [Index(nameof(Name), IsUnique = true)]
    public class BranchTable : BaseTable
    {
        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }
    }
}
