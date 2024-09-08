using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LO.Samples.Garage.Providers.Tables
{
    public class BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAtUTC { get; set; }

        [Required]
        public DateTime LastModifiedAtUTC { get; set; }
    }
}
