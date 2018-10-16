using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Utils.Tributacao.Federal;
using System;

namespace NFe.Testes.Impostos
{
    [TestClass]
    public class PISGeral_Teste
    {
        /// <summary>
        /// Método auxiliar para preencher as propriedades do objeto a ser testado
        /// </summary>
        private void PreenchePropriedades(
            PISGeral pisGeral,
            CSTPIS cst,
            object vBC = null,
            object pPIS = null,
            object vPIS = null,
            object qBCProd = null,
            object vAliqProd = null)
        {
            pisGeral.CST = cst;
            if (vBC != null) pisGeral.vBC = Convert.ToDecimal(vBC);
            if (pPIS != null) pisGeral.pPIS = Convert.ToDecimal(pPIS);
            if (vPIS != null) pisGeral.vPIS = Convert.ToDecimal(vPIS);
            if (qBCProd != null) pisGeral.qBCProd = Convert.ToDecimal(qBCProd);
            if (vAliqProd != null) pisGeral.vAliqProd = Convert.ToDecimal(vAliqProd);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTPIS.pis01, 1000, 1, 10)]
        [DataRow(CSTPIS.pis02, 1000, 1, 10)]
        public void ObterPISBasico_PISAliq_Teste(CSTPIS cst, object vBC, object pPIS, object vPIS)
        {
            /** 1) Preparação **/
            var pisGeral = new PISGeral();
            PreenchePropriedades(pisGeral, cst, vBC, pPIS, vPIS);

            /** 2) Execução **/
            var tagGerada = pisGeral.ObterPISBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(PISAliq));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagPISGerada = (tagGerada as PISAliq);
            Assert.AreEqual(cst, tagPISGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagPISGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pPIS), tagPISGerada.pPIS);
            Assert.AreEqual(Convert.ToDecimal(vPIS), tagPISGerada.vPIS);
        }


        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTPIS.pis03, 1000, 1, 10)]
        public void ObterPISBasico_PISQtde_Teste(CSTPIS cst, object qBCProd, object vAliqProd, object vPIS)
        {
            /** 1) Preparação **/
            var pisGeral = new PISGeral();
            PreenchePropriedades(pisGeral, cst, null, null, vPIS, qBCProd, vAliqProd);

            /** 2) Execução **/
            var tagGerada = pisGeral.ObterPISBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(PISQtde));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagPISGerada = (tagGerada as PISQtde);
            Assert.AreEqual(cst, tagPISGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(qBCProd), tagPISGerada.qBCProd);
            Assert.AreEqual(Convert.ToDecimal(vAliqProd), tagPISGerada.vAliqProd);
            Assert.AreEqual(Convert.ToDecimal(vPIS), tagPISGerada.vPIS);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTPIS.pis04)]
        [DataRow(CSTPIS.pis05)]
        [DataRow(CSTPIS.pis06)]
        [DataRow(CSTPIS.pis07)]
        [DataRow(CSTPIS.pis08)]
        [DataRow(CSTPIS.pis09)]
        public void ObterPISBasico_PISNT_Teste(CSTPIS cst)
        {
            /** 1) Preparação **/
            var pisGeral = new PISGeral();
            PreenchePropriedades(pisGeral, cst);

            /** 2) Execução **/
            var tagGerada = pisGeral.ObterPISBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(PISNT));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagPISGerada = (tagGerada as PISNT);
            Assert.AreEqual(cst, tagPISGerada.CST);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTPIS.pis49, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis50, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis51, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis52, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis53, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis54, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis55, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis56, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis60, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis61, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis62, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis63, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis64, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis65, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis66, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis67, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis70, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis71, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis72, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis73, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis74, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis75, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis98, 1000, 1, 10, 1000, 1)]
        [DataRow(CSTPIS.pis99, 1000, 1, 10, 1000, 1)]
        public void ObterPISBasico_PISOutr_Teste(CSTPIS cst, object vBC, object pPIS, object vPIS, object qBCProd, object vAliqProd)
        {
            /** 1) Preparação **/
            var pisGeral = new PISGeral();
            PreenchePropriedades(pisGeral, cst, vBC, pPIS, vPIS, qBCProd, vAliqProd);

            /** 2) Execução **/
            var tagGerada = pisGeral.ObterPISBasico();

            /** 2) Verificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(PISOutr));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagPISGerada = (tagGerada as PISOutr);
            Assert.AreEqual(cst, tagPISGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagPISGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pPIS), tagPISGerada.pPIS);
            Assert.AreEqual(Convert.ToDecimal(vPIS), tagPISGerada.vPIS);
            Assert.AreEqual(Convert.ToDecimal(qBCProd), tagPISGerada.qBCProd);
            Assert.AreEqual(Convert.ToDecimal(vAliqProd), tagPISGerada.vAliqProd);
        }
    }
}
