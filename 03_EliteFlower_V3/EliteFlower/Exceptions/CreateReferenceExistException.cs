using System;

namespace EliteFlower.Exceptions
{
    public class CreateReferenceExistException : Exception
    {
        private static readonly string DefaultMessage = "La referencia ya existe.";

        public CreateReferenceExistException() : base(DefaultMessage)
        {
            this.HResult = 1201;
            this.Source = "ReferenceExist";
        }
        public CreateReferenceExistException(string message) : base(message)
        {
            this.HResult = 1210;
            this.Source = "ReferenceExist";
        }
    }
}
