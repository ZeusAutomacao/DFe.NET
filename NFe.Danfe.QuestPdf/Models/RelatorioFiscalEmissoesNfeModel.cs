namespace NFe.Danfe.QuestPdf.Models;

public class RelatorioFiscalEmissoesNfeModel
{
    public EmpresaModel EmpresaModel { get; set; }
    public PeriodoModel PeriodoModel { get; set; }
    public IEnumerable<NfeResumidaModel> NfeResumidaModels { get; set; }
    public IEnumerable<NfeInutilizadaModel> NfeInutilizacaoModels { get; set; }

    public decimal QuantidadeNfeAutorizada => NfeResumidaModels.Count(n => n.Situacao == "Autorizada");
    public decimal ValorTotalNfeAutorizada => NfeResumidaModels.Where(n => n.Situacao == "Autorizada")
        .Sum(n => n.ValorTotal);
    public decimal QuantidadeNfeCancelada => NfeResumidaModels.Count(n => n.Situacao == "Cancelada");
    public decimal ValorTotalNfeCancelada => NfeResumidaModels.Where(n => n.Situacao == "Cancelada")
        .Sum(n => n.ValorTotal);
    public decimal QuantidadeNfeDenegada => NfeResumidaModels.Count(n => n.Situacao == "Denegada");

    public decimal ValorTotalNfeDenegada => NfeResumidaModels.Where(n => n.Situacao == "Denegada")
        .Sum(n => n.ValorTotal);
}