using System;
using System.Collections.Generic;
using DFe.Classes.Entidades;
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
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos
{
    /// <summary>
    ///     Fachada unificada para todos os serviços do MDF-e (Manifesto Eletrônico de Documentos Fiscais).
    ///     Substitui o uso direto das classes avulsas (ServicoMDFeRecepcao, ServicoMDFeEvento,
    ///     ServicoMDFeConsultaProtocolo, ServicoMDFeStatusServico, etc.) e dos métodos Assina()/Valida() nos modelos.
    ///     Utilize esta interface para injeção de dependência e testes unitários.
    /// </summary>
    public interface IServicosMDFe : IDisposable
    {
        // === Assinatura e Validação (migrados dos modelos) ===

        /// <summary>
        ///     Assina digitalmente um MDF-e, gerando a chave de acesso, o dígito verificador,
        ///     o QR Code e a assinatura XML. Substitui a chamada direta a <c>mdfe.Assina()</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser assinado</param>
        /// <returns>O mesmo objeto MDF-e, agora com Signature, Id, cDV e QR Code preenchidos</returns>
        MDFeEletronico Assina(MDFeEletronico mdfe);

        /// <summary>
        ///     Valida um MDF-e contra os schemas XSD (documento principal e modal de transporte).
        ///     Substitui a chamada direta a <c>mdfe.Valida()</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser validado</param>
        /// <returns>O mesmo objeto MDF-e, validado com sucesso</returns>
        MDFeEletronico Valida(MDFeEletronico mdfe);

        /// <summary>
        ///     Valida o envelope de envio (enviMDFe) e o MDF-e contido nele contra os schemas XSD.
        ///     Substitui a chamada direta a <c>enviMdfe.Valida()</c>.
        /// </summary>
        /// <param name="enviMdfe">O envelope de envio contendo o MDF-e</param>
        void Valida(MDFeEnviMDFe enviMdfe);

        // === Consulta de Protocolo ===

        /// <summary>
        ///     Consulta a situação atual de um MDF-e na SEFAZ pela chave de acesso.
        ///     Substitui o uso direto de <c>new ServicoMDFeConsultaProtocolo().MDFeConsultaProtocolo(chave)</c>.
        /// </summary>
        /// <param name="chave">Chave de acesso do MDF-e (44 dígitos)</param>
        /// <returns>Retorno da SEFAZ com a situação do MDF-e</returns>
        MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave);

        // === Consulta Não Encerrados ===

        /// <summary>
        ///     Consulta os MDF-e não encerrados para um determinado CNPJ/CPF.
        ///     Substitui o uso direto de <c>new ServicoMDFeConsultaNaoEncerrados().MDFeConsultaNaoEncerrados(cnpjCpf)</c>.
        /// </summary>
        /// <param name="cnpjCpf">CNPJ ou CPF do emitente</param>
        /// <returns>Retorno da SEFAZ com a lista de MDF-e não encerrados</returns>
        MDFeRetConsMDFeNao MDFeConsultaNaoEncerrados(string cnpjCpf);

        // === Eventos ===

        /// <summary>
        ///     Registra o evento de inclusão de condutor em um MDF-e.
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoIncluirCondutor(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e ao qual incluir o condutor</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="nome">Nome do condutor</param>
        /// <param name="cpf">CPF do condutor</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoIncluirCondutor(MDFeEletronico mdfe, byte sequenciaEvento, string nome, string cpf);

        /// <summary>
        ///     Registra o evento de inclusão de DF-e em um MDF-e.
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoIncluirDFe(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e ao qual incluir os documentos</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="protocolo">Protocolo de autorização do MDF-e</param>
        /// <param name="codigoMunicipioCarregamento">Código IBGE do município de carregamento</param>
        /// <param name="nomeMunicipioCarregamento">Nome do município de carregamento</param>
        /// <param name="informacoesDocumentos">Lista de documentos fiscais a incluir</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoIncluirDFe(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos);

        /// <summary>
        ///     Registra o evento de encerramento de um MDF-e (usando UF e município da configuração).
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoEncerramentoMDFeEventoEncerramento(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser encerrado</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="protocolo">Protocolo de autorização do MDF-e</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo);

        /// <summary>
        ///     Registra o evento de encerramento de um MDF-e informando UF e município de encerramento.
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoEncerramentoMDFeEventoEncerramento(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser encerrado</param>
        /// <param name="estadoEncerramento">UF onde ocorreu o encerramento</param>
        /// <param name="codigoMunicipioEncerramento">Código IBGE do município de encerramento</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="protocolo">Protocolo de autorização do MDF-e</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoEncerramento(MDFeEletronico mdfe, Estado estadoEncerramento,
            long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo);

        /// <summary>
        ///     Registra o evento de cancelamento de um MDF-e.
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoCancelar(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser cancelado</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="protocolo">Protocolo de autorização do MDF-e</param>
        /// <param name="justificativa">Justificativa do cancelamento (mínimo 15 caracteres)</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo, string justificativa);

        /// <summary>
        ///     Registra o evento de pagamento da operação de transporte de um MDF-e.
        ///     Substitui o uso direto de <c>new ServicoMDFeEvento().MDFeEventoPagamentoOperacaoTransporte(...)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e relacionado ao pagamento</param>
        /// <param name="sequenciaEvento">Número sequencial do evento</param>
        /// <param name="protocolo">Protocolo de autorização do MDF-e</param>
        /// <param name="infViagens">Informações das viagens</param>
        /// <param name="infPagamentos">Lista de informações de pagamento</param>
        /// <returns>Retorno da SEFAZ com o resultado do evento</returns>
        MDFeRetEventoMDFe MDFeEventoPagamentoOperacaoTransporte(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo,
            MDFeInfViagens infViagens, List<MDFeInfPag> infPagamentos);

        // === Recepção ===

        /// <summary>
        ///     Envia um MDF-e para a SEFAZ (recepção assíncrona por lote).
        ///     Substitui o uso direto de <c>new ServicoMDFeRecepcao().MDFeRecepcao(lote, mdfe)</c>.
        /// </summary>
        /// <param name="lote">Número do lote</param>
        /// <param name="mdfe">O MDF-e a ser enviado</param>
        /// <returns>Retorno da SEFAZ com o recibo do lote</returns>
        MDFeRetEnviMDFe MDFeRecepcao(long lote, MDFeEletronico mdfe);

        /// <summary>
        ///     Envia um MDF-e para a SEFAZ com recepção síncrona.
        ///     O retorno já contém o protocolo de autorização.
        ///     Substitui o uso direto de <c>new ServicoMDFeRecepcao().MDFeRecepcaoSinc(mdfe)</c>.
        /// </summary>
        /// <param name="mdfe">O MDF-e a ser enviado</param>
        /// <returns>Retorno da SEFAZ com o protocolo de autorização</returns>
        MDFeRetMDFe MDFeRecepcaoSinc(MDFeEletronico mdfe);

        // === Retorno Recepção ===

        /// <summary>
        ///     Consulta o resultado do processamento de um lote de MDF-e pelo número do recibo.
        ///     Substitui o uso direto de <c>new ServicoMDFeRetRecepcao().MDFeRetRecepcao(recibo)</c>.
        /// </summary>
        /// <param name="numeroRecibo">Número do recibo do lote</param>
        /// <returns>Retorno da SEFAZ com o resultado do processamento do lote</returns>
        MDFeRetConsReciMDFe MDFeRetRecepcao(string numeroRecibo);

        // === Status do Serviço ===

        /// <summary>
        ///     Consulta o status do serviço MDF-e na SEFAZ.
        ///     Substitui o uso direto de <c>new ServicoMDFeStatusServico().MDFeStatusServico()</c>.
        /// </summary>
        /// <returns>Retorno da SEFAZ com o status do serviço</returns>
        MDFeRetConsStatServ MDFeStatusServico();
    }
}
