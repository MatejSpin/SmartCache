using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCache.Service.Models.Exceptions
{
    public class EmailsServiceException : Exception
    {
        public EmailsServiceException() : base()
        {

        }

        public EmailsServiceException(string message) : base(message)
        {

        }
    }
}
