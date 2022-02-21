using DFe.Classes.Flags;
using FastReport;
using FastReport.Barcode;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base.NFCe;
using NFe.Danfe.Base.NFe;
using NFe.Utils;
using NFe.Utils.InformacoesSuplementares;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Shared.DFe.Danfe.Fast
{
    public static class DanfeSharedHelper
    {
        public static Report GenerateDanfeNfceReport(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc, byte[] frx, string arquivoRelatorio, string textoRodape = "")
        {
            //Define as variáveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            Report relatorio = new Report();
            relatorio.RegisterData(new[] { proc }, "NFCe", 20);
            relatorio.GetDataSource("NFCe").Enabled = true;
            
            if (string.IsNullOrEmpty(arquivoRelatorio))
            {
                relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                relatorio.Load(arquivoRelatorio);
            }

            relatorio.SetParameterValue("NfceDetalheVendaNormal", configuracaoDanfeNfce.DetalheVendaNormal);
            relatorio.SetParameterValue("NfceDetalheVendaContigencia", configuracaoDanfeNfce.DetalheVendaContigencia);
            relatorio.SetParameterValue("NfceImprimeDescontoItem", configuracaoDanfeNfce.ImprimeDescontoItem);
            relatorio.SetParameterValue("NfceImprimeFoneEmitente", configuracaoDanfeNfce.ImprimeFoneEmitente);

            string foneEmitente = null;

            if (proc.NFe.infNFe.emit.enderEmit.fone != null)
            {
                foneEmitente = proc.NFe.infNFe.emit.enderEmit.fone.ToString();
            }
            
            if (foneEmitente != null && foneEmitente.Length == 10)
                foneEmitente = string.Format("{0:(00)0000-0000}", Convert.ToInt64(foneEmitente));
            else if (foneEmitente != null && foneEmitente.Length == 11)
                foneEmitente = string.Format("{0:(00)00000-0000}", Convert.ToInt64(foneEmitente));
            
            relatorio.SetParameterValue("NfceFoneEmitente", foneEmitente);
            relatorio.SetParameterValue("NfceModoImpressao", configuracaoDanfeNfce.ModoImpressao);
            relatorio.SetParameterValue("NfceCancelado", configuracaoDanfeNfce.DocumentoCancelado);
            relatorio.SetParameterValue("NfceLayoutQrCode", configuracaoDanfeNfce.NfceLayoutQrCode);
            relatorio.SetParameterValue("TextoRodape", textoRodape);
            ((ReportPage)relatorio.FindObject("PgNfce")).LeftMargin = configuracaoDanfeNfce.MargemEsquerda;
            ((ReportPage)relatorio.FindObject("PgNfce")).RightMargin = configuracaoDanfeNfce.MargemDireita;

            //alteracao necessaria para .netstandard, o código abaixo utiliza um método onde não é compativel para .netstandard:
            //de : //((PictureObject)relatorio.FindObject("poEmitLogo")).Image = configuracaoDanfeNfce.ObterLogo();
            //para:
#if Standard
            ((PictureObject)relatorio.FindObject("poEmitLogo")).SetImageData(configuracaoDanfeNfce.Logomarca);
#else
            ((PictureObject)relatorio.FindObject("poEmitLogo")).Image = configuracaoDanfeNfce.ObterLogo();
#endif

            ((TextObject)relatorio.FindObject("txtUrl")).Text = string.IsNullOrEmpty(proc.NFe.infNFeSupl.urlChave) ? proc.NFe.infNFeSupl.ObterUrlConsulta(proc.NFe, configuracaoDanfeNfce.VersaoQrCode) : proc.NFe.infNFeSupl.urlChave;
            ((BarcodeObject)relatorio.FindObject("bcoQrCode")).Text = proc.NFe.infNFeSupl == null ? proc.NFe.infNFeSupl.ObterUrlQrCode(proc.NFe, configuracaoDanfeNfce.VersaoQrCode, cIdToken, csc) : proc.NFe.infNFeSupl.qrCode;
            ((BarcodeObject)relatorio.FindObject("bcoQrCodeLateral")).Text = proc.NFe.infNFeSupl == null ? proc.NFe.infNFeSupl.ObterUrlQrCode(proc.NFe, configuracaoDanfeNfce.VersaoQrCode, cIdToken, csc) : proc.NFe.infNFeSupl.qrCode;

            //Segundo o Manual de Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2, página 9, nos casos de emissão em contingência deve ser impresso uma segunda cópia como via do estabelecimento
            if (configuracaoDanfeNfce.SegundaViaContingencia)
            {
#if Standard
                /*Se a NFe for autorizada, mesmo que seja em contingência, imprime somente uma via - devendo o usuario enviar 2 copias para a impressora*/
                //throw new Exception("configuracaoDanfeNfce.SegundaViaContingencia não suportado em .net standard, apenas em .net framework");

#else
                relatorio.PrintSettings.Copies = (proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teNormal | (proc.protNFe != null && proc.protNFe.infProt != null && NfeSituacao.Autorizada(proc.protNFe.infProt.cStat))
                /*Se a NFe for autorizada, mesmo que seja em contingência, imprime somente uma via*/ ) ? 1 : 2;
#endif
            }

            return relatorio;
        }

        public static Report GenerateDanfeFrEventoReport(nfeProc proc, procEventoNFe procEventoNFe, ConfiguracaoDanfeNfe configuracaoDanfeNfe, byte[] frx, string desenvolvedor)
        {
            //Define as variáveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            Report relatorio = new Report();
            relatorio.Load(new MemoryStream(frx));
            relatorio.RegisterData(new[] { proc }, "NFe", 20);
            relatorio.RegisterData(new[] { procEventoNFe }, "procEventoNFe", 20);
            relatorio.GetDataSource("NFe").Enabled = true;
            relatorio.GetDataSource("procEventoNFe").Enabled = true;
            relatorio.SetParameterValue("desenvolvedor", desenvolvedor);

            return relatorio;
        }

        public static Report GenerateDanfeFrNfeReport(nfeProc proc, ConfiguracaoDanfeNfe configuracaoDanfeNfe, byte[] frx, string desenvolvedor, string arquivoRelatorio)
        {
            //Define as variáveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            Report relatorio = new Report();
            relatorio.RegisterData(new[] { proc }, "NFe", 20);
            relatorio.GetDataSource("NFe").Enabled = true;

            if (string.IsNullOrEmpty(arquivoRelatorio))
            {
                relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                relatorio.Load(arquivoRelatorio);
            }

            string mensagem = string.Empty;
            string resumoCanhoto = string.Empty;
            string contingenciaDescricao = string.Empty;
            string contingenciaValor = string.Empty;
            string consultaAutenticidade = "Consulta de autenticidade no portal nacional da NF-e" + Environment.NewLine +
                                        "www.nfe.fazenda.gov.br/portal ou no site da Sefaz autorizadora";

            if (configuracaoDanfeNfe.ExibirResumoCanhoto)
            {
                resumoCanhoto = string.IsNullOrEmpty(configuracaoDanfeNfe.ResumoCanhoto) ?
                                string.Format("Emissão: {0: dd/MM/yyyy} Dest/Reme: {1} Valor Total: {2:C}", proc.NFe.infNFe.ide.dhEmi,
                                                proc.NFe.infNFe.dest.xNome, proc.NFe.infNFe.total.ICMSTot.vNF) :
                                configuracaoDanfeNfe.ResumoCanhoto;
            }

            if (proc.NFe.infNFe.ide.tpAmb == TipoAmbiente.Homologacao)
            {
                if (proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teSCAN ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teEPEC ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teFSDA ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teFSIA)
                {
                    if (proc.protNFe != null && proc.protNFe.infProt != null &&
                        (proc.protNFe.infProt.cStat == 101 ||
                         proc.protNFe.infProt.cStat == 135 ||
                         proc.protNFe.infProt.cStat == 151 ||
                         proc.protNFe.infProt.cStat == 155))
                    {
                        mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO" + Environment.NewLine +
                                   "NFe em Contingência - CANCELADA";
                    }
                    else
                    {
                        mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO" + Environment.NewLine +
                                   "NFe em Contingência";
                    }
                }
                else
                {
                    mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO";
                }
            }
            else
            {
                if (configuracaoDanfeNfe.DocumentoCancelado ||
                    (proc.protNFe != null && proc.protNFe.infProt != null &&
                    !string.IsNullOrEmpty(proc.protNFe.infProt.nProt) &&
                    (proc.protNFe.infProt.cStat == 101 ||
                     proc.protNFe.infProt.cStat == 135 ||
                     proc.protNFe.infProt.cStat == 151 ||
                     proc.protNFe.infProt.cStat == 155)))
                {
                    mensagem = "NFe Cancelada";
                }
                else if (proc.protNFe != null && proc.protNFe.infProt != null &&
                    (proc.protNFe.infProt.cStat == 110 ||
                     proc.protNFe.infProt.cStat == 301 ||
                     proc.protNFe.infProt.cStat == 302 ||
                     proc.protNFe.infProt.cStat == 303))
                {
                    mensagem = "NFe denegada pelo Fisco";
                }
                else if (proc.protNFe != null && proc.protNFe.infProt != null &&
                         string.IsNullOrEmpty(proc.protNFe.infProt.nProt))
                {
                    mensagem = "NFe sem Autorização de Uso da SEFAZ";
                }
            }

            switch (proc.NFe.infNFe.ide.tpEmis)
            {
                case TipoEmissao.teNormal:
                case TipoEmissao.teSCAN:
                case TipoEmissao.teSVCAN:
                case TipoEmissao.teSVCRS:
                    contingenciaDescricao = "PROTOCOLO DE AUTORIZAÇÃO DE USO";
                    contingenciaValor = ((proc.protNFe == null || proc.protNFe.infProt == null || string.IsNullOrEmpty(proc.protNFe.infProt.nProt)) ? "NFe sem Autorização de Uso da SEFAZ" : string.Format("{0} - {1:dd/MM/yyyy HH:mm:ss}", proc.protNFe.infProt.nProt, proc.protNFe.infProt.dhRecbto));
                    if (configuracaoDanfeNfe.DocumentoCancelado || (proc.protNFe != null && proc.protNFe.infProt != null && (proc.protNFe.infProt.cStat == 101 || proc.protNFe.infProt.cStat == 151 || proc.protNFe.infProt.cStat == 155)))
                    {
                        contingenciaDescricao = "PROTOCOLO DE HOMOLOGAÇÃO DO CANCELAMENTO";
                    }
                    else if (proc.protNFe != null && proc.protNFe.infProt != null && (proc.protNFe.infProt.cStat == 110 || proc.protNFe.infProt.cStat == 301 || proc.protNFe.infProt.cStat == 302 || proc.protNFe.infProt.cStat == 303))
                    {
                        contingenciaDescricao = "PROTOCOLO DE DENEGAÇÃO DE USO";
                    }
                    break;

                case TipoEmissao.teFSIA:
                case TipoEmissao.teEPEC:
                case TipoEmissao.teFSDA:
                    contingenciaDescricao = "DADOS DA NF-E";
                    contingenciaValor = Regex.Replace(configuracaoDanfeNfe.ChaveContingencia, ".{4}", "$0 ");
                    consultaAutenticidade = string.Empty;
                    break;

                default:
                    contingenciaValor = string.Format("{0} - {1:dd/MM/yyyy HH:mm:ss}", proc.protNFe.infProt.nProt, proc.protNFe.infProt.dhRecbto);
                    break;
            }

            relatorio.SetParameterValue("ResumoCanhoto", resumoCanhoto);
            relatorio.SetParameterValue("Mensagem", mensagem);
            relatorio.SetParameterValue("ConsultaAutenticidade", consultaAutenticidade);
            relatorio.SetParameterValue("ContingenciaDescricao", contingenciaDescricao);
            relatorio.SetParameterValue("ContingenciaValor", contingenciaValor);
            relatorio.SetParameterValue("ContingenciaID", configuracaoDanfeNfe.ChaveContingencia);
            relatorio.SetParameterValue("DuasLinhas", configuracaoDanfeNfe.DuasLinhas);
            relatorio.SetParameterValue("Desenvolvedor", desenvolvedor);
            relatorio.SetParameterValue("QuebrarLinhasObservacao", configuracaoDanfeNfe.QuebrarLinhasObservacao);
            relatorio.SetParameterValue("ImprimirISSQN", configuracaoDanfeNfe.ImprimirISSQN);
            relatorio.SetParameterValue("ImprimirDescPorc", configuracaoDanfeNfe.ImprimirDescPorc);
            relatorio.SetParameterValue("ImprimirTotalLiquido", configuracaoDanfeNfe.ImprimirTotalLiquido);
            relatorio.SetParameterValue("ImprimirUnidQtdeValor", configuracaoDanfeNfe.ImprimirUnidQtdeValor);
            relatorio.SetParameterValue("ExibeCampoFatura", configuracaoDanfeNfe.ExibeCampoFatura);
            relatorio.SetParameterValue("Logo", configuracaoDanfeNfe.Logomarca);
            relatorio.SetParameterValue("ExibirTotalTributos", configuracaoDanfeNfe.ExibirTotalTributos);
            relatorio.SetParameterValue("ExibeRetencoes", configuracaoDanfeNfe.ExibeRetencoes);
            relatorio.SetParameterValue("DecimaisValorUnitario", configuracaoDanfeNfe.DecimaisValorUnitario);
            relatorio.SetParameterValue("DecimaisQuantidadeItem", configuracaoDanfeNfe.DecimaisQuantidadeItem);
            relatorio.SetParameterValue("DataHoraImpressao", configuracaoDanfeNfe.DataHoraImpressao ?? DateTime.Now);

            return relatorio;
        }
    }
}
