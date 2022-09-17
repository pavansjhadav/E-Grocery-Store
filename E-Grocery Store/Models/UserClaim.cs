using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Models
{
    [NotMapped]
    public class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
