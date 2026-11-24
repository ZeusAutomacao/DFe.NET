using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.DeclaracaoImportacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesCbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesIbs;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Total.IbsCbs;

namespace DFe.Testes.Impostos
{
    /// <summary>
    ///     NT 2026.005 - Reforma Tributária / Simples Nacional
    /// </summary>
    [TestClass]
    public class NT2026005SimplesNacional_Teste
    {
        #region C21a - tpRegimeIBSCBS

        [DataTestMethod]
        [DataRow(tpRegimeIBSCBS.SimplesNacionalRegimeRegular, "1")]
        [DataRow(tpRegimeIBSCBS.RegimeNormalOpcaoPendenteSimplesNacional, "2")]
        [DataRow(tpRegimeIBSCBS.RegimeNormalOpcaoPendenteSimplesNacionalExcessoSublimite, "3")]
        [DataRow(tpRegimeIBSCBS.RegimeNormalOpcaoPendenteSimplesNacionalRegimeRegular, "4")]
        public void Emit_TpRegimeIBSCBS_Informado_GeraTagAposCRT(tpRegimeIBSCBS tipo, string valorEsperado)
        {
            var emit = new emit { CRT = CRT.RegimeNormal, tpRegimeIBSCBS = tipo };

            var xml = FuncoesXml.ClasseParaXmlString(emit);

            Assert.IsTrue(xml.Contains($"<tpRegimeIBSCBS>{valorEsperado}</tpRegimeIBSCBS>"));
            Assert.IsTrue(xml.IndexOf("<CRT>") < xml.IndexOf("<tpRegimeIBSCBS>"));
        }

        [TestMethod]
        public void Emit_TpRegimeIBSCBS_Ausente_NaoGeraTag()
        {
            var xml = FuncoesXml.ClasseParaXmlString(new emit { CRT = CRT.SimplesNacional });

            Assert.IsFalse(xml.Contains("tpRegimeIBSCBS"));
        }

        #endregion

        #region I17d - cAtivSN

        [DataTestMethod]
        [DataRow(cAtivSN.RevendaMercadoriasAnexoI, "01")]
        [DataRow(cAtivSN.DemaisBensMateriaisSemIcmsAnexoI, "02")]
        [DataRow(cAtivSN.VendaIndustrializadasZonaFrancaAnexoII, "03")]
        [DataRow(cAtivSN.DemaisBensMateriaisZonaFrancaSemIcmsAnexoII, "04")]
        [DataRow(cAtivSN.OperacoesNaoTributaveis, "90")]
        public void Prod_CAtivSN_Informado_GeraTagAposIndBemMovelUsadoEAntesDeDI(cAtivSN atividade, string valorEsperado)
        {
            var prod = new prod
            {
                indBemMovelUsado = IndBemMovelUsado.BemMovelUsado,
                cAtivSN = atividade,
                DI = new System.Collections.Generic.List<DI> { new DI { nDI = "1", tpViaTransp = TipoTransporteInternacional.Maritima, tpIntermedio = TipoIntermediacao.ContaPropria } }
            };

            var xml = FuncoesXml.ClasseParaXmlString(prod);

            Assert.IsTrue(xml.Contains($"<cAtivSN>{valorEsperado}</cAtivSN>"));
            Assert.IsTrue(xml.IndexOf("<indBemMovelUsado>") < xml.IndexOf("<cAtivSN>"));
            Assert.IsTrue(xml.IndexOf("<cAtivSN>") < xml.IndexOf("<DI>"));
        }

        [TestMethod]
        public void Prod_CAtivSN_Ausente_NaoGeraTag()
        {
            var xml = FuncoesXml.ClasseParaXmlString(new prod());

            Assert.IsFalse(xml.Contains("cAtivSN"));
        }

        #endregion

        #region UB115a - gTribSN (item)

        [TestMethod]
        public void IBSCBS_GTribSN_Informado_GeraGrupoEntreGAjusteCompetEGEstornoCred()
        {
            var ibscbs = new IBSCBS
            {
                gAjusteCompet = new gAjusteCompet(),
                gTribSN = new gTribSN
                {
                    gIBSSN = new gIBSSN { pIBSSN = 0.1234m, vIBSSN = 1.234m },
                    gCBSSN = new gCBSSN { pCBSSN = 0.9876m, vCBSSN = 9.876m }
                },
                gEstornoCred = new gEstornoCred()
            };

            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            Assert.IsTrue(xml.Contains("<pIBSSN>0.1234</pIBSSN>"));
            Assert.IsTrue(xml.Contains("<vIBSSN>1.23</vIBSSN>"));
            Assert.IsTrue(xml.Contains("<pCBSSN>0.9876</pCBSSN>"));
            Assert.IsTrue(xml.Contains("<vCBSSN>9.88</vCBSSN>"));
            Assert.IsTrue(xml.IndexOf("<gAjusteCompet") < xml.IndexOf("<gTribSN>"));
            Assert.IsTrue(xml.IndexOf("<gTribSN>") < xml.IndexOf("<gEstornoCred"));
            Assert.IsTrue(xml.IndexOf("<gIBSSN>") < xml.IndexOf("<gCBSSN>"));
        }

        [TestMethod]
        public void IBSCBS_GTribSN_SomenteGCBSSN_NaoGeraGIBSSN()
        {
            var ibscbs = new IBSCBS
            {
                gTribSN = new gTribSN { gCBSSN = new gCBSSN { pCBSSN = 1m, vCBSSN = 10m } }
            };

            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            Assert.IsTrue(xml.Contains("<gCBSSN>"));
            Assert.IsFalse(xml.Contains("gIBSSN"));
        }

        [TestMethod]
        public void IBSCBS_GTribSN_Ausente_NaoGeraTag()
        {
            var xml = FuncoesXml.ClasseParaXmlString(new IBSCBS());

            Assert.IsFalse(xml.Contains("gTribSN"));
        }

        #endregion

        #region W59h - gTribSN (total)

        [TestMethod]
        public void IBSCBSTot_GTribSN_Informado_GeraGrupoAposGEstornoCred()
        {
            var tot = new IBSCBSTot
            {
                gEstornoCred = new gEstornoCredTotal(),
                gTribSN = new gTribSNTotal { vIBSSN = 1.234m, vCBSSN = 5.678m }
            };

            var xml = FuncoesXml.ClasseParaXmlString(tot);

            Assert.IsTrue(xml.Contains("<vIBSSN>1.23</vIBSSN>"));
            Assert.IsTrue(xml.Contains("<vCBSSN>5.68</vCBSSN>"));
            Assert.IsTrue(xml.IndexOf("<gEstornoCred>") < xml.IndexOf("<gTribSN>"));
            Assert.IsTrue(xml.IndexOf("<vIBSSN>") < xml.IndexOf("<vCBSSN>"));
        }

        [TestMethod]
        public void IBSCBSTot_GTribSN_Ausente_NaoGeraTag()
        {
            var xml = FuncoesXml.ClasseParaXmlString(new IBSCBSTot());

            Assert.IsFalse(xml.Contains("gTribSN"));
        }

        #endregion
    }
}
