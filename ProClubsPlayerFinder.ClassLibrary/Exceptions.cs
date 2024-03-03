using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProClubsPlayerFinder.ClassLibrary
{
    internal class Exceptions
    {
    }
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public IDictionary<string, object> AdditionalData { get; }

        public ApiException(int statusCode, string errorMessage, IDictionary<string, object>? additionalData = null) : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            AdditionalData = additionalData;
        }
    }
}