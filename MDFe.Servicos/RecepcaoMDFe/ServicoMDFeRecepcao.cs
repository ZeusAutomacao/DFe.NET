using DFe.Classes.Extencoes;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Autorizacao;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Wsdl.Gerado.MDFeRecepcao;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.RecepcaoMDFe
{
    public class ServicoMDFeRecepcao
    {
        public void MDFeRecepcao(long lote, MDFeEletronico mdfe)
        {
            #region Cria o objeto wdsl para consulta


            var codigoIbgeEstado = mdfe.InfMDFe.Ide.CUF.GetCodigoIbgeEmString();
            var versaoServico = MDFeConfiguracao.VersaoWebService.VersaoMDFeRecepcao.GetVersaoString();
            var urlRecepcao = UrlHelper.ObterUrlServico(mdfe.InfMDFe.Ide.TpAmb).MDFeRecepcao;

            var ws = new MDFeRecepcao(urlRecepcao, codigoIbgeEstado, versaoServico, MDFeConfiguracao.X509Certificate2);

            #endregion

            #region Cria o objeto enviMDFe

            var enviMDFe = new MDFeEnviMDFe
            {
                Versao = MDFeVersaoServico.Versao100,
                IdLote = lote.ToString(),
                MDFe = mdfe
            };

            #endregion

            enviMDFe.Valida();

            enviMDFe.SalvarXmlEmDisco();

            var xmlEnvio = enviMDFe.XmlEnvio();

            var retorno = ws.mdfeRecepcaoLote(xmlEnvio);

        }
    }
}