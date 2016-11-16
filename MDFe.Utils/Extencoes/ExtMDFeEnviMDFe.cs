using System;
using System.Xml;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Autorizacao;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeEnviMDFe
    {
        public static MDFeEnviMDFe Valida(this MDFeEnviMDFe enviMDFe)
        {
            if (enviMDFe == null) throw new ArgumentException("Erro de assinatura, EnviMDFe esta null");

            var xmlMdfe = FuncoesXml.ClasseParaXmlString(enviMDFe);

            Validador.Valida(xmlMdfe, "enviMDFe_v1.00.xsd");

            enviMDFe.MDFe.Valida();

            return enviMDFe;
        }

        public static XmlDocument XmlEnvio(this MDFeEnviMDFe enviMDFe)
        {
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(enviMDFe.ObterXmlString());

            return dadosEnvio;
        }

        public static string ObterXmlString(this MDFeEnviMDFe enviMDFe)
        {
            var xmlString = FuncoesXml.ClasseParaXmlString(enviMDFe);

            return xmlString;
        }

        public static void SalvarXmlEmDisco(this MDFeEnviMDFe enviMDFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var arquivoSalvar = MDFeConfiguracao.CaminhoSalvarXml += @"\" + enviMDFe.MDFe.Chave() + "-mdfe.xml";

            FuncoesXml.ClasseParaArquivoXml(enviMDFe, arquivoSalvar);
        }
    }
}