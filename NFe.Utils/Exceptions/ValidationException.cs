using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ValidationException : Exception
    {
        private string message;

        public override string ToString()
        {
            return "Erros da validação: " + this.message;
        }

        public ValidationException(string msg):base(msg)
        {
            this.message = msg;
        }

        public ValidationException(string msg , Exception innerException):base(msg, innerException)
        {
            this.message = msg;
        }
    }
}
