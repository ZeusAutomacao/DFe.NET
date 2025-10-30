using DFe.Classes.Entidades;

namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class origComb
    {
        private decimal _pOrig;

        /// <summary>
        /// LA19 - Indicador de importação
        /// </summary>
        public int indImport { get; set; }

        /// <summary>
        /// LA20 - Código da UF
        /// </summary>
        public Estado cUFOrig { get; set; }

        /// <summary>
        /// LA21 - Percentual originário para a UF
        /// </summary>
        public decimal pOrig 
        { 
            get { return _pOrig.Arredondar(4); }
            set { _pOrig = value.Arredondar(4); }
        }

    }
}