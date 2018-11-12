using System;
using System.IO;
using System.Text;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Constantes;
using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.ManipuladorDeXml;
using DFe.DocumentosEletronicos.ManipulaPasta;
using DFe.Ext;

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

            var dataEnvio = retEnviCte?.protCTe?.infProt?.dhRecbto ?? DateTime.Now;

            var caminhoXml = new ResolvePasta(config, dataEnvio).PastaRetornoEnviados();

            var protocolo = retEnviCte?.protCTe?.infProt?.nProt ?? "000000";

            var arquivoSalvar = Path.Combine(caminhoXml, new StringBuilder(protocolo).Append("-rec.xml").ToString());

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }

        public static bool IsAutorizado(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe.IsNotNull() && retConsSitCTe.protCTe.IsNotNull() && retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Autorizado; // manual cte 3.00 página 89
        }

        public static bool IsCancelada(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe.IsNotNull() && retConsSitCTe.protCTe.IsNotNull() && retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Cancelada; // manual cte 3.00 página 89
        }

        public static bool IsDenegada(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe.IsNotNull() && retConsSitCTe.protCTe.IsNotNull() && retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Denegada; // manual cte 3.00 página 89
        }

        public static bool IsRejeicao(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe.IsNotNull() && !IsAutorizado(retConsSitCTe) && !IsCancelada(retConsSitCTe) && !IsDenegada(retConsSitCTe);
        }

        public static bool IsRejeicao999(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe.IsNotNull() && !IsAutorizado(retConsSitCTe) && !IsCancelada(retConsSitCTe) && !IsDenegada(retConsSitCTe) && retConsSitCTe.cStat == 999;
        }
    }
}