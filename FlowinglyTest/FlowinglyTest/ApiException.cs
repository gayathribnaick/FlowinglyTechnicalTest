using System.Xml.Linq;

namespace FlowinglyTest
{
    public class ApiException : Exception
    {
        public ApiException() { }
        public ApiException(string message) : base(message) { }

        public override string? StackTrace => string.Empty;
    }
}
