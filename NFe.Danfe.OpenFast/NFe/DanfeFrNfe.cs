using DFe.Utils;
using NFe.Classes;
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe;

namespace NFe.Danfe.OpenFast.NFe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE da NFe em Fast Reports
    /// </summary>
    public class DanfeFrNfe : DanfeOpenFastBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrNfe(nfeProc proc, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            byte[] frx = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"NFe\NFeRetrato.frx";
                frx = FrxFileHelper.TryGetFrxFile(caminho);
            }

            Relatorio = DanfeSharedHelper.GenerateDanfeFrNfeReport(proc, configuracaoDanfeNfe, frx, desenvolvedor, arquivoRelatorio);
        }

        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFe em Fast Reports.
        /// Use esse construtor apenas para impressão em contingência, já que neste modo ainda não é possível obter o grupo protNFe
        /// </summary>
        /// <param name="nfe">Objeto do tipo <see cref="Classes.NFe"/></param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrNfe(Classes.NFe nfe, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor) : this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfe, desenvolvedor)
        {
        }
    }
}