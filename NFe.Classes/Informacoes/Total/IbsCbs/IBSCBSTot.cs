using NFe.Classes.Informacoes.Total.IbsCbs.Cbs;
using NFe.Classes.Informacoes.Total.IbsCbs.Ibs;
using NFe.Classes.Informacoes.Total.IbsCbs.Monofasica;

namespace NFe.Classes.Informacoes.Total.IbsCbs
{
    public class IBSCBSTot
    {
        private decimal _vBCIBSCBS;

        /// <summary>
        ///     W35 - Valor total da BC do IBS e da CBS
        /// </summary>
        public decimal vBCIBSCBS
        {
            get => _vBCIBSCBS;
            set => _vBCIBSCBS = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W36 - Grupo total do IBS
        /// </summary>
        public gIBS gIBS  { get; set; }
        
        /// <summary>
        ///     W50 - Grupo total do CBS
        /// </summary>
        public gCBS gCBS { get; set; }
        
        /// <summary>
        ///     W57 - Grupo total da Monofasia
        /// </summary>
        public gMono gMono { get; set; }
    }
}