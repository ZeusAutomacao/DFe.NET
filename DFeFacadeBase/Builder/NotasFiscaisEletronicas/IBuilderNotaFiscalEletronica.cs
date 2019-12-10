using System;
using DFeFacadeBase.Builder.NotasFiscaisEletronicas.Identificacao;

namespace DFeFacadeBase.Builder.NotasFiscaisEletronicas
{
    public interface IBuilderNotaFiscalEletronica
    {
        IBuilderNotaFiscalEletronica BuildIdentificacaoNFe(
            DFeEstado cUf
            , string cNF
            , string natOp
            , IdicadorPagamentoNFe? indPag
            , int serie
            , long nNF
            , DateTime dEmi
            , DateTime dSaiEnt
            , DateTimeOffset dhEmi
            , DateTimeOffset? dhSaiEnt
            , TipoOperacaoNFe tpNF
        );
    }
}