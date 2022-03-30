using System;

namespace EliteFlower.Exceptions
{
    public class CreateRelationalFieldsException : Exception
    {
        private static readonly string DefaultMessage = "El campo se encuentra vinculado con otra base de datos.";

        public CreateRelationalFieldsException() : base(DefaultMessage)
        {
            this.HResult = 1301;
            this.Source = "RelationalFields";
        }
        public CreateRelationalFieldsException(string message) : base(message)
        {
            this.HResult = 1310;
            this.Source = "RelationalFields";
        }
    }
}
