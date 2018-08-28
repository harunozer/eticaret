using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Exceptions
{
    public class ExceptionDetail
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Path { get; set; }
    }
}
