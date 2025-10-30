using System;
using System.IO;
using System.Xml;
using DFe.Utils;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEnviMDFe
    {
        public static void Valida(this MDFeEnviMDFe enviMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (enviMDFe == null) throw new ArgumentException("Erro de assinatura, EnviMDFe esta null");

            var xmlMdfe = FuncoesXml.ClasseParaXmlString(enviMDFe);

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlMdfe, "enviMDFe_v1.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlMdfe, "enviMDFe_v3.00.xsd", config);
                    break;
            }

            enviMDFe.MDFe.Valida(config);
        }

        public static XmlDocument CriaXmlRequestWs(this MDFeEnviMDFe enviMDFe)
        {
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(enviMDFe.XmlString());

            return dadosEnvio;
        }

        public static XmlDocument CriaXmlRequestWs(this Informacoes.MDFe mdfe)
        {
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(mdfe.XmlString());

            return dadosEnvio;
        }

        public static string XmlString(this MDFeEnviMDFe enviMDFe)
        {
            var xmlString = FuncoesXml.ClasseParaXmlString(enviMDFe);

            return xmlString;
        }

        public static void SalvarXmlEmDisco(this MDFeEnviMDFe enviMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, enviMDFe.MDFe.Chave() + "-completo-mdfe.xml");

            FuncoesXml.ClasseParaArquivoXml(enviMDFe, arquivoSalvar);

            enviMDFe.MDFe.SalvarXmlEmDisco(cfgMdfe: config);
        }
    }
}