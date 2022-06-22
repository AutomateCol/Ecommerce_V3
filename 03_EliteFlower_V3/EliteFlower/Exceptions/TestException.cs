using System;

namespace EliteFlower.Exceptions
{
    public class TestException : Exception
    {
        private static readonly string DefaultMessage = "This is a test of custom exception in C#.";

        public TestException() : base(DefaultMessage)
        {
            this.HResult = 1001;
            this.Source = "TestError";
        }
        public TestException(string message) : base(message)
        {
            this.HResult = 1001;
            this.Source = "TestError";
        }
    }
}
