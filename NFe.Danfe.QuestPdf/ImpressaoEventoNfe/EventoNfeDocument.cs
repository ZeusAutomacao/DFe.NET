using System.Text;
using System.Text.RegularExpressions;
using BarcodeStandard;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Servicos.Consulta;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NFe.Danfe.QuestPdf.ImpressaoEventoNfe;

public class EventoNfeDocument : IDocument
{
    private nfeProc _nfeProc;
    private NFe.Classes.NFe _nfe;
    private procEventoNFe _procEventoNFe;
    private readonly byte[]? _logo;
    private static string _fontFamily = "Times New Roman";

    public EventoNfeDocument(string xmlNfe, string xmlCce, byte[]? logo)
    {
        _logo = logo;
        CarregarXmlNfe(xmlNfe);
        CarregarXmlCce(xmlCce);
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                page.Header().Element(Cabecalho);

                page.Content().Element(Conteudo);

                page.Footer().Element(Rodape);
            });
    }

    private void Cabecalho(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignCenter().Text(_procEventoNFe.evento.infEvento.detEvento.descEvento).FontSize(14).FontFamily(_fontFamily).Bold();
                    c.Item().AlignCenter().Text("Não possui valor fiscal. Simples representação do evento indicado abaixo.\r\nCONSULTE A AUTENTICIDADE NO SITE DA SEFAZ AUTORIZADORA").FontSize(10).FontFamily(_fontFamily);
                });
            });

            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text("NOTA FISCAL ELETRÔNICA - NF-e").FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("MODELO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.ide.mod).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("SÉRIE").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.ide.serie.ToString("D3")).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("NÚMERO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.ide.nNF.ToString("D9")).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("MÊS/ANO DA EMISSÃO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.ide.dhEmi.ToString("MM/yyyy")).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    var b = new Barcode();

                    b.Encode(BarcodeStandard.Type.Code128, _nfe.infNFe.Id.Substring(3), 300, 30);

                    t.Cell().ColumnSpan(6).RowSpan(2).Border(0.5f).Padding(5).Image(b.GetImageData(SaveTypes.Png));

                    t.Cell().ColumnSpan(6).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("CHAVE DE ACESSO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.Id.Substring(3)).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });
                });
            });

            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text("CARTA DE CORREÇÃO ELETRÔNICA").FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("ÓRGÃO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_procEventoNFe.evento.infEvento.cOrgao).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(9).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("AMBIENTE").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_procEventoNFe.evento.infEvento.tpAmb).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("DATA E HORÁRIO DO EVENTO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_procEventoNFe.evento.infEvento.dhEvento.ToString("G")).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("EVENTO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_procEventoNFe.evento.infEvento.tpEvento).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(6).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("DESCRIÇÃO DO EVENTO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_procEventoNFe.evento.infEvento.detEvento.descEvento).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("SEQUÊNCIA DO EVENTO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignRight().AlignTop().PaddingRight(2).Text(_procEventoNFe.evento.infEvento.nSeqEvento).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("VERSÃO DO EVENTO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignRight().AlignTop().PaddingRight(2).Text(_procEventoNFe.evento.infEvento.verEvento).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(6).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("STATUS").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text($"{_procEventoNFe.retEvento.infEvento.cStat} - {_procEventoNFe.retEvento.infEvento.xMotivo}").FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("PROTOCOLO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text($"{_procEventoNFe.retEvento.infEvento.nProt}").FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("DATA E HORÁRIO DO REGISTRO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text($"{_procEventoNFe.retEvento.infEvento.dhRegEvento:G}").FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });
                });
            });
        });
    }

    private void Conteudo(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text("EMITENTE").FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().ColumnSpan(9).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("NOME / RAZÃO SOCIAL").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.xNome).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("CNPJ / CPF").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.CNPJ ?? _nfe.infNFe.emit.CPF).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(6).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("ENDEREÇO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text($"{_nfe.infNFe.emit.enderEmit.xLgr} {_nfe.infNFe.emit.enderEmit.nro} {_nfe.infNFe.emit.enderEmit.xCpl}").FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(4).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("BAIRRO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.enderEmit.xBairro).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("CEP").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.enderEmit.CEP).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(5).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("MUNICÍPIO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.enderEmit.xMun).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("ESTADO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.emit.enderEmit.UF).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        var temTelefone = _nfe.infNFe.emit.enderEmit.fone.HasValue;

                        tx.Item().Border(temTelefone ? 0.5f : 0).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("FONE / FAX").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.enderEmit.fone).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("INSCRIÇÃO ESTADUAL").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.emit.IE).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });
                });
            });

            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text("DESTINATÁRIO / REMETENTE").FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().ColumnSpan(9).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("NOME / RAZÃO SOCIAL").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.xNome).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("CNPJ / CPF").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.CNPJ ?? _nfe.infNFe.dest.CPF).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(6).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("ENDEREÇO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text($"{_nfe.infNFe.dest.enderDest.xLgr} {_nfe.infNFe.dest.enderDest.nro} {_nfe.infNFe.dest.enderDest.xCpl}").FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(4).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("BAIRRO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.enderDest.xBairro).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(2).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("CEP").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.enderDest.CEP).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(5).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("MUNICÍPIO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.enderDest.xMun).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("ESTADO").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignCenter().AlignTop().Text(_nfe.infNFe.dest.enderDest.UF).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        var temTelefone = _nfe.infNFe.dest.enderDest.fone.HasValue;

                        tx.Item().Border(temTelefone ? 0.5f : 0).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("FONE / FAX").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.enderDest.fone).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });

                    t.Cell().ColumnSpan(3).Column(tx =>
                    {
                        tx.Item().Border(0.5f).Column(txc =>
                        {
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text("INSCRIÇÃO ESTADUAL").FontSize(5).FontFamily(_fontFamily);
                            txc.Item().AlignLeft().AlignTop().PaddingLeft(2).Text(_nfe.infNFe.dest.IE).FontSize(9).FontFamily(_fontFamily).Bold();
                        });
                    });
                });
            });

            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text("CONDIÇÕES DE USO").FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().RowSpan(8).ColumnSpan(12).Column(tx =>
                    {
                        tx.Item().Border(0.5f).PaddingLeft(2).Text(Regex.Replace(_procEventoNFe.evento.infEvento.detEvento.xCondUso.Replace("\r", string.Empty).Replace("\n", ""), @"\s+", " ")).FontSize(9).FontFamily(_fontFamily).Bold();
                    });
                });
            });

            column.Item().PaddingTop(10);

            column.Item().Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text(_procEventoNFe.evento.infEvento.detEvento.descEvento).FontSize(10).FontFamily(_fontFamily)
                        .Bold();
                });
            });

            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Table(t =>
                {
                    t.ColumnsDefinition(cd =>
                    {
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                        cd.RelativeColumn();
                    });


                    t.Cell().RowSpan(8).ColumnSpan(12).Column(tx =>
                    {
                        var texto = new StringBuilder();

                        if (_procEventoNFe.evento.infEvento.detEvento.xJust != null)
                        {
                            texto.Append(Regex.Replace(
                                _procEventoNFe.evento.infEvento.detEvento.xJust.Replace("\r", string.Empty)
                                    .Replace("\n", ""), @"\s+", " "));

                            texto.Append("\n");
                        }

                        if (_procEventoNFe.evento.infEvento.detEvento.xCorrecao != null)
                        {
                            texto.Append(Regex.Replace(
                                _procEventoNFe.evento.infEvento.detEvento.xCorrecao.Replace("\r", string.Empty)
                                    .Replace("\n", ""), @"\s+", " "));
                        }

                        tx.Item().Border(0.5f).PaddingLeft(2).Text(texto).FontSize(9).FontFamily(_fontFamily).Bold();
                    });
                });
            });
        });
    }

    private void Rodape(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Border(0.5f).Row(x =>
            {
                x.RelativeItem().Column(c =>
                {
                    c.Item().AlignLeft().Text($"Data e hora da impressão {DateTime.UtcNow:G}").FontSize(6).FontFamily(_fontFamily);
                });
            });
        });
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    private void CarregarXmlNfe(string xmlNfe)
    {
        try
        {
            _nfeProc = FuncoesXml.XmlStringParaClasse<nfeProc>(xmlNfe);
            _nfe = _nfeProc.NFe;
        }
        catch (Exception)
        {
            try
            {
                NFe.Classes.NFe nfe = FuncoesXml.XmlStringParaClasse<NFe.Classes.NFe>(xmlNfe);
                _nfe = nfe;
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    "Ei! Verifique se seu xml da NF-e ou NF-e Proc não está correto, pois identificamos uma falha ao tentar carregar ele.");
            }
        }
    }

    private void CarregarXmlCce(string xmlCce)
    {
        try
        {
            _procEventoNFe = FuncoesXml.XmlStringParaClasse<procEventoNFe>(xmlCce);
        }
        catch (Exception)
        {
            throw new ArgumentException(
                "Ei! Verifique se seu xml da Carta Correção não está correto, pois identificamos uma falha ao tentar carregar ele.");
        }
    }
}