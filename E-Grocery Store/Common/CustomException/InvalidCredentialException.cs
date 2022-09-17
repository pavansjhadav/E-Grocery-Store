using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Common.CustomException
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException(string message) : base(message)
        {
        }
    }
}
