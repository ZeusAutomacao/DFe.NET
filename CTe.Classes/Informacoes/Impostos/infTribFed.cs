namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class infTribFed
    {
        public decimal? vPIS { get; set; }
        public decimal? vCOFINS { get; set; }
        public decimal? vIR { get; set; }
        public decimal? vINSS { get; set; }
        public decimal? vCSLL { get; set; }


        public bool vPISSpecified => vPIS.HasValue;
        public bool vCOFINSSpecified => vCOFINS.HasValue;
        public bool vIRSpecified => vIR.HasValue;
        public bool vINSSSpecified => vINSS.HasValue;
        public bool vCSLLSpecified => vCSLL.HasValue;
    }
}