using System;
using SMDFe.Classes.Servicos.Autorizacao;

namespace SMDFe.Servicos.RecepcaoMDFe
{
    public class AntesDeEnviar : EventArgs
    {
        private MDFeEnviMDFe _enviMdFe;

        public AntesDeEnviar(MDFeEnviMDFe enviMdfe)
        {
            _enviMdFe = enviMdfe;
        }

        public MDFeEnviMDFe enviMdFe
        {
            get { return _enviMdFe; }
            set { _enviMdFe = value; }
        }
    }
}