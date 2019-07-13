using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base()
        {
        }

        public DatabaseException(string Message) : base(Message)
        {
        }

        public override string Message => base.Message;
    }
}
