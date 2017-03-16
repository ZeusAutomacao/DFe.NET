using System;
using System.Xml;
using CTeDLL;
using CTeDLL.Classes.Servicos.Status;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Utils.Validacao;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtconsStatServCte
    {
        public static void ValidarSchema(this consStatServCte consStatServCte)
        {
            var xmlValidacao = consStatServCte.ObterXmlString();

            switch (consStatServCte.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v2.00.xsd");
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "consStatServCTe_v3.00.xsd");
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

        public static void SalvarXmlEmDisco(this consStatServCte statuServCte)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + DateTime.Now.ParaDataHoraString() + "-ped-sta.xml";

            FuncoesXml.ClasseParaArquivoXml(statuServCte, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consStatServCte consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.ObterXmlString());

            return request;
        }
    }
}