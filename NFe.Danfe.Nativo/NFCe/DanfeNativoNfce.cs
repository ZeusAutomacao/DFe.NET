using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using GraphicsPrinter;
using NFe.Classes;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Servicos.Download;
using NFe.Utils.InformacoesSuplementares;
using NFe.Utils.NFe;
using NFeZeus = NFe.Classes.NFe;

namespace NFe.Danfe.Nativo.NFCe
{
    public class DanfeNativoNfce
    {
        private readonly string _cIdToken;
        private readonly string _csc;
        private readonly NFeZeus _nfe;
        private readonly nfeProc _proc;
        private readonly decimal _troco;
        private readonly Image _logo;

        public DanfeNativoNfce(string xml, Image logo, string cIdToken, string csc,
            decimal troco, FontFamily fontFamily)
        {
            _cIdToken = cIdToken;
            _csc = csc;
            _troco = troco;
            AdicionarTexto.FontPadrao = fontFamily;
            _logo = logo;

            try
            {
                var procNfe = new procNFe().nfeProc.CarregarDeXmlString(xml);
                _proc = procNfe;
                _nfe = _proc.NFe;
            }
            catch (Exception)
            {
                var nfe = new NFeZeus().CarregarDeXmlString(xml);
                _nfe = nfe;
            }
        }

        //Função para mandar imprimir na impressora padrão
        public void Imprimir()
        {
            var printCupom = new PrintDocument();
            printCupom.PrintPage += printCupom_PrintPage;
            printCupom.Print();
        }

        private void printCupom_PrintPage(object sender, PrintPageEventArgs e)
        {
            GerarNfCe(e.Graphics);
        }

        private void GerarNfCe(Graphics graphics)
        {
            var g = graphics;

            const int larguraLogo = 64;
            const int larguraLinha = 284;
            const int larguraLinhaMargemDireita = 277;

            var x = 3;
            var y = 3;

            if (_logo != null)
            {
                new AdicionarImagem(g, _logo, x, y).Desenhar();
            }

            var tamanhoFonteTitulo = 6;
            var cnpjERazaoSocial = $"CNPJ: {_nfe.infNFe.emit.CNPJ} {_nfe.infNFe.emit.xNome ?? _nfe.infNFe.emit.xFant}";

            y = EscreverLinhaTitulo(g, cnpjERazaoSocial, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            var enderEmit = _nfe.infNFe.emit.enderEmit;
            var foneEmit = enderEmit.fone != null ? " - FONE: " + enderEmit.fone : string.Empty;
            var enderecoEmitente = $"{enderEmit.xLgr} {enderEmit.nro ?? "S/N"}, " +
                                   $"BAIRRO: {enderEmit.xBairro}, {enderEmit.xMun}, " +
                                   $"{enderEmit.UF}" +
                                   $"{foneEmit}";

            y = EscreverLinhaTitulo(g, enderecoEmitente, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            const string mensagemGoverno = "Documento Auxiliar Da Nota Fiscal de Consumidor Eletrônica";

            y = EscreverLinhaTitulo(g, mensagemGoverno, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            y += 5;

            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                LinhaHorizontal(g, x, y, larguraLinha);
                y += 2;

                y = MensagemContingencia(g, larguraLinha, y);
            }

            LinhaHorizontal(g, x, y, larguraLinha);


            var iniX = x;

            CriaHeaderColuna("CÓDIGO", g, iniX, y);
            iniX += 50;

            var colunaDescricaoHeader = CriaHeaderColuna("DESCRIÇÃO", g, iniX, y);
            y += colunaDescricaoHeader.Medida.Altura;

            CriaHeaderColuna("QTDE", g, iniX, y);
            iniX += 25;

            CriaHeaderColuna("UN", g, iniX, y);
            iniX += 25;

            CriaHeaderColuna("x", g, iniX, y);
            iniX += 20;

            var colunaValorUnitarioHeader = CriaHeaderColuna("VALOR UNITÁRIO", g, iniX, y);
            iniX += 85;

            CriaHeaderColuna("=", g, iniX, y);
            iniX += 41;

            var colunaTotalHeader = CriaHeaderColuna("TOTAL", g, iniX, y);
            y += colunaTotalHeader.Medida.Altura + 10;

            var det = _nfe.infNFe.det;


            foreach (var detalhe in det)
            {
                var codigo = new AdicionarTexto(g, detalhe.prod.cProd, 7);
                codigo.Desenhar(x, y);

                var nome = new AdicionarTexto(g, detalhe.prod.xProd, 7);
                var quebraNome = new DefineQuebraDeLinha(nome, new ComprimentoMaximo(227), nome.Medida.Largura);
                nome = quebraNome.DesenharComQuebras(g);
                nome.Desenhar(x + 50, y);
                y += nome.Medida.Altura;

                var quantidade = new AdicionarTexto(g, detalhe.prod.qCom.ToString("N3"), 7);
                var valorUnitario = new AdicionarTexto(g, detalhe.prod.vUnCom.ToString("N4"), 7);
                var vezesX = new AdicionarTexto(g, "x", 7);
                var unidadeSigla = new AdicionarTexto(g, detalhe.prod.uCom.Substring(0, 2), 7);

                var detalheTotal = detalhe.prod.vProd;
                var valorTotalProduto = new AdicionarTexto(g, detalheTotal.ToString("N2"), 7);

                iniX = x + 50;

                quantidade.Desenhar(iniX, y);

                iniX += 25;

                unidadeSigla.Desenhar(iniX, y);

                iniX += 25;

                vezesX.Desenhar(iniX, y);

                iniX += 20;

                var tituloColunaUnidadeLargura = colunaValorUnitarioHeader.Medida.Largura;
                valorUnitario.Desenhar((iniX + tituloColunaUnidadeLargura) - valorUnitario.Medida.Largura, y);


                iniX += 85;

                var igualColuna = new AdicionarTexto(g, "=", 7);
                igualColuna.Desenhar(iniX, y);

                iniX += 41;

                var tituloColunaTotal = colunaTotalHeader.Medida.Largura;
                valorTotalProduto.Desenhar((iniX + tituloColunaTotal) - valorTotalProduto.Medida.Largura, y);

                y += quantidade.Medida.Altura;

                var valorDescontoItem = detalhe.prod.vDesc ?? 0.0m;
                if (valorDescontoItem > 0.0m)
                {
                    var descontoColuna = new AdicionarTexto(g, "Desconto", 7);
                    descontoColuna.Desenhar(x + 50, y);

                    var descontoItemTexto = new StringBuilder("-");
                    descontoItemTexto.Append(valorDescontoItem.ToString("N2"));
                    var valorDescontoItemColuna = new AdicionarTexto(g, descontoItemTexto.ToString(), 7);
                    var valorDescontoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                   valorDescontoItemColuna.Medida.Largura;
                    valorDescontoItemColuna.Desenhar(valorDescontoItemColunaX, y);

                    y += descontoColuna.Medida.Altura;
                }

                var valorAcrescimoItem = detalhe.prod.vOutro ?? 0.0m;
                if (valorAcrescimoItem > 0.0m)
                {
                    var acrescimoColuna = new AdicionarTexto(g, "Acréscimo", 7);
                    acrescimoColuna.Desenhar(x + 50, y);

                    var acrescimoItemTexto = new StringBuilder("+");
                    acrescimoItemTexto.Append(valorAcrescimoItem.ToString("N2"));
                    var valorAcrescimoItemColuna = new AdicionarTexto(g, acrescimoItemTexto.ToString(), 7);
                    var valorAcrescimoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                    valorAcrescimoItemColuna.Medida.Largura;
                    valorAcrescimoItemColuna.Desenhar(valorAcrescimoItemColunaX, y);

                    y += acrescimoColuna.Medida.Altura;
                }

                if (valorDescontoItem > 0.0m || valorAcrescimoItem > 0.0m)
                {
                    var valorLiquidoTexto = new AdicionarTexto(g, "Valor Líquido", 7);
                    valorLiquidoTexto.Desenhar(x + 50, y);

                    var valorLiquidoTotalTexto = new AdicionarTexto(g,
                        ((detalheTotal + valorAcrescimoItem) - valorDescontoItem).ToString("N2"), 7);
                    var valorLiquidoTotalTextoX = ((x + 246) + tituloColunaTotal) -
                                                  valorLiquidoTotalTexto.Medida.Largura;
                    valorLiquidoTotalTexto.Desenhar(valorLiquidoTotalTextoX, y);

                    y += valorLiquidoTexto.Medida.Altura;
                }
            }

            y += 3;

            LinhaHorizontal(g, x, y, larguraLinha);

            var textoQuantidadeTotalItens = new AdicionarTexto(g, "Qtde. total de itens", 7);
            textoQuantidadeTotalItens.Desenhar(x, y);

            var qtdTotalItens = new AdicionarTexto(g, det.Count.ToString(), 7);
            var qtdTotalItensX = (larguraLinhaMargemDireita - qtdTotalItens.Medida.Largura);
            qtdTotalItens.Desenhar(qtdTotalItensX, y);
            y += textoQuantidadeTotalItens.Medida.Altura;

            var textoValorTotal = new AdicionarTexto(g, "Valor total R$", 7);
            textoValorTotal.Desenhar(x, y);

            var valorTotal = det.Sum(prod => prod.prod.vProd);
            var valorTotalTexto = new AdicionarTexto(g, valorTotal.ToString("N2"), 7);
            var qtdValorTotalX = (larguraLinhaMargemDireita - valorTotalTexto.Medida.Largura);
            valorTotalTexto.Desenhar(qtdValorTotalX, y);
            y += textoValorTotal.Medida.Altura;

            var totalDesconto = det.Sum(prod => prod.prod.vDesc) ?? 0.0m;
            var totalOutras = det.Sum(prod => prod.prod.vOutro);
            var valorTotalAPagar = valorTotal + totalOutras -
                                   totalDesconto ?? 0.0m;

            if (totalDesconto > 0)
            {
                var textoDesconto = new AdicionarTexto(g, "Desconto R$", 7);
                textoDesconto.Desenhar(x, y);

                var valorDesconto = new AdicionarTexto(g, totalDesconto.ToString("N2"), 7);
                var valorDescontoX = (larguraLinhaMargemDireita - valorDesconto.Medida.Largura);
                valorDesconto.Desenhar(valorDescontoX, y);
                y += textoDesconto.Medida.Altura;

                var textoValorAPagar = new AdicionarTexto(g, "Valor a Pagar R$", 7);
                textoValorAPagar.Desenhar(x, y);

                var valorAPagar = new AdicionarTexto(g, valorTotalAPagar.ToString("N2"), 7);
                var valorAPagarX = (larguraLinhaMargemDireita - valorAPagar.Medida.Largura);
                valorAPagar.Desenhar(valorAPagarX, y);
                y += textoValorAPagar.Medida.Altura + 2;
            }

            var tituloFormaPagamento = new AdicionarTexto(g, "FORMA PAGAMENTO", 7);
            tituloFormaPagamento.Desenhar(x, y);

            var tituloValorPago = new AdicionarTexto(g, "VALOR PAGO R$", 7);
            var tituloValorPagoX = (larguraLinhaMargemDireita - tituloValorPago.Medida.Largura);
            tituloValorPago.Desenhar(tituloValorPagoX, y);
            y += tituloFormaPagamento.Medida.Altura;


            foreach (var pag in _nfe.infNFe.pag)
            {
                var textoFormaPagamento = new AdicionarTexto(g, pag.ObtemDescricao(), 7);
                textoFormaPagamento.Desenhar(x, y);

                var textoValorFormaPagamento = new AdicionarTexto(g, pag.vPag.ToString("N2"), 7);
                var textoValorFormaPagamentoX = (larguraLinhaMargemDireita - textoValorFormaPagamento.Medida.Largura);
                textoValorFormaPagamento.Desenhar(textoValorFormaPagamentoX, y);

                y += textoFormaPagamento.Medida.Altura;
            }

            y += 2;

            if (_troco > 0)
            {
                var textoTroco = new AdicionarTexto(g, "Troco R$", 7);
                textoTroco.Desenhar(x, y);

                var textoTrocoValor = new AdicionarTexto(g, _troco.ToString("N2"), 7);
                var textoTrocoValorX = (larguraLinhaMargemDireita - textoTrocoValor.Medida.Largura);
                textoTrocoValor.Desenhar(textoTrocoValorX, y);
                y += textoTroco.Medida.Altura;
            }

            y += 5;

            LinhaHorizontal(g, x, y, larguraLinha);

            var textoConsulteChave = new AdicionarTexto(g, "Consulte pela Chave de Acesso em", 7);
            var textoConsulteChaveX = ((larguraLinha - textoConsulteChave.Medida.Largura)/2);
            textoConsulteChave.Desenhar(textoConsulteChaveX, y);

            y += textoConsulteChave.Medida.Altura;

            var urlConsulta = new AdicionarTexto(g,
                _nfe.infNFeSupl.ObterUrl(_nfe.infNFe.ide.tpAmb, _nfe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlQrCode),
                7);
            var urlConsultaX = ((larguraLinha - urlConsulta.Medida.Largura)/2);
            urlConsulta.Desenhar(urlConsultaX, y);

            y += urlConsulta.Medida.Altura;

            var novaChave = GeraChaveAcesso(_nfe);

            var chave = new AdicionarTexto(g, novaChave, 7);
            var urlChaveX = ((larguraLinha - chave.Medida.Largura)/2);
            chave.Desenhar(urlChaveX, y);

            y += chave.Medida.Altura;
            y += 10;


            var mensagemConsumidor = MontaMensagemConsumidor(_nfe.infNFe.dest);

            var consumidor = new AdicionarTexto(g, mensagemConsumidor, 9);
            var quebraLinhaConsumidor = new DefineQuebraDeLinha(consumidor,
                new ComprimentoMaximo(larguraLinhaMargemDireita), consumidor.Medida.Largura);
            consumidor = quebraLinhaConsumidor.DesenharComQuebras(g);
            var consumidorX = (larguraLinha - consumidor.Medida.Largura)/2;
            consumidor.Desenhar(consumidorX, y);

            y += consumidor.Medida.Altura + 10;

            var mensagemDadosNfCe = MontaMensagemDadosNfce(_nfe);

            var dadosNfce = new AdicionarTexto(g, mensagemDadosNfCe, 7);
            var dadosNfceX = (larguraLinha - dadosNfce.Medida.Largura)/2;
            dadosNfce.Desenhar(dadosNfceX, y);

            y += dadosNfce.Medida.Altura;

            if (_nfe.infNFe.ide.tpEmis == TipoEmissao.teNormal)
            {
                var textoProtocoloAutorizacao = new StringBuilder("Protocolo de autorização: ");
                textoProtocoloAutorizacao.Append(_proc.protNFe.infProt.nProt);
                var protocoloAutorizacao = new AdicionarTexto(g, textoProtocoloAutorizacao.ToString(), 7);
                var protocoloAutorizacaoX = (larguraLinha - protocoloAutorizacao.Medida.Largura)/2;
                protocoloAutorizacao.Desenhar(protocoloAutorizacaoX, y);
                y += protocoloAutorizacao.Medida.Altura;


                var textoDataAutorizacao = new StringBuilder("Data de autorização ");
                textoDataAutorizacao.Append(_proc.protNFe.infProt.dhRecbto.ToString("G"));
                var dataAutorizacao = new AdicionarTexto(g, textoDataAutorizacao.ToString(), 7);
                var dataAutorizacaoX = (larguraLinha - dataAutorizacao.Medida.Largura)/2;
                dataAutorizacao.Desenhar(dataAutorizacaoX, y);
                y += dataAutorizacao.Medida.Altura;
            }

            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                y += 10;
                y = MensagemContingencia(g, larguraLinha, y);
            }

            y += 8;

            var urlQrCode = ObtemUrlQrCode(_nfe, _cIdToken, _csc);

            var qrCodeImagem = QrCode.Gerar(urlQrCode);
            var qrCodeImagemX = (larguraLinha - qrCodeImagem.Size.Width)/2;
            var desenharQrCode = new AdicionarImagem(g, qrCodeImagem, qrCodeImagemX, y);
            desenharQrCode.Desenhar();

            y += qrCodeImagem.Size.Height + 10;

            LinhaHorizontal(g, x, y, larguraLinha);

            y += 5;

            var tributosIncidentes = _nfe.infNFe.total.ICMSTot.vTotTrib;


            if (tributosIncidentes != 0)
            {
                var mensagemTributosTotais =
                    new StringBuilder("Tributos Totais Incidentes (Lei Federal 12.741/2012): R$");
                mensagemTributosTotais.Append(tributosIncidentes.ToString("N2"));

                var tributosTotais = new AdicionarTexto(g, mensagemTributosTotais.ToString(), 7);
                var tributosTotaisX = (larguraLinha - tributosTotais.Medida.Largura)/2;
                tributosTotais.Desenhar(tributosTotaisX, y);

                y += tributosTotais.Medida.Altura;

                y += 5;

                LinhaHorizontal(g, x, y, larguraLinha);
            }

            var observacoes = _nfe.infNFe?.infAdic.infCpl;

            if (!string.IsNullOrEmpty(observacoes))
            {
                y += 5;
                var observacao = new AdicionarTexto(g, observacoes, 7);
                var quebraObservacao = new DefineQuebraDeLinha(observacao,
                    new ComprimentoMaximo(larguraLinhaMargemDireita), observacao.Medida.Largura);
                observacao = quebraObservacao.DesenharComQuebras(g);
                observacao.Desenhar(x, y);
            }
        }

        private static string ObtemUrlQrCode(NFeZeus nfce, string idToken, string csc)
        {
            var urlQrCode = nfce.infNFeSupl == null
                ? nfce.infNFeSupl.ObterUrlQrCode(nfce,
                    idToken,
                    csc)
                : nfce.infNFeSupl.qrCode;
            return urlQrCode;
        }

        private static string MontaMensagemDadosNfce(NFeZeus nfce)
        {
            var mensagem = new StringBuilder("NFC-e nº ");
            mensagem.Append(nfce.infNFe.ide.nNF.ToString("D9"));
            mensagem.Append(" Série ");
            mensagem.Append(nfce.infNFe.ide.serie.ToString("D3"));
            mensagem.Append(" ");
            mensagem.Append(nfce.infNFe.ide.dhEmi.ToString("G"));
            mensagem.Append(" - ");
            mensagem.Append("Via consumidor");

            return mensagem.ToString();
        }

        private static string MontaMensagemConsumidor(dest dest)
        {
            var mensagem = new StringBuilder("CONSUMIDOR ");

            if (dest == null || (string.IsNullOrEmpty(dest.CPF) && string.IsNullOrEmpty(dest.CNPJ)))
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

            mensagem.Append(" ");
            mensagem.Append(dest.xNome);

            var enderecoDest = dest.enderDest;

            if (enderecoDest == null) return mensagem.ToString().Replace(", ,", ", ");

            var rua = enderecoDest.xLgr ?? string.Empty;
            var numero = enderecoDest.nro ?? "S/N";
            var bairro = enderecoDest.xBairro;
            var cidade = enderecoDest.xMun;
            var siglaUf = enderecoDest.UF;

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

        private static string GeraChaveAcesso(NFeZeus nfce)
        {
            var chaveAcesso = nfce.infNFe.Id.Substring(3);
            var novaChave = string.Empty;
            var contaChaveAcesso = 0;

            foreach (var c in chaveAcesso)
            {
                contaChaveAcesso++;
                novaChave += c;

                if (contaChaveAcesso == 4)
                {
                    novaChave += " ";
                    contaChaveAcesso = 0;
                }
            }
            return novaChave;
        }

        private static AdicionarTexto CriaHeaderColuna(string texto, Graphics graphics, int x, int y)
        {
            var coluna = new AdicionarTexto(graphics, texto, 7);
            coluna.Desenhar(x, y);

            return coluna;
        }

        private static void LinhaHorizontal(Graphics g, int x, int y, int larguraLinha)
        {
            new LinhaHorizontal(g, Pens.Black, x, y, larguraLinha, y).Desenhar();
        }

        private static int EscreverLinhaTitulo(Graphics g, string texto, int tamanhoFonteTitulo, int larguraLogo, int x,
            int y, int larguraLinha)
        {
            var adicionarTexto = new AdicionarTexto(g, texto, tamanhoFonteTitulo);
            var larguraMaximaTexto = new ComprimentoMaximo((larguraLinha - larguraLogo));
            var laguraDoTexto = adicionarTexto.Medida.Largura;
            var quebrarLinha = new DefineQuebraDeLinha(adicionarTexto, larguraMaximaTexto, laguraDoTexto);
            adicionarTexto = quebrarLinha.DesenharComQuebras(g);
            var posisaoXTexto = x + larguraLogo + (((larguraLinha - larguraLogo) - adicionarTexto.Medida.Largura)/2);
            adicionarTexto.Desenhar(posisaoXTexto, y);
            y += adicionarTexto.Medida.Altura;
            return y;
        }

        private static int MensagemContingencia(Graphics g, int larguraLinha, int y)
        {
            var contingenciaTitulo = new AdicionarTexto(g, "EMITIDA EM CONTINGÊNCIA", 10);
            var restoContingenciaTituloX = (larguraLinha - contingenciaTitulo.Medida.Largura)/2;
            contingenciaTitulo.Desenhar(restoContingenciaTituloX, y);
            y += contingenciaTitulo.Medida.Altura;

            var pendenteAutorizacaoTitulo = new AdicionarTexto(g, "Pendente de Autorização", 8);
            var restoPendenteAutorizacaoTituloX = (larguraLinha - pendenteAutorizacaoTitulo.Medida.Largura)/2;
            pendenteAutorizacaoTitulo.Desenhar(restoPendenteAutorizacaoTituloX, y);
            y += pendenteAutorizacaoTitulo.Medida.Altura + 2;
            return y;
        }
    }
}