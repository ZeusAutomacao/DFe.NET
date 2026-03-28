using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos;

namespace DFe.Testes.Impostos
{
    [TestClass]
    public class IBSCBSGeral_Teste
    {
        [TestMethod]
        public void DadoIBSCBSSemIndDoacaoQuandoSerializarEntaoXmlNaoDeveConterElementoIndDoacao()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst000,
                cClassTrib = "100010"
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            Assert.IsFalse(xml.Contains("<indDoacao>"));
        }

        [TestMethod]
        public void DadoIBSCBSComIndDoacaoQuandoSerializarEntaoXmlDeveConterElementoIndDoacao()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst000,
                cClassTrib = "100010",
                indDoacao = IndDoacao.Doacao
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            Assert.IsTrue(xml.Contains("<indDoacao>1</indDoacao>"));
        }

        [TestMethod]
        public void DadoIBSCBSComGEstornoCredQuandoSerializarEntaoXmlDeveConterValoresDeEstorno()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst410,
                cClassTrib = "410030",
                gEstornoCred = new gEstornoCred
                {
                    vIBSEstCred = 100.50m,
                    vCBSEstCred = 50.25m
                }
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            Assert.IsTrue(xml.Contains("<vIBSEstCred>100.50</vIBSEstCred>"));
            Assert.IsTrue(xml.Contains("<vCBSEstCred>50.25</vCBSEstCred>"));
        }

        [TestMethod]
        public void DadoIBSCBSComGCredPresOperQuandoSerializarEntaoXmlDeveConterGrupoCredPresOper()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst000,
                cClassTrib = "100010",
                gCredPresOper = new gCredPresOper
                {
                    vBCCredPres = 1000.00m,
                    cCredPres = "001"
                }
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            Assert.IsTrue(xml.Contains("<gCredPresOper>"));
            Assert.IsTrue(xml.Contains("<vBCCredPres>1000.00</vBCCredPres>"));
        }

        [TestMethod]
        public void DadoIBSCBSComGAjusteCompetQuandoSerializarEntaoXmlDeveConterGrupoAjusteCompet()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst810,
                cClassTrib = "810010",
                gAjusteCompet = new gAjusteCompet
                {
                    competApur = new System.DateTime(2026, 1, 1),
                    vIBS = 200.00m,
                    vCBS = 100.00m
                }
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            Assert.IsTrue(xml.Contains("<gAjusteCompet>"));
            Assert.IsTrue(xml.Contains("<vIBS>200.00</vIBS>"));
            Assert.IsTrue(xml.Contains("<vCBS>100.00</vCBS>"));
        }

        [TestMethod]
        public void DadoIBSCBSComDoacaoEEstornoQuandoSerializarEntaoIndDoacaoDeveVirAntesDeGEstornoCred()
        {
            /** 1) Preparação **/
            var ibscbs = new IBSCBS
            {
                CST = CST.Cst410,
                cClassTrib = "410030",
                indDoacao = IndDoacao.Doacao,
                gEstornoCred = new gEstornoCred
                {
                    vIBSEstCred = 100.00m,
                    vCBSEstCred = 50.00m
                }
            };

            /** 2) Execução **/
            var xml = FuncoesXml.ClasseParaXmlString(ibscbs);

            /** 3) Verificação **/
            var posIndDoacao = xml.IndexOf("<indDoacao>");
            var posEstornoCred = xml.IndexOf("<gEstornoCred>");

            Assert.IsTrue(posIndDoacao >= 0, "indDoacao deve estar presente no XML");
            Assert.IsTrue(posEstornoCred >= 0, "gEstornoCred deve estar presente no XML");
            Assert.IsTrue(posIndDoacao < posEstornoCred, "indDoacao deve vir antes de gEstornoCred no XML");
        }
    }
}
