using DFe.CertificadosDigitais;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Status;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Servicos.StatusServicoNFe;
using DFe.Facade;

namespace DFe.DocumentosEletronicos.NFe.Facade
{
    public class NFeFacade : FacadeBase
    {
        private readonly NFeStatusServico _nfeStatusServico;

        public NFeFacade(NFeBaseConfig dfeConfig, CertificadoDigital certificadoDigital) : base(dfeConfig, certificadoDigital)
        {
            _nfeStatusServico = new NFeStatusServico(dfeConfig, certificadoDigital);
        }

        public RetornoNfeStatusServico StatusConsulta()
        {
            return _nfeStatusServico.StatusServico();
        }
    }
}