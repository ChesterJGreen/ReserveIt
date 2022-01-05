using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Utilities.Error
{
    public class ServiceBadRequestException : Exception
    {
        public ServiceBadRequestException() : this(string.Empty) { }

        public ServiceBadRequestException(string message) : this(message, new Object[0]) { }

        public ServiceBadRequestException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
        
    }
}
