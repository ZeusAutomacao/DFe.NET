using System;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.RecepcaoMDFe
{
    public class AntesDeEnviar : EventArgs
    {
        private enviMDFe _enviMdFe;

        public AntesDeEnviar(enviMDFe enviMdfe)
        {
            _enviMdFe = enviMdfe;
        }

        public enviMDFe enviMdFe
        {
            get { return _enviMdFe; }
            set { _enviMdFe = value; }
        }
    }
}