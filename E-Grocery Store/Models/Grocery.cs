using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Models
{
    public class Grocery
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public GroceryCategory Category { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public User Vendor { get; set; }
        public int StatusId { get; set; }
        public GroceryStatus Status { get; set; }

    }
}
