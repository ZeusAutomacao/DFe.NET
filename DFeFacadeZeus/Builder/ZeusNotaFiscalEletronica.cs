using System;
using DFe.Classes.Entidades;
using DFeFacadeBase;
using DFeFacadeBase.Builder.NotasFiscaisEletronicas;
using DFeFacadeBase.Builder.NotasFiscaisEletronicas.Identificacao;
using NFe.Classes.Informacoes.Identificacao;

namespace DFeFacadeZeus.Builder
{
    public class ZeusNotaFiscalEletronica : IBuilderNotaFiscalEletronica
    {
        private ide Ide { get; }

        public ZeusNotaFiscalEletronica()
        {
            Ide = new ide();
        }

        public IBuilderNotaFiscalEletronica BuildIdentificacaoNFe(DFeEstado cUf, string cNF, string natOp,
            IdicadorPagamentoNFe? indPag, int serie, long nNF, DateTime dEmi, DateTime dSaiEnt, DateTimeOffset dhEmi,
            DateTimeOffset? dhSaiEnt, TipoOperacaoNFe tpNF)
        {
            Ide.cUF = (Estado) cUf;

            return this;
        }
    }
}