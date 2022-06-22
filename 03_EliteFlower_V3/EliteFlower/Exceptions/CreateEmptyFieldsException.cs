using System;

namespace EliteFlower.Exceptions
{
    public class CreateEmptyFieldsException : Exception
    {
        private static readonly string DefaultMessage = "Algunos campos estan vacios.";

        public CreateEmptyFieldsException() : base(DefaultMessage)
        {
            this.HResult = 1101;
            this.Source = "EmptyFields";
        }
        public CreateEmptyFieldsException(string message) : base(message)
        {
            this.HResult = 1110;
            this.Source = "EmptyFields";
        }
    }
}
