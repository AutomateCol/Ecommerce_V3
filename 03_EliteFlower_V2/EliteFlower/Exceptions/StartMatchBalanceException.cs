using System;

namespace EliteFlower.Exceptions
{
    class StartMatchBalanceException : Exception
    {
        private static readonly string DefaultMessage = "El balanceo y las estaciones no coinciden.";

        public StartMatchBalanceException() : base(DefaultMessage)
        {
            this.HResult = 5501;
            this.Source = "NotMatchBalance";
        }
        public StartMatchBalanceException(string message) : base(message)
        {
            this.HResult = 5510;
            this.Source = "NotMatchBalance";
        }
    }
}
