using System;
using MDFe.Classes.Servicos.Autorizacao;

namespace MDFe.Servicos.RecepcaoMDFe
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