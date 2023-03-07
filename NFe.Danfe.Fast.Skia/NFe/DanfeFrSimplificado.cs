using DFe.Utils;
using NFe.Classes;
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe;

namespace NFe.Danfe.Fast.Skia.NFe
{
    public class DanfeFrSimplificado : DanfeFastBase
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
            byte[] frx = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"NFe\NFeSimplificado.frx";
                frx = FrxFileHelper.TryGetFrxFile(caminho);
            }

            Relatorio = DanfeSharedHelper.GenerateDanfeFrNfeReport(proc, configuracaoDanfeNfe, frx, desenvolvedor, arquivoRelatorio);
        }

    }
}
