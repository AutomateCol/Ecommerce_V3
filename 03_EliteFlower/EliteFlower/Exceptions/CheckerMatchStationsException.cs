using System;

namespace EliteFlower.Exceptions
{
    class CheckerMatchStationsException : Exception
    {
        private static readonly string DefaultMessage = "No coinciden las estaciones con el balanceo actual.";

        public CheckerMatchStationsException() : base(DefaultMessage)
        {
            this.HResult = 3101;
            this.Source = "NotMatchStations";
        }
        public CheckerMatchStationsException(string message) : base(message)
        {
            this.HResult = 3110;
            this.Source = "NotMatchStations";
        }
    }
}
