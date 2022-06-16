﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class ServiceValidationException : Exception
    {
        public ServiceValidationException()
            : base()
        { }

        public ServiceValidationException(string message)
            : base(message)
        {
            // Logging
        }

        public ServiceValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
