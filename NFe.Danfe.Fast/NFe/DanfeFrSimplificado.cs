using NFe.Classes;
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe.Fast;

namespace NFe.Danfe.Fast.NFe
{
    public class DanfeFrSimplificado : DanfeBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrSimplificado(nfeProc proc, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            Relatorio = DanfeSharedHelper.GenerateDanfeFrNfeReport(proc, configuracaoDanfeNfe, Properties.Resources.NFeSimplificado, desenvolvedor, arquivoRelatorio);
        }

    }
}
