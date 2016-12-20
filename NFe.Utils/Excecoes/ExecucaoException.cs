using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ExecucaoException : Exception
    {
        private string message;
        public string Titulo = "Falha na rede internet: ";

        public override string ToString()
        {
            return this.message;
        }

        public ExecucaoException(string msg)
        {
            this.message = msg;
            new Exception();
        }
    }
}
