using System;

namespace Common.Exceptions
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
