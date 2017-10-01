using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using DFe.CertificadosDigitais;
using DFe.Compressoes;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Excecoes;
using DFe.DocumentosEletronicos.NFe.Flags;
using DFe.DocumentosEletronicos.NFe.Servicos.Factory;
using DFe.DocumentosEletronicos.NFe.Wsdl;

namespace DFe.DocumentosEletronicos.NFe.Servicos.EnviarNFe
{
    public class NFeEnviar
    {
        private readonly NFeBaseConfig _dfeConfig;
        private readonly CertificadoDigital _certificadoDigital;

        public NFeEnviar(NFeBaseConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            _dfeConfig = dfeConfig;
            _certificadoDigital = certificadoDigital;
            _dfeConfig.ServicoNFe = ServicoNFe.NFeAutorizacao;
        }

        public RetornoNFeAutorizacao Enviar(int idLote, List<Classes.Informacoes.NFe> nfes, IndicadorSincronizacao indSinc, bool compactarMensagem)
        {
            var enviNFe3 = ClassesFactory.CriaEnviNFe3(_dfeConfig, idLote, indSinc, nfes);

            enviNFe3.ValidarSchema(_dfeConfig);

            enviNFe3.SalvarXmlEmDisco(_dfeConfig, idLote);

            var servico = WsdlFactory.CriarServicoAutorizacao(ServicoNFe.NFeAutorizacao, _dfeConfig, _certificadoDigital);

            if (_dfeConfig.VersaoNFeAutorizacao != VersaoServico.Versao400)
            {
                servico.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _dfeConfig.EstadoUf,
                    versaoDados = _dfeConfig.VersaoNFeAutorizacao.VersaoServicoParaString()
                };
            }

            var xmlEnvio = enviNFe3.ObterXmlString();
            if (_dfeConfig.EstadoUf == Estado.PR)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
                xmlEnvio = xmlEnvio.Replace("<NFe>", "<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            XmlNode retorno;
            try
            {
                if (compactarMensagem)
                {
                    var xmlCompactado = Convert.ToBase64String(Compressao.Zip(xmlEnvio));
                    retorno = servico.ExecuteZip(xmlCompactado);
                }
                else
                {
                    retorno = servico.Execute(dadosEnvio);
                }
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeAutorizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            retEnvio.SalvarXmlEmDisco(_dfeConfig, idLote);

            return new RetornoNFeAutorizacao(enviNFe3.ObterXmlString(), ExtretEnviNFe.ObterXmlString(retEnvio), retornoXmlString, retEnvio);
        }
    }
}