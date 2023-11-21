using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models.DTO
{
    public class ReservationUpdateDto
    {
        [Display(Name = "Number of Persons"), Range(1, 250, ErrorMessage = "Sorry, Persons should be between 1 and 250 Persons")]
        public byte? PersonsNumber { get; set; }



        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }



        [Display(Name = "Table Id")]
        public int? TableId { get; set; }

        [ DataType(DataType.DateTime)]
        public DateTime? ReserveDate { get; set; }
    }
}
