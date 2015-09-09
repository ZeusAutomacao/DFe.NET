using System.IO;
using FastReport;
using FastReport.Barcode;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Impressao.NFCe.FastReports
{
    public class DanfeFrNfce
    {
        private readonly Report _relatorio;

        public DanfeFrNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce)
        {
            #region Define as varíaveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            _relatorio = new Report();
            _relatorio.RegisterData(new[] { proc }, "NFCe", 20);
            _relatorio.GetDataSource("NFCe").Enabled = true;
            _relatorio.Load(new MemoryStream(Properties.Resources.NFCe));
            _relatorio.SetParameterValue("NfceDetalheVendaNormal", configuracaoDanfeNfce.DetalheVendaNormal);
            _relatorio.SetParameterValue("NfceDetalheVendaContigencia", configuracaoDanfeNfce.DetalheVendaContigencia);
            ((PictureObject) _relatorio.FindObject("poEmitLogo")).Image = configuracaoDanfeNfce.ObterLogo();
            ((TextObject)_relatorio.FindObject("txtUrl")).Text = EnderecadorDanfeNfce.ObterUrl(proc.NFe.infNFe.ide.tpAmb, proc.NFe.infNFe.ide.cUF, TipoUrlDanfeNfce.UrlConsulta);
            ((BarcodeObject)_relatorio.FindObject("bcoQrCode")).Text = EnderecadorDanfeNfce.ObterUrlQrCode(proc, configuracaoDanfeNfce);

            //Segundo o Manual de Padrões Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2, página 9, nos casos de emissão em contigência deve ser impresso uma segunda cópia como via do estabelecimento
            _relatorio.PrintSettings.Copies = proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teNormal ? 1 : 2;

            #endregion
        }

        public void Visualizar(bool modal = true)
        {
            _relatorio.Show(modal);
        }

        public void ExibirDesign(bool modal = false)
        {
            _relatorio.Design(modal);
        }

        public void Imprimir(bool exibirDialogo = true)
        {
            _relatorio.PrintSettings.ShowDialog = exibirDialogo;
            _relatorio.Print();
        }

    }
}
