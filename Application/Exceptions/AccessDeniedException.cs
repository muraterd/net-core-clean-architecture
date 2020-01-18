using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : base()
        {

        }

        public AccessDeniedException(string message) : base(message)
        {

        }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
