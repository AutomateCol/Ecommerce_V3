using System;

namespace EliteFlower.Exceptions
{
    class StartNoBandException : Exception
    {
        private static readonly string DefaultMessage = "No se ha seleccionado ninguna banda.";

        public StartNoBandException() : base(DefaultMessage)
        {
            this.HResult = 5201;
            this.Source = "NoBand";
        }
        public StartNoBandException(string message) : base(message)
        {
            this.HResult = 5210;
            this.Source = "NoBand";
        }
    }
}
