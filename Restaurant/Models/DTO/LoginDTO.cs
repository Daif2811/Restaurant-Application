using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class LoginDTO
    {

        [Required, Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }

}
