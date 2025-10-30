using DFe.Utils;
using DFe.Utils.Assinatura;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using System.IO;
using System.Xml;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEventoMDFe
    {
        public static void ValidarSchema(this MDFeEventoMDFe evento, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var xmlValido = evento.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValido, "eventoMDFe_v1.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValido, "eventoMDFe_v3.00.xsd", config);
                    break;
            }

            var tipoEvento = evento.InfEvento.DetEvento.EventoContainer.GetType();

            if (tipoEvento == typeof(MDFeEvCancMDFe))
            {
                var objetoXml = (MDFeEvCancMDFe)evento.InfEvento.DetEvento.EventoContainer;
                objetoXml.ValidaSchema(config);
            }

            if (tipoEvento == typeof(MDFeEvEncMDFe))
            {
                var objetoXml = (MDFeEvEncMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema(config);
            }

            if (tipoEvento == typeof(MDFeEvIncCondutorMDFe))
            {
                var objetoXml = (MDFeEvIncCondutorMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema(config);
            }

            if (tipoEvento == typeof(MDFeEvIncDFeMDFe))
            {
                var objetoXml = (MDFeEvIncDFeMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema(config);
            }

            if (tipoEvento == typeof(evPagtoOperMDFe))
            {
                var objetoXml = (evPagtoOperMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema(config);
            }
        }

        public static XmlDocument CriaXmlRequestWs(this MDFeEventoMDFe evento)
        {
            var xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(evento.XmlString());

            return xmlRequest;
        }

        public static string XmlString(this MDFeEventoMDFe evento)
        {
            return FuncoesXml.ClasseParaXmlString(evento);
        }

        public static void Assinar(this MDFeEventoMDFe evento, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            evento.Signature = AssinaturaDigital.Assina(evento, evento.InfEvento.Id,
                config.X509Certificate2);
        }

        public static void SalvarXmlEmDisco(this MDFeEventoMDFe evento, string chave, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, chave + "-ped-eve.xml");

            FuncoesXml.ClasseParaArquivoXml(evento, arquivoSalvar);
        }

    }
}