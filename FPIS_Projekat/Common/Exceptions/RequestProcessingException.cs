using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Common.Exceptions
{
    public class RequestProcessingException : Exception
    {
        public RequestProcessingException() : base()
        {
        }

        public RequestProcessingException(string Message) : base(Message)
        {
        }

        public override string Message => base.Message;
    }
}
