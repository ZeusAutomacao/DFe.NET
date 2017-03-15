using System;
using CTeDLL.Classes.Servicos.Recepcao;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Utils.Validacao;
using DFe.Utils;

namespace CTeDLL.Utils.CTe
{
    public static class ExtEnvCte
    {
        public static void ValidaSchema(this enviCTe enviCTe)
        {
            var xmlValidacao = enviCTe.ObterXmlString();

            switch (enviCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "enviCTe_v2.00.xsd");
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "enviCTe_v3.00.xsd");
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Converte o objeto enviCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedEnvio"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto enviCTe</returns>
        public static string ObterXmlString(this enviCTe pedEnvio)
        {
            return FuncoesXml.ClasseParaXmlString(pedEnvio);
        }

        public static void SalvarXmlEmDisco(this enviCTe enviCte)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + enviCte.idLote + "-env-lot.xml";

            FuncoesXml.ClasseParaArquivoXml(enviCte, arquivoSalvar);
        }
    }
}