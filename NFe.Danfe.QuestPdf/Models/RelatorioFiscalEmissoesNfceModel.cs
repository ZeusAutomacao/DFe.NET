namespace NFe.Danfe.QuestPdf.Models;

public class RelatorioFiscalEmissoesNfceModel
{
    public EmpresaModel EmpresaModel { get; set; }
    public PeriodoModel PeriodoModel { get; set; }
    public IEnumerable<NfceResumidaModel> NfceResumidaModels { get; set; }
    public IEnumerable<NfceInutilizacaoModel> NfceInutilizacaoModels { get; set; }

    public decimal QuantidadeNfceAutorizada => NfceResumidaModels.Count(n => n.Situacao == "Autorizada");
    public decimal ValorTotalNfceAutorizada => NfceResumidaModels.Where(n => n.Situacao == "Autorizada")
        .Sum(n => n.ValorTotal);
    public decimal QuantidadeNfceCancelada => NfceResumidaModels.Count(n => n.Situacao == "Cancelada");
    public decimal ValorTotalNfceCancelada => NfceResumidaModels.Where(n => n.Situacao == "Cancelada")
        .Sum(n => n.ValorTotal);
    public decimal QuantidadeNfceDenegada => NfceResumidaModels.Count(n => n.Situacao == "Denegada");

    public decimal ValorTotalNfceDenegada => NfceResumidaModels.Where(n => n.Situacao == "Denegada")
        .Sum(n => n.ValorTotal);
}