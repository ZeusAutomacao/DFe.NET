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
        public static void ValidarSchema(this MDFeEventoMDFe evento)
        {
            var xmlValido = evento.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValido, "eventoMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValido, "eventoMDFe_v3.00.xsd");
                    break;
            }

            var tipoEvento = evento.InfEvento.DetEvento.EventoContainer.GetType();

            if (tipoEvento == typeof(MDFeEvCancMDFe))
            {
                var objetoXml = (MDFeEvCancMDFe)evento.InfEvento.DetEvento.EventoContainer;
                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvEncMDFe))
            {
                var objetoXml = (MDFeEvEncMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvIncCondutorMDFe))
            {
                var objetoXml = (MDFeEvIncCondutorMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvIncDFeMDFe))
            {
                var objetoXml = (MDFeEvIncDFeMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(evPagtoOperMDFe))
            {
                var objetoXml = (evPagtoOperMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
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

        public static void Assinar(this MDFeEventoMDFe evento)
        {
            evento.Signature = AssinaturaDigital.Assina(evento, evento.InfEvento.Id,
                MDFeConfiguracao.X509Certificate2);
        }

        public static void SalvarXmlEmDisco(this MDFeEventoMDFe evento, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, chave + "-ped-eve.xml");

            FuncoesXml.ClasseParaArquivoXml(evento, arquivoSalvar);
        }

    }
}