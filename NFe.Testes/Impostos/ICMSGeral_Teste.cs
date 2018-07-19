using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Utils.Tributacao.Estadual;
using System;

namespace NFe.Testes.Impostos
{
    [TestClass]
    public class ICMSGeral_Teste
    {
        #region CRT - Normal

        [TestMethod]
        [DataTestMethod]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, DeterminacaoBaseIcms.DbiMargemValorAgregado, 1000, 18, 180, null, null)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasilSemSimilar, DeterminacaoBaseIcms.DbiMargemValorAgregado, 1000, 18, 180, 0, 0)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraImportacaoDireta, DeterminacaoBaseIcms.DbiValorOperacao, 1000, 18, 180, 0, 0)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraImportacaoDiretaSemSimilar, DeterminacaoBaseIcms.DbiMargemValorAgregado, 1000, 18, 180, null, null)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmNacional, DeterminacaoBaseIcms.DbiPrecoTabelado, 1000, 18, 180, null, null)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmNacionalConteudoImportacaoInferiorIgual40, DeterminacaoBaseIcms.DbiPauta, 1000, 18, 180, 0, 0)]
        public void ObterICMSBasico_ICMS00_Teste(CRT crt, OrigemMercadoria origem, DeterminacaoBaseIcms modBC, object vBC, object pICMS, object vICMS, object pFCP, object vFCP)
        {
            /** 1) Preparação **/
            var icmsGeral = new ICMSGeral()
            {
                orig = origem,
                CST = Csticms.Cst00,
                modBC = modBC,
                vBC = Convert.ToDecimal(vBC),
                pICMS = Convert.ToDecimal(pICMS),
                vICMS = Convert.ToDecimal(vICMS),
                pFCP = Convert.ToDecimal(pFCP),
                vFCP = Convert.ToDecimal(vFCP)
            };

            /** 2) Execução **/
            var tagGerada = icmsGeral.ObterICMSBasico(crt);

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(ICMS00));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagICMSGerada = (tagGerada as ICMS00);
            Assert.AreEqual(origem, tagICMSGerada.orig);
            Assert.AreEqual(Csticms.Cst00, tagICMSGerada.CST);
            Assert.AreEqual(modBC, tagICMSGerada.modBC);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagICMSGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pICMS), tagICMSGerada.pICMS);
            Assert.AreEqual(Convert.ToDecimal(vICMS), tagICMSGerada.vICMS);
            Assert.AreEqual(Convert.ToDecimal(pFCP), tagICMSGerada.pFCP);
            Assert.AreEqual(Convert.ToDecimal(vFCP), tagICMSGerada.vFCP);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, DeterminacaoBaseIcms.DbiMargemValorAgregado, 1000, 18, 180, 0, 0, 0, DeterminacaoBaseIcmsSt.DbisListaNegativa, 15, 2, 1000, 18, 25, 0, 0, 0)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasilSemSimilar, DeterminacaoBaseIcms.DbiMargemValorAgregado, 1000, 18, 180, 0, 0, 0, DeterminacaoBaseIcmsSt.DbisListaNegativa, 15, 2, 1000, 18, 25, 100, 10, 10)]
        [DataRow(CRT.RegimeNormal, OrigemMercadoria.OmNacionalConteudoImportacaoInferiorIgual40, DeterminacaoBaseIcms.DbiMargemValorAgregado, 100, 18, 18, 0, 0, 0, DeterminacaoBaseIcmsSt.DbisListaNegativa, 15, 2, 1000, 18, 25, 0, 0, 0)]
        public void ObterICMSBasico_ICMS10_Teste(CRT crt, OrigemMercadoria origem, DeterminacaoBaseIcms modBC, object vBC, object pICMS, object vICMS, object vBCFCP, object pFCP, object vFCP, DeterminacaoBaseIcmsSt modBCST, object pMVAST, object pRedBCST, object vBCST, object pICMSST, object vICMSST, object vBCFCPST, object pFCPST, object vFCPST)
        {
            /** 1) Preparação **/
            var icmsGeral = new ICMSGeral()
            {
                orig = origem,
                CST = Csticms.Cst10,
                modBC = modBC,
                vBC = Convert.ToDecimal(vBC),
                pICMS = Convert.ToDecimal(pICMS),
                vICMS = Convert.ToDecimal(vICMS),
                pFCP = Convert.ToDecimal(pFCP),
                vFCP = Convert.ToDecimal(vFCP),
                vBCFCP = Convert.ToDecimal(vBCFCP),
                modBCST = modBCST,
                pMVAST = Convert.ToDecimal(pMVAST),
                pRedBCST = Convert.ToDecimal(pRedBCST),
                vBCST = Convert.ToDecimal(vBCST),
                pICMSST = Convert.ToDecimal(pICMSST),
                vICMSST = Convert.ToDecimal(vICMSST),
                vBCFCPST = Convert.ToDecimal(vBCFCPST),
                pFCPST = Convert.ToDecimal(pFCPST),
                vFCPST = Convert.ToDecimal(vFCPST)
            };

            /** 2) Execução **/
            var tagGerada = icmsGeral.ObterICMSBasico(crt);

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(ICMS10));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagICMSGerada = (tagGerada as ICMS10);
            Assert.AreEqual(origem, tagICMSGerada.orig);
            Assert.AreEqual(Csticms.Cst10, tagICMSGerada.CST);
            Assert.AreEqual(modBC, tagICMSGerada.modBC);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagICMSGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pICMS), tagICMSGerada.pICMS);
            Assert.AreEqual(Convert.ToDecimal(vICMS), tagICMSGerada.vICMS);
            Assert.AreEqual(Convert.ToDecimal(pFCP), tagICMSGerada.pFCP);
            Assert.AreEqual(Convert.ToDecimal(vFCP), tagICMSGerada.vFCP);
            Assert.AreEqual(Convert.ToDecimal(vBCFCP), tagICMSGerada.vBCFCP);
            Assert.AreEqual(modBCST, tagICMSGerada.modBCST);
            Assert.AreEqual(Convert.ToDecimal(pMVAST), tagICMSGerada.pMVAST);
            Assert.AreEqual(Convert.ToDecimal(pRedBCST), tagICMSGerada.pRedBCST);
            Assert.AreEqual(Convert.ToDecimal(vBCST), tagICMSGerada.vBCST);
            Assert.AreEqual(Convert.ToDecimal(pICMSST), tagICMSGerada.pICMSST);
            Assert.AreEqual(Convert.ToDecimal(vICMSST), tagICMSGerada.vICMSST);
            Assert.AreEqual(Convert.ToDecimal(vBCFCPST), tagICMSGerada.vBCFCPST);
            Assert.AreEqual(Convert.ToDecimal(pFCPST), tagICMSGerada.pFCPST);
            Assert.AreEqual(Convert.ToDecimal(vFCPST), tagICMSGerada.vFCPST);
        }

        //TODO: Falta criar os métodos de testes dos demais CSTs do ICMS (CTR = Normal)

        #endregion

        #region CRT - Simples

        [TestMethod]
        [DataTestMethod]
        [DataRow(CRT.SimplesNacional, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, 100, 18)]
        public void ObterICMSBasico_CSOSN101_Teste(CRT crt, OrigemMercadoria origem, object pCredSN, object vCredICMSSN)
        {
            /** 1) Preparação **/
            var icmsGeral = new ICMSGeral()
            {
                orig = origem,
                CSOSN = Csosnicms.Csosn101,
                pCredSN = Convert.ToDecimal(pCredSN),
                vCredICMSSN = Convert.ToDecimal(vCredICMSSN)
            };

            /** 2) Execução **/
            var tagGerada = icmsGeral.ObterICMSBasico(crt);

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(ICMSSN101));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagICMSGerada = (tagGerada as ICMSSN101);
            Assert.AreEqual(origem, tagICMSGerada.orig);
            Assert.AreEqual(Csosnicms.Csosn101, tagICMSGerada.CSOSN);
            Assert.AreEqual(Convert.ToDecimal(pCredSN), tagICMSGerada.pCredSN);
            Assert.AreEqual(Convert.ToDecimal(vCredICMSSN), tagICMSGerada.vCredICMSSN);
        }
        
        //TODO: Falta criar os métodos de testes dos demais CSOSNs do ICMS (CTR = Simples)

        #endregion
    }
}
