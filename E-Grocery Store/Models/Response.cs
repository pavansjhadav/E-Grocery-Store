using System.ComponentModel.DataAnnotations.Schema;

namespace E_Grocery_Store.Models
{
    [NotMapped]
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
