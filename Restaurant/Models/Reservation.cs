using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required, Display(Name = "Number of Persons"), Range(1, 250, ErrorMessage = "Sorry, Persons should be between 1 and 250 persons")]
        public byte PersonsNumber { get; set; }

       
        [Required, Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Table Id")]
        public int TableId { get; set; }



        public DateTime ReserveDate { get; set; }

        [Required]
        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Table Table { get; set; }

    }
}
