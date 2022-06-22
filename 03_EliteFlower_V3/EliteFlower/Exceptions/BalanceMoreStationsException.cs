using System;

namespace EliteFlower.Exceptions
{
    class BalanceMoreStationsException : Exception
    {
        private static readonly string DefaultMessage = "Habilite mas estaciones, el archivo cuenta con mas productos.";

        public BalanceMoreStationsException() : base(DefaultMessage)
        {
            this.HResult = 4101;
            this.Source = "MoreStations";
        }
        public BalanceMoreStationsException(string message) : base(message)
        {
            this.HResult = 4110;
            this.Source = "MoreStations";
        }
    }
}
