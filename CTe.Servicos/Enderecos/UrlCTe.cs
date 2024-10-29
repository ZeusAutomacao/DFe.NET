namespace CTe.Servicos.Enderecos
{
    public class UrlCTe
    {
        public string CteConsulta { get; set; }
        public string CteInutilizacao { get; set; }
        public string CteRecepcaoSinc { get; set; }
        public string CteRecepcao { get; set; }
        public string CteRecepcaoEvento { get; set; }
        public string CteRetRecepcao { get; set; }
        public string CteStatusServico { get; set; }
        public string CTeDistribuicaoDFe { get; set; }
        public string QrCode { get; set; }
        public string CteRecepcaoOs { get; set; }
        public string CteRecepcaoGtve { get; set; }

        public UrlCTe()
        {
            //CTe tem como URL de distribuição ambinente nacional
            CTeDistribuicaoDFe = @"https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx";
        }
    }
}