using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class ReservationDto
    {
        [Required, Display(Name = "Number of Persons"), Range(1, 250, ErrorMessage = "Sorry, Persons should be between 1 and 250 Persons")]
        public byte PersonsNumber { get; set; }



        [Required, Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }



        [Required, Display(Name = "Table Id")]
        public int TableId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ReserveDate { get; set; }
    }

}
