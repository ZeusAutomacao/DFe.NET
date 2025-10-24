using NFe.Danfe.QuestPdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NFe.Danfe.QuestPdf.ParaContadores;

public class RelatorioFiscalEmissoesNfeDocument : IDocument
{
    private static string Fonte = "Arial";
    private RelatorioFiscalEmissoesNfeModel Model;

    public RelatorioFiscalEmissoesNfeDocument(RelatorioFiscalEmissoesNfeModel model)
    {
        Model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    DocumentSettings IDocument.GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                page.Header().Element(Cabecalho);


                page.Content().Element(Conteudo);


                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
    }

    private void Cabecalho(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(11).FontFamily(Fonte);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text("Relatório fiscal de emissões na NF-e").FontSize(12).FontFamily(Fonte).SemiBold();
                column.Item().AlignRight().Text($"Impresso em: {GetMetadata().CreationDate}").FontSize(10).FontFamily(Fonte).Italic().SemiBold();
                column.Item().AlignLeft().Text($"Razão Social: {Model.EmpresaModel.RazaoSocial}").Style(titleStyle);
                column.Item().AlignLeft().Text($"CNPJ: {Model.EmpresaModel.Cnpj}").Style(titleStyle);
                column.Item().AlignLeft().Text($"Período: {Model.PeriodoModel.DataInicial:d} até {Model.PeriodoModel.DataFinal:d}");
            });
        });
    }

    private void Conteudo(IContainer container)
    {
        void TituloInicioTabela(ColumnDescriptor column, string titulo)
        {
            column.Item()
                .PaddingTop(15)
                .AlignCenter()
                .Text(titulo)
                .FontSize(9)
                .Italic()
                .SemiBold()
                .FontFamily(Fonte);
        }


        container
            .PaddingVertical(10)
            .Column(column =>
            {
                TituloInicioTabela(column, "Documento Fiscal Modelo 55 - NFE");

                column.Item().Element(TabelaNFeResumida);

                TituloInicioTabela(column, "Inutilização feitas no periodo");

                column.Item().Element(TabelaInutilizacoes);

                column.Item()
                    .PaddingTop(15)
                    .AlignCenter();

                ResumoTotais(column);
            });
    }

    private void TabelaNFeResumida(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(80);
                columns.ConstantColumn(50);
                columns.ConstantColumn(30);
                columns.ConstantColumn(55);
                columns.RelativeColumn();
                columns.ConstantColumn(55);
            });


            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Data Autorização");
                header.Cell().Element(CellStyle).Text("Situação");
                header.Cell().Element(CellStyle).Text("Série");
                header.Cell().Element(CellStyle).Text("Número");
                header.Cell().Element(CellStyle).Text("Chave");
                header.Cell().Element(CellStyle).AlignRight().Text("Valo Total");


                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x =>
                            x.SemiBold()
                            .FontSize(9)
                            .FontFamily(Fonte)
                        )
                        .PaddingVertical(5)
                        .BorderBottom(1)
                        .BorderColor(Colors.Black);
                }
            });

            foreach (var item in Model.NfeResumidaModels)
            {
                table.Cell().Element(CellStyle).Text(item.AutorizacaoEm.ToString("g")).Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text(item.Situacao).Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.Serie:D3}").Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.NumeroFiscal:D9}").Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.Chave}").Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.ValorTotal:C}").Style(EstiloTextoColunas());

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(0);
                }

                static TextStyle EstiloTextoColunas()
                {
                    return TextStyle.Default.FontSize(8.5f).FontFamily(Fonte);
                }
            }

            table.Footer(footer =>
            {
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
            });
        });
    }

    private void TabelaInutilizacoes(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Data Inutilização");
                header.Cell().Element(CellStyle).Text("Modelo");
                header.Cell().Element(CellStyle).Text("Série");
                header.Cell().Element(CellStyle).Text("Número Inicial");
                header.Cell().Element(CellStyle).Text("Número Final");


                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold().FontSize(9).FontFamily(Fonte))
                        .PaddingVertical(5)
                        .BorderBottom(1)
                        .BorderColor(Colors.Black);
                }
            });

            foreach (var item in Model.NfeInutilizacaoModels)
            {
                table.Cell().Element(CellStyle).Text(item.InutilizacaoEm.ToString("g")).Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text(item.DescricaoModelo).Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.Serie:D3}").Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.NumeroInicial:D9}").Style(EstiloTextoColunas());
                table.Cell().Element(CellStyle).Text($"{item.NumeroFinal:D9}").Style(EstiloTextoColunas());

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(0);
                }

                static TextStyle EstiloTextoColunas()
                {
                    return TextStyle.Default.FontSize(8.5f).FontFamily(Fonte);
                }
            }

            table.Footer(footer =>
            {
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
                footer.Cell().BorderBottom(1);
            });
        });
    }

    private void ResumoTotais(ColumnDescriptor column)
    {
        column.Item().Row(row =>
        {
            row.RelativeItem(7)
                .Border(0.3f)
                .Text("Resumo das saidas").FontSize(10).FontFamily(Fonte).SemiBold().Italic();

            row.RelativeItem(3)
                .Border(0.3f)
                .Text(text =>
                {
                    text.AlignCenter();
                    text.Span("Notas Fiscais").FontSize(10).FontFamily(Fonte).SemiBold().Italic();
                });
        });

        column.Item().Row(LinhaTotal("Resumo notas autorizadas"
            , Model.QuantidadeNfeAutorizada
            , Model.ValorTotalNfeAutorizada));

        column.Item().Row(LinhaTotal("Resumo notas canceladas"
            , Model.QuantidadeNfeCancelada
            , Model.ValorTotalNfeCancelada));

        column.Item().Row(LinhaTotal("Resumo notas denegadas"
            , Model.QuantidadeNfeDenegada
            , Model.ValorTotalNfeDenegada));


    }

    private Action<RowDescriptor> LinhaTotal(string descricaoLinha, decimal quantidadeNfe, decimal valorTotal)
    {
        return row =>
        {
            row.RelativeItem(7)
                .Border(0.3f)
                .Text(descricaoLinha).FontSize(10).FontFamily(Fonte).Italic();

            row.RelativeItem(3)
                .Border(0.3f)
                .Row(resumoAutoriadasRow =>
                {
                    resumoAutoriadasRow.RelativeItem(5)
                        .Border(0.3f)
                        .Text(quantidadeNfe).FontSize(10)
                        .FontFamily(Fonte).Italic();

                    resumoAutoriadasRow.RelativeItem(5)
                        .Border(0.3f)
                        .Text(text =>
                        {
                            text.AlignRight();
                            text.Span(valorTotal.ToString("N")).FontSize(10).FontFamily(Fonte).Italic();
                        });
                });
        };
    }
}