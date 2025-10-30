using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Extencoes;
using CTe.Utils.Validacao;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTe.Utils.Evento
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
        /// <param name="configuracaoServico"></param>
        /// <returns>Retorna um objeto do tipo evento assinado</returns>
        public static void Assina(this eventoCTe eventoCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            if (eventoCTe.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            eventoCTe.Signature = AssinaturaDigital.Assina(eventoCTe, eventoCTe.infEvento.Id,
                configServico.X509Certificate2);
        }

        public static void ValidarSchema(this eventoCTe eventoCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var xmlEvento = eventoCTe.ObterXmlString();

            switch (eventoCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlEvento, "eventoCTe_v2.00.xsd", configServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlEvento, "eventoCTe_v3.00.xsd", configServico);
                    break;
                case versao.ve400:
                    Validador.Valida(xmlEvento, "eventoCTe_v4.00.xsd", configServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00, 3.00, 4.00");
            }

            ValidarSchemaEventoContainer(eventoCTe.infEvento.detEvento.EventoContainer, eventoCTe.versao, configuracaoServico);
        }

        private static void ValidarSchemaEventoContainer(EventoContainer container, versao versao, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (container.GetType() == typeof(evCancCTe))
            {
                var evCancCTe = (evCancCTe)container;

                var xmlEventoCancelamento = evCancCTe.ObterXmlString();

                switch (versao)
                {
                    case versao.ve200:
                        Validador.Valida(xmlEventoCancelamento, "evCancCTe_v2.00.xsd", configServico);
                        break;
                    case versao.ve300:
                        Validador.Valida(xmlEventoCancelamento, "evCancCTe_v3.00.xsd", configServico);
                        break;
                    case versao.ve400:
                        Validador.Valida(xmlEventoCancelamento, "evCancCTe_v4.00.xsd", configServico);
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                       "a versão está inválida, somente é permitido " +
                                                       "versão 2.00, 3.00, 4.00");
                }


            }

            if (container.GetType() == typeof(evCCeCTe))
            {
                var evCCeCTe = (evCCeCTe)container;

                var xmlEventoCCe = evCCeCTe.ObterXmlString();

                switch (versao)
                {
                    case versao.ve200:
                        Validador.Valida(xmlEventoCCe, "evCCeCTe_v2.00.xsd", configServico);
                        break;
                    case versao.ve300:
                        Validador.Valida(xmlEventoCCe, "evCCeCTe_v3.00.xsd", configServico);
                        break;
                    case versao.ve400:
                        Validador.Valida(xmlEventoCCe, "evCCeCTe_v4.00.xsd", configServico);
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                       "a versão está inválida, somente é permitido " +
                                                       "versão 2.00, 3.00, 4.00");
                }
            }

            if (container.GetType() == typeof(evPrestDesacordo))
            {
                var evPrestDesacordo = (evPrestDesacordo)container;

                var xmlEventoCCe = evPrestDesacordo.ObterXmlString();

                switch (versao)
                {
                    case versao.ve200:
                        Validador.Valida(xmlEventoCCe, "evPrestDesacordo_v2.00.xsd", configServico);
                        break;
                    case versao.ve300:
                        Validador.Valida(xmlEventoCCe, "evPrestDesacordo_v3.00.xsd", configServico);
                        break;
                    case versao.ve400:
                        Validador.Valida(xmlEventoCCe, "evPrestDesacordo_v4.00.xsd", configServico);
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                       "a versão está inválida, somente é permitido " +
                                                       "versão 2.00, 3.00, 4.00");
                }
            }
        }

        public static void SalvarXmlEmDisco(this eventoCTe eventoCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, eventoCTe.infEvento.chCTe + "-ped-eve.xml");

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