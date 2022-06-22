using System;

namespace EliteFlower.Exceptions
{
    class StartNeedScannersException : Exception
    {
        private static readonly string DefaultMessage = "Faltan escaners por seleccionar.";

        public StartNeedScannersException() : base(DefaultMessage)
        {
            this.HResult = 5301;
            this.Source = "NeedScanners";
        }
        public StartNeedScannersException(string message) : base(message)
        {
            this.HResult = 5310;
            this.Source = "NeedScanners";
        }
    }
}
