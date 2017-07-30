using System;
using DFe.CTe.Classes.Servicos.Recepcao;

namespace DFe.CTe.Servicos.Recepcao
{
    public class AntesEnviarRecepcao : EventArgs
    {
        private readonly enviCTe _enviCTe;

        public AntesEnviarRecepcao(enviCTe enviCTe)
        {
            _enviCTe = enviCTe;
        }

        public enviCTe enviCTe
        {
            get { return _enviCTe; }
        }
    }
}