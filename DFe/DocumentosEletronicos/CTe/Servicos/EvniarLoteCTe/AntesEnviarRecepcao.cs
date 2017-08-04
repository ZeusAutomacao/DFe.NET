using System;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Recepcao;

namespace DFe.DocumentosEletronicos.CTe.Servicos.Recepcao
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