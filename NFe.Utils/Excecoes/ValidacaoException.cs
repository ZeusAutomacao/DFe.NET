using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ValidacaoException : Exception
    {
        private string message;
        public string Titulo = "Erros na validação: ";

        public override string ToString()
        {
            return this.message;
        }

        public ValidacaoException(string msg)
        {
            this.message = msg;
            new Exception();
        }
    }
}