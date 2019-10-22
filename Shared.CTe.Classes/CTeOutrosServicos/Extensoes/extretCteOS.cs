using System;
using System.IO;
using System.Text;
using CTe.Classes;
using CTe.CTeOSDocumento.CTe.Constantes;
using CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao;
using DFe.Utils;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Extensoes
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

        public static void SalvarXmlEmDisco(this retCTeOS retEnviCte, ConfiguracaoServico config)
        {
            if (config.NaoSalvarXml()) return;

            var dataEnvio = DateTimeOffset.Now;

            if (retEnviCte != null && retEnviCte.protCTe != null 
                && retEnviCte.protCTe.infProt != null
                && retEnviCte.protCTe.infProt.dhRecbto != null)
            {
                dataEnvio = retEnviCte.protCTe.infProt.dhRecbto;
            }

            var caminhoXml = config.DiretorioSalvarXml;

            var protocolo = "000000";


            if (retEnviCte != null && retEnviCte.protCTe != null
                                   && retEnviCte.protCTe.infProt != null
                                   && retEnviCte.protCTe.infProt.nProt != null)
            {
                protocolo = retEnviCte.protCTe.infProt.nProt;
            }

            var arquivoSalvar = Path.Combine(caminhoXml, new StringBuilder(protocolo).Append("-rec.xml").ToString());

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }

        public static bool IsAutorizado(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe != null && retConsSitCTe.protCTe != null && retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Autorizado; // manual cte 3.00 página 89
        }

        public static bool IsCancelada(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe != null && retConsSitCTe.protCTe != null && retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Cancelada; // manual cte 3.00 página 89
        }

        public static bool IsDenegada(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe != null && retConsSitCTe.protCTe != null && (retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Denegada
                                                                                      || retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.Denegado205
                                                                                      || retConsSitCTe.protCTe.infProt.cStat == (int)StatusAutorizacao.DenegadoEmitente); // manual cte 3.00 página 89
        }

        public static bool IsRejeicao(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe != null && !IsAutorizado(retConsSitCTe) && !IsCancelada(retConsSitCTe) && !IsDenegada(retConsSitCTe);
        }

        public static bool IsRejeicao999(this retCTeOS retConsSitCTe)
        {
            return retConsSitCTe != null && !IsAutorizado(retConsSitCTe) && !IsCancelada(retConsSitCTe) && !IsDenegada(retConsSitCTe) && retConsSitCTe.cStat == 999;
        }
    }
}