using NFe.Classes.Servicos.Consulta;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Consulta;
using Xunit;

namespace NFe.Utils.Testes.Consulta
{
    /// <summary>
    /// Eventos registrados pelo Ambiente Nacional na NF-e quando um CT-e que a referencia é autorizado/cancelado
    /// (NT 2013.008: 610600 - CT-e Autorizado e 610601 - CT-e Cancelado)
    /// </summary>
    public class RetConsSitNFeEventosCTeTestes
    {
        private const string ChaveNFe = "35240112345678000195550010000000011000000010";

        internal static string MontarRetConsSitNFe(string tpEvento, string descEvento)
        {
            return
                "<retConsSitNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">" +
                "<tpAmb>1</tpAmb><verAplic>SP_NFE_PL009_V4</verAplic><cStat>100</cStat>" +
                "<xMotivo>Autorizado o uso da NF-e</xMotivo><cUF>35</cUF><chNFe>" + ChaveNFe + "</chNFe>" +
                "<procEventoNFe versao=\"1.00\">" +
                "<evento versao=\"1.00\"><infEvento Id=\"ID" + tpEvento + ChaveNFe + "01\">" +
                "<cOrgao>91</cOrgao><tpAmb>1</tpAmb><CNPJ>12345678000195</CNPJ><chNFe>" + ChaveNFe + "</chNFe>" +
                "<dhEvento>2024-01-10T10:00:00-03:00</dhEvento><tpEvento>" + tpEvento + "</tpEvento>" +
                "<nSeqEvento>1</nSeqEvento><verEvento>1.00</verEvento>" +
                "<detEvento versao=\"1.00\"><descEvento>" + descEvento + "</descEvento>" +
                "<cOrgaoAutor>91</cOrgaoAutor><tpAutor>5</tpAutor><verAplic>AN_1.0.0</verAplic></detEvento>" +
                "</infEvento></evento>" +
                "<retEvento versao=\"1.00\"><infEvento Id=\"ID891240000000001\">" +
                "<tpAmb>1</tpAmb><verAplic>AN_1.0.0</verAplic><cOrgao>91</cOrgao><cStat>135</cStat>" +
                "<xMotivo>Evento registrado e vinculado a NF-e</xMotivo><chNFe>" + ChaveNFe + "</chNFe>" +
                "<tpEvento>" + tpEvento + "</tpEvento><xEvento>" + descEvento + "</xEvento><nSeqEvento>1</nSeqEvento>" +
                "<dhRegEvento>2024-01-10T10:00:01-03:00</dhRegEvento><nProt>891240000000001</nProt>" +
                "</infEvento></retEvento>" +
                "</procEventoNFe>" +
                "</retConsSitNFe>";
        }

        [Theory]
        [InlineData("610600", "CT-e Autorizado", NFeTipoEvento.TeNfeCTeAutorizado)]
        [InlineData("610601", "CT-e Cancelado", NFeTipoEvento.TeNfeCTeCancelado)]
        public void CarregarDeXmlString_ComEventoDeCTeVinculado_DeserializaTipoEvento(string tpEvento, string descEvento, NFeTipoEvento esperado)
        {
            // Arrange
            var xml = MontarRetConsSitNFe(tpEvento, descEvento);

            // Act
            var retorno = new retConsSitNFe().CarregarDeXmlString(xml);

            // Assert
            var procEvento = Assert.Single(retorno.procEventoNFe);
            Assert.Equal(esperado, procEvento.evento.infEvento.tpEvento);
            Assert.Equal(esperado, procEvento.retEvento.infEvento.tpEvento);
            Assert.Equal(descEvento, procEvento.retEvento.infEvento.xEvento);
        }
    }
}
