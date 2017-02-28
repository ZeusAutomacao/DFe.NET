using System;
using System.Xml;
using CTeDLL;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Utils.Validacao;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTe.Utils.Extencoes
{
    public static class ExtinutCTe
    {
        public static void Assinar(this inutCTe inutCTe)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            inutCTe.Signature = AssinaturaDigital.Assina(inutCTe, inutCTe.infInut.Id,
                configuracaoServico.X509Certificate2);
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

        public static void ValidarShcema(this inutCTe inutCTe)
        {

            var xmlValidacao = inutCTe.ObterXmlString();

            switch (inutCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "inutCTe_v2.00.xsd");
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "inutCTe_v3.00.xsd");
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00 é 3.00");
            }
        }

        public static void SalvarXmlEmDisco(this inutCTe inutCTe)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\"+inutCTe.infInut.Id+ "-ped-inu.xml";

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