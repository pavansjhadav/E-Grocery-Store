using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Grocery_Store.Common.CustomException
{
    public class ResponseException : Exception
    {
        public ResponseException(string message) : base(message)
        {
        }
    }
}
