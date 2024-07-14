namespace FlowinglyTest.Exceptions
{
    public class APIExceptionBase : Exception
    {
        public APIExceptionBase(string message) : base(message) { }

        public override string? StackTrace => string.Empty; // Hide stack trace to display the error without trace.
    }
}
