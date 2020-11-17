using System;

namespace WaktuSolat_API.Exceptions
{
    public class WaktuSolatException : Exception
    {
        public WaktuSolatException() {}
        public WaktuSolatException(string message)
        {
            Message = message;
        }
        public override string Message { get; }
    }
}