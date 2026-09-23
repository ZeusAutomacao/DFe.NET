using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Danfe.QuestPdf.ImpressaoNfce;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace NFe.Danfe.QuestPdf.Testes.ImpressaoNfce;

[TestClass]
public class DanfeNfceDocumentTesteUnitario
{
    [ClassInitialize]
    public static void ConfigurarQuestPdf(TestContext _)
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    [DataTestMethod]
    [DataRow(TamanhoImpressao.Impressao80)]
    [DataRow(TamanhoImpressao.Impressao72)]
    [DataRow(TamanhoImpressao.Impressao50)]
    public void DadaNfceComAjustesNoItemQuandoGerarDanfeEntaoDeveGerarPdf(TamanhoImpressao tamanhoImpressao)
    {
        var documento = new DanfeNfceDocument(XmlNfce, null)
        {
            ImprimeDescontoItem = true,
            ImprimeAcrescimoItem = true,
            ImprimeInformacaoAdicionalItem = true
        };

        documento.TamanhoImpressao(tamanhoImpressao);

        var pdf = documento.GeneratePdf();

        Assert.IsNotNull(pdf);
        Assert.IsTrue(pdf.Length > 0);
    }

    private const string XmlNfce = @"<?xml version=""1.0"" encoding=""utf-8""?>
<NFe xmlns=""http://www.portalfiscal.inf.br/nfe"">
  <infNFe Id=""NFe42260800000000000100650010000000011000000010"" versao=""4.00"">
    <ide>
      <cUF>42</cUF>
      <cNF>00000001</cNF>
      <natOp>VENDA</natOp>
      <mod>65</mod>
      <serie>1</serie>
      <nNF>1</nNF>
      <dhEmi>2026-08-14T12:00:00-03:00</dhEmi>
      <tpNF>1</tpNF>
      <idDest>1</idDest>
      <cMunFG>4209102</cMunFG>
      <tpImp>4</tpImp>
      <tpEmis>1</tpEmis>
      <cDV>0</cDV>
      <tpAmb>2</tpAmb>
      <finNFe>1</finNFe>
      <indFinal>1</indFinal>
      <indPres>1</indPres>
      <procEmi>0</procEmi>
      <verProc>Teste</verProc>
    </ide>
    <emit>
      <CNPJ>00000000000100</CNPJ>
      <xNome>EMPRESA DE TESTE</xNome>
      <xFant>EMPRESA DE TESTE</xFant>
      <enderEmit>
        <xLgr>RUA DE TESTE</xLgr>
        <nro>100</nro>
        <xBairro>CENTRO</xBairro>
        <cMun>4209102</cMun>
        <xMun>CIDADE DE TESTE</xMun>
        <UF>SC</UF>
        <CEP>00000000</CEP>
        <cPais>1058</cPais>
        <xPais>BRASIL</xPais>
      </enderEmit>
      <IE>000000000</IE>
      <CRT>1</CRT>
    </emit>
    <det nItem=""1"">
      <prod>
        <cProd>1</cProd>
        <cEAN>SEM GTIN</cEAN>
        <xProd>PRODUTO DE TESTE</xProd>
        <NCM>00000000</NCM>
        <CFOP>5102</CFOP>
        <uCom>UN</uCom>
        <qCom>1.0000</qCom>
        <vUnCom>21.0000000000</vUnCom>
        <vProd>21.00</vProd>
        <cEANTrib>SEM GTIN</cEANTrib>
        <uTrib>UN</uTrib>
        <qTrib>1.0000</qTrib>
        <vUnTrib>21.0000000000</vUnTrib>
        <vDesc>3.00</vDesc>
        <vOutro>2.00</vOutro>
        <indTot>1</indTot>
      </prod>
      <infAdProd>AZUL/M: 1</infAdProd>
    </det>
    <total>
      <ICMSTot>
        <vBC>0.00</vBC>
        <vICMS>0.00</vICMS>
        <vICMSDeson>0.00</vICMSDeson>
        <vFCP>0.00</vFCP>
        <vBCST>0.00</vBCST>
        <vST>0.00</vST>
        <vFCPST>0.00</vFCPST>
        <vFCPSTRet>0.00</vFCPSTRet>
        <vProd>21.00</vProd>
        <vFrete>0.00</vFrete>
        <vSeg>0.00</vSeg>
        <vDesc>3.00</vDesc>
        <vII>0.00</vII>
        <vIPI>0.00</vIPI>
        <vIPIDevol>0.00</vIPIDevol>
        <vPIS>0.00</vPIS>
        <vCOFINS>0.00</vCOFINS>
        <vOutro>2.00</vOutro>
        <vNF>20.00</vNF>
        <vTotTrib>0.00</vTotTrib>
      </ICMSTot>
    </total>
    <pag>
      <detPag>
        <tPag>01</tPag>
        <vPag>20.00</vPag>
      </detPag>
    </pag>
  </infNFe>
  <infNFeSupl>
    <qrCode>https://example.com/qrcode</qrCode>
    <urlChave>https://example.com/consulta</urlChave>
  </infNFeSupl>
</NFe>";
}
