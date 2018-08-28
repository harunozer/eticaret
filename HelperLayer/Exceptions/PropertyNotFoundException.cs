using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string Message) : base(Message)
        {
        }
        public PropertyNotFoundException() : base("Property bulunamadı.(Bug)")
        {

        }
    }
}
