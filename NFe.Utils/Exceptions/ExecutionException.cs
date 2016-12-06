using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ExecutionException : Exception
    {
        private string message;

        public override string ToString()
        {
            return "Falha na rede internet: " + this.message;
        }

        public ExecutionException(string msg):base(msg)
        {
            this.message = msg;
        }
        public ExecutionException(string msg, Exception innerException):base(msg, innerException)
        {
            this.message = msg;
        }
    }
}
