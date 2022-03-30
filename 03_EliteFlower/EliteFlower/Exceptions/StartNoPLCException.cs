using System;

namespace EliteFlower.Exceptions
{
    class StartNoPLCException : Exception
    {
        private static readonly string DefaultMessage = "PLCs no estan conectados.";

        public StartNoPLCException() : base(DefaultMessage)
        {
            this.HResult = 5101;
            this.Source = "NoPLC";
        }
        public StartNoPLCException(string message) : base(message)
        {
            this.HResult = 5110;
            this.Source = "NoPLC";
        }
    }
}
