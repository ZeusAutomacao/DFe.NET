using System;
using System.Xml;
using CTeDLL;
using CTeDLL.Classes.Servicos.Recepcao.Retorno;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Utils.Recepcao;
using CTeDLL.Utils.Validacao;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtConsReciCTe
    {
        public static void ValidarSchema(this consReciCTe consReciCTe)
        {
            var xmlValidacao = consReciCTe.ObterXmlString();

            switch (consReciCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consReciCTe_v2.00.xsd");
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consReciCTe_v3.00.xsd");
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

        public static void SalvarXmlEmDisco(this consReciCTe consReciCTe)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\"+ consReciCTe.nRec + @"-ped-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(consReciCTe, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consReciCTe consReciCTe)
        {
            var request = new XmlDocument();
            request.LoadXml(consReciCTe.ObterXmlString());

            return request;
        }
    }
}