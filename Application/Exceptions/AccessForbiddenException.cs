using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class AccessForbiddenException : Exception
    {
        public AccessForbiddenException() : base()
        {

        }

        public AccessForbiddenException(string message) : base(message)
        {

        }

        public AccessForbiddenException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
