using System;
using System.Xml;
using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Utils.Validacao;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoCancelar
    {
        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo, string justificativa)
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRecepcaoEvento.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            var ws = new MDFeRecepcaoEvento(url, codigoEstado, versao, certificadoDigital);

            var cancelamento = new MDFeEvCancMDFe
            {
                DescEvento = "Cancelamento",
                NProt = protocolo,
                XJust = justificativa
            };

            var evento = FactoryEvento.CriaEvento(mdfe,
                MDFeTipoEvento.Cancelamento,
                sequenciaEvento,
                cancelamento);

            // converte o objeto para uma string de xml
            var xmlEnvio = FuncoesXml.ClasseParaXmlString(evento);

            Validador.Valida(xmlEnvio, "eventoMDFe_v1.00.xsd");

            var condutorXml = (MDFeEvCancMDFe)evento.InfEvento.DetEvento.EventoContainer;

            var xmlCondutor = FuncoesXml.ClasseParaXmlString(condutorXml);

            Validador.Valida(xmlCondutor, "evCancMDFe_v1.00.xsd");

            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlEnvio);

            SalvarArquivoXml(evento, mdfe);

            var retornoXml = ws.mdfeRecepcaoEvento(dadosRecibo);

            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetEventoMDFe>(retornoXml.OuterXml);

            SalvarArquivoXmlRetorno(retorno, mdfe);

            return retorno;
        }

        private void SalvarArquivoXmlRetorno(MDFeRetEventoMDFe retorno, MDFeEletronico mdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + mdfe.Chave() + "-env.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }

        private void SalvarArquivoXml(MDFeEventoMDFe evento, MDFeEletronico mdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + mdfe.Chave() + "-ped-eve.xml";

            FuncoesXml.ClasseParaArquivoXml(evento, arquivoSalvar);
        }
    }
}