using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAppTriathlon.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public string Surname { get; set; }
    }
}
