// Copyright (c) colaboradores do DFe.NET. Licença LGPL-2.1-or-later; consulte LICENSE.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Danfe.PdfClown.Modelo;

namespace DFe.Testes.Gerais
{
    /// <summary>Verifica leitura de grupos opcionais e preservação dos valores do XML da RTC.</summary>
    [TestClass]
    public class DanfeRtcLeituraTestes
    {
        private static string Xml(string imposto, string total) => @"<nfeProc xmlns='http://www.portalfiscal.inf.br/nfe' versao='4.00'>
<NFe><infNFe Id='NFe52250711222333000181550010000000011000000010' versao='4.00'>
<ide><mod>55</mod><serie>1</serie><nNF>1</nNF><tpImp>1</tpImp><tpEmis>1</tpEmis><tpAmb>2</tpAmb><tpNF>1</tpNF></ide>
<emit><CNPJ>11222333000181</CNPJ><xNome>TESTE</xNome><CRT>1</CRT></emit>
<dest><CNPJ>11222333000181</CNPJ><xNome>TESTE</xNome></dest>
<det nItem='1'><prod><cProd>1</cProd><xProd>TESTE</xProd><CFOP>5102</CFOP><qCom>1</qCom><vUnCom>100</vUnCom><vProd>100</vProd></prod>
<imposto><ICMS><ICMSSN102><orig>0</orig><CSOSN>102</CSOSN></ICMSSN102></ICMS>" + imposto + @"</imposto></det>
<total><ICMSTot><vProd>100</vProd><vNF>100</vNF></ICMSTot>" + total + @"</total><transp><modFrete>9</modFrete></transp>
</infNFe></NFe><protNFe><infProt><tpAmb>2</tpAmb><cStat>100</cStat><nProt>000000000000000</nProt><dhRecbto>2026-11-02T12:00:00-03:00</dhRecbto></infProt></protNFe></nfeProc>";

        /// <summary>Ausência de RTC não deve gerar valores ou ativar o novo layout.</summary>
        [TestMethod]
        public void DadoXmlSemRtc_QuandoLer_EntaoPreservaAusencia()
        {
            var modelo = DanfeViewModel.CreateFromXmlString(Xml("", ""));
            Assert.IsFalse(modelo.ExibirTributosRtc);
            Assert.IsNull(modelo.Produtos[0].BaseIbsCbs);
            Assert.IsNull(modelo.CalculoImposto.ValorCbs);
            Assert.IsTrue(modelo.Produtos[0].PossuiCsosn);
        }

        /// <summary>Alíquota efetiva zero deve prevalecer sobre a nominal.</summary>
        [TestMethod]
        public void DadoReducaoIntegral_QuandoLer_EntaoPreservaAliquotaEfetivaZero()
        {
            var modelo = DanfeViewModel.CreateFromXmlString(Xml(@"<IBSCBS><CST>200</CST><cClassTrib>200001</cClassTrib><gIBSCBS><vBC>100</vBC>
<gIBSUF><pIBSUF>0.1</pIBSUF><gRed><pAliqEfet>0</pAliqEfet></gRed><vIBSUF>0</vIBSUF></gIBSUF>
<gCBS><pCBS>0.9</pCBS><gRed><pAliqEfet>0.36</pAliqEfet></gRed><vCBS>0.36</vCBS></gCBS></gIBSCBS></IBSCBS>
<IS><vBCIS>100</vBCIS><pIS>2</pIS><vIS>2</vIS></IS>", @"<ISTot><vIS>2</vIS></ISTot><IBSCBSTot><gCBS><vCBS>0.36</vCBS></gCBS><gMono><vIBSMono>12.3</vIBSMono><vCBSMono>45.6</vCBSMono><vIBSMonoReten>7.89</vIBSMonoReten><vCBSMonoReten>9.87</vCBSMonoReten></gMono></IBSCBSTot>"));
            Assert.IsTrue(modelo.ExibirTributosRtc);
            Assert.AreEqual(0m, modelo.Produtos[0].AliquotaIbsUf);
            Assert.AreEqual(0.36m, modelo.Produtos[0].AliquotaCbs);
            Assert.IsNull(modelo.Produtos[0].AliquotaIbsMunicipio);
            Assert.AreEqual(2m, modelo.Produtos[0].ValorImpostoSeletivo);
            Assert.AreEqual(2m, modelo.CalculoImposto.ValorImpostoSeletivo);
            Assert.AreEqual(12.3m, modelo.CalculoImposto.ValorIbsMono);
            Assert.AreEqual(45.6m, modelo.CalculoImposto.ValorCbsMono);
            Assert.AreEqual(7.89m, modelo.CalculoImposto.ValorIbsMonoReten);
            Assert.AreEqual(9.87m, modelo.CalculoImposto.ValorCbsMonoReten);
        }
    }
}
