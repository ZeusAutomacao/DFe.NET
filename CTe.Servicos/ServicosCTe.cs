using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using System.Xml;
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Classes.Servicos.Recepcao;
using CTeDLL.Classes.Servicos.Recepcao.Retorno;
using CTeDLL.Classes.Servicos.Status;
using CTeDLL.Classes.Servicos.Tipos;
using CTeDLL.Servicos.Retorno;
using CTeDLL.Wsdl;
using CTeDLL.Wsdl.ConsultaProtocolo;
using CTeDLL.Wsdl.Evento;
using CTeDLL.Wsdl.Inutilizacao;
using CTeDLL.Wsdl.Recepcao;
using CTeDLL.Wsdl.Status;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTeDLL.Servicos
{
    public class ServicosCTe /*: IDisposable*/
    {
        private readonly X509Certificate2 _certificado;
        private readonly ConfiguracaoServico _cFgServico;
        private readonly string _path;

        /*/// <summary>
        ///     Cria uma instância da Classe responsável pelos serviços relacionados à CTe
        /// </summary>
        /// <param name="cFgServico"></param>
        public ServicosCTe(ConfiguracaoServico cFgServico)
        {
            _cFgServico = cFgServico;
            _certificado = string.IsNullOrEmpty(_cFgServico.Certificado.Arquivo)
                ? CertificadoDigital.ObterDoRepositorio(_cFgServico.Certificado.Serial, _cFgServico.Certificado.Senha)
                : CertificadoDigital.ObterDeArquivo(_cFgServico.Certificado.Arquivo, _cFgServico.Certificado.Senha);
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void SalvarArquivoXml(string nomeArquivo, string xmlString, ServicoCTe servicoCTe)
        {
            if (!_cFgServico.SalvarXmlServicos) return;
            var dir = string.IsNullOrEmpty(_cFgServico.DiretorioSalvarXml) ? _path : _cFgServico.DiretorioSalvarXml;

            switch (servicoCTe)
            {
                case ServicoCTe.RecepcaoEventoCancelmento:
                    dir += @"\arquivos\\procEventoCancCTe";
                    break;

                case ServicoCTe.RecepcaoEventoCartaCorrecao:
                    dir += @"\arquivos\\procEventoCCeCTe";
                    break;
                
                case ServicoCTe.RecepcaoEventoEpec:
                    dir += @"\arquivos\\procEventoEpec";
                    break;
                
                case ServicoCTe.RecepcaoEventoManifestacaoDestinatario:
                    dir += @"\arquivos\\procEventoManDest";
                    break;

                case ServicoCTe.CteRecepcao:
                    dir += @"\lotes";
                    break;

                case ServicoCTe.CteRetRecepcao:
                    dir += @"\retornos";
                    break;

                case ServicoCTe.CteConsultaCadastro:
                    dir += @"\arquivos\\ConsCad";
                    break;

                case ServicoCTe.CteInutilizacao:
                    dir += @"\arquivos\\Inutilizacao";
                    break;

                case ServicoCTe.CteConsultaProtocolo:
                    dir += @"\arquivos\\ConsProt";
                    break;

                case ServicoCTe.CteStatusServico:
                    dir += @"\arquivos\\ConsStatus";
                    break;

                case ServicoCTe.CteAutorizacao:
                    dir += @"\lotes";
                    break;

                case ServicoCTe.CteRetAutorizacao:
                    dir += @"\retornos";
                    break;

                case ServicoCTe.CteDistribuicaoDFe:
                    dir += @"\arquivos\\ConfRecebto";
                    break;

                case ServicoCTe.CteConsultaDest:
                    dir += @"\arquivos\\ConfRecebto";
                    break;

                case ServicoCTe.CteDownloadNF:
                    //dir += @"\arquivos\\DownloadNFe";
                    dir += @"\retornos";
                    break;
            }

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var stw = new StreamWriter(dir + @"\" + nomeArquivo);
            stw.WriteLine(xmlString);
            stw.Close();
        }

        private ICteServico CriarServico(ServicoCTe servico)
        {
            var url = Enderecador.ObterUrlServico(servico, _cFgServico);
            switch (servico)
            {
                case ServicoCTe.CteStatusServico:
                    return new CteStatusServico(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteConsultaProtocolo:
                    return new CteConsulta(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteRecepcao:
                    return new CteRecepcao(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteRetRecepcao:
                    return new CteRetRecepcao(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteAutorizacao:
                    return new CteRecepcao(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteRetAutorizacao:
                    return new CteRetRecepcao(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.CteInutilizacao:
                    return new CteInutilizacao(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.RecepcaoEventoCancelmento:
                    return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);

                case ServicoCTe.RecepcaoEventoCartaCorrecao:
                    return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoCTe.RecepcaoEventoManifestacaoDestinatario:
                    return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoCTe.RecepcaoEventoEpec:
                        return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);

                //case ServicoCTe.CteConsultaCadastro:
                //    switch (_cFgServico.cUF)
                //    {
                //        case Estado.CE:
                //            return new Wsdl.ConsultaCadastro.CE.CadConsultaCadastro2(url, _certificado, _cFgServico.TimeOut);
                //    }
                //    return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro2(url, _certificado, _cFgServico.TimeOut);

                //case ServicoCTe.CteDownloadNF:
                //    return new NfeDownloadNF(url, _certificado, _cFgServico.TimeOut);

                //case ServicoCTe.CteDistribuicaoDFe:
                //    return new NfeDistDFeInteresse(url, _certificado, _cFgServico.TimeOut);

            }

            return null;
        }

        #region CTeStatusServico
        /// <summary>
        ///     Consulta o status do Serviço de CTe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoCteStatusServico com os dados status do serviço</returns>
        public RetornoCteStatusServico CteStatusServico()
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteStatusServico, _cFgServico.VersaoCteStatusServico);
            var _tpAmb = Auxiliar.AmbienteParaString(_cFgServico.tpAmb);
            var _UF = Auxiliar.EstadoParaString(_cFgServico.cUF);
            var _cUF = Auxiliar.EstadoParaID(_UF);
            var _url = Enderecador.ObterUrlServico(ServicoCTe.CteStatusServico, _cFgServico);

            var certificado = string.IsNullOrEmpty(_cFgServico.Certificado.Arquivo)
                                                    ? CertificadoDigital.ObterDoRepositorio(_cFgServico.Certificado.Serial, _cFgServico.Certificado.Senha)
                                                    : CertificadoDigital.ObterDeArquivo(_cFgServico.Certificado.Arquivo, _cFgServico.Certificado.Senha);

            #region Cria o objeto wdsl para consulta

            //var ws = CriarServico(ServicoCTe.CteStatusServico);

            //ws.cteCabecMsg = new cteCabecMsg
            //{
            //    cUF = _cFgServico.cUF,
            //    versaoDados = versaoServico
            //};

            CertDigital cert = new CertDigital();
            CteStatusServico2.CteStatusServico ws = new CteStatusServico2.CteStatusServico();
            CteStatusServico2.cteCabecMsg _cab = new CteStatusServico2.cteCabecMsg();

            _cab.cUF = Convert.ToString(_cUF);
            _cab.versaoDados = versaoServico;

            ws.Url = _url;
            ws.cteCabecMsgValue = _cab;
            ws.SoapVersion = SoapProtocolVersion.Soap12;
            ws.Timeout = _cFgServico.TimeOut;

            ws.ClientCertificates.Add(cert.BuscaNroSerie(certificado.SerialNumber));

            #endregion

            #region Cria o objeto consStatServ

            var pedStatus = new consStatServCte
            {
                tpAmb = _cFgServico.tpAmb,
                versao = versaoServico
            };

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlStatus = pedStatus.ObterXmlString();            
            Validador.Valida(ServicoCTe.CteStatusServico, _cFgServico.VersaoCteStatusServico, xmlStatus, true, _cFgServico.DiretorioSchemas);
            var dadosStatus = new XmlDocument();
            dadosStatus.LoadXml(xmlStatus);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-ped-sta.xml", xmlStatus, ServicoCTe.CteStatusServico);

            var retorno = ws.cteStatusServicoCT(dadosStatus);
            var retornoXmlString = retorno.OuterXml;
            var retConsStatServ = new retConsStatServCte().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-sta.xml", retornoXmlString, ServicoCTe.CteStatusServico);

            return new RetornoCteStatusServico(pedStatus.ObterXmlString(), retConsStatServ.ObterXmlString(), retornoXmlString, retConsStatServ);
            
            #endregion

        }
        #endregion

        #region CTeConsultaProtocolo
        /// <summary>
        ///     Consulta a Situação da CTe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoCteConsultaProtocolo com os dados da Situação da CTe</returns>
        public RetornoCTeConsultaProtocolo CteConsultaProtocolo(string chave)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteConsultaProtocolo, _cFgServico.VersaoCteConsultaProtocolo);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteConsultaProtocolo);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consSitCTe

            var pedConsulta = new consSitCTe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                chCTe = chave
            };

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedConsulta.ObterXmlString();
            Validador.Valida(ServicoCTe.CteConsultaProtocolo, _cFgServico.VersaoCteConsultaProtocolo, xmlConsulta,true,_cFgServico.DiretorioSchemas);
            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            SalvarArquivoXml(chave + "-ped-sit.xml", xmlConsulta, ServicoCTe.CteConsultaProtocolo);

            var retorno = ws.Execute(dadosConsulta);
            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retConsSitCTe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(chave + "-sit.xml", retornoXmlString, ServicoCTe.CteConsultaProtocolo);

            return new RetornoCTeConsultaProtocolo(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

            #endregion
        }
        #endregion

        #region CteInutilizacao
        /// <summary>
        ///     Inutiliza uma faixa de números
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ano"></param>
        /// <param name="modelo"></param>
        /// <param name="serie"></param>
        /// <param name="numeroInicial"></param>
        /// <param name="numeroFinal"></param>
        /// <param name="justificativa"></param>
        /// <returns>Retorna um objeto da classe RetornoCteInutilizacao com o retorno do serviço CteInutilizacao</returns>
        public RetornoCteInutilizacao CteInutilizacao(string cnpj, int ano, ModeloDocumento modelo, int serie, long numeroInicial, long numeroFinal, string justificativa)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteInutilizacao, _cFgServico.VersaoCteInutilizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteInutilizacao);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto inutNFe

            var pedInutilizacao = new inutCTe
            {
                versao = versaoServico,
                infInut = new infInutEnv
                {
                    tpAmb = _cFgServico.tpAmb,
                    cUF = _cFgServico.cUF,
                    ano = ano,
                    CNPJ = cnpj,
                    mod = modelo,
                    serie = serie,
                    nNFIni = numeroInicial,
                    nNFFin = numeroFinal,
                    xJust = justificativa
                }
            };

            var numId = String.Concat((int)pedInutilizacao.infInut.cUF, pedInutilizacao.infInut.ano,
                pedInutilizacao.infInut.CNPJ, (int)pedInutilizacao.infInut.mod, pedInutilizacao.infInut.serie.ToString().PadLeft(3, '0'),
                pedInutilizacao.infInut.nNFIni.ToString().PadLeft(9, '0'), pedInutilizacao.infInut.nNFFin.ToString().PadLeft(9, '0'));
            pedInutilizacao.infInut.Id = "ID" + numId;

            pedInutilizacao.Assina();

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlInutilizacao = pedInutilizacao.ObterXmlString();
            Validador.Valida(ServicoCTe.CteInutilizacao, _cFgServico.VersaoCteInutilizacao, xmlInutilizacao, true, _cFgServico.DiretorioSchemas);
            var dadosInutilizacao = new XmlDocument();
            dadosInutilizacao.LoadXml(xmlInutilizacao);

            SalvarArquivoXml(numId + "-ped-inu.xml", xmlInutilizacao, ServicoCTe.CteInutilizacao);

            var retorno = ws.Execute(dadosInutilizacao);
            var retornoXmlString = retorno.OuterXml;
            var retInutNFe = new retInutCTe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(numId + "-inu.xml", retornoXmlString, ServicoCTe.CteInutilizacao);

            return new RetornoCteInutilizacao(pedInutilizacao.ObterXmlString(), retInutNFe.ObterXmlString(), retornoXmlString, retInutNFe);

            #endregion
        }

        #endregion

        #region RecepcaoEvento

        /// <summary>
        ///     Envia um evento genérico
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="eventos"></param>
        /// <param name="servicoEvento">Tipo de serviço do evento: valores válidos: RecepcaoEventoCancelmento, RecepcaoEventoCartaCorrecao, RecepcaoEventoEpec e RecepcaoEventoManifestacaoDestinatario</param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        private RetornoRecepcaoEvento RecepcaoEvento(int idlote, List<evento> eventos, ServicoCTe servicoEvento)
        {
            var listaEventos = new List<ServicoCTe>
            {
                ServicoCTe.RecepcaoEventoCartaCorrecao,
                ServicoCTe.RecepcaoEventoCancelmento,
                ServicoCTe.RecepcaoEventoEpec,
                ServicoCTe.RecepcaoEventoManifestacaoDestinatario
            };
            if (
                !listaEventos.Contains(servicoEvento))
                throw new Exception(string.Format("Serviço {0} é inválido para o método {1}!\nServiços válidos: \n • {2}", servicoEvento,
                    MethodBase.GetCurrentMethod().Name, string.Join("\n • ", listaEventos.ToArray())));

            var versaoServico = Auxiliar.VersaoServicoParaString(servicoEvento, _cFgServico.VersaoRecepcaoEventoCceCancelamento);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(servicoEvento);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto envEvento

            var pedEvento = new envEvento
            {
                versao = versaoServico,
                idLote = idlote,
                evento = eventos
            };

            foreach (var evento in eventos)
            {
                evento.infEvento.Id = "ID" + evento.infEvento.tpEvento + evento.infEvento.chNFe + evento.infEvento.nSeqEvento.ToString().PadLeft(2, '0');
                evento.Assina();
            }

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlEvento = pedEvento.ObterXmlString();
            Validador.Valida(servicoEvento, _cFgServico.VersaoRecepcaoEventoCceCancelamento, xmlEvento, true, _cFgServico.DiretorioSchemas);
            var dadosEvento = new XmlDocument();
            dadosEvento.LoadXml(xmlEvento);

            SalvarArquivoXml(idlote + "-ped-eve.xml", xmlEvento, servicoEvento);

            var retorno = ws.Execute(dadosEvento);
            var retornoXmlString = retorno.OuterXml;
            var retEnvEvento = new retEnvEvento().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(idlote + "-eve.xml", retornoXmlString, servicoEvento);

            #region Obtém um procEventoNFe de cada evento e salva em arquivo

            var listprocEventoNFe = new List<procEventoCTe>();

            foreach (var evento in eventos)
            {
                var eve = evento;
                var query = (from retevento in retEnvEvento.retEvento
                             where retevento.infEvento.chNFe == eve.infEvento.chNFe && retevento.infEvento.tpEvento == eve.infEvento.tpEvento
                             select retevento).SingleOrDefault();

                var procevento = new procEventoCTe { evento = eve, versao = eve.versao, retEvento = new List<retEvento> { query } };
                listprocEventoNFe.Add(procevento);
                if (!_cFgServico.SalvarXmlServicos) continue;
                var proceventoXmlString = procevento.ObterXmlString();
                SalvarArquivoXml(procevento.evento.infEvento.Id.Substring(2) + "-procEventoCTe.xml", proceventoXmlString, servicoEvento);
            }

            #endregion

            return new RetornoRecepcaoEvento(pedEvento.ObterXmlString(), retEnvEvento.ObterXmlString(), retornoXmlString, retEnvEvento, listprocEventoNFe);

            #endregion
        }

        /// <summary>
        ///     Envia um evento do tipo "Cancelamento"
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="protocoloAutorizacao"></param>
        /// <param name="chaveNFe"></param>
        /// <param name="justificativa"></param>
        /// <param name="cpfcnpj"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public RetornoRecepcaoEvento RecepcaoEventoCancelamento(int idlote, int sequenciaEvento, string protocoloAutorizacao, string chaveNFe, string justificativa, string cpfcnpj)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.RecepcaoEventoCancelmento, _cFgServico.VersaoRecepcaoEventoCceCancelamento);
            var detEvento = new detEvento { nProt = protocoloAutorizacao, versao = versaoServico, xJust = justificativa };
            var infEvento = new infEventoEnv
            {
                cOrgao = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaveNFe,
                dhEvento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpEvento = 110111,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detEvento
            };
            if (cpfcnpj.Length == 11)
                infEvento.CPF = cpfcnpj;
            else
                infEvento.CNPJ = cpfcnpj;

            var evento = new evento { versao = versaoServico, infEvento = infEvento };

            var retorno = RecepcaoEvento(idlote, new List<evento> { evento }, ServicoCTe.RecepcaoEventoCancelmento);
            return retorno;
        }

        /// <summary>
        ///     Envia um evento do tipo "Carta de Correção"
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="chaveNFe"></param>
        /// <param name="correcao"></param>
        /// <param name="cpfcnpj"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public RetornoRecepcaoEvento RecepcaoEventoCartaCorrecao(int idlote, int sequenciaEvento, string chaveNFe, string correcao, string cpfcnpj)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.RecepcaoEventoCartaCorrecao, _cFgServico.VersaoRecepcaoEventoCceCancelamento);
            var detEvento = new detEvento { versao = versaoServico, xCorrecao = correcao };
            var infEvento = new infEventoEnv
            {
                cOrgao = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaveNFe,
                dhEvento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpEvento = 110110,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detEvento
            };
            if (cpfcnpj.Length == 11)
                infEvento.CPF = cpfcnpj;
            else
                infEvento.CNPJ = cpfcnpj;

            var evento = new evento { versao = versaoServico, infEvento = infEvento };

            var retorno = RecepcaoEvento(idlote, new List<evento> { evento }, ServicoCTe.RecepcaoEventoCartaCorrecao);
            return retorno;
        }

        /// <summary>
        ///     Envia um evento do tipo "Manifestação do Destinatário"
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="chaveNFe"></param>
        /// <param name="tipoEvento"></param>
        /// <param name="cpfcnpj"></param>
        /// <param name="justificativa"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public RetornoRecepcaoEvento RecepcaoEventoManifestacaoDestinatario(int idlote, int sequenciaEvento, string chaveNFe, TipoEventoManifestacaoDestinatario tipoEventoManifestacaoDestinatario, string cpfcnpj, string justificativa = null)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.RecepcaoEventoManifestacaoDestinatario, _cFgServico.VersaoRecepcaoEventoManifestacaoDestinatario);
            var detEvento = new detEvento { versao = versaoServico, descEvento = tipoEventoManifestacaoDestinatario.Descricao(), xJust = justificativa };
            var infEvento = new infEventoEnv
            {
                cOrgao = _cFgServico.cUF == Estado.RS ? _cFgServico.cUF : Estado.AN, //RS possui endereço próprio para manifestação do destinatário. Demais UFs usam o ambiente nacional
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaveNFe,
                dhEvento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpEvento = (int)tipoEventoManifestacaoDestinatario,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detEvento
            };
            if (cpfcnpj.Length == 11)
                infEvento.CPF = cpfcnpj;
            else
                infEvento.CNPJ = cpfcnpj;

            var evento = new evento { versao = versaoServico, infEvento = infEvento };

            var retorno = RecepcaoEvento(idlote, new List<evento> { evento }, ServicoCTe.RecepcaoEventoManifestacaoDestinatario);
            return retorno;
        }

        /// <summary>
        ///     Envia um evento do tipo "EPEC"
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="nfe"></param>
        /// <param name="veraplic"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public RetornoRecepcaoEvento RecepcaoEventoEpec(int idlote, int sequenciaEvento, Classes.CTe nfe, string veraplic)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.RecepcaoEventoEpec, _cFgServico.VersaoRecepcaoEventoEpec);

            if (string.IsNullOrEmpty(nfe.infCte.Id))
                nfe.Assina().Valida();

            var detevento = new detEvento
            {
                versao = versaoServico,
                cOrgaoAutor = nfe.infCte.ide.cUF,
                tpAutor = TipoAutor.taEmpresaEmitente,
                verAplic = veraplic,
                dhEmi = nfe.infCte.ide.dhEmi,
                tpCTe = nfe.infCte.ide.tpCTe,
                IE = nfe.infCte.emit.IE,
                dest = new dest
                {
                    UF = nfe.infCte.dest.enderDest.UF,
                    CNPJ = nfe.infCte.dest.CNPJ,
                    CPF = nfe.infCte.dest.CPF,
                    IE = nfe.infCte.dest.IE
                }
            };

            var infEvento = new infEventoEnv
            {
                cOrgao = Estado.AN,
                tpAmb = nfe.infCte.ide.tpAmb,
                CNPJ = nfe.infCte.emit.CNPJ,
                chNFe = nfe.infCte.Id.Substring(3),
                dhEvento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpEvento = 110140,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detevento
            };

            var evento = new evento { versao = versaoServico, infEvento = infEvento };

            var retorno = RecepcaoEvento(idlote, new List<evento> { evento }, ServicoCTe.RecepcaoEventoEpec);
            return retorno;
        }

        #endregion

        #region CteConsultaCadastro
        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        ///     <para>O documento pode ser: CPF ou CNPJ. O serviço avaliará o tamanho da string passada e determinará se a coonsulta será por CPF ou por CNPJ</para>
        /// </summary>
        /// <param name="uf"></param>
        /// <param name="documento"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaCadastro com o retorno do serviço NfeConsultaCadastro</returns>
        //public RetornoCteConsultaCadastro CteConsultaCadastro(string uf, string documento, int tipoConsulta)
        //{
        //    var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteConsultaCadastro, _cFgServico.VersaoCteConsultaCadastro);

        //    #region Cria o objeto wdsl para consulta

        //    var ws = CriarServico(ServicoCTe.CteConsultaCadastro);

        //    ws.cteCabecMsg = new cteCabecMsg
        //    {
        //        cUF = _cFgServico.cUF,
        //        versaoDados = versaoServico
        //    };

        //    #endregion

        //    #region Cria o objeto ConsCad

        //    var pedConsulta = new ConsCad
        //    {
        //        versao = versaoServico,
        //        infCons = new infConsEnv { UF = uf }
        //    };

        //    if (tipoConsulta == 0)
        //        pedConsulta.infCons.IE = documento;

        //    if (tipoConsulta == 1)
        //    {
        //        if (documento.Length == 11)
        //            pedConsulta.infCons.CPF = documento;
        //        if (documento.Length > 11)
        //            pedConsulta.infCons.CNPJ = documento;
        //    }

        //    #endregion

        //    #region Valida, Envia os dados e obtém a resposta

        //    var xmlConsulta = pedConsulta.ObterXmlString();
        //    Validador.Valida(ServicoCTe.CteConsultaCadastro, _cFgServico.VersaoCteConsultaCadastro, xmlConsulta, true, _cFgServico.DiretorioSchemas);
        //    var dadosConsulta = new XmlDocument();
        //    dadosConsulta.LoadXml(xmlConsulta);

        //    SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-ped-cad.xml", xmlConsulta, ServicoCTe.CteConsultaCadastro);

        //    var retorno = ws.Execute(dadosConsulta);
        //    var retornoXmlString = retorno.OuterXml;
        //    var retConsulta = new retConsCad().CarregarDeXmlString(retornoXmlString);

        //    SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-cad.xml", retornoXmlString, ServicoCTe.CteConsultaCadastro);

        //    return new RetornoNfeConsultaCadastro(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

        //    #endregion
        //}

        #endregion

        #region NFeDistribuicaoDFe

        ///// <summary>
        ///// Serviço destinado à distribuição de informações resumidas e documentos fiscais eletrônicos de interesse de um ator, seja este pessoa física ou jurídica.
        ///// </summary>
        ///// <param name="ufAutor">Código da UF do Autor</param>
        ///// <param name="documento">CNPJ/CPF do interessado no DF-e</param>
        ///// <param name="ultNSU">Último NSU recebido pelo Interessado</param>
        ///// <param name="nSU">Número Sequencial Único</param>
        ///// <returns>Retorna um objeto da classe RetornoNfeDistDFeInt com os documentos de interesse do CNPJ/CPF pesquisado</returns>
        //public RetornoNfeDistDFeInt NfeDistDFeInteresse(string ufAutor, string documento, string ultNSU, string nSU = "0")
        //{
        //    var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NFeDistribuicaoDFe, _cFgServico.VersaoNFeDistribuicaoDFe);

        //    #region Cria o objeto wdsl para consulta

        //    var ws = CriarServico(ServicoNFe.NFeDistribuicaoDFe);

        //    ws.nfeCabecMsg = new nfeCabecMsg
        //    {
        //        cUF = _cFgServico.cUF,
        //        versaoDados = versaoServico
        //    };

        //    #endregion

        //    #region Cria o objeto distDFeInt

        //    var pedDistDFeInt = new distDFeInt
        //    {
        //        versao = versaoServico,
        //        tpAmb = _cFgServico.tpAmb,
        //        cUFAutor = _cFgServico.cUF,
        //        distNSU = new distNSU { ultNSU = ultNSU.PadLeft(15, '0') }

        //    };

        //    if (documento.Length == 11)
        //        pedDistDFeInt.CPF = documento;
        //    if (documento.Length > 11)
        //        pedDistDFeInt.CNPJ = documento;
        //    if (!nSU.Equals("0"))
        //        pedDistDFeInt.consNSU = new consNSU { NSU = nSU.PadLeft(15, '0') };

        //    #endregion

        //    #region Valida, Envia os dados e obtém a resposta

        //    var xmlConsulta = pedDistDFeInt.ObterXmlString();
        //    Validador.Valida(ServicoNFe.NFeDistribuicaoDFe, _cFgServico.VersaoNFeDistribuicaoDFe, xmlConsulta, true,_cFgServico.DiretorioSchemas);
        //    var dadosConsulta = new XmlDocument();
        //    dadosConsulta.LoadXml(xmlConsulta);

        //    SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-ped-DistDFeInt.xml", xmlConsulta, ServicoNFe.NFeDistribuicaoDFe);

        //    var retorno = ws.Execute(dadosConsulta);
        //    var retornoXmlString = retorno.OuterXml;
        //    var retConsulta = new retDistDFeInt().CarregarDeXmlString(retornoXmlString);

        //    SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-distDFeInt.xml", retornoXmlString, ServicoNFe.NFeDistribuicaoDFe);

        //    return new RetornoNfeDistDFeInt(pedDistDFeInt.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

        //    #endregion
        //}

        #endregion

        #region Recepção

        /// <summary>
        ///     Envia uma ou mais CTe
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="cTes"></param>
        /// <returns>Retorna um objeto da classe RetornoCteRecepcao com com os dados do resultado da transmissão</returns>
        public RetornoCteRecepcao CteRecepcao(int idLote, List<Classes.CTe> cTes)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteRecepcao, _cFgServico.VersaoCteRecepcao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteRecepcao);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto enviNFe

            var pedEnvio = new enviCTe(versaoServico, idLote, cTes);

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();
            Validador.Valida(ServicoCTe.CteRecepcao, _cFgServico.VersaoCteRecepcao, xmlEnvio, true, _cFgServico.DiretorioSchemas);
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio, ServicoCTe.CteRecepcao);

            var retorno = ws.Execute(dadosEnvio);
            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviCTe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString, ServicoCTe.CteRetRecepcao);

            return new RetornoCteRecepcao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString, retEnvio);

            #endregion
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais CTe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoCteRetRecepcao com com os dados do processamento do lote</returns>
        public RetornoCteRetRecepcao CteRetRecepcao(string recibo)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteRetRecepcao, _cFgServico.VersaoCteRetRecepcao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteRetRecepcao);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consReciNFe

            var pedRecibo = new consReciCTe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                nRec = recibo
            };

            #endregion

            #region Envia os dados e obtém a resposta

            var xmlRecibo = pedRecibo.ObterXmlString();
            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlRecibo);

            SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo, ServicoCTe.CteRetRecepcao);

            var retorno = ws.Execute(dadosRecibo);
            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retconsReciCTe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString, ServicoCTe.CteRetRecepcao);

            return new RetornoCteRetRecepcao(pedRecibo.ObterXmlString(), retRecibo.ObterXmlString(), retornoXmlString, retRecibo);

            #endregion
        }

        #endregion

        #region Autorização

        /// <summary>
        ///     Envia uma ou mais CTe
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="indSinc"></param>
        /// <param name="cTes"></param>
        /// <returns>Retorna um objeto da classe RetornoCTeAutorizacao com com os dados do resultado da transmissão</returns>
        public RetornoCteRecepcao CTeAutorizacao(int idLote, IndicadorSincronizacao indSinc, List<Classes.CTe> cTes)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteAutorizacao, _cFgServico.VersaoCteAutorizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteAutorizacao);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto enviNFe

            var pedEnvio = new enviCTe(versaoServico, idLote, cTes);

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();
            //if (_cFgServico.cUF == Estado.PR) //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
            //    xmlEnvio = xmlEnvio.Replace("<CTe>", "<CTe xmlns=\"http://www.portalfiscal.inf.br/cte\">");
            Validador.Valida(ServicoCTe.CteAutorizacao, _cFgServico.VersaoCteAutorizacao, xmlEnvio, true, _cFgServico.DiretorioSchemas);
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio, ServicoCTe.CteAutorizacao);

            var retorno = ws.Execute(dadosEnvio);
            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviCTe().CarregarDeXmlString(retornoXmlString);

            if (retEnvio.infRec.nRec != null)
                SalvarArquivoXml(retEnvio.infRec.nRec + "-rec.xml", retornoXmlString, ServicoCTe.CteRetAutorizacao);

            return new RetornoCteRecepcao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString, retEnvio);

            #endregion
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais CTe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoCTeRetAutorizacao com com os dados do processamento do lote</returns>
        public RetornoCteRetRecepcao CTeRetAutorizacao(string recibo)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoCTe.CteRetAutorizacao, _cFgServico.VersaoCteRetAutorizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoCTe.CteRetAutorizacao);

            ws.cteCabecMsg = new cteCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consReciCTe

            var pedRecibo = new consReciCTe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                nRec = recibo
            };

            #endregion

            #region Envia os dados e obtém a resposta

            var xmlRecibo = pedRecibo.ObterXmlString();
            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlRecibo);

            SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo, ServicoCTe.CteRetAutorizacao);

            var retorno = ws.Execute(dadosRecibo);
            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retconsReciCTe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString, ServicoCTe.CteRetAutorizacao);

            return new RetornoCteRetRecepcao(pedRecibo.ObterXmlString(), retRecibo.ObterXmlString(), retornoXmlString, retRecibo);

            #endregion
        }

        #endregion

        #region CteDownloadNF
        /// <summary>
        ///     Consulta a Situação da CTe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da CTe</returns>
        //public RetornoNfeDownload NfeDownloadNf(string cnpj, List<string> chaves)
        //{
        //    var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeDownloadNF, _cFgServico.VersaoNfeDownloadNF);

        //    #region Cria o objeto wdsl para envio do pedido de Download

        //    var ws = CriarServico(ServicoNFe.NfeDownloadNF);

        //    ws.nfeCabecMsg = new nfeCabecMsg
        //    {
        //        cUF = _cFgServico.cUF,
        //        //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
        //        versaoDados =  "1.00"
        //    };

        //    #endregion

        //    #region Cria o objeto downloadNFe

        //    var pedDownload = new downloadNFe
        //    {
        //        //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
        //        versao =  "1.00",
        //        CNPJ = cnpj,
        //        tpAmb = _cFgServico.tpAmb,
        //        chNFe = chaves
        //    };

        //    #endregion

        //    #region Valida, Envia os dados e obtém a resposta

        //    var xmlDownload = pedDownload.ObterXmlString();
        //    Validador.Valida(ServicoNFe.NfeDownloadNF, _cFgServico.VersaoNfeDownloadNF, xmlDownload, true, _cFgServico.DiretorioSchemas);
        //    var dadosDownload = new XmlDocument();
        //    dadosDownload.LoadXml(xmlDownload);

        //    SalvarArquivoXml(chaves.FirstOrDefault() + "-ped-down.xml", xmlDownload, ServicoNFe.NfeDownloadNF);

        //    var retorno = ws.Execute(dadosDownload);
        //    var retornoXmlString = retorno.OuterXml;
        //    var retDownload = new retDownloadNFe().CarregarDeXmlString(retornoXmlString);

        //    SalvarArquivoXml(chaves.FirstOrDefault() + "-down.xml", retornoXmlString, ServicoNFe.NfeDownloadNF);

        //    return new RetornoNfeDownload(pedDownload.ObterXmlString(), retDownload.ObterXmlString(), retornoXmlString, retDownload);

        //    #endregion
        //}

        #region Implementação do padrão Dispose

        // Flag: Dispose já foi chamado?
        //private bool _disposed;

        //// Implementação protegida do padrão Dispose.
        //private void Dispose(bool disposing)
        //{
        //    if (_disposed)
        //        return;

        //    if (disposing)
        //    {
        //        if (_certificado.PrivateKey != null)
        //            _certificado.PrivateKey.Clear();
        //        if (_certificado.PrivateKey != null)
        //            _certificado.PrivateKey.Dispose();
        //        _certificado.Reset();
        //    }

        //    _disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //~ServicosCTe()
        //{
        //    Dispose(false);
        //}

        #endregion
        */

    }
}