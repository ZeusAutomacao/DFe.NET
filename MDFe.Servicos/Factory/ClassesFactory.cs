using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpj)
        {
            var consMDFeNaoEnc = new MDFeCosMDFeNaoEnc
            {
                CNPJ = cnpj,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc,
                XServ = "CONSULTAR NÃO ENCERRADOS"
            };

            return consMDFeNaoEnc;
        }

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave)
        {
            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsulta,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                XServ = "CONSULTAR",
                ChMDFe = chave
            };

            return consSitMdfe;
        }
    }
}