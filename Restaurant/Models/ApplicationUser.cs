using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public string Address { get; set; }
    }
}
