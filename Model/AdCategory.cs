using System.ComponentModel.DataAnnotations;

namespace Oglasi.Model
{
    public class AdCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
