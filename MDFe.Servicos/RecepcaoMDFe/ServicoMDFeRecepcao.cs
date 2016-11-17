using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRecepcao;
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
        public MDFeRetEnviMDFe MDFeRecepcao(long lote, MDFeEletronico mdfe)
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

            enviMDFe.MDFe.Assina();
            enviMDFe.Valida();

            enviMDFe.SalvarXmlEmDisco();

            var xmlEnvio = enviMDFe.XmlEnvio();

            // Envia para a sefaz
            var retornoXmlDocument = ws.mdfeRecepcaoLote(xmlEnvio);

            // trata retorno
            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetEnviMDFe>(retornoXmlDocument.OuterXml);

            // salva arquivo de retorno
            SalvarRetorno(retorno);

            return retorno;
        }

        private void SalvarRetorno(MDFeRetEnviMDFe retorno)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + retorno.InfRec.NRec + "-mdfe.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }
    }
}