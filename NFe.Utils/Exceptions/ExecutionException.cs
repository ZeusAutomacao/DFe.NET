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

        public ExecutionException(string msg)
        {
            this.message = msg;
            new Exception();
        }
    }
}
