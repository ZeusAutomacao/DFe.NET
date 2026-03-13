using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Inutilizacao;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Classes.Servicos.Status;
using CTe.CTeOSClasses;
using CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao;
using CTe.Servicos.DistribuicaoDFe;
using CTe.Servicos.EnviarCte;
using CTe.Servicos.Inutilizacao;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos
{
    /// <summary>
    ///     Fachada unificada para todos os serviços do CT-e (modelo 57) e CT-e OS (modelo 67).
    ///     Substitui o uso direto das classes avulsas (ConsultaProtcoloServico, StatusServico,
    ///     ServicoCTeDistribuicaoDFe, ServicoCTeRecepcao, etc.) e dos métodos Assina() nos modelos.
    ///     Utilize esta interface para injeção de dependência e testes unitários.
    /// </summary>
    public interface IServicosCTe : IDisposable
    {
        // === Assinatura (migrados dos modelos) ===

        /// <summary>
        ///     Assina digitalmente um CT-e (modelo 57), gerando a chave de acesso, o dígito verificador
        ///     e a assinatura XML. Substitui a chamada direta a <c>cte.Assina()</c>.
        /// </summary>
        /// <param name="cte">O CT-e a ser assinado</param>
        /// <returns>O mesmo objeto CT-e, agora com Signature, Id e cDV preenchidos</returns>
        CTeEletronico Assina(CTeEletronico cte);

        /// <summary>
        ///     Assina digitalmente um CT-e OS (modelo 67), gerando a chave de acesso, o dígito verificador
        ///     e a assinatura XML. Substitui a chamada direta a <c>cteOs.Assina()</c>.
        /// </summary>
        /// <param name="cteOs">O CT-e OS a ser assinado</param>
        /// <returns>O mesmo objeto CT-e OS, agora com Signature, Id e cDV preenchidos</returns>
        CTeOS Assina(CTeOS cteOs);

        /// <summary>
        ///     Assina digitalmente um evento do CT-e (cancelamento, carta de correção, etc.).
        ///     Substitui a chamada direta a <c>evento.Assina()</c>.
        /// </summary>
        /// <param name="evento">O evento a ser assinado (deve ter infEvento.Id preenchido)</param>
        /// <returns>O mesmo objeto evento, agora com Signature preenchido</returns>
        eventoCTe Assina(eventoCTe evento);

        // === Validação de Schema (migrados dos modelos) ===

        /// <summary>
        ///     Valida o schema XML de um CT-e usando a configuração de serviço da instância.
        ///     Substitui a chamada obsoleta <c>cte.ValidaSchema(configuracaoServico)</c>.
        /// </summary>
        /// <param name="cte">O CT-e a ser validado</param>
        void ValidaSchema(CTeEletronico cte);

        /// <summary>
        ///     Valida o schema XML de um evento CT-e usando a configuração de serviço da instância.
        ///     Substitui a chamada obsoleta <c>eventoCTe.ValidarSchema(configuracaoServico)</c>.
        /// </summary>
        /// <param name="evento">O evento CT-e a ser validado</param>
        void ValidarSchema(eventoCTe evento);

        // === Consulta de Protocolo ===

        /// <summary>
        ///     Consulta a situação atual de um CT-e na SEFAZ pela chave de acesso (versões 2.00/3.00).
        ///     Substitui o uso direto de <c>new ConsultaProtcoloServico().ConsultaProtocolo(chave)</c>.
        /// </summary>
        /// <param name="chave">Chave de acesso do CT-e (44 dígitos)</param>
        /// <returns>Retorno da SEFAZ com a situação do CT-e</returns>
        retConsSitCTe ConsultaProtocolo(string chave);

        /// <summary>
        ///     Consulta a situação atual de um CT-e na SEFAZ pela chave de acesso (versão 4.00).
        ///     Substitui o uso direto de <c>new ConsultaProtcoloServico().ConsultaProtocoloV4(chave)</c>.
        /// </summary>
        /// <param name="chave">Chave de acesso do CT-e (44 dígitos)</param>
        /// <returns>Retorno da SEFAZ com a situação do CT-e</returns>
        retConsSitCTe ConsultaProtocoloV4(string chave);

        /// <summary>
        ///     Consulta a situação de um CT-e de forma assíncrona.
        ///     Substitui o uso direto de <c>new ConsultaProtcoloServico().ConsultaProtocoloAsync(chave)</c>.
        /// </summary>
        /// <param name="chave">Chave de acesso do CT-e (44 dígitos)</param>
        /// <returns>Retorno da SEFAZ com a situação do CT-e</returns>
        Task<retConsSitCTe> ConsultaProtocoloAsync(string chave);

        // === Consulta de Recibo ===

        /// <summary>
        ///     Consulta o resultado do processamento de um lote de CT-e pelo número do recibo.
        ///     Substitui o uso direto de <c>new ConsultaReciboServico(recibo).Consultar()</c>.
        /// </summary>
        /// <param name="recibo">Número do recibo do lote</param>
        /// <returns>Retorno da SEFAZ com o resultado do processamento do lote</returns>
        retConsReciCTe ConsultarRecibo(string recibo);

        /// <summary>
        ///     Consulta o resultado do processamento de um lote de CT-e de forma assíncrona.
        ///     Substitui o uso direto de <c>new ConsultaReciboServico(recibo).ConsultarAsync()</c>.
        /// </summary>
        /// <param name="recibo">Número do recibo do lote</param>
        /// <returns>Retorno da SEFAZ com o resultado do processamento do lote</returns>
        Task<retConsReciCTe> ConsultarReciboAsync(string recibo);

        // === Status do Serviço ===

        /// <summary>
        ///     Consulta o status do serviço CT-e na SEFAZ (versões 2.00/3.00).
        ///     Substitui o uso direto de <c>new StatusServico().ConsultaStatus()</c>.
        /// </summary>
        /// <returns>Retorno da SEFAZ com o status do serviço</returns>
        retConsStatServCte ConsultaStatus();

        /// <summary>
        ///     Consulta o status do serviço CT-e na SEFAZ (versão 4.00).
        ///     Substitui o uso direto de <c>new StatusServico().ConsultaStatusV4()</c>.
        /// </summary>
        /// <returns>Retorno da SEFAZ com o status do serviço</returns>
        retConsStatServCTe ConsultaStatusV4();

        /// <summary>
        ///     Consulta o status do serviço CT-e de forma assíncrona.
        ///     Substitui o uso direto de <c>new StatusServico().ConsultaStatusAsync()</c>.
        /// </summary>
        /// <returns>Retorno da SEFAZ com o status do serviço</returns>
        Task<retConsStatServCte> ConsultaStatusAsync();

        // === Distribuição DFe ===

        /// <summary>
        ///     Consulta documentos fiscais eletrônicos de interesse (distribuição DFe) do CT-e.
        ///     Permite baixar CT-e destinados ao CNPJ/CPF informado.
        ///     Substitui o uso direto de <c>new ServicoCTeDistribuicaoDFe(config, cert).CTeDistDFeInteresse(...)</c>.
        /// </summary>
        /// <param name="ufAutor">Código da UF do autor</param>
        /// <param name="documento">CNPJ ou CPF do interessado</param>
        /// <param name="ultNSU">Último NSU recebido (default "0")</param>
        /// <param name="nSU">NSU específico para consulta (default "0")</param>
        /// <returns>Retorno com os documentos de interesse</returns>
        RetornoCteDistDFeInt CTeDistDFeInteresse(string ufAutor, string documento, string ultNSU = "0", string nSU = "0");

        /// <summary>
        ///     Consulta documentos fiscais eletrônicos de interesse (distribuição DFe) de forma assíncrona.
        ///     Substitui o uso direto de <c>new ServicoCTeDistribuicaoDFe(config, cert).CTeDistDFeInteresseAsync(...)</c>.
        /// </summary>
        /// <param name="ufAutor">Código da UF do autor</param>
        /// <param name="documento">CNPJ ou CPF do interessado</param>
        /// <param name="ultNSU">Último NSU recebido (default "0")</param>
        /// <param name="nSU">NSU específico para consulta (default "0")</param>
        /// <returns>Retorno com os documentos de interesse</returns>
        Task<RetornoCteDistDFeInt> CTeDistDFeInteresseAsync(string ufAutor, string documento, string ultNSU = "0", string nSU = "0");

        // === Enviar CTe ===

        /// <summary>
        ///     Envia um CT-e para a SEFAZ, aguarda o recibo e consulta o resultado automaticamente.
        ///     Substitui o uso direto de <c>new ServicoEnviarCte().Enviar(lote, cte)</c>.
        /// </summary>
        /// <param name="lote">Número do lote</param>
        /// <param name="cte">O CT-e a ser enviado</param>
        /// <returns>Retorno completo com envio, recibo e protocolo</returns>
        RetornoEnviarCte Enviar(int lote, CTeEletronico cte);

        /// <summary>
        ///     Envia um CT-e para a SEFAZ de forma assíncrona, aguarda o recibo e consulta o resultado.
        ///     Substitui o uso direto de <c>new ServicoEnviarCte().EnviarAsync(lote, cte)</c>.
        /// </summary>
        /// <param name="lote">Número do lote</param>
        /// <param name="cte">O CT-e a ser enviado</param>
        /// <returns>Retorno completo com envio, recibo e protocolo</returns>
        Task<RetornoEnviarCte> EnviarAsync(int lote, CTeEletronico cte);

        // === Inutilização ===

        /// <summary>
        ///     Inutiliza uma faixa de numeração de CT-e na SEFAZ.
        ///     Substitui o uso direto de <c>new InutilizacaoServico(config).Inutilizar()</c>.
        /// </summary>
        /// <param name="configInutiliza">Configuração com CNPJ, série, faixa numérica e justificativa</param>
        /// <returns>Retorno da SEFAZ com o resultado da inutilização</returns>
        retInutCTe Inutilizar(ConfigInutiliza configInutiliza);

        /// <summary>
        ///     Inutiliza uma faixa de numeração de CT-e na SEFAZ de forma assíncrona.
        ///     Substitui o uso direto de <c>new InutilizacaoServico(config).InutilizarAsync()</c>.
        /// </summary>
        /// <param name="configInutiliza">Configuração com CNPJ, série, faixa numérica e justificativa</param>
        /// <returns>Retorno da SEFAZ com o resultado da inutilização</returns>
        Task<retInutCTe> InutilizarAsync(ConfigInutiliza configInutiliza);

        // === Recepção CTe ===

        /// <summary>
        ///     Envia um lote de CT-e para a SEFAZ (recepção assíncrona por lote).
        ///     Substitui o uso direto de <c>new ServicoCTeRecepcao().CTeRecepcao(lote, lista)</c>.
        /// </summary>
        /// <param name="lote">Número do lote</param>
        /// <param name="cteEletronicosList">Lista de CT-e a enviar no lote</param>
        /// <returns>Retorno da SEFAZ com o recibo do lote</returns>
        retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList);

        /// <summary>
        ///     Envia um lote de CT-e para a SEFAZ de forma assíncrona.
        ///     Substitui o uso direto de <c>new ServicoCTeRecepcao().CTeRecepcaoAsync(lote, lista)</c>.
        /// </summary>
        /// <param name="lote">Número do lote</param>
        /// <param name="cteEletronicosList">Lista de CT-e a enviar no lote</param>
        /// <returns>Retorno da SEFAZ com o recibo do lote</returns>
        Task<retEnviCte> CTeRecepcaoAsync(int lote, List<CTeEletronico> cteEletronicosList);

        /// <summary>
        ///     Envia um CT-e para a SEFAZ com recepção síncrona (versão 4.00).
        ///     O retorno já contém o protocolo de autorização.
        ///     Substitui o uso direto de <c>new ServicoCTeRecepcao().CTeRecepcaoSincronoV4(cte)</c>.
        /// </summary>
        /// <param name="cte">O CT-e a ser enviado</param>
        /// <returns>Retorno da SEFAZ com o protocolo de autorização</returns>
        retCTe CTeRecepcaoSincronoV4(CTeEletronico cte);

        // === Recepção CTeOS ===

        /// <summary>
        ///     Envia um CT-e OS (modelo 67) para a SEFAZ com recepção síncrona (versão 4.00).
        ///     Substitui o uso direto de <c>new ServicoCTeOSRecepcao().CTeRecepcaoSincronoV4(cte)</c>.
        /// </summary>
        /// <param name="cte">O CT-e OS a ser enviado</param>
        /// <returns>Retorno da SEFAZ com o protocolo de autorização</returns>
        retCTeOS CTeOSRecepcaoSincronoV4(CTeOS cte);
    }
}
