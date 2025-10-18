using System.Text;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Utils;
using NFe.Utils.InformacoesSuplementares;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using SkiaSharp.QrCode.Image;

namespace NFe.Danfe.QuestPdf.ImpressaoNfce;

public class DanfeNfceDocument : IDocument
{
    private readonly byte[]? _logo;
    private nfeProc? _nfeProc;
    private NFe.Classes.NFe? _nfe;
    private float _tamanhoImpressao = 300f;
    private float _tamanhoFontePadrao;

    public DanfeNfceDocument(string xml, byte[]? logo)
    {
        _logo = logo;
        CarregarXml(xml);
    }

    public void TamanhoImpressao(TamanhoImpressao tamanhoImpressao)
    {
        switch (tamanhoImpressao)
        {
            case ImpressaoNfce.TamanhoImpressao.Impressao80:
                _tamanhoImpressao = 8f;
                _tamanhoFontePadrao = 8f;
                break;
            case ImpressaoNfce.TamanhoImpressao.Impressao72:
                _tamanhoImpressao = 7.2f;
                _tamanhoFontePadrao = 8f;
                break;
            case ImpressaoNfce.TamanhoImpressao.Impressao50:
                _tamanhoImpressao = 5f;
                _tamanhoFontePadrao = 5f;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tamanhoImpressao), tamanhoImpressao, null);
        }
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.MarginLeft(0.4f, Unit.Centimetre);
                page.MarginRight(0.8f, Unit.Centimetre);
                page.MarginTop(0);
                page.MarginBottom(0);
                page.ContinuousSize(_tamanhoImpressao, Unit.Centimetre);

                page.Header().Element(Cabecalho);

                page.Content().Element(Conteudo);

                page.Footer().Element(Rodape);
            });
    }

    private void Cabecalho(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                if (_logo != null)
                {
                    column.Item().AlignCenter().MaxWidth(60).MaxHeight(60).Image(_logo);
                }
                column.Item().AlignCenter().Text(_nfe.infNFe.emit.xFant).FontSize(_tamanhoFontePadrao + 2).SemiBold();
                column.Item().AlignCenter().Text(_nfe.infNFe.emit.xNome).FontSize(_tamanhoFontePadrao + 2);
                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text($"CNPJ: {_nfe.infNFe.emit.CNPJ}").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"IE: {_nfe.infNFe.emit.IE}").FontSize(_tamanhoFontePadrao);
                    });
                });
                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text($"Endereço: {EnderecoEmitente()}").FontSize(_tamanhoFontePadrao).Italic();
                    });
                });

                if (_nfe.infNFe.ide.tpAmb == TipoAmbiente.Homologacao)
                {
                    column.Item().LineHorizontal(1);
                    column.Item().Row(r =>
                    {
                        r.RelativeItem().AlignCenter().Column(c =>
                        {
                            c.Item().AlignCenter().Text("EMITIDO EM AMBIENTE DE HOMOLOGAÇÃO SEM VALOR FISCAL").FontSize(_tamanhoFontePadrao).SemiBold().Underline();
                        });
                    });
                    column.Item().LineHorizontal(1);
                }

                column.Item().LineHorizontal(1);
                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().AlignCenter().Text("DANFE NFC-e").FontSize(_tamanhoFontePadrao).ExtraBlack();
                        c.Item().AlignCenter().Text("Documento Auxiliar Da Nota Fiscal de Consumidor Eletrônica").FontSize(_tamanhoFontePadrao - 1);
                        c.Item().AlignCenter().Text("Não permite aproveitamento de crédito do ICMS").FontSize(_tamanhoFontePadrao - 1);
                    });
                });
                column.Item().LineHorizontal(1);
            });
        });
    }

    private void Conteudo(IContainer container)
    {
        container.Table(t =>
        {
            t.ColumnsDefinition(c =>
            {
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
                c.RelativeColumn();
            });

            t.Header(h =>
            {
                h.Cell().Element(CellStyle).Text("Num.").ExtraBlack();
                h.Cell().ColumnSpan(11).Element(CellStyle).Text("Descrição").ExtraBlack();
                h.Cell().RowSpan(1);
                h.Cell().ColumnSpan(2).Element(CellStyle).AlignRight().Text("Qtde Un ").ExtraBlack();
                h.Cell().ColumnSpan(1).Element(CellStyle).AlignCenter().Text("X").ExtraBlack();
                h.Cell().ColumnSpan(4).Element(CellStyle).AlignRight().Text("Valor Unitário").ExtraBlack();
                h.Cell().ColumnSpan(1).Element(CellStyle).AlignCenter().Text("=").ExtraBlack();
                h.Cell().ColumnSpan(3).Element(CellStyle).AlignRight().AlignRight().Text("Valor Total").ExtraBlack();
                h.Cell().RowSpan(1);

                IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x =>
                            x.SemiBold()
                                .FontSize(_tamanhoFontePadrao - 0.5f)
                        )
                        .BorderColor(Colors.Black);
                }
            });

            t.Cell().ColumnSpan(12).LineHorizontal(1).LineColor(Colors.Grey.Medium);

            foreach (var det in _nfe.infNFe.det)
            {
                t.Cell().Text(det.nItem.ToString("D3")).FontSize(_tamanhoFontePadrao - 0.5f);
                t.Cell().ColumnSpan(11).Text(det.prod.xProd).FontSize(_tamanhoFontePadrao - 0.5f);
                t.Cell().RowSpan(1);
                t.Cell().ColumnSpan(2).AlignRight().Text(det.prod.qCom.ToString("N3")).FontSize(_tamanhoFontePadrao - 0.5f);
                t.Cell().ColumnSpan(1).AlignCenter();
                t.Cell().ColumnSpan(4).AlignRight().Text(det.prod.vUnCom.ToString("N2")).FontSize(_tamanhoFontePadrao - 0.5f);
                t.Cell().ColumnSpan(1).AlignCenter();
                t.Cell().ColumnSpan(3).AlignRight().Text(det.prod.vProd.ToString("N2")).FontSize(_tamanhoFontePadrao - 0.5f);
                t.Cell().ColumnSpan(12).RowSpan(1).Element(CellStyle);
            }

            t.Footer(footer =>
            {
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
                footer.Cell().LineHorizontal(1);
            });
            static IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(0);
            }
        });
    }

    private void Rodape(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Quantidade de itens").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"{_nfe.infNFe.det.Count}").FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Total Desconto").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"R$ {_nfe.infNFe.total.ICMSTot.vDesc:N2}").FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Total Outros").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"R$ {_nfe.infNFe.total.ICMSTot.vOutro:N2}").FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Total Cupom").FontSize(_tamanhoFontePadrao).ExtraBlack();
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"R$ {_nfe.infNFe.total.ICMSTot.vNF:N2}").FontSize(_tamanhoFontePadrao).ExtraBlack();
                    });
                });

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Tributos Totais Incidentes (Lei Federal 12.741/2012)").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text($"R$ {_nfe.infNFe.total.ICMSTot.vTotTrib:N2}").FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().LineHorizontal(1);

                column.Item().Row(r =>
                {

                    r.RelativeItem().AlignLeft().Column(c =>
                    {
                        c.Item().Text("Forma Pagamento").FontSize(_tamanhoFontePadrao);
                    });

                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text("Valor Pago").FontSize(_tamanhoFontePadrao);
                    });
                });

                foreach (pag pag in _nfe.infNFe.pag)
                {
                    if (pag.detPag != null)
                    {
                        foreach (var detPag in pag.detPag)
                        {
                            column.Item().Row(r =>
                            {

                                r.RelativeItem().AlignLeft().Column(c =>
                                {
                                    c.Item().Text(ObtemDescricao(detPag.tPag)).FontSize(_tamanhoFontePadrao);
                                });

                                r.RelativeItem().AlignRight().Column(c =>
                                {
                                    c.Item().Text($"{detPag.vPag:N2}").FontSize(_tamanhoFontePadrao);
                                });
                            });
                        }
                    }
                }

                column.Item().LineHorizontal(1);

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().Text("Consulte pela chave de acesso em:").FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().Text(string.IsNullOrEmpty(_nfe.infNFeSupl.urlChave) ? _nfe.infNFeSupl.ObterUrlConsulta(_nfe, VersaoQrCode.QrCodeVersao2) : _nfe.infNFeSupl.urlChave).FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().LineHorizontal(1);

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().AlignCenter().Text(_nfe.infNFe.Id.Substring(3)).FontSize(_tamanhoFontePadrao);
                    });
                });

                column.Item().LineHorizontal(1);

                if (DeveExibirMensagemContingencia())
                {
                    column.Item().Row(r =>
                    {
                        r.RelativeItem().AlignCenter().Column(c =>
                        {
                            c.Item().AlignCenter().Text("EMITIDA EM CONTINGÊNCIA").FontSize(_tamanhoFontePadrao).ExtraBlack();
                        });
                    });

                    column.Item().Row(r =>
                    {
                        r.RelativeItem().AlignCenter().Column(c =>
                        {
                            c.Item().AlignCenter().Text("Pendente de autorização").FontSize(_tamanhoFontePadrao).ExtraBlack();
                        });
                    });

                    column.Item().LineHorizontal(1);
                }

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().AlignCenter().Text(MontaMensagemConsumidor()).FontSize(_tamanhoFontePadrao).ExtraBlack();
                    });
                });

                column.Item().LineHorizontal(1);

                column.Item().Row(r =>
                {
                    r.RelativeItem().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(2);
                            c.RelativeColumn(3);
                        });

                        t.Cell().AlignCenter().Image(ImagemQrCode());
                        t.Cell().AlignLeft().Column(c =>
                        {
                            if (_nfe?.infNFe?.ide != null)
                            {
                                c.Item().Text($"Série: {_nfe.infNFe.ide.serie:D3}").FontSize(_tamanhoFontePadrao);
                                c.Item().Text($"Número: {_nfe.infNFe.ide.nNF:D9}").FontSize(_tamanhoFontePadrao);
                                c.Item().Text($"Emissão: {_nfe.infNFe.ide.dhEmi:G}").FontSize(_tamanhoFontePadrao);
                            }

                            if (DeveExibirDadosProtocolo())
                            {
                                c.Item().Text($"Protocolo: {_nfeProc!.protNFe!.infProt.nProt}").FontSize(_tamanhoFontePadrao);
                                c.Item().Text($"Autorização: {_nfeProc!.protNFe!.infProt.dhRecbto:G}").FontSize(_tamanhoFontePadrao);
                            }
                        });
                    });
                });

                column.Item().LineHorizontal(1);

                column.Item().Row(r =>
                {
                    r.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Item().AlignCenter().Text("Nota Fiscal de Consumidor Eletrônica").FontSize(_tamanhoFontePadrao).ExtraBlack();
                    });
                });

                column.Item().LineHorizontal(1);
            });
        });
    }

    private byte[] ImagemQrCode()
    {
        using var memoryStream = new MemoryStream();
        var qrCode = new QrCode(ObtemUrlQrCode(), new Vector2Slim(256, 256), SKEncodedImageFormat.Png);
        qrCode.GenerateImage(memoryStream);

        var qrCodeBytes = memoryStream.ToArray();

        return qrCodeBytes;
    }

    private string ObtemUrlQrCode()
    {
        var urlQrCode = _nfe.infNFeSupl.qrCode;

        return urlQrCode;
    }

    private string MontaMensagemConsumidor()
    {
        var dest = _nfe.infNFe.dest;

        var mensagem = new StringBuilder("CONSUMIDOR ");

        if (dest == null || string.IsNullOrEmpty(dest.CPF) && string.IsNullOrEmpty(dest.CNPJ))
        {
            mensagem.Append("NÃO IDENTIFICADO");
            return mensagem.ToString();
        }

        if (!string.IsNullOrEmpty(dest.idEstrangeiro))
        {
            mensagem.Append("Id ");
            mensagem.Append(dest.idEstrangeiro);
        }


        if (!string.IsNullOrEmpty(dest.CPF))
        {
            mensagem.Append("CPF ");
            mensagem.Append(dest.CPF);
        }

        if (!string.IsNullOrEmpty(dest.CNPJ))
        {
            mensagem.Append("CNPJ ");
            mensagem.Append(dest.CNPJ);
        }

        if (!string.IsNullOrEmpty(dest.xNome))
        {
            mensagem.Append(" ");
            mensagem.Append(dest.xNome);
        }

        enderDest enderecoDest = dest.enderDest;

        if (enderecoDest == null) return mensagem.ToString().Replace(", ,", ", ");

        string rua = string.Empty;
        if (!string.IsNullOrEmpty(enderecoDest.xLgr))
            rua = enderecoDest.xLgr;

        string numero = "S/N";
        if (!string.IsNullOrEmpty(enderecoDest.nro))
            numero = enderecoDest.nro;

        var bairro = string.Empty;
        if (!string.IsNullOrEmpty(enderecoDest.xBairro))
            bairro = enderecoDest.xBairro;

        var cidade = string.Empty;
        if (!string.IsNullOrEmpty(enderecoDest.xMun))
            bairro = enderecoDest.xMun;

        var siglaUf = string.Empty;
        if (!string.IsNullOrEmpty(enderecoDest.UF))
            bairro = enderecoDest.UF;

        if (string.IsNullOrEmpty(rua)) return mensagem.ToString();
        mensagem.Append(" - ");
        mensagem.Append(rua);
        mensagem.Append(", ");
        mensagem.Append(numero);
        mensagem.Append(", ");
        mensagem.Append(bairro);
        mensagem.Append(", ");
        mensagem.Append(cidade);
        mensagem.Append(" - ");
        mensagem.Append(siglaUf);

        return mensagem.ToString().Replace(", ,", ", ");
    }

    private string EnderecoEmitente()
    {
        var enderEmit = _nfe.infNFe.emit.enderEmit;
        var foneEmit = string.Empty;

        if (enderEmit.fone != null)
        {
            StringBuilder fone = new StringBuilder(" - FONE: ");
            fone.Append(enderEmit.fone);
            foneEmit = fone.ToString();
        }

        var enderecoEmitenteBuilder = new StringBuilder();
        enderecoEmitenteBuilder.Append(enderEmit.xLgr);
        enderecoEmitenteBuilder.Append(" ");

        if (string.IsNullOrEmpty(enderEmit.nro))
        {
            enderecoEmitenteBuilder.Append("S/N, ");
        }

        if (!string.IsNullOrEmpty(enderEmit.nro))
        {
            enderecoEmitenteBuilder.Append(enderEmit.nro);
            enderecoEmitenteBuilder.Append(", ");
        }

        enderecoEmitenteBuilder.Append(enderEmit.xBairro);
        enderecoEmitenteBuilder.Append(", ");
        enderecoEmitenteBuilder.Append(enderEmit.xMun);
        enderecoEmitenteBuilder.Append(", ");
        enderecoEmitenteBuilder.Append(enderEmit.UF);
        enderecoEmitenteBuilder.Append(foneEmit);

        return enderecoEmitenteBuilder.ToString();
    }

    private void CarregarXml(string xml)
    {
        try
        {
            _nfeProc = null;
            _nfe = null;
            _nfeProc = FuncoesXml.XmlStringParaClasse<nfeProc>(xml);
            _nfe = _nfeProc.NFe;
        }
        catch (Exception)
        {
            try
            {
                NFe.Classes.NFe nfe = FuncoesXml.XmlStringParaClasse<NFe.Classes.NFe>(xml);
                _nfe = nfe;
                _nfeProc = null;
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    "Ei! Verifique se seu xml está correto, pois identificamos uma falha ao tentar carregar ele.");
            }
        }
    }

    private bool DeveExibirMensagemContingencia()
    {
        return _nfe?.infNFe?.ide?.tpEmis == TipoEmissao.teOffLine && _nfeProc == null;
    }

    private bool DeveExibirDadosProtocolo()
    {
        return _nfeProc?.protNFe?.infProt != null && !DeveExibirMensagemContingencia();
    }

    private string ObtemDescricao(FormaPagamento? formaPagamento)
    {
        var existeEnum = Enum
            .GetValues(typeof(FormaPagamento))
            .Cast<FormaPagamento>()
            .Any(p => formaPagamento.HasValue && p.Equals(formaPagamento));

        if (existeEnum)
            return formaPagamento.Descricao();
        else
            throw new ArgumentException("Forma pagamento inválida");
    }
}