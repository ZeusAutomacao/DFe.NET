using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Validacao;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtConsReciCTe
    {
        public static void ValidarSchema(this consReciCTe consReciCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var xmlValidacao = consReciCTe.ObterXmlString();

            switch (consReciCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consReciCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consReciCTe_v3.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Converte o objeto retconsReciCTe para uma string no formato XML
        /// </summary>
        /// <param name="consReciCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retconsReciCTe</returns>
        public static string ObterXmlString(this consReciCTe consReciCTe)
        {
            return FuncoesXml.ClasseParaXmlString(consReciCTe);
        }

        public static void SalvarXmlEmDisco(this consReciCTe consReciCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, consReciCTe.nRec + "-ped-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(consReciCTe, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consReciCTe consReciCTe)
        {
            var request = new XmlDocument();
            request.LoadXml(consReciCTe.ObterXmlString());

            return request;
        }


        // Salvar Retorno de Envio de Recibo
        public static void SalvarXmlEmDisco(this retConsReciCTe retConsReciCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retConsReciCTe.nRec + "-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsReciCTe, arquivoSalvar);
        }
    }
}