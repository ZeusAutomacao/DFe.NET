using System;
using System.Globalization;
using CTeDLL.Classes.Servicos.Tipos;
using System.Runtime.InteropServices;
using DFe.Utils;
using CteEletronica = CTe.Classes.CTe;

namespace CTeDLL.Utils.CTe
{
    [Guid("1b8cc7c5-eeb0-41e5-8ed3-aea95cbdac3c")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("CTeDLL.Classes")]
    [ComVisible(true)]
    public static class ExtCTe
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe CTe
        /// </summary>
        /// <param name="cte"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna uma NFe carregada com os dados do XML</returns>
        public static CteEletronica CarregarDeArquivoXml(this CteEletronica cte, string arquivoXml)
        {
            var s = FuncoesXml.ObterNodeDeArquivoXml(typeof (CteEletronica).Name, arquivoXml);
            return FuncoesXml.XmlStringParaClasse<CteEletronica>(s);
        }

        /// <summary>
        ///     Converte o objeto CTe para uma string no formato XML
        /// </summary>
        /// <param name="cte"></param>
        /// <returns>Retorna uma string no formato XML com os dados da CTe</returns>
        public static string ObterXmlString(this CteEletronica cte)
        {
            return FuncoesXml.ClasseParaXmlString(cte);
        }

        /// <summary>
        ///     Coverte uma string XML no formato CTe para um objeto CTe
        /// </summary>
        /// <param name="cte"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo CTe</returns>
        public static CteEletronica CarregarDeXmlString(this CteEletronica cte, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof (CteEletronica).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<CteEletronica>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto CTe em um arquivo XML
        /// </summary>
        /// <param name="cte">Objeto CTe</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this CteEletronica cte, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(cte, arquivoXml);
        }

        /// <summary>
        ///     Gera id, cdv, assina e faz alguns ajustes nos dados da classe CTe antes de utilizá-la
        /// </summary>
        /// <param name="cte"></param>
        /// <returns>Retorna um objeto CTe devidamente tradado</returns>
        public static CteEletronica Valida(this CteEletronica cte)
        {
            if (cte == null) throw new ArgumentNullException("cte");

            // todo var versao = (Decimal.Parse(cte.infCte.versao, CultureInfo.InvariantCulture));

            var xmlNfe = cte.ObterXmlString();
            // todo var cfgServico = ConfiguracaoServico.Instancia;
            // todo Validador.Valida(ServicoCTe.CteRecepcao, cfgServico.VersaoCteRecepcao, xmlNfe, false, cfgServico.DiretorioSchemas);

            return cte; //Para uso no formato fluent
        }

        /// <summary>
        ///     Assina um objeto CTe
        /// </summary>
        /// <param name="cte"></param>
        /// <returns>Retorna um objeto do tipo CTe assinado</returns>
        public static CteEletronica Assina(this CteEletronica cte)
        {
            var cteLocal = cte;
            if (cteLocal == null) throw new ArgumentNullException("cte");

            #region Define cNF

            var tamanhocNf = 9;

            // todo

            //var versao = (Decimal.Parse(cteLocal.infCte.versao, CultureInfo.InvariantCulture));
            //if (versao >= 2) tamanhocNf = 8;
            //cteLocal.infCte.ide.cCT = Convert.ToInt32(cteLocal.infCte.ide.cCT).ToString().PadLeft(tamanhocNf, '0');

            #endregion

            // todo
            /*var chave = Gerador.GerarChave(cteLocal.infCte);

            cteLocal.infCte.Id = Gerador.GerarId(chave);
            cteLocal.infCte.ide.cDV = Convert.ToInt16(chave.Substring(chave.Length - 1, 1));

            var assinatura = Assinador.ObterAssinatura(cteLocal, cteLocal.infCte.Id);
            cteLocal.Signature = assinatura;*/
            return cteLocal;
        }
    }
}