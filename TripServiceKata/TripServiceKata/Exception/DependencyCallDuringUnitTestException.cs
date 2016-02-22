using System;
using System.Runtime.Serialization;

namespace TripServiceKata.Exception
{
    [Serializable]
    public class DependencyCallDuringUnitTestException : System.Exception
    {
        public DependencyCallDuringUnitTestException() : base() { }

        public DependencyCallDuringUnitTestException(string message, System.Exception innerException) : base(message, innerException) { }

        public DependencyCallDuringUnitTestException(string message) : base(message) { }

        private DependencyCallDuringUnitTestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
