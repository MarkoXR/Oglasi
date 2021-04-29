using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oglasi.Model
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(1000, MinimumLength = 5)]
        public string ImagePath { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{8,11}$")]
        public string ContactNumber { get; set; }
        [ForeignKey(nameof(County))]
        public int CountyId { get; set; }
        public virtual County County { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual AdCategory Category { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string Address { get; set; }

    }
}
