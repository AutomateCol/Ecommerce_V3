using System;

namespace EliteFlower.Exceptions
{
    class FormCurrentExecutionException : Exception
    {
        private static readonly string DefaultMessage = "Se esta ejecutando un proceso en el programa, pararlo antes de cerrar.";

        public FormCurrentExecutionException() : base(DefaultMessage)
        {
            this.HResult = 6101;
            this.Source = "ProcessInExecution";
        }
        public FormCurrentExecutionException(string message) : base(message)
        {
            this.HResult = 6110;
            this.Source = "ProcessInExecution";
        }
    }
}
