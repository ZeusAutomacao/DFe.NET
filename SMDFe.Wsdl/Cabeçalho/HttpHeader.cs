using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SMDFe.Wsdl.Cabeçalho
{
    public static class HttpHeader
    {
        #region Constantes

        private static string _action = "SOAPAction";
        private static string _contettype = "application/soap+xml; charset=\"UTF-8\"";
        private static string _accept = "*/*";
        private static string _method = "POST";
        private static string _agent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
        #endregion

        #region Propriedades

        public static string ACTION
        {
            get { return _action; }
        }

        public static string CONTETTYPE
        {
            get { return _contettype; }
        }

        public static string ACCEPT
        {
            get { return _accept; }
        }

        public static string METHOD
        {
            get { return _method; }
        }

        public static string AGENT
        {
            get { return _agent; }
        }
        #endregion

    }
}
