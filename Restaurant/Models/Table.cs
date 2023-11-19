using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Table
    {
        public int Id { get; set; }


        [Required , Display(Name ="Table Name")]
        public string TableName { get; set; }



        [Required , Display(Name ="Is Available ??")]
        public bool IsAvailable { get; set; }
    }
}
