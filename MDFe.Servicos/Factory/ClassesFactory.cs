using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados;
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
    }
}