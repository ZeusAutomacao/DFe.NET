using System;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.ManipuladorDeXml;
using DFe.DocumentosEletronicos.ManipulaPasta;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Extensoes
{
    public static class extretCteOS
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retEnviCte
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retEnviCte</returns>
        public static retCTeOS CarregarDeXmlString(this retCTeOS retEnviCte, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retCTeOS>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retEnviCte para uma string no formato XML
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnviCte</returns>
        public static string ObterXmlString(this retCTeOS retEnviCte)
        {
            return FuncoesXml.ClasseParaXmlString(retEnviCte);
        }

        public static void SalvarXmlEmDisco(this retCTeOS retEnviCte, DFeConfig config)
        {
            if (config.NaoSalvarXml()) return;

            var caminhoXml = new ResolvePasta(config, retEnviCte?.protCTe?.infProt?.dhRecbto ?? DateTime.Now).PastaRetornoEnviados();

            var arquivoSalvar = caminhoXml + @"\" + retEnviCte?.protCTe?.infProt?.nProt + "-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }
    }
}