using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Utils.Tributacao.Federal;
using System;

namespace NFe.Testes.Impostos
{
    [TestClass]
    public class COFINSGeral_Teste
    {
        /// <summary>
        /// Método auxiliar para preencher as propriedades do objeto a ser testado
        /// </summary>
        private void PreenchePropriedades(
            COFINSGeral cofinsGeral,
            CSTCOFINS cst,
            object vBC = null,
            object pCOFINS = null,
            object vCOFINS = null,
            object qBCProd = null,
            object vAliqProd = null)
        {
            cofinsGeral.CST = cst;
            if (vBC != null) cofinsGeral.vBC = Convert.ToDecimal(vBC);
            if (pCOFINS != null) cofinsGeral.pCOFINS = Convert.ToDecimal(pCOFINS);
            if (vCOFINS != null) cofinsGeral.vCOFINS = Convert.ToDecimal(vCOFINS);
            if (qBCProd != null) cofinsGeral.qBCProd = Convert.ToDecimal(qBCProd);
            if (vAliqProd != null) cofinsGeral.vAliqProd = Convert.ToDecimal(vAliqProd);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTCOFINS.cofins01, 1000, 1, 10)]
        [DataRow(CSTCOFINS.cofins02, 1000, 1, 10)]
        public void ObterCOFINSBasico_COFINSAliq_Teste(CSTCOFINS cst, object vBC, object pCOFINS, object vCOFINS)
        {
            /** 1) Preparação **/
            var cofinsGeral = new COFINSGeral();
            PreenchePropriedades(cofinsGeral, cst, vBC, pCOFINS, vCOFINS);

            /** 2) Execução **/
            var tagGerada = cofinsGeral.ObterCOFINSBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(COFINSAliq));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagCOFINSGerada = (tagGerada as COFINSAliq);
            Assert.AreEqual(cst, tagCOFINSGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagCOFINSGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pCOFINS), tagCOFINSGerada.pCOFINS);
            Assert.AreEqual(Convert.ToDecimal(vCOFINS), tagCOFINSGerada.vCOFINS);
        }


        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTCOFINS.cofins03, 1000, 1, 10)]
        public void ObterCOFINSBasico_COFINSQtde_Teste(CSTCOFINS cst, object qBCProd, object vAliqProd, object vCOFINS)
        {
            /** 1) Preparação **/
            var cofinsGeral = new COFINSGeral();
            PreenchePropriedades(cofinsGeral, cst, null, null, vCOFINS, qBCProd, vAliqProd);

            /** 2) Execução **/
            var tagGerada = cofinsGeral.ObterCOFINSBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(COFINSQtde));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagCOFINSGerada = (tagGerada as COFINSQtde);
            Assert.AreEqual(cst, tagCOFINSGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(qBCProd), tagCOFINSGerada.qBCProd);
            Assert.AreEqual(Convert.ToDecimal(vAliqProd), tagCOFINSGerada.vAliqProd);
            Assert.AreEqual(Convert.ToDecimal(vCOFINS), tagCOFINSGerada.vCOFINS);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTCOFINS.cofins04)]
        [DataRow(CSTCOFINS.cofins05)]
        [DataRow(CSTCOFINS.cofins06)]
        [DataRow(CSTCOFINS.cofins07)]
        [DataRow(CSTCOFINS.cofins08)]
        [DataRow(CSTCOFINS.cofins09)]
        public void ObterCOFINSBasico_COFINSNT_Teste(CSTCOFINS cst)
        {
            /** 1) Preparação **/
            var cofinsGeral = new COFINSGeral();
            PreenchePropriedades(cofinsGeral, cst);

            /** 2) Execução **/
            var tagGerada = cofinsGeral.ObterCOFINSBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(COFINSNT));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagCOFINSGerada = (tagGerada as COFINSNT);
            Assert.AreEqual(cst, tagCOFINSGerada.CST);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTCOFINS.cofins49, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins50, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins51, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins52, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins53, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins54, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins55, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins56, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins60, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins61, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins62, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins63, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins64, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins65, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins66, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins67, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins70, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins71, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins72, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins73, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins74, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins75, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins98, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTCOFINS.cofins99, 1000, 1, 10, 1000, 1)]
        public void ObterCOFINSBasico_COFINSOutr_Teste(CSTCOFINS cst, object vBC, object pCOFINS, object vCOFINS, object qBCProd, object vAliqProd)
        {
            /** 1) Preparação **/
            var cofinsGeral = new COFINSGeral();
            PreenchePropriedades(cofinsGeral, cst, vBC, pCOFINS, vCOFINS, qBCProd, vAliqProd);

            /** 2) Execução **/
            var tagGerada = cofinsGeral.ObterCOFINSBasico();

            /** 2) Verificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(COFINSOutr));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagCOFINSGerada = (tagGerada as COFINSOutr);
            Assert.AreEqual(cst, tagCOFINSGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagCOFINSGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pCOFINS), tagCOFINSGerada.pCOFINS);
            Assert.AreEqual(Convert.ToDecimal(vCOFINS), tagCOFINSGerada.vCOFINS);
            Assert.AreEqual(Convert.ToDecimal(qBCProd), tagCOFINSGerada.qBCProd);
            Assert.AreEqual(Convert.ToDecimal(vAliqProd), tagCOFINSGerada.vAliqProd);
        }
    }
}
