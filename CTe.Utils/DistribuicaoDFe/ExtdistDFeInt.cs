using CTe.Classes;
using CTe.Classes.Servicos.DistribuicaoDFe;
using CTe.Utils.Validacao;
using DFe.Utils;
using System;

namespace CTe.Utils.DistribuicaoDFe
{
    public static class ExtdistDFeInt
    {

        /// <summary>
        /// Recebe um objeto ExtdistDFeInt e devolve a string no formato XML
        /// </summary>
        /// <param name="pedDistDFeInt">Objeto do Tipo distDFeInt</param>
        /// <returns>string com XML no do objeto distDFeInt</returns>
        public static string ObterXmlString(this distDFeInt pedDistDFeInt)
        {
            return FuncoesXml.ClasseParaXmlString(pedDistDFeInt);
        }

        public static void ValidaSchema(this distDFeInt pedDistDFeInt, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlValidacao = pedDistDFeInt.ObterXmlString();

            if (pedDistDFeInt.versao.Equals("1.00"))
            {
                Validador.Valida(xmlValidacao, "distDFeInt_v1.00.xsd", configServico);
            }
            else if (pedDistDFeInt.versao.Equals("1.10"))
            {
                Validador.Valida(xmlValidacao, "distDFeInt_v1.10.xsd", configServico);
            }
            else
            {
                throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                    "a versão está inválida, somente é permitido " +
                                                    "versão 1.00 é 1.10");
            }
        }


        public static void SalvarXmlEmDisco(this distDFeInt pedDistDFeInt, string arquivoSalvar, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var arquivoXml = instanciaServico.DiretorioSalvarXml + arquivoSalvar;

            FuncoesXml.ClasseParaArquivoXml(pedDistDFeInt, arquivoXml);
        }

    }
}