using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Oglasi.Model
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Message { get; set; }
    }
}
