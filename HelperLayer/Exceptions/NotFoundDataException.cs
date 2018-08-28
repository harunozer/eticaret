using System;

namespace HelperLayer.Exceptions
{
    public class NotFoundDataException : Exception
    {
        public NotFoundDataException(string Message) : base(Message)
        {
        }
        public NotFoundDataException() : base("Kayıt bulunamadı")
        {

        }
    }
}