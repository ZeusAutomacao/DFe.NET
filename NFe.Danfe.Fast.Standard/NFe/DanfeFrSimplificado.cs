using NFe.Classes;
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe.Fast;

namespace NFe.Danfe.Fast.Standard.NFe
{
    public class DanfeFrSimplificado : DanfeBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE Simplificada da NFe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        /// <param name="arquivoRelatorio">Caminho do arquivo frx</param>
        public DanfeFrSimplificado(nfeProc proc, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
			byte[] retrato = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                try
                {
                    retrato = Properties.Resources.NFeSimplificado;
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception("Não foi possivel o carregamento do Resource NFeSimplificado, utilize o parametro arquivoRelatorio e passe o caminho manualmente.", ex);
                }
            }
            Relatorio = DanfeSharedHelper.GenerateDanfeFrNfeReport(proc, configuracaoDanfeNfe, retrato, desenvolvedor, arquivoRelatorio);
        }
    }
}
