using System;
using System.Net;
using System.Xml;
using DFe.CertificadosDigitais;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Status;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Excecoes;
using DFe.DocumentosEletronicos.NFe.Flags;
using DFe.DocumentosEletronicos.NFe.Servicos.Factory;
using DFe.DocumentosEletronicos.NFe.Wsdl;

namespace DFe.DocumentosEletronicos.NFe.Servicos.StatusServicoNFe
{
    public class NFeStatusServico
    {
        private readonly NFeBaseConfig _config;
        private readonly CertificadoDigital _certificadoDigital;

        public NFeStatusServico(NFeBaseConfig config, CertificadoDigital certificadoDigital)
        {
            _config = config;
            _certificadoDigital = certificadoDigital;
        }

        public RetornoNfeStatusServico StatusServico()
        {
            var consStatServ = ClassesFactory.CriaConsStatServ(_config);

            consStatServ.ValidarSchema(_config);

            consStatServ.SalvarXmlEmDisco(_config);

            var servico = WsdlFactory.CriarServico(ServicoNFe.NfeStatusServico, _config, _certificadoDigital);

            if (_config.VersaoNfeStatusServico != VersaoServico.Versao400)
            {
                servico.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _config.EstadoUf,
                    versaoDados = _config.VersaoNfeStatusServico.VersaoServicoParaString()
                };
            }

            XmlNode retorno;
            try
            {
                var xmlStatus = consStatServ.ObterXmlString();

                var dadosStatus = new XmlDocument();
                dadosStatus.LoadXml(xmlStatus);

                retorno = servico.Execute(dadosStatus);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeStatusServico, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsStatServ = new retConsStatServ().CarregarDeXmlString(retornoXmlString);

            retConsStatServ.SalvarXmlEmDisco(_config);

            return new RetornoNfeStatusServico(consStatServ.ObterXmlString(), ExtretConsStatServ.ObterXmlString(retConsStatServ),
                retornoXmlString, retConsStatServ);

        }
    }
}