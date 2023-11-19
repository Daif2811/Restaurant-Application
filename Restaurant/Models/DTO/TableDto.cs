using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models.DTO
{
    public class TableDto
    {

        [Required, Display(Name = "Table Name")]
        public string TableName { get; set; }



        [Required, Display(Name = "Is Available ??")]
        public bool IsAvailable { get; set; }
    }

}
