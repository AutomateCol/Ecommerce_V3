using System;

namespace EliteFlower.Exceptions
{
    class PLCUndervoltageException : Exception
    {
        private static readonly string DefaultMessage = "Se detecto una caida en la fase, el programa se cerrara y guardara la informacion actual.";

        public PLCUndervoltageException() : base(DefaultMessage)
        {
            this.HResult = 2101;
            this.Source = "UndervoltageLine";
        }
        public PLCUndervoltageException(string message) : base(message)
        {
            this.HResult = 2110;
            this.Source = "UndervoltageLine";
        }
    }
}
