using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Status;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;
using DFe.DocumentosEletronicos.NFe.Servicos.EnviarNFe;
using DFe.DocumentosEletronicos.NFe.Servicos.StatusServicoNFe;
using DFe.Facade;

namespace DFe.DocumentosEletronicos.NFe.Facade
{
    public class NFeFacade : FacadeBase
    {
        private readonly NFeStatusServico _nfeStatusServico;
        private readonly NFeEnviar _nfeEnviar;

        public NFeFacade(NFeBaseConfig dfeConfig, CertificadoDigital certificadoDigital) : base(dfeConfig, certificadoDigital)
        {
            _nfeStatusServico = new NFeStatusServico(dfeConfig, certificadoDigital);
            _nfeEnviar = new NFeEnviar(dfeConfig, certificadoDigital);
        }

        public RetornoNFeAutorizacao Enviar(int idLote, List<Classes.Informacoes.NFe> nfes,
            IndicadorSincronizacao indSinc = IndicadorSincronizacao.Assincrono, bool compactarMensagem = false)
        {

            _nfeEnviar.Enviar(idLote, nfes, indSinc, compactarMensagem);

            return null;
        }

        public RetornoNfeStatusServico StatusConsulta()
        {
            return _nfeStatusServico.StatusServico();
        }
    }
}