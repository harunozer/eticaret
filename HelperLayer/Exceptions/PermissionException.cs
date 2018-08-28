using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException(string Message) : base(Message)
        {
        }
        public PermissionException() : base("Yetkisiz İstek")
        {

        }
    }
}
