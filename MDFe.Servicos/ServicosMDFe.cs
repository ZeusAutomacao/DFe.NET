using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DFe.Classes.Entidades;
using DFe.Utils;
using DFe.Utils.Assinatura;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Flags;
using MDFe.Classes.Informacoes;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Classes.Retorno.MDFeRecepcao;
using MDFe.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Classes.Retorno.MDFeRetRecepcao.Sincrono;
using MDFe.Classes.Retorno.MDFeStatusServico;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Servicos.ConsultaNaoEncerradosMDFe;
using MDFe.Servicos.ConsultaProtocoloMDFe;
using MDFe.Servicos.EventosMDFe;
using MDFe.Servicos.RecepcaoMDFe;
using MDFe.Servicos.RetRecepcaoMDFe;
using MDFe.Servicos.StatusServicoMDFe;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos
{
    public sealed class ServicosMDFe : IServicosMDFe
    {
        private readonly MDFeConfiguracao _configuracao;
        private readonly X509Certificate2 _certificado;
        private readonly bool _controlarCertificado;

        public ServicosMDFe(MDFeConfiguracao configuracao, X509Certificate2 certificado = null)
        {
            _configuracao = configuracao ?? throw new ArgumentNullException(nameof(configuracao));
            _controlarCertificado = certificado == null;
            _certificado = certificado ?? configuracao.X509Certificate2;
        }

        // === Assinatura ===

        public MDFeEletronico Assina(MDFeEletronico mdfe)
        {
            var modeloDocumentoFiscal = mdfe.InfMDFe.Ide.Mod;
            var tipoEmissao = (int)mdfe.InfMDFe.Ide.TpEmis;
            var codigoNumerico = mdfe.InfMDFe.Ide.CMDF;
            var estado = mdfe.InfMDFe.Ide.CUF;
            var dataEHoraEmissao = mdfe.InfMDFe.Ide.DhEmi;
            var cnpj = mdfe.InfMDFe.Emit.CNPJ;
            var numeroDocumento = mdfe.InfMDFe.Ide.NMDF;
            int serie = mdfe.InfMDFe.Ide.Serie;

            if (cnpj == null)
            {
                cnpj = mdfe.InfMDFe.Emit.CPF.PadLeft(14, '0');
            }

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal,
                serie, numeroDocumento, tipoEmissao, codigoNumerico);

            mdfe.InfMDFe.Id = "MDFe" + dadosChave.Chave;
            mdfe.InfMDFe.Versao = _configuracao.VersaoWebService.VersaoLayout;
            mdfe.InfMDFe.Ide.CDV = dadosChave.DigitoVerificador;

            mdfe.InfMDFeSupl = new MdfeInfMDFeSupl();
            mdfe.InfMDFeSupl.QrCodMDFe = MdfeInfMDFeSupl.GerarQrCode(dadosChave.Chave, mdfe.InfMDFe.Ide.TpAmb);
            if (mdfe.InfMDFe.Ide.TpEmis == MDFeTipoEmissao.Contingencia)
            {
                var encoding = Encoding.UTF8;
                var sign = Convert.ToBase64String(
                    AssinaturaDigital.CriarAssinaturaPkcs1(_certificado,
                        encoding.GetBytes(mdfe.Chave())));
                mdfe.InfMDFeSupl.QrCodMDFe += "&sign=" + sign;
            }

            var assinatura = AssinaturaDigital.Assina(mdfe, mdfe.InfMDFe.Id, _certificado);
            mdfe.Signature = assinatura;

            return mdfe;
        }

        // === Validação ===

        public MDFeEletronico Valida(MDFeEletronico mdfe)
        {
            var xmlMdfe = FuncoesXml.ClasseParaXmlString(mdfe);

            switch (_configuracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlMdfe, "mdfe_v1.00.xsd", _configuracao);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlMdfe, "mdfe_v3.00.xsd", _configuracao);
                    break;
            }

            var tipoModal = mdfe.InfMDFe.InfModal.Modal.GetType();
            var xmlModal = FuncoesXml.ClasseParaXmlString(mdfe.InfMDFe.InfModal);

            if (tipoModal == typeof(MDFeRodo))
            {
                switch (_configuracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "mdfeModalRodoviario_v1.00.xsd", _configuracao);
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "mdfeModalRodoviario_v3.00.xsd", _configuracao);
                        break;
                }
            }

            if (tipoModal == typeof(MDFeAereo))
            {
                switch (_configuracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "mdfeModalAereo_v1.00.xsd", _configuracao);
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "mdfeModalAereo_v3.00.xsd", _configuracao);
                        break;
                }
            }

            if (tipoModal == typeof(MDFeAquav))
            {
                switch (_configuracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "mdfeModalAquaviario_v1.00.xsd", _configuracao);
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "mdfeModalAquaviario_v3.00.xsd", _configuracao);
                        break;
                }
            }

            if (tipoModal == typeof(MDFeFerrov))
            {
                switch (_configuracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "mdfeModalFerroviario_v1.00.xsd", _configuracao);
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "mdfeModalFerroviario_v3.00.xsd", _configuracao);
                        break;
                }
            }

            return mdfe;
        }

        public void Valida(MDFeEnviMDFe enviMdfe)
        {
            var xmlMdfe = FuncoesXml.ClasseParaXmlString(enviMdfe);

            switch (_configuracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlMdfe, "enviMDFe_v1.00.xsd", _configuracao);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlMdfe, "enviMDFe_v3.00.xsd", _configuracao);
                    break;
            }

            Valida(enviMdfe.MDFe);
        }

        // === Consulta de Protocolo ===

        public MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave)
        {
            return new ServicoMDFeConsultaProtocolo().MDFeConsultaProtocolo(chave, _configuracao);
        }

        // === Consulta Não Encerrados ===

        public MDFeRetConsMDFeNao MDFeConsultaNaoEncerrados(string cnpjCpf)
        {
            return new ServicoMDFeConsultaNaoEncerrados().MDFeConsultaNaoEncerrados(cnpjCpf, _configuracao);
        }

        // === Eventos ===

        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(MDFeEletronico mdfe, byte sequenciaEvento, string nome, string cpf)
        {
            return new ServicoMDFeEvento().MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf, _configuracao);
        }

        public MDFeRetEventoMDFe MDFeEventoIncluirDFe(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos)
        {
            return new ServicoMDFeEvento().MDFeEventoIncluirDFe(mdfe, sequenciaEvento, protocolo,
                codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos, _configuracao);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo)
        {
            return new ServicoMDFeEvento().MDFeEventoEncerramentoMDFeEventoEncerramento(mdfe, sequenciaEvento, protocolo, _configuracao);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, Estado estadoEncerramento,
            long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo)
        {
            return new ServicoMDFeEvento().MDFeEventoEncerramentoMDFeEventoEncerramento(mdfe, estadoEncerramento,
                codigoMunicipioEncerramento, sequenciaEvento, protocolo, _configuracao);
        }

        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo, string justificativa)
        {
            return new ServicoMDFeEvento().MDFeEventoCancelar(mdfe, sequenciaEvento, protocolo, justificativa, _configuracao);
        }

        public MDFeRetEventoMDFe MDFeEventoPagamentoOperacaoTransporte(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo,
            MDFeInfViagens infViagens, List<MDFeInfPag> infPagamentos)
        {
            return new ServicoMDFeEvento().MDFeEventoPagamentoOperacaoTransporte(mdfe, sequenciaEvento, protocolo,
                infViagens, infPagamentos, _configuracao);
        }

        // === Recepção ===

        public MDFeRetEnviMDFe MDFeRecepcao(long lote, MDFeEletronico mdfe)
        {
            return new ServicoMDFeRecepcao().MDFeRecepcao(lote, mdfe, _configuracao);
        }

        public MDFeRetMDFe MDFeRecepcaoSinc(MDFeEletronico mdfe)
        {
            return new ServicoMDFeRecepcao().MDFeRecepcaoSinc(mdfe, _configuracao);
        }

        // === Retorno Recepção ===

        public MDFeRetConsReciMDFe MDFeRetRecepcao(string numeroRecibo)
        {
            return new ServicoMDFeRetRecepcao().MDFeRetRecepcao(numeroRecibo, _configuracao);
        }

        // === Status do Serviço ===

        public MDFeRetConsStatServ MDFeStatusServico()
        {
            return new ServicoMDFeStatusServico().MDFeStatusServico(_configuracao);
        }

        // === Dispose ===

        public void Dispose()
        {
            if (_controlarCertificado)
                _certificado?.Dispose();
        }
    }
}
