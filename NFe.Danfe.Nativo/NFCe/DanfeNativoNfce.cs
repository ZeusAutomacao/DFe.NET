/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using DFe.Utils;
using GraphicsPrinter;
using NFe.Classes;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Servicos.Download;
using NFe.Danfe.Base.NFCe;
using NFe.Utils.InformacoesSuplementares;
using NFe.Utils.NFe;
using NFeZeus = NFe.Classes.NFe;

namespace NFe.Danfe.Nativo.NFCe
{
    public class DanfeNativoNfce
    {
        private static bool _viaEstabelecimento;
        private string _cIdToken;
        private string _csc;
        private NFeZeus _nfe;
        private nfeProc _proc;
        private decimal _troco;
        private Image _logo;
        private decimal _totalPago;
        private int _y;
        private static ConfiguracaoDanfeNfce _configuracaoDanfeNfce;

        public DanfeNativoNfce(string xml, ConfiguracaoDanfeNfce configuracaoDanfe, string cIdToken, string csc,
            decimal troco = decimal.Zero, decimal totalPago = decimal.Zero, string font = null, bool viaEstabelecimento = false)
        {
            Inicializa(xml, configuracaoDanfe, cIdToken, csc, troco, totalPago, font);
        }

        private void Inicializa(string xml, ConfiguracaoDanfeNfce configuracaoDanfe, string cIdToken, string csc, decimal troco, decimal totalPago, string font = null, bool viaEstabelecimento = false)
        {
            _cIdToken = cIdToken;
            _csc = csc;
            _troco = troco;
            _totalPago = totalPago;
            _viaEstabelecimento = viaEstabelecimento;
            AdicionarTexto.FontPadrao = configuracaoDanfe.CarregarFontePadraoNfceNativa(font);
            _logo = configuracaoDanfe.ObterLogo();
            _configuracaoDanfeNfce = configuracaoDanfe;

            CarregarXml(xml);
        }

        //Função para mandar imprimir na impressora padrão
        public void Imprimir(string nomeImpressora = null, string salvarArquivoPdfEm = null)
        {
            PrintDocument printCupom = new PrintDocument();

            printCupom.PrinterSettings.PrinterName = !string.IsNullOrEmpty(nomeImpressora) ?
                    nomeImpressora : printCupom.PrinterSettings.PrinterName;

            if (!string.IsNullOrEmpty(salvarArquivoPdfEm))
            {
                printCupom.DefaultPageSettings.PrinterSettings.PrintToFile = true;
                printCupom.DefaultPageSettings.PrinterSettings.PrintFileName = salvarArquivoPdfEm;
                printCupom.PrintController = new StandardPrintController();
            }

            printCupom.PrintPage += printCupom_PrintPage;
            printCupom.Print();
        }

        private void printCupom_PrintPage(object sender, PrintPageEventArgs e)
        {
            GerarNfCe(e.Graphics);
        }

        public void GerarImagem(string filename, ImageFormat format)
        {

            // Feito esse de cima para poder pegar o tamanho real da mesma desenhando
            using (Bitmap bmp = new Bitmap(300, 70000))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    GerarNfCe(g);
                }

                // Obtive o tamanho real na posição y agora vou fazer um com tamanho exato
                using (Bitmap bmpFinal = new Bitmap(300, _y))
                {
                    using (Graphics g = Graphics.FromImage(bmpFinal))
                    {
                        g.Clear(Color.White);
                        g.DrawImage(bmp, 0, 0);
                        bmpFinal.Save(filename, format);
                    }
                }
            }
        }

        public void GerarJPEG(string filename)
        {
            GerarImagem(filename, ImageFormat.Jpeg);
        }

        public void GerarJPG(string filename)
        {
            GerarImagem(filename, ImageFormat.Png);
        }

        private void GerarNfCe(Graphics graphics)
        {
            var g = graphics;

            var larguraLogo = 64;
            const int larguraLinha = 284;
            const int larguraLinhaMargemDireita = 277;

            var x = 3;
            _y = 3;

            if (_logo != null)
            {
                new RedimensionaImagemPara(new AdicionarImagem(g, _logo, x, _y), 50, 24).Desenhar();
            }

            if (_logo == null)
            {
                larguraLogo = 0;
            }

            #region cabeçalho
            int tamanhoFonteTitulo = 6;

            string cnpjERazaoSocial = CnpjERazaoSocial();

            _y = EscreverLinhaTitulo(g, cnpjERazaoSocial, tamanhoFonteTitulo, larguraLogo, x, _y, larguraLinha);

            string enderecoEmitente = EnderecoEmitente();

            _y = EscreverLinhaTitulo(g, enderecoEmitente, tamanhoFonteTitulo, larguraLogo, x, _y, larguraLinha);

            const string mensagemGoverno = "Documento Auxiliar Da Nota Fiscal de Consumidor Eletrônica";

            _y = EscreverLinhaTitulo(g, mensagemGoverno, tamanhoFonteTitulo, larguraLogo, x, _y, larguraLinha);

            _y += 5;
            #endregion

            #region contingência
            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                LinhaHorizontal(g, x, _y, larguraLinha);
                _y += 2;

                _y = MensagemContingencia(g, larguraLinha, _y);
            }
            #endregion

            LinhaHorizontal(g, x, _y, larguraLinha);

            #region tabela de itens
            int iniX = x;

            CriaHeaderColuna("CÓDIGO", g, iniX, _y);
            iniX += 75;

            AdicionarTexto colunaDescricaoHeader = CriaHeaderColuna("DESCRIÇÃO", g, iniX, _y);
            iniX -= 25;
            _y += colunaDescricaoHeader.Medida.Altura;

            CriaHeaderColuna("QTDE", g, iniX, _y);
            iniX += 25;

            CriaHeaderColuna("UN", g, iniX, _y);
            iniX += 25;

            CriaHeaderColuna("x", g, iniX, _y);
            iniX += 20;

            AdicionarTexto colunaValorUnitarioHeader = CriaHeaderColuna("VALOR UNITÁRIO", g, iniX, _y);
            iniX += 85;

            CriaHeaderColuna("=", g, iniX, _y);
            iniX += 41;

            AdicionarTexto colunaTotalHeader = CriaHeaderColuna("TOTAL", g, iniX, _y);
            _y += colunaTotalHeader.Medida.Altura + 10;

            List<det> det = _nfe.infNFe.det;

            #region preencher itens
            foreach (det detalhe in det)
            {
                AdicionarTexto codigo = new AdicionarTexto(g, detalhe.prod.cProd, 7);
                codigo.Desenhar(x, _y);

                AdicionarTexto nome = new AdicionarTexto(g, detalhe.prod.xProd, 7);
                DefineQuebraDeLinha quebraNome = new DefineQuebraDeLinha(nome, new ComprimentoMaximo(202), nome.Medida.Largura);
                nome = quebraNome.DesenharComQuebras(g);
                nome.Desenhar(x + 75, _y);
                _y += nome.Medida.Altura;

                AdicionarTexto quantidade = new AdicionarTexto(g, detalhe.prod.qCom.ToString("N3"), 7);
                AdicionarTexto valorUnitario = new AdicionarTexto(g, detalhe.prod.vUnCom.ToString("N4"), 7);
                AdicionarTexto vezesX = new AdicionarTexto(g, "x", 7);
                AdicionarTexto unidadeSigla = new AdicionarTexto(g, detalhe.prod.uCom.Length <= 2 ? detalhe.prod.uCom : detalhe.prod.uCom.Substring(0, 2), 7);

                decimal detalheTotal = detalhe.prod.vProd;
                AdicionarTexto valorTotalProduto = new AdicionarTexto(g, detalheTotal.ToString("N2"), 7);

                iniX = x + 50;

                quantidade.Desenhar(iniX, _y);

                iniX += 25;

                unidadeSigla.Desenhar(iniX, _y);

                iniX += 25;

                vezesX.Desenhar(iniX, _y);

                iniX += 20;

                int tituloColunaUnidadeLargura = colunaValorUnitarioHeader.Medida.Largura;
                valorUnitario.Desenhar((iniX + tituloColunaUnidadeLargura) - valorUnitario.Medida.Largura, _y);


                iniX += 85;

                AdicionarTexto igualColuna = new AdicionarTexto(g, "=", 7);
                igualColuna.Desenhar(iniX, _y);

                iniX += 41;

                int tituloColunaTotal = colunaTotalHeader.Medida.Largura;
                valorTotalProduto.Desenhar((iniX + tituloColunaTotal) - valorTotalProduto.Medida.Largura, _y);

                _y += quantidade.Medida.Altura;

                decimal valorDescontoItem = detalhe.prod.vDesc ?? 0.0m;
                if (valorDescontoItem > 0.0m)
                {
                    AdicionarTexto descontoColuna = new AdicionarTexto(g, "Desconto", 7);
                    descontoColuna.Desenhar(x + 50, _y);

                    StringBuilder descontoItemTexto = new StringBuilder("-");
                    descontoItemTexto.Append(valorDescontoItem.ToString("N2"));
                    AdicionarTexto valorDescontoItemColuna = new AdicionarTexto(g, descontoItemTexto.ToString(), 7);
                    int valorDescontoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                   valorDescontoItemColuna.Medida.Largura;
                    valorDescontoItemColuna.Desenhar(valorDescontoItemColunaX, _y);

                    _y += descontoColuna.Medida.Altura;
                }

                decimal valorAcrescimoItem = detalhe.prod.vOutro ?? 0.0m;
                if (valorAcrescimoItem > 0.0m)
                {
                    AdicionarTexto acrescimoColuna = new AdicionarTexto(g, "Acréscimo", 7);
                    acrescimoColuna.Desenhar(x + 50, _y);

                    StringBuilder acrescimoItemTexto = new StringBuilder("+");
                    acrescimoItemTexto.Append(valorAcrescimoItem.ToString("N2"));
                    AdicionarTexto valorAcrescimoItemColuna = new AdicionarTexto(g, acrescimoItemTexto.ToString(), 7);
                    int valorAcrescimoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                    valorAcrescimoItemColuna.Medida.Largura;
                    valorAcrescimoItemColuna.Desenhar(valorAcrescimoItemColunaX, _y);

                    _y += acrescimoColuna.Medida.Altura;
                }

                if (valorDescontoItem > 0.0m || valorAcrescimoItem > 0.0m)
                {
                    AdicionarTexto valorLiquidoTexto = new AdicionarTexto(g, "Valor Líquido", 7);
                    valorLiquidoTexto.Desenhar(x + 50, _y);

                    AdicionarTexto valorLiquidoTotalTexto = new AdicionarTexto(g,
                        ((detalheTotal + valorAcrescimoItem) - valorDescontoItem).ToString("N2"), 7);
                    int valorLiquidoTotalTextoX = ((x + 246) + tituloColunaTotal) -
                                                  valorLiquidoTotalTexto.Medida.Largura;
                    valorLiquidoTotalTexto.Desenhar(valorLiquidoTotalTextoX, _y);

                    _y += valorLiquidoTexto.Medida.Altura;
                }
            }
            #endregion

            #endregion

            _y += 3;

            LinhaHorizontal(g, x, _y, larguraLinha);

            #region totais
            AdicionarTexto textoQuantidadeTotalItens = new AdicionarTexto(g, "Qtde. total de itens", 7);
            textoQuantidadeTotalItens.Desenhar(x, _y);

            AdicionarTexto qtdTotalItens = new AdicionarTexto(g, det.Count.ToString(), 7);
            int qtdTotalItensX = (larguraLinhaMargemDireita - qtdTotalItens.Medida.Largura);
            qtdTotalItens.Desenhar(qtdTotalItensX, _y);
            _y += textoQuantidadeTotalItens.Medida.Altura;

            AdicionarTexto textoValorTotal = new AdicionarTexto(g, "Valor total R$", 7);
            textoValorTotal.Desenhar(x, _y);

            decimal valorTotal = det.Sum(prod => prod.prod.vProd);
            AdicionarTexto valorTotalTexto = new AdicionarTexto(g, valorTotal.ToString("N2"), 7);
            int qtdValorTotalX = (larguraLinhaMargemDireita - valorTotalTexto.Medida.Largura);
            valorTotalTexto.Desenhar(qtdValorTotalX, _y);
            _y += textoValorTotal.Medida.Altura;

            decimal totalDesconto = _nfe.infNFe.total.ICMSTot.vDesc;
            decimal totalOutras = _nfe.infNFe.total.ICMSTot.vOutro;
            decimal valorTotalAPagar = valorTotal + totalOutras - totalDesconto;

            if (totalDesconto > 0)
            {
                AdicionarTexto textoDesconto = new AdicionarTexto(g, "Desconto R$", 7);
                textoDesconto.Desenhar(x, _y);

                AdicionarTexto valorDesconto = new AdicionarTexto(g, totalDesconto.ToString("N2"), 7);
                int valorDescontoX = (larguraLinhaMargemDireita - valorDesconto.Medida.Largura);
                valorDesconto.Desenhar(valorDescontoX, _y);
                _y += textoDesconto.Medida.Altura;
            }
            if (totalOutras > 0)
            {
                AdicionarTexto textoOutras = new AdicionarTexto(g, "Acréscimo R$", 7);
                textoOutras.Desenhar(x, _y);

                AdicionarTexto valorAcrescimo = new AdicionarTexto(g, totalOutras.ToString("N2"), 7);
                int valorAcrescimoX = (larguraLinhaMargemDireita - valorAcrescimo.Medida.Largura);
                valorAcrescimo.Desenhar(valorAcrescimoX, _y);
                _y += textoOutras.Medida.Altura;
            }
            if (totalDesconto > 0 || totalOutras > 0)
            {
                AdicionarTexto textoValorAPagar = new AdicionarTexto(g, "Valor a Pagar R$", 7);
                textoValorAPagar.Desenhar(x, _y);

                AdicionarTexto valorAPagar = new AdicionarTexto(g, valorTotalAPagar.ToString("N2"), 7);
                int valorAPagarX = (larguraLinhaMargemDireita - valorAPagar.Medida.Largura);
                valorAPagar.Desenhar(valorAPagarX, _y);
                _y += textoValorAPagar.Medida.Altura + 2;
            }

            AdicionarTexto tituloFormaPagamento = new AdicionarTexto(g, "FORMA PAGAMENTO", 7);
            tituloFormaPagamento.Desenhar(x, _y);

            AdicionarTexto tituloValorPago = new AdicionarTexto(g, "VALOR PAGO R$", 7);
            int tituloValorPagoX = (larguraLinhaMargemDireita - tituloValorPago.Medida.Largura);
            tituloValorPago.Desenhar(tituloValorPagoX, _y);
            _y += tituloFormaPagamento.Medida.Altura;

            foreach (pag pag in _nfe.infNFe.pag)
            {
                // v3.1
                if (pag.tPag != null)
                    AdicionaFormaPagamento(x, larguraLinhaMargemDireita, g, pag.tPag, pag.vPag);

                // v4.0
                if (pag.detPag != null)
                    foreach (var detPag in pag.detPag)
                    {
                        AdicionaFormaPagamento(x, larguraLinhaMargemDireita, g, detPag.tPag, detPag.vPag);
                    }
            }

            _y += 2;

            if (_troco > 0)
            {
                AdicionarTexto textoTroco = new AdicionarTexto(g, "Troco R$ (TOTAL PAGO R$" + _totalPago.ToString("N2") + ")", 7);
                textoTroco.Desenhar(x, _y);

                AdicionarTexto textoTrocoValor = new AdicionarTexto(g, _troco.ToString("N2"), 7);
                int textoTrocoValorX = (larguraLinhaMargemDireita - textoTrocoValor.Medida.Largura);
                textoTrocoValor.Desenhar(textoTrocoValorX, _y);
                _y += textoTroco.Medida.Altura;
            }
            #endregion

            _y += 5;

            LinhaHorizontal(g, x, _y, larguraLinha);

            #region consulta QrCode
            AdicionarTexto textoConsulteChave = new AdicionarTexto(g, "Consulte pela Chave de Acesso em", 7);
            int textoConsulteChaveX = ((larguraLinha - textoConsulteChave.Medida.Largura) / 2);
            textoConsulteChave.Desenhar(textoConsulteChaveX, _y);

            _y += textoConsulteChave.Medida.Altura;

            AdicionarTexto urlConsulta = new AdicionarTexto(g,
                string.IsNullOrEmpty(_nfe.infNFeSupl.urlChave) ? _nfe.infNFeSupl.ObterUrlConsulta(_nfe, _configuracaoDanfeNfce.VersaoQrCode) : _nfe.infNFeSupl.urlChave, 7);
            int urlConsultaX = ((larguraLinha - urlConsulta.Medida.Largura) / 2);
            urlConsulta.Desenhar(urlConsultaX, _y);

            _y += urlConsulta.Medida.Altura;

            string novaChave = GeraChaveAcesso(_nfe);

            AdicionarTexto chave = new AdicionarTexto(g, novaChave, 7);
            int urlChaveX = ((larguraLinha - chave.Medida.Largura) / 2);
            chave.Desenhar(urlChaveX, _y);

            _y += chave.Medida.Altura;
            _y += 10;
            #endregion


            var mensagemConsumidor = MontaMensagemConsumidor(_nfe.infNFe.dest);

            var consumidor = new AdicionarTexto(g, mensagemConsumidor, 9);
            var quebraLinhaConsumidor = new DefineQuebraDeLinha(
                consumidor,
                new ComprimentoMaximo(larguraLinhaMargemDireita),
                consumidor.Medida.Largura);

            consumidor = quebraLinhaConsumidor.DesenharComQuebras(g);
            var consumidorX = (larguraLinha - consumidor.Medida.Largura) / 2;
            consumidor.Desenhar(consumidorX, _y);

            _y += consumidor.Medida.Altura + 10;

            var mensagemDadosNfCe = MontaMensagemDadosNfce(_nfe);

            var dadosNfce = new AdicionarTexto(g, mensagemDadosNfCe, 7);
            var dadosNfceX = (larguraLinha - dadosNfce.Medida.Largura) / 2;
            dadosNfce.Desenhar(dadosNfceX, _y);

            _y += dadosNfce.Medida.Altura;

            if (_nfe.infNFe.ide.tpEmis == TipoEmissao.teNormal)
            {
                var textoProtocoloAutorizacao = new StringBuilder("Protocolo de autorização: ");
                textoProtocoloAutorizacao.Append(_proc.protNFe.infProt.nProt);
                var protocoloAutorizacao = new AdicionarTexto(g, textoProtocoloAutorizacao.ToString(), 7);
                int protocoloAutorizacaoX = (larguraLinha - protocoloAutorizacao.Medida.Largura) / 2;
                protocoloAutorizacao.Desenhar(protocoloAutorizacaoX, _y);
                _y += protocoloAutorizacao.Medida.Altura;


                var textoDataAutorizacao = new StringBuilder("Data de autorização ");
                textoDataAutorizacao.Append(_proc.protNFe.infProt.dhRecbto.ToString("G"));
                var dataAutorizacao = new AdicionarTexto(g, textoDataAutorizacao.ToString(), 7);
                int dataAutorizacaoX = (larguraLinha - dataAutorizacao.Medida.Largura) / 2;
                dataAutorizacao.Desenhar(dataAutorizacaoX, _y);
                _y += dataAutorizacao.Medida.Altura;
            }

            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                _y += 10;
                _y = MensagemContingencia(g, larguraLinha, _y);
            }

            _y += 8;

            var urlQrCode = ObtemUrlQrCode(_nfe, _cIdToken, _csc);

            var qrCodeImagem = QrCode.Gerar(urlQrCode);
            var qrCodeImagemX = (larguraLinha - qrCodeImagem.Size.Width) / 2;
            var desenharQrCode = new AdicionarImagem(g, qrCodeImagem, qrCodeImagemX, _y);
            desenharQrCode.Desenhar();

            _y += qrCodeImagem.Size.Height + 10;

            LinhaHorizontal(g, x, _y, larguraLinha);

            _y += 5;

            var tributosIncidentes = _nfe.infNFe.total.ICMSTot.vTotTrib;

            if (tributosIncidentes != 0)
            {
                StringBuilder mensagemTributosTotais =
                    new StringBuilder("Tributos Totais Incidentes (Lei Federal 12.741/2012): R$");
                mensagemTributosTotais.Append(tributosIncidentes.ToString("N2"));

                AdicionarTexto tributosTotais = new AdicionarTexto(g, mensagemTributosTotais.ToString(), 7);
                int tributosTotaisX = (larguraLinha - tributosTotais.Medida.Largura) / 2;
                tributosTotais.Desenhar(tributosTotaisX, _y);

                _y += tributosTotais.Medida.Altura;

                _y += 5;

                LinhaHorizontal(g, x, _y, larguraLinha);
            }


            string observacoes = string.Empty;

            if (_nfe != null)
                if (_nfe.infNFe != null)
                    if (_nfe.infNFe.infAdic != null)
                        observacoes = _nfe.infNFe.infAdic.infCpl;

            if (!string.IsNullOrEmpty(observacoes))
            {
                _y += 5;
                var observacao = new AdicionarTexto(g, observacoes, 7);
                var quebraObservacao = new DefineQuebraDeLinha(observacao,
                    new ComprimentoMaximo(larguraLinhaMargemDireita), observacao.Medida.Largura);
                observacao = quebraObservacao.DesenharComQuebras(g);
                observacao.Desenhar(x, _y);

                _y += observacao.Medida.Altura;
            }
        }

        private void AdicionaFormaPagamento(int x, int larguraLinhaMargemDireita, Graphics g, FormaPagamento? formaPagamento, decimal? vPag)
        {
            var textoFormaPagamento = new AdicionarTexto(g, ObtemDescricao(formaPagamento), 7);
            textoFormaPagamento.Desenhar(x, _y);

            var textoValorFormaPagamento = new AdicionarTexto(g, vPag.Value.ToString("N2"), 7);
            var textoValorFormaPagamentoX = (larguraLinhaMargemDireita - textoValorFormaPagamento.Medida.Largura);
            textoValorFormaPagamento.Desenhar(textoValorFormaPagamentoX, _y);

            _y += textoFormaPagamento.Medida.Altura;
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

        private string CnpjERazaoSocial()
        {
            string nomeEmpresa = string.Empty;

            emit emitente = _nfe.infNFe.emit;

            if (!string.IsNullOrEmpty(emitente.xNome))
            {
                nomeEmpresa = emitente.xNome;
            }

            if (!string.IsNullOrEmpty(emitente.xFant))
            {
                nomeEmpresa = emitente.xFant;
            }
            string cnpjERazaoSocial = string.Format("CNPJ: {0} {1}", emitente.CNPJ, nomeEmpresa);
            return cnpjERazaoSocial;
        }

        private static string ObtemUrlQrCode(NFeZeus nfce, string idToken, string csc)
        {
            var urlQrCode = nfce.infNFeSupl == null
                ? nfce.infNFeSupl.ObterUrlQrCode(nfce, _configuracaoDanfeNfce.VersaoQrCode, idToken, csc)
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
            if (_viaEstabelecimento)
                mensagem.Append("Via estabelecimento");
            else
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

        private static string GeraChaveAcesso(NFeZeus nfce)
        {
            var chaveAcesso = nfce.infNFe.Id.Substring(3);
            var novaChave = string.Empty;
            var contaChaveAcesso = 0;

            foreach (char c in chaveAcesso)
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

        private static int EscreverLinhaTitulo(Graphics g, string texto, int tamanhoFonteTitulo, int larguraLogo, int x, int y, int larguraLinha)
        {
            var adicionarTexto = new AdicionarTexto(g, texto, tamanhoFonteTitulo);
            var larguraMaximaTexto = new ComprimentoMaximo((larguraLinha - larguraLogo));
            var laguraDoTexto = adicionarTexto.Medida.Largura;
            var quebrarLinha = new DefineQuebraDeLinha(adicionarTexto, larguraMaximaTexto, laguraDoTexto);
            adicionarTexto = quebrarLinha.DesenharComQuebras(g);
            var posisaoXTexto = x + larguraLogo + (((larguraLinha - larguraLogo) - adicionarTexto.Medida.Largura) / 2);
            adicionarTexto.Desenhar(posisaoXTexto, y);
            y += adicionarTexto.Medida.Altura;

            return y;
        }

        private static int MensagemContingencia(Graphics g, int larguraLinha, int y)
        {
            var contingenciaTitulo = new AdicionarTexto(g, "EMITIDA EM CONTINGÊNCIA", 10);
            var restoContingenciaTituloX = (larguraLinha - contingenciaTitulo.Medida.Largura) / 2;
            contingenciaTitulo.Desenhar(restoContingenciaTituloX, y);
            y += contingenciaTitulo.Medida.Altura;

            var pendenteAutorizacaoTitulo = new AdicionarTexto(g, "Pendente de Autorização", 8);
            var restoPendenteAutorizacaoTituloX = (larguraLinha - pendenteAutorizacaoTitulo.Medida.Largura) / 2;
            pendenteAutorizacaoTitulo.Desenhar(restoPendenteAutorizacaoTituloX, y);
            y += pendenteAutorizacaoTitulo.Medida.Altura + 2;

            return y;
        }

        private void CarregarXml(string xml)
        {
            try
            {
                nfeProc proc = new procNFe().nfeProc.CarregarDeXmlString(xml);
                _proc = proc;
                _nfe = _proc.NFe;
            }
            catch (Exception)
            {
                try
                {
                    NFeZeus nfe = new NFeZeus().CarregarDeXmlString(xml);
                    _nfe = nfe;
                }
                catch (Exception)
                {
                    throw new ArgumentException(
                        "Ei! Verifique se seu xml está correto, pois identificamos uma falha ao tentar carregar ele.");
                }
            }
        }

        private static string ObtemDescricao(FormaPagamento? formaPagamento)
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
}