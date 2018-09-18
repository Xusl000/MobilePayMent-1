using System;

namespace PayCore.Exceptions
{
    public class GatewayException : Exception
    {
        public GatewayException(string message)
            : base(message)
        {
        }
    }
}
