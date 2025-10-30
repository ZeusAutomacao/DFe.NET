using DFe.Utils;
using NFe.Classes;
using NFe.Danfe.Base.NFCe;
using Shared.DFe.Danfe;

namespace NFe.Danfe.OpenFast.NFCe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE da NFCe em Fast Reports
    /// </summary>
    public class DanfeFrNfce : DanfeOpenFastBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFCe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        /// <param name="arquivoRelatorio">Caminho e arquivo frx contendo as definições do relatório personalizado</param>
        /// <param name="textoRodape">Texto para ser impresso no final do documento</param>
        public DanfeFrNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc, string arquivoRelatorio = "", string textoRodape = "")
        {
            byte[] frx = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"NFCe\NFCe.frx";
                frx = FrxFileHelper.TryGetFrxFile(caminho);
            }

            Relatorio = DanfeSharedHelper.GenerateDanfeNfceReport(proc, configuracaoDanfeNfce, cIdToken, csc, frx, arquivoRelatorio, textoRodape);
        }

        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFCe em Fast Reports.
        /// Use esse construtor apenas para impressão em contingência, já que neste modo ainda não é possível obter o grupo protNFe 
        /// </summary>
        /// <param name="nfe">Objeto do tipo NFe</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        /// <param name="textoRodape">Texto para ser impresso no final do documento</param>
        public DanfeFrNfce(Classes.NFe nfe, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc, string textoRodape = "") :
            this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfce, cIdToken, csc, arquivoRelatorio: string.Empty, textoRodape: textoRodape)
        {
        }
    }
}