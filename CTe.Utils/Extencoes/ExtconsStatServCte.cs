using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Status;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Validacao;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtconsStatServCte
    {
        public static void ValidarSchema(this consStatServCte consStatServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlValidacao = consStatServCte.ObterXmlString();

            switch (consStatServCte.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v3.00.xsd", configServico);
                    break;
                case versao.ve400:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v4.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Recebe um objeto consStatServ e devolve a string no formato XML
        /// </summary>
        /// <param name="pedStatus">Objeto do tipo consStatServ</param>
        /// <returns>string com XML no do objeto consStatServ</returns>
        public static string ObterXmlString(this consStatServCte pedStatus)
        {
            return FuncoesXml.ClasseParaXmlString(pedStatus);
        }

        public static void SalvarXmlEmDisco(this consStatServCte statuServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, DateTime.Now.ParaDataHoraString() + "-ped-sta.xml");

            FuncoesXml.ClasseParaArquivoXml(statuServCte, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consStatServCte consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.ObterXmlString());

            return request;
        }
    }

    public static class ExtconsStatServCTe
    {
        public static void ValidarSchema(this consStatServCTe consStatServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlValidacao = consStatServCte.ObterXmlString();

            switch (consStatServCte.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v3.00.xsd", configServico);
                    break;
                case versao.ve400:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v4.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Recebe um objeto consStatServ e devolve a string no formato XML
        /// </summary>
        /// <param name="pedStatus">Objeto do tipo consStatServ</param>
        /// <returns>string com XML no do objeto consStatServ</returns>
        public static string ObterXmlString(this consStatServCTe pedStatus)
        {
            return FuncoesXml.ClasseParaXmlString(pedStatus);
        }

        public static void SalvarXmlEmDisco(this consStatServCTe statuServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, DateTime.Now.ParaDataHoraString() + "-ped-sta.xml");

            FuncoesXml.ClasseParaArquivoXml(statuServCte, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consStatServCTe consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.ObterXmlString());

            return request;
        }
    }
}