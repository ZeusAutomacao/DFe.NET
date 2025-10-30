using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Validacao;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtconsSitCTe
    {

        public static void ValidarSchema(this consSitCTe consSitCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlValidacao = consSitCTe.ObterXmlString();

            switch (consSitCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consSitCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consSitCTe_v3.00.xsd", configServico);
                    break;
                case versao.ve400:
                    Validador.Valida(xmlValidacao, "consSitCTe_v4.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Converte o objeto consSitCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedConsulta"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto consSitCTe</returns>
        public static string ObterXmlString(this consSitCTe pedConsulta)
        {
            return FuncoesXml.ClasseParaXmlString(pedConsulta);
        }

        public static void SalvarXmlEmDisco(this consSitCTe statuServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, "-ped-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(statuServCte, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consSitCTe consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.ObterXmlString());

            return request;
        }
    }
}