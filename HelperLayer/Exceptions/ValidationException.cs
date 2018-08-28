using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string Mesaj) : base(Mesaj)
        {

        }
    }
}
