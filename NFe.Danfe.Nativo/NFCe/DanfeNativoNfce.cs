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
using System.Drawing.Printing;
using System.Linq;
using System.Text;
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
        private string _cIdToken;
        private string _csc;
        private NFeZeus _nfe;
        private nfeProc _proc;
        private decimal _troco;
        private Image _logo;
        private decimal _totalPago;

        public DanfeNativoNfce(string xml, ConfiguracaoDanfeNfce configuracaoDanfe, string cIdToken, string csc,
            decimal troco = decimal.Zero, decimal totalPago = decimal.Zero, string font = null)
        {
            Inicializa(xml, configuracaoDanfe, cIdToken, csc, troco, totalPago, font);
        }

        private void Inicializa(string xml, ConfiguracaoDanfeNfce configuracaoDanfe, string cIdToken, string csc, decimal troco, decimal totalPago, string font = null)
        {
            _cIdToken = cIdToken;
            _csc = csc;
            _troco = troco;
            _totalPago = totalPago;
            AdicionarTexto.FontPadrao = configuracaoDanfe.CarregarFontePadraoNfceNativa(font);
            _logo = configuracaoDanfe.ObterLogo();

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

        private void GerarNfCe(Graphics graphics)
        {
            Graphics g = graphics;

            const int larguraLogo = 64;
            const int larguraLinha = 284;
            const int larguraLinhaMargemDireita = 277;

            int x = 3;
            int y = 3;

            if (_logo != null)
            {
                new AdicionarImagem(g, _logo, x, y).Desenhar();
            }

#region cabeçalho
            int tamanhoFonteTitulo = 6;

            string cnpjERazaoSocial = CnpjERazaoSocial();

            y = EscreverLinhaTitulo(g, cnpjERazaoSocial, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            string enderecoEmitente = EnderecoEmitente();

            y = EscreverLinhaTitulo(g, enderecoEmitente, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            const string mensagemGoverno = "Documento Auxiliar Da Nota Fiscal de Consumidor Eletrônica";

            y = EscreverLinhaTitulo(g, mensagemGoverno, tamanhoFonteTitulo, larguraLogo, x, y, larguraLinha);

            y += 5;
            #endregion

#region contingência
            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                LinhaHorizontal(g, x, y, larguraLinha);
                y += 2;

                y = MensagemContingencia(g, larguraLinha, y);
            }
#endregion

            LinhaHorizontal(g, x, y, larguraLinha);

#region tabela de itens
            int iniX = x;

            CriaHeaderColuna("CÓDIGO", g, iniX, y);
            iniX += 50;

            AdicionarTexto colunaDescricaoHeader = CriaHeaderColuna("DESCRIÇÃO", g, iniX, y);
            y += colunaDescricaoHeader.Medida.Altura;

            CriaHeaderColuna("QTDE", g, iniX, y);
            iniX += 25;

            CriaHeaderColuna("UN", g, iniX, y);
            iniX += 25;

            CriaHeaderColuna("x", g, iniX, y);
            iniX += 20;

            AdicionarTexto colunaValorUnitarioHeader = CriaHeaderColuna("VALOR UNITÁRIO", g, iniX, y);
            iniX += 85;

            CriaHeaderColuna("=", g, iniX, y);
            iniX += 41;

            AdicionarTexto colunaTotalHeader = CriaHeaderColuna("TOTAL", g, iniX, y);
            y += colunaTotalHeader.Medida.Altura + 10;

            List<det> det = _nfe.infNFe.det;

#region preencher itens
            foreach (det detalhe in det)
            {
                AdicionarTexto codigo = new AdicionarTexto(g, detalhe.prod.cProd, 7);
                codigo.Desenhar(x, y);

                AdicionarTexto nome = new AdicionarTexto(g, detalhe.prod.xProd, 7);
                DefineQuebraDeLinha quebraNome = new DefineQuebraDeLinha(nome, new ComprimentoMaximo(227), nome.Medida.Largura);
                nome = quebraNome.DesenharComQuebras(g);
                nome.Desenhar(x + 50, y);
                y += nome.Medida.Altura;

                AdicionarTexto quantidade = new AdicionarTexto(g, detalhe.prod.qCom.ToString("N3"), 7);
                AdicionarTexto valorUnitario = new AdicionarTexto(g, detalhe.prod.vUnCom.ToString("N4"), 7);
                AdicionarTexto vezesX = new AdicionarTexto(g, "x", 7);
                AdicionarTexto unidadeSigla = new AdicionarTexto(g, detalhe.prod.uCom.Substring(0, 2), 7);

                decimal detalheTotal = detalhe.prod.vProd;
                AdicionarTexto valorTotalProduto = new AdicionarTexto(g, detalheTotal.ToString("N2"), 7);

                iniX = x + 50;

                quantidade.Desenhar(iniX, y);

                iniX += 25;

                unidadeSigla.Desenhar(iniX, y);

                iniX += 25;

                vezesX.Desenhar(iniX, y);

                iniX += 20;

                int tituloColunaUnidadeLargura = colunaValorUnitarioHeader.Medida.Largura;
                valorUnitario.Desenhar((iniX + tituloColunaUnidadeLargura) - valorUnitario.Medida.Largura, y);


                iniX += 85;

                AdicionarTexto igualColuna = new AdicionarTexto(g, "=", 7);
                igualColuna.Desenhar(iniX, y);

                iniX += 41;

                int tituloColunaTotal = colunaTotalHeader.Medida.Largura;
                valorTotalProduto.Desenhar((iniX + tituloColunaTotal) - valorTotalProduto.Medida.Largura, y);

                y += quantidade.Medida.Altura;

                decimal valorDescontoItem = detalhe.prod.vDesc ?? 0.0m;
                if (valorDescontoItem > 0.0m)
                {
                    AdicionarTexto descontoColuna = new AdicionarTexto(g, "Desconto", 7);
                    descontoColuna.Desenhar(x + 50, y);

                    StringBuilder descontoItemTexto = new StringBuilder("-");
                    descontoItemTexto.Append(valorDescontoItem.ToString("N2"));
                    AdicionarTexto valorDescontoItemColuna = new AdicionarTexto(g, descontoItemTexto.ToString(), 7);
                    int valorDescontoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                   valorDescontoItemColuna.Medida.Largura;
                    valorDescontoItemColuna.Desenhar(valorDescontoItemColunaX, y);

                    y += descontoColuna.Medida.Altura;
                }

                decimal valorAcrescimoItem = detalhe.prod.vOutro ?? 0.0m;
                if (valorAcrescimoItem > 0.0m)
                {
                    AdicionarTexto acrescimoColuna = new AdicionarTexto(g, "Acréscimo", 7);
                    acrescimoColuna.Desenhar(x + 50, y);

                    StringBuilder acrescimoItemTexto = new StringBuilder("+");
                    acrescimoItemTexto.Append(valorAcrescimoItem.ToString("N2"));
                    AdicionarTexto valorAcrescimoItemColuna = new AdicionarTexto(g, acrescimoItemTexto.ToString(), 7);
                    int valorAcrescimoItemColunaX = ((x + 246) + tituloColunaTotal) -
                                                    valorAcrescimoItemColuna.Medida.Largura;
                    valorAcrescimoItemColuna.Desenhar(valorAcrescimoItemColunaX, y);

                    y += acrescimoColuna.Medida.Altura;
                }

                if (valorDescontoItem > 0.0m || valorAcrescimoItem > 0.0m)
                {
                    AdicionarTexto valorLiquidoTexto = new AdicionarTexto(g, "Valor Líquido", 7);
                    valorLiquidoTexto.Desenhar(x + 50, y);

                    AdicionarTexto valorLiquidoTotalTexto = new AdicionarTexto(g,
                        ((detalheTotal + valorAcrescimoItem) - valorDescontoItem).ToString("N2"), 7);
                    int valorLiquidoTotalTextoX = ((x + 246) + tituloColunaTotal) -
                                                  valorLiquidoTotalTexto.Medida.Largura;
                    valorLiquidoTotalTexto.Desenhar(valorLiquidoTotalTextoX, y);

                    y += valorLiquidoTexto.Medida.Altura;
                }
            }
#endregion

            #endregion

            y += 3;

            LinhaHorizontal(g, x, y, larguraLinha);

            #region totais
            AdicionarTexto textoQuantidadeTotalItens = new AdicionarTexto(g, "Qtde. total de itens", 7);
            textoQuantidadeTotalItens.Desenhar(x, y);

            AdicionarTexto qtdTotalItens = new AdicionarTexto(g, det.Count.ToString(), 7);
            int qtdTotalItensX = (larguraLinhaMargemDireita - qtdTotalItens.Medida.Largura);
            qtdTotalItens.Desenhar(qtdTotalItensX, y);
            y += textoQuantidadeTotalItens.Medida.Altura;

            AdicionarTexto textoValorTotal = new AdicionarTexto(g, "Valor total R$", 7);
            textoValorTotal.Desenhar(x, y);

            decimal valorTotal = det.Sum(prod => prod.prod.vProd);
            AdicionarTexto valorTotalTexto = new AdicionarTexto(g, valorTotal.ToString("N2"), 7);
            int qtdValorTotalX = (larguraLinhaMargemDireita - valorTotalTexto.Medida.Largura);
            valorTotalTexto.Desenhar(qtdValorTotalX, y);
            y += textoValorTotal.Medida.Altura;

            decimal totalDesconto = det.Sum(prod => prod.prod.vDesc) ?? 0.0m;
            decimal totalOutras = det.Sum(prod => prod.prod.vOutro) ?? 0.0m;
            decimal valorTotalAPagar = valorTotal + totalOutras - totalDesconto;

            if (totalDesconto > 0)
            {
                AdicionarTexto textoDesconto = new AdicionarTexto(g, "Desconto R$", 7);
                textoDesconto.Desenhar(x, y);

                AdicionarTexto valorDesconto = new AdicionarTexto(g, totalDesconto.ToString("N2"), 7);
                int valorDescontoX = (larguraLinhaMargemDireita - valorDesconto.Medida.Largura);
                valorDesconto.Desenhar(valorDescontoX, y);
                y += textoDesconto.Medida.Altura;

                AdicionarTexto textoValorAPagar = new AdicionarTexto(g, "Valor a Pagar R$", 7);
                textoValorAPagar.Desenhar(x, y);

                AdicionarTexto valorAPagar = new AdicionarTexto(g, valorTotalAPagar.ToString("N2"), 7);
                int valorAPagarX = (larguraLinhaMargemDireita - valorAPagar.Medida.Largura);
                valorAPagar.Desenhar(valorAPagarX, y);
                y += textoValorAPagar.Medida.Altura + 2;
            }

            AdicionarTexto tituloFormaPagamento = new AdicionarTexto(g, "FORMA PAGAMENTO", 7);
            tituloFormaPagamento.Desenhar(x, y);

            AdicionarTexto tituloValorPago = new AdicionarTexto(g, "VALOR PAGO R$", 7);
            int tituloValorPagoX = (larguraLinhaMargemDireita - tituloValorPago.Medida.Largura);
            tituloValorPago.Desenhar(tituloValorPagoX, y);
            y += tituloFormaPagamento.Medida.Altura;


            foreach (pag pag in _nfe.infNFe.pag)
            {
                AdicionarTexto textoFormaPagamento = new AdicionarTexto(g, ObtemDescricao(pag), 7);
                textoFormaPagamento.Desenhar(x, y);

                AdicionarTexto textoValorFormaPagamento = new AdicionarTexto(g, pag.vPag.ToString("N2"), 7);
                int textoValorFormaPagamentoX = (larguraLinhaMargemDireita - textoValorFormaPagamento.Medida.Largura);
                textoValorFormaPagamento.Desenhar(textoValorFormaPagamentoX, y);

                y += textoFormaPagamento.Medida.Altura;
            }

            y += 2;

            if (_troco > 0)
            {
                AdicionarTexto textoTroco = new AdicionarTexto(g, "Troco R$ (TOTAL PAGO R$" + _totalPago.ToString("N2") + ")", 7);
                textoTroco.Desenhar(x, y);

                AdicionarTexto textoTrocoValor = new AdicionarTexto(g, _troco.ToString("N2"), 7);
                int textoTrocoValorX = (larguraLinhaMargemDireita - textoTrocoValor.Medida.Largura);
                textoTrocoValor.Desenhar(textoTrocoValorX, y);
                y += textoTroco.Medida.Altura;
            }
#endregion

            y += 5;

            LinhaHorizontal(g, x, y, larguraLinha);

            #region consulta QrCode
            AdicionarTexto textoConsulteChave = new AdicionarTexto(g, "Consulte pela Chave de Acesso em", 7);
            int textoConsulteChaveX = ((larguraLinha - textoConsulteChave.Medida.Largura)/2);
            textoConsulteChave.Desenhar(textoConsulteChaveX, y);

            y += textoConsulteChave.Medida.Altura;

            AdicionarTexto urlConsulta = new AdicionarTexto(g,
                _nfe.infNFeSupl.ObterUrl(_nfe.infNFe.ide.tpAmb, _nfe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlQrCode),
                7);
            int urlConsultaX = ((larguraLinha - urlConsulta.Medida.Largura)/2);
            urlConsulta.Desenhar(urlConsultaX, y);

            y += urlConsulta.Medida.Altura;

            string novaChave = GeraChaveAcesso(_nfe);

            AdicionarTexto chave = new AdicionarTexto(g, novaChave, 7);
            int urlChaveX = ((larguraLinha - chave.Medida.Largura)/2);
            chave.Desenhar(urlChaveX, y);

            y += chave.Medida.Altura;
            y += 10;
#endregion


            string mensagemConsumidor = MontaMensagemConsumidor(_nfe.infNFe.dest);

            AdicionarTexto consumidor = new AdicionarTexto(g, mensagemConsumidor, 9);
            DefineQuebraDeLinha quebraLinhaConsumidor = new DefineQuebraDeLinha(consumidor,
                new ComprimentoMaximo(larguraLinhaMargemDireita), consumidor.Medida.Largura);
            consumidor = quebraLinhaConsumidor.DesenharComQuebras(g);
            int consumidorX = (larguraLinha - consumidor.Medida.Largura)/2;
            consumidor.Desenhar(consumidorX, y);

            y += consumidor.Medida.Altura + 10;

            string mensagemDadosNfCe = MontaMensagemDadosNfce(_nfe);

            AdicionarTexto dadosNfce = new AdicionarTexto(g, mensagemDadosNfCe, 7);
            int dadosNfceX = (larguraLinha - dadosNfce.Medida.Largura)/2;
            dadosNfce.Desenhar(dadosNfceX, y);

            y += dadosNfce.Medida.Altura;

            if (_nfe.infNFe.ide.tpEmis == TipoEmissao.teNormal)
            {
                StringBuilder textoProtocoloAutorizacao = new StringBuilder("Protocolo de autorização: ");
                textoProtocoloAutorizacao.Append(_proc.protNFe.infProt.nProt);
                AdicionarTexto protocoloAutorizacao = new AdicionarTexto(g, textoProtocoloAutorizacao.ToString(), 7);
                int protocoloAutorizacaoX = (larguraLinha - protocoloAutorizacao.Medida.Largura)/2;
                protocoloAutorizacao.Desenhar(protocoloAutorizacaoX, y);
                y += protocoloAutorizacao.Medida.Altura;


                StringBuilder textoDataAutorizacao = new StringBuilder("Data de autorização ");
                textoDataAutorizacao.Append(_proc.protNFe.infProt.dhRecbto.ToString("G"));
                AdicionarTexto dataAutorizacao = new AdicionarTexto(g, textoDataAutorizacao.ToString(), 7);
                int dataAutorizacaoX = (larguraLinha - dataAutorizacao.Medida.Largura)/2;
                dataAutorizacao.Desenhar(dataAutorizacaoX, y);
                y += dataAutorizacao.Medida.Altura;
            }

            if (_nfe.infNFe.ide.tpEmis != TipoEmissao.teNormal)
            {
                y += 10;
                y = MensagemContingencia(g, larguraLinha, y);
            }

            y += 8;

            string urlQrCode = ObtemUrlQrCode(_nfe, _cIdToken, _csc);

            Image qrCodeImagem = QrCode.Gerar(urlQrCode);
            int qrCodeImagemX = (larguraLinha - qrCodeImagem.Size.Width)/2;
            AdicionarImagem desenharQrCode = new AdicionarImagem(g, qrCodeImagem, qrCodeImagemX, y);
            desenharQrCode.Desenhar();

            y += qrCodeImagem.Size.Height + 10;

            LinhaHorizontal(g, x, y, larguraLinha);

            y += 5;

            decimal tributosIncidentes = _nfe.infNFe.total.ICMSTot.vTotTrib;


            if (tributosIncidentes != 0)
            {
                StringBuilder mensagemTributosTotais =
                    new StringBuilder("Tributos Totais Incidentes (Lei Federal 12.741/2012): R$");
                mensagemTributosTotais.Append(tributosIncidentes.ToString("N2"));

                AdicionarTexto tributosTotais = new AdicionarTexto(g, mensagemTributosTotais.ToString(), 7);
                int tributosTotaisX = (larguraLinha - tributosTotais.Medida.Largura)/2;
                tributosTotais.Desenhar(tributosTotaisX, y);

                y += tributosTotais.Medida.Altura;

                y += 5;

                LinhaHorizontal(g, x, y, larguraLinha);
            }


            string observacoes = string.Empty;

            if (_nfe != null)
                if(_nfe.infNFe != null)
                    if (_nfe.infNFe.infAdic != null)
                        observacoes = _nfe.infNFe.infAdic.infCpl;

            if (!string.IsNullOrEmpty(observacoes))
            {
                y += 5;
                AdicionarTexto observacao = new AdicionarTexto(g, observacoes, 7);
                DefineQuebraDeLinha quebraObservacao = new DefineQuebraDeLinha(observacao,
                    new ComprimentoMaximo(larguraLinhaMargemDireita), observacao.Medida.Largura);
                observacao = quebraObservacao.DesenharComQuebras(g);
                observacao.Desenhar(x, y);
            }
        }

        private string EnderecoEmitente()
        {
            enderEmit enderEmit = _nfe.infNFe.emit.enderEmit;

            string foneEmit = string.Empty;

            if (enderEmit.fone != null)
            {
                StringBuilder fone = new StringBuilder(" - FONE: ");
                fone.Append(enderEmit.fone);
                foneEmit = fone.ToString();
            }


            StringBuilder enderecoEmitenteBuilder = new StringBuilder();
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
            string urlQrCode = nfce.infNFeSupl == null
                ? nfce.infNFeSupl.ObterUrlQrCode(nfce,
                    idToken,
                    csc)
                : nfce.infNFeSupl.qrCode;
            return urlQrCode;
        }

        private static string MontaMensagemDadosNfce(NFeZeus nfce)
        {
            StringBuilder mensagem = new StringBuilder("NFC-e nº ");
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
            StringBuilder mensagem = new StringBuilder("CONSUMIDOR ");

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

            enderDest enderecoDest = dest.enderDest;

            if (enderecoDest == null) return mensagem.ToString().Replace(", ,", ", ");

            string rua = string.Empty;
            if (!string.IsNullOrEmpty(enderecoDest.xLgr))
                rua = enderecoDest.xLgr;

            string numero = "S/N";
            if (!string.IsNullOrEmpty(enderecoDest.nro))
                numero = enderecoDest.nro;

            string bairro = string.Empty;
            if (!string.IsNullOrEmpty(enderecoDest.xBairro))
                bairro = enderecoDest.xBairro;

            string cidade = string.Empty;
            if (!string.IsNullOrEmpty(enderecoDest.xMun))
                bairro = enderecoDest.xMun;

            string siglaUf = string.Empty;
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
            string chaveAcesso = nfce.infNFe.Id.Substring(3);
            string novaChave = string.Empty;
            int contaChaveAcesso = 0;

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
            AdicionarTexto coluna = new AdicionarTexto(graphics, texto, 7);
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
            AdicionarTexto adicionarTexto = new AdicionarTexto(g, texto, tamanhoFonteTitulo);
            ComprimentoMaximo larguraMaximaTexto = new ComprimentoMaximo((larguraLinha - larguraLogo));
            int laguraDoTexto = adicionarTexto.Medida.Largura;
            DefineQuebraDeLinha quebrarLinha = new DefineQuebraDeLinha(adicionarTexto, larguraMaximaTexto, laguraDoTexto);
            adicionarTexto = quebrarLinha.DesenharComQuebras(g);
            int posisaoXTexto = x + larguraLogo + (((larguraLinha - larguraLogo) - adicionarTexto.Medida.Largura)/2);
            adicionarTexto.Desenhar(posisaoXTexto, y);
            y += adicionarTexto.Medida.Altura;
            return y;
        }

        private static int MensagemContingencia(Graphics g, int larguraLinha, int y)
        {
            AdicionarTexto contingenciaTitulo = new AdicionarTexto(g, "EMITIDA EM CONTINGÊNCIA", 10);
            int restoContingenciaTituloX = (larguraLinha - contingenciaTitulo.Medida.Largura)/2;
            contingenciaTitulo.Desenhar(restoContingenciaTituloX, y);
            y += contingenciaTitulo.Medida.Altura;

            AdicionarTexto pendenteAutorizacaoTitulo = new AdicionarTexto(g, "Pendente de Autorização", 8);
            int restoPendenteAutorizacaoTituloX = (larguraLinha - pendenteAutorizacaoTitulo.Medida.Largura)/2;
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

        private static string ObtemDescricao(pag pag)
        {
            switch (pag.tPag)
            {
                case FormaPagamento.fpDinheiro:
                    return "Dinheiro";
                case FormaPagamento.fpCheque:
                    return "Cheque";
                case FormaPagamento.fpCartaoCredito:
                    return "Cartão de Crédito";
                case FormaPagamento.fpCartaoDebito:
                    return "Cartão de Débito";
                case FormaPagamento.fpCreditoLoja:
                    return "Crédito Loja";
                case FormaPagamento.fpValeAlimentacao:
                    return "Vale Alimentação";
                case FormaPagamento.fpValeRefeicao:
                    return "Vale Refeição";
                case FormaPagamento.fpValePresente:
                    return "Vale Presente";
                case FormaPagamento.fpValeCombustivel:
                    return "Vale Combustível";
                case FormaPagamento.fpOutro:
                    return "Outros";
                default: throw new ArgumentException("Forma pagamento inválida");

            }
        }
    }
}