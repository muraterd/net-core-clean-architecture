using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class DuplicateResultException : Exception
    {
        public DuplicateResultException() : base()
        {

        }

        public DuplicateResultException(string message) : base(message)
        {

        }

        public DuplicateResultException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
