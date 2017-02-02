using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using NFe.Classes;
using NFe.Danfe.Base.Fontes;
using NFe.Danfe.Base.NFCe;
using NFe.Danfe.Base.Properties;

namespace NFe.Danfe.Nativo.NFCe
{
    public class DanfeNativoNfce
    {
        private readonly nfeProc _proc;
        private readonly ConfiguracaoDanfeNfce _configuracaoDanfeNfce;
        private readonly string _cIdToken;
        private readonly string _csc;

        public DanfeNativoNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc)
        {
            _proc = proc;
            _configuracaoDanfeNfce = configuracaoDanfeNfce;
            _cIdToken = cIdToken;
            _csc = csc;
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

        public Graphics GerarNfCe(Graphics graphics)
        {
            #region Define fonte e parâmetros iniciais

            PrivateFontCollection colecaoDeFontes; //todo dispose na coleção
            var openSans = Fonte.CarregarDeByteArray(Resources.OpenSans_CondBold, out colecaoDeFontes);

            var fonte = new Font(openSans, 12);
            const int largImagem = 100;
            const int larguraLinha = 284;
            const int espacamentoVertical = 10;
            const int x = 3;
            var y = 3;

            #endregion

            #region Adiciona Logotipo. Se disponível nas configurações

            var logo = _configuracaoDanfeNfce.ObterLogo();
            if (logo != null)
            {
                var centroX = (larguraLinha - (logo.Width / 2))/2;
                graphics.DrawImage(logo, centroX, y + espacamentoVertical, logo.Width, logo.Height);
                y += logo.Height + espacamentoVertical;
            }

            #endregion
            
            #region Adiciona Dados Do Cabeçalho

            var razaoSocial = _proc.NFe.infNFe.emit.xNome;
            var tpMedidaLinha = MedidasLinha(razaoSocial, fonte);
            var larguraEscrita = tpMedidaLinha.Item1;
            var alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(razaoSocial, fonte, Brushes.Black, new PointF(x + largImagem + (larguraLinha - largImagem - larguraEscrita) / 2, y));
            y += alturaEscrita;
            y += 8;

            fonte = new Font(openSans, 7);
            var cnpj = "CNPJ: " + _proc.NFe.infNFe.emit.CNPJ;
            tpMedidaLinha = MedidasLinha(cnpj, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(cnpj, fonte, Brushes.Black, new PointF(x + largImagem + (((larguraLinha - largImagem) - larguraEscrita) / 2), y));
            y += alturaEscrita;

            fonte = new Font(openSans, 7);
            var logradouro = _proc.NFe.infNFe.emit.enderEmit.xLgr + " " + _proc.NFe.infNFe.emit.enderEmit.nro + ", " + _proc.NFe.infNFe.emit.enderEmit.xCpl + ", Bairro: " + _proc.NFe.infNFe.emit.enderEmit.xBairro + ", " +
                             _proc.NFe.infNFe.emit.enderEmit.xMun + ", " + _proc.NFe.infNFe.emit.enderEmit.UF + " - Fone: " + _proc.NFe.infNFe.emit.enderEmit.fone;
            logradouro = logradouro.Replace(", ,", ", ");
            tpMedidaLinha = MedidasLinha(logradouro, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            if (larguraEscrita > (larguraLinha - largImagem))
            {
                var tpQuebraLinha = QuebraDeLinha(logradouro, (larguraLinha - largImagem), fonte);
                logradouro = tpQuebraLinha.Item1;
            }

            var linhas = logradouro.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF(x + largImagem + (((larguraLinha - largImagem) - larguraEscrita) / 2), y));
                y += alturaEscrita;
            }

            y += 5;
            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
            y += 2;

            #endregion

            var msgDocAuxiliar = "DOCUMENTO AUXILIAR DA NOTA FISCAL DE CONSUMIDOR ELETRÔNICA.";
            tpMedidaLinha = MedidasLinha(msgDocAuxiliar, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(msgDocAuxiliar, larguraLinha, fonte);
                msgDocAuxiliar = tpQuebraLinha.Item1;
            }

            linhas = msgDocAuxiliar.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }

            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);

            fonte = new Font(openSans, 7);

            #region Cria Colunas Para Descrever Produtos

            var largColuna = new Dictionary<string, int>
            {
                {"COD", 23},
                {"DESCRIÇÃO", 143},
                {"QTD", 20},
                {"UN", 17},
                {"VL.UNIT.", 37},
                {"TOTAL", 37}
            };

            var iniX = x;
            var cod = "COD";
            MedidasLinha(cod, fonte);
            graphics.DrawString(cod, fonte, Brushes.Black, new PointF(iniX, y));
            iniX += largColuna["COD"];

            var descricao = "DESCRIÇÃO";
            MedidasLinha(descricao, fonte);
            graphics.DrawString(descricao, fonte, Brushes.Black, new PointF(iniX, y));
            iniX += largColuna["DESCRIÇÃO"];

            var qut = "QUT";
            tpMedidaLinha = MedidasLinha(qut, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            graphics.DrawString(qut, fonte, Brushes.Black, new PointF((iniX + largColuna["QTD"]) - larguraEscrita, y));
            iniX += largColuna["QTD"];

            var un = "UN";
            tpMedidaLinha = MedidasLinha(un, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            graphics.DrawString(un, fonte, Brushes.Black, new PointF((iniX + largColuna["UN"]) - larguraEscrita, y));
            iniX += largColuna["UN"];

            var vlUnit = "VL.UNIT.";
            tpMedidaLinha = MedidasLinha(vlUnit, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(vlUnit, fonte, Brushes.Black, new PointF((iniX + largColuna["VL.UNIT."]) - larguraEscrita, y));
            iniX += largColuna["VL.UNIT."];

            var total = "TOTAL";
            tpMedidaLinha = MedidasLinha("TOTAL", fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            graphics.DrawString(total, fonte, Brushes.Black, new PointF((iniX + largColuna["TOTAL"]) - larguraEscrita, y));
            y += alturaEscrita;

            decimal qutTotalItens = 0;
            decimal valorTotal = 0;
            decimal desconto = 0;

            #endregion

            #region Adiciona Produtos

            foreach (var det in _proc.NFe.infNFe.det)
            {
                iniX = x;
                cod = det.prod.cProd;
                MedidasLinha(cod, fonte);
                graphics.DrawString(cod, fonte, Brushes.Black, new PointF(iniX, y));
                iniX += largColuna["COD"];

                descricao = det.prod.xProd;
                tpMedidaLinha = MedidasLinha(descricao, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                if (larguraEscrita > largColuna["DESCRIÇÃO"])
                {
                    var tpQuebraLinha = QuebraDeLinha(descricao, largColuna["DESCRIÇÃO"], fonte);
                    descricao = tpQuebraLinha.Item1;
                }

                linhas = descricao.Split('\n');
                var n = 0;
                foreach (var lin in linhas)
                {
                    n++;
                    tpMedidaLinha = MedidasLinha(lin, fonte);
                    alturaEscrita = tpMedidaLinha.Item2;
                    graphics.DrawString(lin, fonte, Brushes.Black, new PointF(iniX, y));
                    if (n < linhas.Length)
                    {
                        y += alturaEscrita;
                    }
                }
                iniX += largColuna["DESCRIÇÃO"];
                n--;

                qut = det.prod.qCom.ToString("0");
                tpMedidaLinha = MedidasLinha(qut, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(qut, fonte, Brushes.Black, new PointF((iniX + largColuna["QTD"]) - larguraEscrita, y - (alturaEscrita * n)));
                iniX += largColuna["QTD"];

                un = det.prod.uCom;
                tpMedidaLinha = MedidasLinha(un, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(un, fonte, Brushes.Black, new PointF((iniX + largColuna["UN"]) - larguraEscrita, y - (alturaEscrita * n)));
                iniX += largColuna["UN"];

                vlUnit = det.prod.vUnCom.ToString("0.00");
                tpMedidaLinha = MedidasLinha(vlUnit, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(vlUnit, fonte, Brushes.Black, new PointF((iniX + largColuna["VL.UNIT."]) - larguraEscrita, y - (alturaEscrita * n)));
                iniX += largColuna["VL.UNIT."];

                total = det.prod.vProd.ToString("0.00");
                tpMedidaLinha = MedidasLinha(total, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(total, fonte, Brushes.Black, new PointF((iniX + largColuna["TOTAL"]) - larguraEscrita, y - (alturaEscrita * n)));

                y += alturaEscrita;

                qutTotalItens = qutTotalItens + det.prod.qCom;
                valorTotal = valorTotal + det.prod.vProd;
                desconto = desconto + Convert.ToDecimal(det.prod.vDesc);
            }
            var valorPagar = valorTotal - desconto;

            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
            y += 3;

                #endregion

            //Adiciona Detalhes Da Venda
            fonte = new Font(openSans, 8);
            graphics.DrawString("QTD TOTAL DE ITENS", fonte, Brushes.Black, new PointF(x, y));

            tpMedidaLinha = MedidasLinha(qutTotalItens.ToString("0"), fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(qutTotalItens.ToString("0"), fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
            y += alturaEscrita;

            graphics.DrawString("VALOR TOTAL R$", fonte, Brushes.Black, new PointF(x, y));

            tpMedidaLinha = MedidasLinha(valorTotal.ToString("0.00"), fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(valorTotal.ToString("0.00"), fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
            y += alturaEscrita;

            if (desconto > 0) //Imprime somente quando houver desconto
            {
                graphics.DrawString("DESCONTO R$", fonte, Brushes.Black, new PointF(x, y));

                tpMedidaLinha = MedidasLinha(desconto.ToString("0.00"), fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(desconto.ToString("0.00"), fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
                y += alturaEscrita;

                graphics.DrawString("VALOR A PAGAR R$", fonte, Brushes.Black, new PointF(x, y));

                tpMedidaLinha = MedidasLinha(valorPagar.ToString("0.00"), fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(valorPagar.ToString("0.00"), fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
                y += alturaEscrita + 10;
            }

            graphics.DrawString("FORMA DE PAGAMENTO", fonte, Brushes.Black, new PointF(x, y));

            tpMedidaLinha = MedidasLinha("VALOR PAGO", fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString("VALOR PAGO", fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
            y += alturaEscrita;

            var troco = "0,00";
            var infTributos = "";
            //Prepara Informação Adicional, Para Adicionar No Final Do Cupom
            if (_proc.NFe.infNFe.infAdic != null)
            {
                linhas = _proc.NFe.infNFe.infAdic.infCpl.Split('|');
                foreach (var lin in linhas)
                {
                    if (lin.Contains("TROCO"))
                    {
                        troco = lin.Substring(lin.IndexOf(":", StringComparison.Ordinal) + 2);
                    }
                    else if (lin.Contains("Tributos Totais Incidentes"))
                    {
                        infTributos = lin;
                    }
                }
            }

            //Adiciona Descrição Do Pagamento
            foreach (var pag in _proc.NFe.infNFe.pag)
            {
                var formaPagamento = EnumHelper<Classes.Informacoes.Pagamento.FormaPagamento>.GetEnumDescription(pag.tPag.ToString());
                graphics.DrawString(formaPagamento, fonte, Brushes.Black, new PointF(x, y));

                var valorPagamento = pag.vPag.ToString("0.00");
                if (formaPagamento == "Dinheiro" && troco != "0,00")
                {
                    valorPagamento = (Convert.ToDecimal(valorPagamento) + Convert.ToDecimal(troco)).ToString("0.00");
                }

                tpMedidaLinha = MedidasLinha(valorPagamento, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(valorPagamento, fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));
                y += alturaEscrita;
            }

            if (troco != "0,00") //Imprime somente quando houver Troco
            {
                graphics.DrawString("Troco R$", fonte, Brushes.Black, new PointF(x, y));

                tpMedidaLinha = MedidasLinha(troco, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(troco, fonte, Brushes.Black, new PointF(larguraLinha - larguraEscrita, y));

                y += alturaEscrita + 5;
            }

            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
            y += 2;

            //Adiciona Detalhes Do Cupom
            var msgConsulta = "Consulte Pela Chave De Acesso Em www.sefaz.rs.gov.br/NFCE";
            tpMedidaLinha = MedidasLinha(msgConsulta, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(msgConsulta, larguraLinha, fonte);
                msgConsulta = tpQuebraLinha.Item1;
            }

            linhas = msgConsulta.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }

            var nProc = _proc.NFe.infNFe.Id.Substring(3);
            tpMedidaLinha = MedidasLinha(nProc, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(nProc, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
            y += alturaEscrita;
            y += 5;

            //Adiciona Identificação Do Consumidor
            var infconsumidor = "";
            try
            {
                infconsumidor = _proc.NFe.infNFe.dest.CPF;
                if (_proc.NFe.infNFe.dest.CPF == "")
                {
                    infconsumidor = "CONSUMIDOR NÃO IDENTIFICADO";
                }
                else
                {
                    infconsumidor = "CONSUMIDOR CPF: " + infconsumidor;
                }
            }
            catch
            {
                infconsumidor = "CONSUMIDOR NÃO IDENTIFICADO";
            }

            fonte = new Font(openSans, 10);
            tpMedidaLinha = MedidasLinha(infconsumidor, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            graphics.DrawString(infconsumidor, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
            y += alturaEscrita;
            y += 8;

            //Adiciona Dados Do Protocolo
            fonte = new Font(openSans, 8);
            var nCupom = "NFC-e Nº C" + _proc.NFe.infNFe.ide.nNF + " Série " + _proc.NFe.infNFe.ide.serie + " Emissão " +
                Convert.ToDateTime(_proc.NFe.infNFe.ide.dhEmi).ToShortDateString() + " " + Convert.ToDateTime(_proc.NFe.infNFe.ide.dhEmi).ToShortTimeString();
            tpMedidaLinha = MedidasLinha(nCupom, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(nCupom, larguraLinha, fonte);
                nCupom = tpQuebraLinha.Item1;
            }

            linhas = nCupom.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }

            var nProtocolo = "Protocolo De Autorização: " + _proc.protNFe.infProt.nProt;
            tpMedidaLinha = MedidasLinha(nProtocolo, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(nProtocolo, larguraLinha, fonte);
                nProtocolo = tpQuebraLinha.Item1;
            }

            linhas = nProtocolo.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }

            var dataAutorizacao = "Data De Autorização: " + Convert.ToDateTime(_proc.protNFe.infProt.dhRecbto).ToShortDateString() + " " +
                                     Convert.ToDateTime(_proc.protNFe.infProt.dhRecbto).ToShortTimeString();
            tpMedidaLinha = MedidasLinha(dataAutorizacao, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(dataAutorizacao, larguraLinha, fonte);
                dataAutorizacao = tpQuebraLinha.Item1;
            }

            linhas = dataAutorizacao.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }

            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
            y += 10;

            //Adiciona QrCode
            var qrCode = GerarQrCode(130, 130, _proc.NFe.infNFeSupl == null ? Utils.InformacoesSuplementares.ExtinfNFeSupl.ObterUrlQrCode(_proc.NFe.infNFeSupl, _proc.NFe, _cIdToken, _csc) : _proc.NFe.infNFeSupl.qrCode);
            graphics.DrawImage(qrCode, new Point(x + (larguraLinha - qrCode.Size.Width) / 2, y));
            y += qrCode.Size.Height;
            y += 10;

            graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
            y += 5;

            var impIncidentes = infTributos;
            tpMedidaLinha = MedidasLinha(impIncidentes, fonte);
            larguraEscrita = tpMedidaLinha.Item1;
            alturaEscrita = tpMedidaLinha.Item2;
            if (larguraEscrita > larguraLinha)
            {
                var tpQuebraLinha = QuebraDeLinha(impIncidentes, larguraLinha, fonte);
                impIncidentes = tpQuebraLinha.Item1;
            }

            linhas = impIncidentes.Split('\n');
            foreach (var lin in linhas)
            {
                tpMedidaLinha = MedidasLinha(lin, fonte);
                larguraEscrita = tpMedidaLinha.Item1;
                alturaEscrita = tpMedidaLinha.Item2;
                graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));
                y += alturaEscrita;
            }
            y += 5;

            if (_proc.NFe.infNFe.infAdic != null)
            {
                graphics.DrawLine(Pens.Black, x, y, larguraLinha, y);
                y += 5;

                fonte = new Font(openSans, 7);
                var infCpl = _proc.NFe.infNFe.infAdic.infCpl.Split('|');
                foreach (var inf in infCpl)
                {
                    if (inf.Contains("TROCO") == false && inf.Contains("Tributos Totais") == false)
                    {
                        var observacao = inf;
                        tpMedidaLinha = MedidasLinha(observacao, fonte);
                        larguraEscrita = tpMedidaLinha.Item1;
                        if (larguraEscrita > larguraLinha)
                        {
                            var tpQuebraLinha = QuebraDeLinha(observacao, larguraLinha, fonte);
                            observacao = tpQuebraLinha.Item1;
                        }

                        linhas = observacao.Split('\n');
                        foreach (var lin in linhas)
                        {
                            tpMedidaLinha = MedidasLinha(lin, fonte);
                            larguraEscrita = tpMedidaLinha.Item1;
                            alturaEscrita = tpMedidaLinha.Item2;
                            graphics.DrawString(lin, fonte, Brushes.Black, new PointF((larguraLinha - larguraEscrita) / 2, y));//Deixa Centralizado
                            y += alturaEscrita;
                        }
                    }
                }
            }
            return graphics;
        }

        //Gera QrCode. Necessário uso de dll para geração. Estão dentro da pasta NuGet. Se quiserem fazer o download diretamente http://zxingnet.codeplex.com/
        public static Image GerarQrCode(int larg, int alt, string qrCode)
        {
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = larg, Height = alt, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            var imageQrCode = new Bitmap(bw.Write(qrCode));
            return imageQrCode;
        }

        //Retorna altura e largura da linha, para posicionar no cupom
        private static Tuple<int, int> MedidasLinha(string linha, Font fonte)
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            var comprLinha = 0;
            var altLinha = 0;
            var stringSize = g.MeasureString(linha, fonte);
            comprLinha = Convert.ToInt32(stringSize.Width);
            altLinha = Convert.ToInt32(stringSize.Height);

            return new Tuple<int, int>(comprLinha, altLinha);
        }

        //Retorna linha dividida, se for maior que a largura do cupom
        private static Tuple<string, int> QuebraDeLinha(string linha, int comprMaximo, Font fonte)
        {
            linha = linha.Replace("\n", " ").Replace("\r", "");
            var palavras = linha.Split(' ');
            var linhaFormat = "";

            var partes = new Dictionary<int, string>();
            var parte = string.Empty;
            var parteCounter = 0;
            foreach (var palavra in palavras)
            {
                var tupleParte = MedidasLinha(parte, fonte);
                var tuplePalavra = MedidasLinha(palavra, fonte);
                var largLinha = tupleParte.Item1 + tuplePalavra.Item1;

                if (largLinha < comprMaximo)
                {
                    parte += string.IsNullOrEmpty(parte) ? palavra : " " + palavra;
                }
                else
                {
                    partes.Add(parteCounter, parte);
                    parte = palavra;
                    parteCounter++;
                }
            }
            partes.Add(parteCounter, parte);

            var nQuebras = 0;
            foreach (var item in partes)
            {
                linhaFormat += item.Value;
                if (nQuebras < partes.Count - 1)
                {
                    linhaFormat += "\n";
                    nQuebras++;
                }
            }
            return new Tuple<string, int>(linhaFormat, nQuebras);
        }

        //Retorna descrição da forma de pagamento
        private static class EnumHelper<T>
        {
            public static string GetEnumDescription(string value)
            {
                var type = typeof(T);
                var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

                if (name == null)
                {
                    return string.Empty;
                }
                var field = type.GetField(name);
                var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
            }
        }
    }
}
