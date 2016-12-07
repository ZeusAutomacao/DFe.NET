using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFe.Utils.Exceptions
{
    public class ValidacaoException : Exception
    {
        private string message;

        public override string ToString()
        {
            return "Erros na validação: " + this.message;
        }

        public ValidacaoException(string msg)
        {
            this.message = msg;
            new Exception();
        }
    }
}
