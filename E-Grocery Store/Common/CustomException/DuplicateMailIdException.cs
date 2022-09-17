using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Common.CustomException
{
    public class DuplicateMailIdException : Exception
    {
        public DuplicateMailIdException(string message) : base(message)
        {
        }
    }
}
