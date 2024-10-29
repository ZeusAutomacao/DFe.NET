using DFe.Utils;
using NFe.Classes;
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe;

namespace NFe.Danfe.Fast.Skia.NFe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE dos eventos da NFe, em Fast Reports
    /// </summary>
    public class DanfeFrEvento : DanfeFastBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE do evento da NFe, em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo <see cref="nfeProc"/></param>
        /// <param name="procEventoNFe">Objeto do tipo <see cref="Classes.Servicos.Consulta.procEventoNFe"/></param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        /// <param name="arquivoRelatorio">Caminho e arquivo frx contendo as definições do relatório personalizado</param>
        public DanfeFrEvento(nfeProc proc, Classes.Servicos.Consulta.procEventoNFe procEventoNFe, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            byte[] frx = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"NFe\NFeEvento.frx";
                frx = FrxFileHelper.TryGetFrxFile(caminho);
            }

            Relatorio = DanfeSharedHelper.GenerateDanfeFrEventoReport(proc, procEventoNFe, configuracaoDanfeNfe, frx, desenvolvedor, arquivoRelatorio);
        }
    }
}