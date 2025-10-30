namespace NFe.Classes.Informacoes.Observacoes
{
    public class procRef
    {
        /// <summary>
        ///     Z11 - Identificador do processo ou ato concessório
        /// </summary>
        public string nProc { get; set; }

        /// <summary>
        ///     Z12 - Indicador da origem do processo
        /// </summary>
        public IndicadorProcesso indProc { get; set; }

        /// <summary>
        ///     Z13 - Tipo do ato concessório
        /// </summary>
        public TipoAtoConcessorio? tpAto { get; set; }

        public bool ShouldSerializetpAto()
        {
            return tpAto.HasValue;
        }
    }
}