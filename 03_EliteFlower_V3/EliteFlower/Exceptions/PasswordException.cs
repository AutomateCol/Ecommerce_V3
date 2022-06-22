using System;

namespace EliteFlower.Exceptions
{
    public class PasswordException : Exception
    {
        private static readonly string DefaultMessage = "La contrasena no es correcta.";

        public PasswordException() : base(DefaultMessage)
        {
            this.HResult = 6101;
            this.Source = "NoPassword";
        }
        public PasswordException(string message) : base(message)
        {
            this.HResult = 6110;
            this.Source = "NoPassword";
        }
    }
}
