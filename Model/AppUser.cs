using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oglasi.Model
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Ad> Ads { get; set; }
    }
}
