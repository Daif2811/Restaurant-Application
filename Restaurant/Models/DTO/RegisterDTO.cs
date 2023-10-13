using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class RegisterDTO
    {

        [Required, Display(Name = "User Name"), MaxLength(50, ErrorMessage ="User Name should not be greater than 50 character. ")]
        public string UserName { get; set; }


        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, Display(Name = "First Name"), MaxLength(30, ErrorMessage = "First Name should not be greater than 30 character. ")]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name"), MaxLength(30, ErrorMessage = "Last Name should not be greater than 30 character. ")]
        public string LastName { get; set; }


        [Required, Display(Name ="Phone Number"),MaxLength(11, ErrorMessage ="Phone Number should be ( 11 ) number"), MinLength(11, ErrorMessage = "Phone Number should be ( 11 ) number")]
        public string PhoneNumber { get; set; }



        [Required,  MaxLength(100, ErrorMessage = "Address should not be greater than 100 character. ")]
        public string Address { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }



        [Required,Compare("Password"), Display(Name ="Confirm Password"),DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }



    }
}
