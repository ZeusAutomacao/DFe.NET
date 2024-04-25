using System.ComponentModel;

namespace NFe.Danfe.QuestPdf.ImpressaoNfce;

public enum TamanhoImpressao
{
    [Description("Impressão 80mm")]
    Impressao80 = 1,

    [Description("Impressão 72mm")]
    Impressao72 = 2,

    [Description("Impressão 50mm")]
    Impressao50 = 3
}