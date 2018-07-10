using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Utils.Tributacao.Federal;
using System;

namespace NFe.Testes.Impostos
{
    [TestClass]
    public class IPIGeral_Teste
    {
        /// <summary>
        /// Método auxiliar para preencher as propriedades do objeto a ser testado
        /// </summary>
        private void PreenchePropriedades(
            IPIGeral ipiGeral,
            CSTIPI cst,
            object vBC = null,
            object pIPI = null,
            object vIPI = null,
            object qUnid = null,
            object vUnid = null)
        {
            ipiGeral.CST = cst;
            if (vBC != null) ipiGeral.vBC = Convert.ToDecimal(vBC);
            if (pIPI != null) ipiGeral.pIPI = Convert.ToDecimal(pIPI);
            if (vIPI != null) ipiGeral.vIPI = Convert.ToDecimal(vIPI);
            if (qUnid != null) ipiGeral.qUnid = Convert.ToDecimal(qUnid);
            if (vUnid != null) ipiGeral.vUnid = Convert.ToDecimal(vUnid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTIPI.ipi00, 1000, 1, 10, 5, 50)]
        [DataRow(CSTIPI.ipi49, 1000, 1, 10, 5, 50)]
        [DataRow(CSTIPI.ipi50, 1000, 1, 10, 5, 50)]
        [DataRow(CSTIPI.ipi99, 1000, 1, 10, 5, 50)]
        public void ObterIPIBasico_IPITrib_Teste(CSTIPI cst, object vBC, object pIPI, object vIPI, object qUnid, object vUnid)
        {
            /** 1) Preparação **/
            var ipiGeral = new IPIGeral();
            PreenchePropriedades(ipiGeral, cst, vBC, pIPI, vIPI, qUnid, vUnid);

            /** 2) Execução **/
            var tagGerada = ipiGeral.ObterIPIBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(IPITrib));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagIPIGerada = (tagGerada as IPITrib);
            Assert.AreEqual(cst, tagIPIGerada.CST);
            Assert.AreEqual(Convert.ToDecimal(vBC), tagIPIGerada.vBC);
            Assert.AreEqual(Convert.ToDecimal(pIPI), tagIPIGerada.pIPI);
            Assert.AreEqual(Convert.ToDecimal(vIPI), tagIPIGerada.vIPI);
            Assert.AreEqual(Convert.ToDecimal(qUnid), tagIPIGerada.qUnid);
            Assert.AreEqual(Convert.ToDecimal(vUnid), tagIPIGerada.vUnid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(CSTIPI.ipi01)]
        [DataRow(CSTIPI.ipi02)]
        [DataRow(CSTIPI.ipi03)]
        [DataRow(CSTIPI.ipi04)]
        [DataRow(CSTIPI.ipi05)]
        [DataRow(CSTIPI.ipi51)]
        [DataRow(CSTIPI.ipi52)]
        [DataRow(CSTIPI.ipi53)]
        [DataRow(CSTIPI.ipi54)]
        [DataRow(CSTIPI.ipi55)]
        public void ObterIPIBasico_IPINT_Teste(CSTIPI cst)
        {
            /** 1) Preparação **/
            var ipiGeral = new IPIGeral();
            PreenchePropriedades(ipiGeral, cst);

            /** 2) Execução **/
            var tagGerada = ipiGeral.ObterIPIBasico();

            /** 2) Veerificação **/
            /** 2.1) Garante que o tipo da classe gerada foi correta**/
            Assert.IsInstanceOfType(tagGerada, typeof(IPINT));

            /** 2.2) Garante que o conteúdo repassado para as propriedades estejam corretos **/
            var tagIPIGerada = (tagGerada as IPINT);
            Assert.AreEqual(cst, tagIPIGerada.CST);
        }

    }
}
