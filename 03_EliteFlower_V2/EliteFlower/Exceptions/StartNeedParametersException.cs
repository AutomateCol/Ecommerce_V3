using System;

namespace EliteFlower.Exceptions
{
    class StartNeedParametersException : Exception
    {
        private static readonly string DefaultMessage = "No se han seleccionado algunos parametros.";

        public StartNeedParametersException() : base(DefaultMessage)
        {
            this.HResult = 5401;
            this.Source = "NeedParameters";
        }
        public StartNeedParametersException(string message) : base(message)
        {
            this.HResult = 5410;
            this.Source = "NeedParameters";
        }
    }
}
