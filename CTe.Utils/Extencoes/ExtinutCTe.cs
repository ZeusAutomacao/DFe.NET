using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Inutilizacao;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Validacao;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTe.Utils.Extencoes
{
    public static class ExtinutCTe
    {
        public static void Assinar(this inutCTe inutCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            inutCTe.Signature = AssinaturaDigital.Assina(inutCTe, inutCTe.infInut.Id,
                configServico.X509Certificate2);
        }


        /// <summary>
        ///     Converte o objeto inutCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedInutilizacao"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto inutCTe</returns>
        public static string ObterXmlString(this inutCTe pedInutilizacao)
        {
            return FuncoesXml.ClasseParaXmlString(pedInutilizacao);
        }

        public static void ValidarSchema(this inutCTe inutCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlValidacao = inutCTe.ObterXmlString();

            switch (inutCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "inutCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "inutCTe_v3.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00 é 3.00");
            }
        }

        public static void SalvarXmlEmDisco(this inutCTe inutCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, inutCTe.infInut.Id + "-ped-inu.xml");

            FuncoesXml.ClasseParaArquivoXml(inutCTe, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this inutCTe inutCTe)
        {
            var request = new XmlDocument();
            request.LoadXml(inutCTe.ObterXmlString());

            return request;
        }
    }
}