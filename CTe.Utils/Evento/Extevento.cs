using System;
using System.Xml;
using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Utils.Validacao;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTeDLL.Utils.Evento
{
    public static class Extevento
    {
        /// <summary>
        ///     Converte o objeto evento para uma string no formato XML
        /// </summary>
        /// <param name="pedEventoCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto evento</returns>
        public static string ObterXmlString(this eventoCTe pedEventoCTe)
        {
            return FuncoesXml.ClasseParaXmlString(pedEventoCTe);
        }

        /// <summary>
        ///     Assina um objeto evento
        /// </summary>
        /// <param name="eventoCTe"></param>
        /// <returns>Retorna um objeto do tipo evento assinado</returns>
        public static void Assina(this eventoCTe eventoCTe)
        {
            if (eventoCTe.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            eventoCTe.Signature = AssinaturaDigital.Assina(eventoCTe, eventoCTe.infEvento.Id,
                ConfiguracaoServico.Instancia.X509Certificate2);
        }

        public static void ValidarSchema(this eventoCTe eventoCTe)
        {
            var xmlEvento = eventoCTe.ObterXmlString();

            switch (eventoCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlEvento, "eventoCTe_v2.00.xsd");
                    break;
                case versao.ve300:
                    Validador.Valida(xmlEvento, "eventoCTe_v3.00.xsd");
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00 é 3.00");
            }

            ValidarSchemaEventoContainer(eventoCTe.infEvento.detEvento.EventoContainer, eventoCTe.versao);
        }

        private static void ValidarSchemaEventoContainer(IEventoContainer container, versao versao)
        {
            if (container.GetType() == typeof(evCancCTe))
            {
                var evCancCTe = (evCancCTe) container;

                var xmlEventoCancelamento = evCancCTe.ObterXmlString();

                switch (versao)
                {
                    case versao.ve200:
                        Validador.Valida(xmlEventoCancelamento, "evCancCTe_v2.00.xsd");
                        break;
                    case versao.ve300:
                        Validador.Valida(xmlEventoCancelamento, "evCancCTe_v3.00.xsd");
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                       "a versão está inválida, somente é permitido " +
                                                       "versão 2.00 é 3.00");
                }


            }

            if (container.GetType() == typeof(evCCeCTe))
            {
                var evCCeCTe = (evCCeCTe)container;

                var xmlEventoCCe = evCCeCTe.ObterXmlString();

                switch (versao)
                {
                    case versao.ve200:
                        Validador.Valida(xmlEventoCCe, "evCCeCTe_v2.00.xsd");
                        break;
                    case versao.ve300:
                        Validador.Valida(xmlEventoCCe, "evCCeCTe_v3.00.xsd");
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                       "a versão está inválida, somente é permitido " +
                                                       "versão 2.00 é 3.00");
                }
            }
        }

        public static void SalvarXmlEmDisco(this eventoCTe eventoCTe)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + eventoCTe.infEvento.chCTe + "-ped-eve.xml";

            FuncoesXml.ClasseParaArquivoXml(eventoCTe, arquivoSalvar);
        }

        public static XmlDocument CriaXmlRequestWs(this eventoCTe eventoCTe)
        {
            var xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(eventoCTe.ObterXmlString());

            return xmlRequest;
        }
    }
}