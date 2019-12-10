using System.Collections.Generic;

namespace DFeFacadeBase.Builder.NotasFiscaisEletronicas
{
    public class BuilderLoteNFe
    {
        public BuilderLoteNFe()
        {
            _notasFiscaisEletronicas = new List<IBuilderNotaFiscalEletronica>();
        }

        private IList<IBuilderNotaFiscalEletronica> _notasFiscaisEletronicas;

        public BuilderLoteNFe AdicionarNotaFiscal(IBuilderNotaFiscalEletronica notaFiscalEletronica)
        {
            _notasFiscaisEletronicas.Add(notaFiscalEletronica);
            return this;
        }
    }
}