using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models.DTO
{
    public class TableUpdateDto
    {

        [Display(Name = "Table Name")]
        public string TableName { get; set; }



        [Display(Name = "Is Available ??")]
        public bool IsAvailable { get; set; }
    }

}
