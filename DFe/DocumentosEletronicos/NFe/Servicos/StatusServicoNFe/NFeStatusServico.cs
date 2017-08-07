using System;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Status;

namespace DFe.DocumentosEletronicos.NFe.Servicos.StatusServicoNFe
{
    public class NFeStatusServico
    {
        private readonly DFeConfig _config;
        private readonly CertificadoDigital _certificadoDigital;

        public NFeStatusServico(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            _config = config;
            _certificadoDigital = certificadoDigital;
        }

        public RetornoNfeStatusServico StatusServico()
        {
            throw new NotFiniteNumberException();
        }
    }
}