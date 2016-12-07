using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ExecucaoException : Exception
    {
        private string message;

        public override string ToString()
        {
            return "Falha na rede internet: " + this.message;
        }

        public ExecucaoException(string msg)
        {
            this.message = msg;
            new Exception();
        }
    }
}
