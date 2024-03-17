/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.AdmCsc;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Classes.Servicos.Consulta;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Classes.Servicos.DistribuicaoDFe;
using NFe.Classes.Servicos.Download;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Inutilizacao;
using NFe.Classes.Servicos.Recepcao;
using NFe.Classes.Servicos.Recepcao.Retorno;
using NFe.Classes.Servicos.Status;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.AdmCsc;
using NFe.Utils.Autorizacao;
using NFe.Utils.Consulta;
using NFe.Utils.ConsultaCadastro;
using NFe.Utils.DistribuicaoDFe;
using NFe.Utils.Download;
using NFe.Utils.Evento;
using NFe.Utils.Excecoes;
using NFe.Utils.Inutilizacao;
using NFe.Utils.NFe;
using NFe.Utils.Recepcao;
using NFe.Utils.Status;
using NFe.Utils.Validacao;
using NFe.Wsdl;
using NFe.Wsdl.AdmCsc;
using NFe.Wsdl.Autorizacao;
using NFe.Wsdl.ConsultaCadastro.DEMAIS_UFs;
using NFe.Wsdl.ConsultaProtocolo;
using NFe.Wsdl.DistribuicaoDFe;
using NFe.Wsdl.Download;
using NFe.Wsdl.Evento;
using NFe.Wsdl.Evento.SVAN;
using NFe.Wsdl.Inutilizacao;
using NFe.Wsdl.Recepcao;
using NFe.Wsdl.Status;
using NFe.Wsdl.Status.SVAN;
using CadConsultaCadastro2 = NFe.Wsdl.ConsultaCadastro.CE.CadConsultaCadastro2;

namespace NFe.Servicos
{
    public sealed class ServicosNFe : IDisposable
    {
        private const string DefaultHeaderUtf8 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        private readonly X509Certificate2 _certificado;
        private readonly ConfiguracaoServico _cFgServico;
        private readonly string _path;

        /// <summary>
        ///     Cria uma instância da Classe responsável pelos serviços relacionados à NFe
        /// </summary>
        /// <param name="cFgServico">Configurações</param>
        public ServicosNFe(ConfiguracaoServico cFgServico) : this(cFgServico,
            CertificadoDigital.ObterCertificado(cFgServico.Certificado)) { }

        /// <summary>
        ///     Cria uma instância da Classe responsável pelos serviços relacionados à NFe
        /// </summary>
        /// <param name="cFgServico">Configurações</param>
        /// <param name="certificado">Certificado digital</param>
        public ServicosNFe(ConfiguracaoServico cFgServico, X509Certificate2 certificado)
        {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _cFgServico = cFgServico;
            _certificado = certificado;

            // Define a versão do protocolo de segurança
            ServicePointManager.SecurityProtocol = cFgServico.ProtocoloDeSeguranca;
        }

        private void SalvarArquivoXml(string nomeArquivo, string xmlString)
        {
            if (!_cFgServico.SalvarXmlServicos) return;
            var dir = string.IsNullOrEmpty(_cFgServico.DiretorioSalvarXml) ? _path : _cFgServico.DiretorioSalvarXml;
            using (var stw = new StreamWriter(dir + @"\" + nomeArquivo)) stw.WriteLine(xmlString);
        }

        private void SalvarArquivoXml(string nomeArquivo, Stream xmlStream)
        {
            if (!_cFgServico.SalvarXmlServicos) return;
            var dir = string.IsNullOrEmpty(_cFgServico.DiretorioSalvarXml) ? _path : _cFgServico.DiretorioSalvarXml;
            xmlStream.Position = 0;
            using (var stw = new FileStream(dir + @"\" + nomeArquivo, FileMode.Create)) xmlStream.CopyTo(stw);
        }

        private INfeServicoAutorizacao CriarServicoAutorizacao(ServicoNFe servico)
        {
            var url = Enderecador.ObterUrlServico(servico, _cFgServico);

            if (servico != ServicoNFe.NFeAutorizacao)
            {
                var method = MethodBase.GetCurrentMethod();
                var methodName = method != null ? method.Name : string.Empty;

                throw new Exception(string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                    methodName));
            }

            //if (IsSvanNFe4())
            //{
            //return new NFeAutorizacao4SVAN(url, _certificado, _cFgServico.TimeOut);
            //}

            if (_cFgServico.VersaoNFeAutorizacao == VersaoServico.ve400)
                return new NFeAutorizacao4(url, _certificado, _cFgServico.TimeOut);

            if ((_cFgServico.cUF == Estado.PR) & (_cFgServico.VersaoNFeAutorizacao == VersaoServico.ve310))
                return new NfeAutorizacao3(url, _certificado, _cFgServico.TimeOut);

            return new NfeAutorizacao(url, _certificado, _cFgServico.TimeOut);
        }

        private INfeServico CriarServico(ServicoNFe servico, string proxyAddress = null)
        {
            var url = Enderecador.ObterUrlServico(servico, _cFgServico);

            switch (servico)
            {
                case ServicoNFe.NfeStatusServico:
                    if ((_cFgServico.cUF == Estado.PR) & (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve310))
                        return new NfeStatusServico3(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.BA) & (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve310) &
                        (_cFgServico.ModeloDocumento == ModeloDocumento.NFe))
                        return new NfeStatusServico(url, _certificado, _cFgServico.TimeOut);

                    if (IsSvanNFe4()) return new NfeStatusServico4NFeSVAN(url, _certificado, _cFgServico.TimeOut);

                    if (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve400)
                        return new NfeStatusServico4(url, _certificado, _cFgServico.TimeOut);

                    return new NfeStatusServico2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeConsultaProtocolo:

                    //if (IsSvanNFe4())
                    //{
                    //    return new NfeConsulta4SVAN(url, _certificado, _cFgServico.TimeOut);
                    //}

                    if (_cFgServico.VersaoNfeConsultaProtocolo == VersaoServico.ve400)
                        return new NfeConsulta4(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.PR) &
                        (_cFgServico.VersaoNfeConsultaProtocolo == VersaoServico.ve310))
                        return new NfeConsulta3(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.BA) &
                        (_cFgServico.VersaoNfeConsultaProtocolo == VersaoServico.ve310) &
                        (_cFgServico.ModeloDocumento == ModeloDocumento.NFe))
                        return new NfeConsulta(url, _certificado, _cFgServico.TimeOut);

                    return new NfeConsulta2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeRecepcao: return new NfeRecepcao2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeRetRecepcao: return new NfeRetRecepcao2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NFeAutorizacao:
                {
                    var method = MethodBase.GetCurrentMethod();
                    var methodName = method != null ? method.Name : string.Empty;

                    throw new Exception(string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                        methodName));
                }

                case ServicoNFe.NFeRetAutorizacao:
                    //if (IsSvanNFe4())
                    //{
                    //    return new NfeRetAutorizacao4SVAN(url, _certificado, _cFgServico.TimeOut);
                    //}

                    if (_cFgServico.VersaoNFeRetAutorizacao == VersaoServico.ve400)
                        return new NfeRetAutorizacao4(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.PR) & (_cFgServico.VersaoNFeAutorizacao == VersaoServico.ve310))
                        return new NfeRetAutorizacao3(url, _certificado, _cFgServico.TimeOut);

                    return new NfeRetAutorizacao(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeInutilizacao:

                    //if (IsSvanNFe4())
                    //{
                    //    return new NFeInutilizacao4SVAN(url, _certificado, _cFgServico.TimeOut);
                    //}

                    if (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve400)
                        return new NFeInutilizacao4(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.PR) & (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve310))
                        return new NfeInutilizacao3(url, _certificado, _cFgServico.TimeOut);

                    if ((_cFgServico.cUF == Estado.BA) & (_cFgServico.VersaoNfeStatusServico == VersaoServico.ve310) &
                        (_cFgServico.ModeloDocumento == ModeloDocumento.NFe))
                        return new NfeInutilizacao(url, _certificado, _cFgServico.TimeOut);

                    return new NfeInutilizacao2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.RecepcaoEventoEpec:
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                case ServicoNFe.RecepcaoEventoCancelmento:
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    if (IsSvanNFe4()) return new RecepcaoEvento4SVAN(url, _certificado, _cFgServico.TimeOut);

                    if (_cFgServico.VersaoRecepcaoEventoCceCancelamento == VersaoServico.ve400)
                        return new RecepcaoEvento4(url, _certificado, _cFgServico.TimeOut);

                    return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeConsultaCadastro:
                    return new CadConsultaCadastro4(url, _certificado, _cFgServico.TimeOut, proxyAddress);

                case ServicoNFe.NfeDownloadNF: return new NfeDownloadNF(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfceAdministracaoCSC: return new NfceCsc(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NFeDistribuicaoDFe:
                    return new NfeDistDFeInteresse(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeConsultaDest:
                default:
                    return null;
            }
        }

        private bool IsSvanNFe4()
        {
            return (_cFgServico.cUF == Estado.PA || _cFgServico.cUF == Estado.MA) &&
                   _cFgServico.VersaoNfeStatusServico == VersaoServico.ve400 &&
                   _cFgServico.ModeloDocumento == ModeloDocumento.NFe;
        }

        /// <summary>
        ///     Consulta o status do Serviço de NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeStatusServico com os dados status do serviço</returns>
        public async Task<RetornoNfeStatusServico> NfeStatusServicoAsync()
        {
            var versaoServico = ServicoNFe.NfeStatusServico.VersaoServicoParaString(_cFgServico.VersaoNfeStatusServico);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeStatusServico);

            if (_cFgServico.VersaoNfeStatusServico != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto consStatServ

            var pedStatus = new consStatServ
            {
                cUF = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                versao = versaoServico
            };

            // Valida, Envia os dados e obtém a resposta

            var xmlStatus = pedStatus.ObterXmlString();

            Validador.Valida(ServicoNFe.NfeStatusServico, _cFgServico.VersaoNfeStatusServico, xmlStatus,
                cfgServico: _cFgServico);

            var dadosStatus = new XmlDocument();
            dadosStatus.LoadXml(xmlStatus);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-ped-sta.xml", xmlStatus);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosStatus);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeStatusServico, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsStatServ = new retConsStatServ().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-sta.xml", retornoXmlString);

            return new RetornoNfeStatusServico(pedStatus.ObterXmlString(), retConsStatServ.ObterXmlString(),
                retornoXmlString, retConsStatServ);
        }

        /// <summary>
        ///     Consulta a Situação da NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da NFe</returns>
        public async Task<RetornoNfeConsultaProtocolo> NfeConsultaProtocoloAsync(string chave)
        {
            var versaoServico =
                ServicoNFe.NfeConsultaProtocolo.VersaoServicoParaString(_cFgServico.VersaoNfeConsultaProtocolo);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeConsultaProtocolo);

            if (_cFgServico.VersaoNfeConsultaProtocolo != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto consSitNFe

            var pedConsulta = new consSitNFe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chave
            };

            // Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedConsulta.ObterXmlString();

            Validador.Valida(ServicoNFe.NfeConsultaProtocolo, _cFgServico.VersaoNfeConsultaProtocolo, xmlConsulta,
                cfgServico: _cFgServico);

            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            //SalvarArquivoXml(chave + "-ped-sit.xml", xmlConsulta);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosConsulta);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeConsultaProtocolo, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retConsSitNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(chave + "-sit.xml", retornoXmlString);

            return new RetornoNfeConsultaProtocolo(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(),
                retornoXmlString, retConsulta);
        }

        /// <summary>
        ///     Inutiliza uma faixa de números já assinado
        /// </summary>
        /// <param name="xmlInutilizacao">xml de inutilização</param>
        /// <returns>Retorna um objeto da classe RetornoNfeInutilizacao com o retorno do serviço NfeInutilizacao</returns>
        public async Task<RetornoNfeInutilizacao> InutilizacaoAsync(string xmlInutilizacao)
        {
            var versaoServico = ServicoNFe.NfeInutilizacao.VersaoServicoParaString(_cFgServico.VersaoNfeInutilizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeInutilizacao);

            if (_cFgServico.VersaoNfeStatusServico != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Valida, Envia os dados e obtém a resposta

            Validador.Valida(ServicoNFe.NfeInutilizacao, _cFgServico.VersaoNfeInutilizacao, xmlInutilizacao,
                cfgServico: _cFgServico);

            var dadosInutilizacao = new XmlDocument();
            dadosInutilizacao.LoadXml(xmlInutilizacao);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosInutilizacao);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeInutilizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retInutNFe = new retInutNFe().CarregarDeXmlString(retornoXmlString);

            return new RetornoNfeInutilizacao(xmlInutilizacao, retInutNFe.ObterXmlString(), retornoXmlString,
                retInutNFe);
        }

        /// <summary>
        ///     Inutiliza uma faixa de números e assina
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ano"></param>
        /// <param name="modelo"></param>
        /// <param name="serie"></param>
        /// <param name="numeroInicial"></param>
        /// <param name="numeroFinal"></param>
        /// <param name="justificativa"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeInutilizacao com o retorno do serviço NfeInutilizacao</returns>
        public async Task<RetornoNfeInutilizacao> NfeInutilizacaoAsync(string cnpj, int ano, ModeloDocumento modelo,
            int serie, int numeroInicial, int numeroFinal, string justificativa)
        {
            var versaoServico = ServicoNFe.NfeInutilizacao.VersaoServicoParaString(_cFgServico.VersaoNfeInutilizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeInutilizacao);

            if (_cFgServico.VersaoNfeStatusServico != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto inutNFe

            var pedInutilizacao = new inutNFe
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

            var numId = string.Concat((int)pedInutilizacao.infInut.cUF, pedInutilizacao.infInut.ano,
                pedInutilizacao.infInut.CNPJ, (int)pedInutilizacao.infInut.mod,
                pedInutilizacao.infInut.serie.ToString().PadLeft(3, '0'),
                pedInutilizacao.infInut.nNFIni.ToString().PadLeft(9, '0'),
                pedInutilizacao.infInut.nNFFin.ToString().PadLeft(9, '0'));

            pedInutilizacao.infInut.Id = "ID" + numId;

            pedInutilizacao.Assina(_certificado, _cFgServico.Certificado.SignatureMethodSignedXml,
                _cFgServico.Certificado.DigestMethodReference);

            // Valida, Envia os dados e obtém a resposta

            var xmlInutilizacao = pedInutilizacao.ObterXmlString();

            Validador.Valida(ServicoNFe.NfeInutilizacao, _cFgServico.VersaoNfeInutilizacao, xmlInutilizacao,
                cfgServico: _cFgServico);

            var dadosInutilizacao = new XmlDocument();
            dadosInutilizacao.LoadXml(xmlInutilizacao);

            //SalvarArquivoXml(numId + "-ped-inu.xml", xmlInutilizacao);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosInutilizacao);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeInutilizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retInutNFe = new retInutNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(numId + "-inu.xml", retornoXmlString);

            return new RetornoNfeInutilizacao(pedInutilizacao.ObterXmlString(), retInutNFe.ObterXmlString(),
                retornoXmlString, retInutNFe);
        }

        /// <summary>
        ///     Envia um evento genérico
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="eventos"></param>
        /// <param name="servicoEvento">
        ///     Tipo de serviço do evento: valores válidos: RecepcaoEventoCancelmento,
        ///     RecepcaoEventoCartaCorrecao, RecepcaoEventoEpec e RecepcaoEventoManifestacaoDestinatario
        /// </param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public async Task<RetornoRecepcaoEvento> RecepcaoEventoAsync(long idlote, List<evento> eventos,
            ServicoNFe servicoEvento)
        {
            var listaEventos = new List<ServicoNFe>
            {
                ServicoNFe.RecepcaoEventoCartaCorrecao,
                ServicoNFe.RecepcaoEventoCancelmento,
                ServicoNFe.RecepcaoEventoEpec,
                ServicoNFe.RecepcaoEventoManifestacaoDestinatario
            };

            if (!listaEventos.Contains(servicoEvento))
            {
                var method = MethodBase.GetCurrentMethod();
                var methodName = method != null ? method.Name : string.Empty;

                throw new Exception(string.Format(
                    "Serviço {0} é inválido para o método {1}!\nServiços válidos: \n • {2}", servicoEvento, methodName,
                    string.Join("\n • ", listaEventos.ToArray())));
            }

            VersaoServico versaoEvento;

            switch (servicoEvento)
            {
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                    versaoEvento = _cFgServico.VersaoRecepcaoEventoCceCancelamento;
                    break;
                case ServicoNFe.RecepcaoEventoCancelmento:
                    versaoEvento = _cFgServico.VersaoRecepcaoEventoCceCancelamento;
                    break;
                case ServicoNFe.RecepcaoEventoEpec:
                    versaoEvento = _cFgServico.VersaoRecepcaoEventoEpec;
                    break;
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    versaoEvento = _cFgServico.VersaoRecepcaoEventoManifestacaoDestinatario;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("servicoEvento", servicoEvento, null);
            }

            var versaoServico =
                servicoEvento.VersaoServicoParaString(versaoEvento, _cFgServico.cUF);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(servicoEvento);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto envEvento

            var pedEvento = new envEvento
            {
                versao = versaoServico,
                idLote = idlote,
                evento = eventos
            };

            // Valida, Envia os dados e obtém a resposta

            var xmlEvento = pedEvento.ObterXmlString();

            Validador.Valida(servicoEvento, versaoEvento, xmlEvento, cfgServico: _cFgServico);

            var dadosEvento = new XmlDocument();
            dadosEvento.LoadXml(xmlEvento);

            //SalvarArquivoXml(idlote + "-ped-eve.xml", xmlEvento);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosEvento);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(servicoEvento, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvEvento = new retEnvEvento().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(idlote + "-eve.xml", retornoXmlString);

            // Obtém um procEventoNFe de cada evento e salva em arquivo

            var listprocEventoNFe = (from eve in eventos
                let retEvento =
                    (from retevento in retEnvEvento.retEvento
                        where retevento.infEvento.chNFe == eve.infEvento.chNFe &&
                              retevento.infEvento.tpEvento == eve.infEvento.tpEvento
                        select retevento).SingleOrDefault()
                select new procEventoNFe
                {
                    evento = eve,
                    versao = eve.versao,
                    retEvento = retEvento
                }).ToList();

            return new RetornoRecepcaoEvento(pedEvento.ObterXmlString(), retEnvEvento.ObterXmlString(),
                retornoXmlString, retEnvEvento, listprocEventoNFe);
        }

        /// <summary>
        ///     Envia um evento genérico
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="eventos"></param>
        /// <param name="servicoEvento">
        ///     Tipo de serviço do evento: valores válidos: RecepcaoEventoCancelmento,
        ///     RecepcaoEventoCartaCorrecao, RecepcaoEventoEpec e RecepcaoEventoManifestacaoDestinatario
        /// </param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        private async Task<RetornoRecepcaoEvento> RecepcaoEventoEAssinaAsync(long idlote, List<evento> eventos,
            ServicoNFe servicoEvento)
        {
            var listaEventos = new List<ServicoNFe>
            {
                ServicoNFe.RecepcaoEventoCartaCorrecao,
                ServicoNFe.RecepcaoEventoCancelmento,
                ServicoNFe.RecepcaoEventoEpec,
                ServicoNFe.RecepcaoEventoManifestacaoDestinatario
            };

            if (!listaEventos.Contains(servicoEvento))
            {
                var method = MethodBase.GetCurrentMethod();
                var methodName = method != null ? method.Name : string.Empty;

                throw new Exception(string.Format(
                    "Serviço {0} é inválido para o método {1}!\nServiços válidos: \n • {2}", servicoEvento, methodName,
                    string.Join("\n • ", listaEventos.ToArray())));
            }

            var versaoServico =
                servicoEvento.VersaoServicoParaString(_cFgServico.VersaoRecepcaoEventoCceCancelamento, _cFgServico.cUF);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(servicoEvento, versaoServico);

            if (_cFgServico.VersaoRecepcaoEventoCceCancelamento != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto envEvento

            var pedEvento = new envEvento
            {
                versao = versaoServico,
                idLote = idlote,
                evento = eventos
            };

            foreach (var evento in eventos)
            {
                evento.infEvento.Id = "ID" + evento.infEvento.tpEvento + evento.infEvento.chNFe +
                                      evento.infEvento.nSeqEvento.ToString().PadLeft(2, '0');

                evento.Assina(_certificado, _cFgServico.Certificado.SignatureMethodSignedXml,
                    _cFgServico.Certificado.DigestMethodReference);
            }

            // Valida, Envia os dados e obtém a resposta

            var xmlEvento = pedEvento.ObterXmlString();

            Validador.Valida(servicoEvento, _cFgServico.VersaoRecepcaoEventoCceCancelamento, xmlEvento,
                cfgServico: _cFgServico);

            var dadosEvento = new XmlDocument();
            dadosEvento.LoadXml(xmlEvento);

            //SalvarArquivoXml(idlote + "-ped-eve.xml", xmlEvento);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosEvento);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(servicoEvento, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvEvento = new retEnvEvento().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(idlote + "-eve.xml", retornoXmlString);

            // Obtém um procEventoNFe de cada evento e salva em arquivo

            var listprocEventoNFe = (from eve in eventos
                let retEvento =
                    (from retevento in retEnvEvento.retEvento
                        where retevento.infEvento.chNFe == eve.infEvento.chNFe &&
                              retevento.infEvento.tpEvento == eve.infEvento.tpEvento
                        select retevento).SingleOrDefault()
                select new procEventoNFe
                {
                    evento = eve,
                    versao = eve.versao,
                    retEvento = retEvento
                }).ToList();

            return new RetornoRecepcaoEvento(pedEvento.ObterXmlString(), retEnvEvento.ObterXmlString(),
                retornoXmlString, retEnvEvento, listprocEventoNFe);
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
        public Task<RetornoRecepcaoEvento> RecepcaoEventoCancelamentoAsync(long idlote, int sequenciaEvento,
            string protocoloAutorizacao, string chaveNFe, string justificativa, string cpfcnpj)
        {
            var versaoServico =
                ServicoNFe.RecepcaoEventoCancelmento.VersaoServicoParaString(
                    _cFgServico.VersaoRecepcaoEventoCceCancelamento, _cFgServico.cUF);

            var detEvento = new detEvento
            {
                nProt = protocoloAutorizacao,
                versao = versaoServico,
                xJust = justificativa
            };

            var infEvento = new infEventoEnv
            {
                cOrgao = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaveNFe,
                dhEvento = DateTime.Now,
                tpEvento = 110111,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detEvento
            };

            if (cpfcnpj.Length == 11)
                infEvento.CPF = cpfcnpj;
            else
                infEvento.CNPJ = cpfcnpj;

            var evento = new evento
            {
                versao = versaoServico,
                infEvento = infEvento
            };

            return RecepcaoEventoEAssinaAsync(idlote, new List<evento>
            {
                evento
            }, ServicoNFe.RecepcaoEventoCancelmento);
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
        public Task<RetornoRecepcaoEvento> RecepcaoEventoCartaCorrecaoAsync(long idlote, int sequenciaEvento,
            string chaveNFe, string correcao, string cpfcnpj)
        {
            var versaoServico =
                ServicoNFe.RecepcaoEventoCartaCorrecao.VersaoServicoParaString(
                    _cFgServico.VersaoRecepcaoEventoCceCancelamento, _cFgServico.cUF);

            var detEvento = new detEvento
            {
                versao = versaoServico,
                xCorrecao = correcao,
                xJust = null,
                descEvento = "Carta de Correção",
                xCondUso =
                    "A Carta de Correção é disciplinada pelo § 1º-A do art. 7º do Convênio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularização de erro ocorrido na emissão de documento fiscal, desde que o erro não esteja relacionado com: I - as variáveis que determinam o valor do imposto tais como: base de cálculo, alíquota, diferença de preço, quantidade, valor da operação ou da prestação; II - a correção de dados cadastrais que implique mudança do remetente ou do destinatário; III - a data de emissão ou de saída."
            };

            if (_cFgServico.cUF == Estado.MT)
            {
                detEvento.xCondUso =
                    "A Carta de Correcao e disciplinada pelo paragrafo 1o-A do art. 7o do Convenio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularizacao de erro ocorrido na emissao de documento fiscal, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da operacao ou da prestacao; II - a correcao de dados cadastrais que implique mudanca do remetente ou do destinatario; III - a data de emissao ou de saida.";

                detEvento.descEvento = "Carta de Correcao";
            }

            var infEvento = new infEventoEnv
            {
                cOrgao = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaveNFe,
                dhEvento = DateTime.Now,
                tpEvento = 110110,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detEvento
            };

            if (cpfcnpj.Length == 11)
                infEvento.CPF = cpfcnpj;
            else
                infEvento.CNPJ = cpfcnpj;

            var evento = new evento
            {
                versao = versaoServico,
                infEvento = infEvento
            };

            return RecepcaoEventoEAssinaAsync(idlote, new List<evento>
            {
                evento
            }, ServicoNFe.RecepcaoEventoCartaCorrecao);
        }

        public Task<RetornoRecepcaoEvento> RecepcaoEventoManifestacaoDestinatarioAsync(int idlote, int sequenciaEvento,
            string chaveNFe, TipoEventoManifestacaoDestinatario tipoEventoManifestacaoDestinatario, string cpfcnpj,
            string justificativa = null)
        {
            return RecepcaoEventoManifestacaoDestinatarioAsync(idlote, sequenciaEvento, new[] { chaveNFe },
                tipoEventoManifestacaoDestinatario, cpfcnpj, justificativa);
        }

        public Task<RetornoRecepcaoEvento> RecepcaoEventoManifestacaoDestinatarioAsync(int idlote, int sequenciaEvento,
            IEnumerable<string> chavesNFe, TipoEventoManifestacaoDestinatario tipoEventoManifestacaoDestinatario,
            string cpfcnpj, string justificativa = null)
        {
            var versaoServico =
                ServicoNFe.RecepcaoEventoManifestacaoDestinatario.VersaoServicoParaString(_cFgServico
                    .VersaoRecepcaoEventoCceCancelamento);

            var detEvento = new detEvento
            {
                versao = versaoServico,
                descEvento = tipoEventoManifestacaoDestinatario.Descricao(),
                xJust = justificativa
            };

            var eventos = new List<evento>();

            foreach (var chaveNFe in chavesNFe)
            {
                var infEvento = new infEventoEnv
                {
                    cOrgao = _cFgServico.cUF == Estado.RS ? _cFgServico.cUF : Estado.AN,
                    //RS possui endereço próprio para manifestação do destinatário. Demais UFs usam o ambiente nacional
                    tpAmb = _cFgServico.tpAmb,
                    chNFe = chaveNFe,
                    dhEvento = DateTime.Now,
                    tpEvento = (int)tipoEventoManifestacaoDestinatario,
                    nSeqEvento = sequenciaEvento,
                    verEvento = versaoServico,
                    detEvento = detEvento
                };

                if (cpfcnpj.Length == 11)
                    infEvento.CPF = cpfcnpj;
                else
                    infEvento.CNPJ = cpfcnpj;

                eventos.Add(new evento
                {
                    versao = versaoServico,
                    infEvento = infEvento
                });
            }

            return RecepcaoEventoEAssinaAsync(idlote, eventos, ServicoNFe.RecepcaoEventoManifestacaoDestinatario);
        }

        /// <summary>
        ///     Envia um evento do tipo "EPEC"
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="nfe"></param>
        /// <param name="veraplic"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        public Task<RetornoRecepcaoEvento> RecepcaoEventoEpecAsync(long idlote, int sequenciaEvento, Classes.NFe nfe,
            string veraplic)
        {
            var versaoServico =
                ServicoNFe.RecepcaoEventoEpec.VersaoServicoParaString(_cFgServico.VersaoRecepcaoEventoCceCancelamento);

            if (string.IsNullOrEmpty(nfe.infNFe.Id)) nfe.Assina().Valida();

            var detevento = new detEvento
            {
                versao = versaoServico,
                cOrgaoAutor = nfe.infNFe.ide.cUF,
                tpAutor = TipoAutor.taEmpresaEmitente,
                verAplic = veraplic,
                dhEmi = nfe.infNFe.ide.dhEmi,
                tpNF = nfe.infNFe.ide.tpNF,
                IE = nfe.infNFe.emit.IE,
                dest = new detEventoDest
                {
                    UF = nfe.infNFe.dest.enderDest.UF,
                    CNPJ = nfe.infNFe.dest.CNPJ,
                    CPF = nfe.infNFe.dest.CPF,
                    IE = nfe.infNFe.dest.IE,
                    vNF = nfe.infNFe.total.ICMSTot.vNF,
                    vICMS = nfe.infNFe.total.ICMSTot.vICMS,
                    vST = nfe.infNFe.total.ICMSTot.vST
                }
            };

            var infEvento = new infEventoEnv
            {
                cOrgao = Estado.AN,
                tpAmb = nfe.infNFe.ide.tpAmb,
                CNPJ = nfe.infNFe.emit.CNPJ,
                CPF = nfe.infNFe.emit.CPF,
                chNFe = nfe.infNFe.Id.Substring(3),
                dhEvento = DateTime.Now,
                tpEvento = 110140,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detevento
            };

            var evento = new evento
            {
                versao = versaoServico,
                infEvento = infEvento
            };

            return RecepcaoEventoEAssinaAsync(idlote, new List<evento>
            {
                evento
            }, ServicoNFe.RecepcaoEventoEpec);
        }

        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        ///     <para>O documento pode ser: IE, CNPJ ou CPF</para>
        /// </summary>
        /// <param name="uf">Sigla da UF consultada, informar 'SU' para SUFRAMA</param>
        /// <param name="tipoDocumento">Tipo de documento a ser consultado</param>
        /// <param name="documento">Documento a ser consultado</param>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaCadastro com o retorno do serviço NfeConsultaCadastro</returns>
        public async Task<RetornoNfeConsultaCadastro> NfeConsultaCadastroAsync(string uf,
            ConsultaCadastroTipoDocumento tipoDocumento, string documento, string proxyAddress = null)
        {
            var versaoServico =
                ServicoNFe.NfeConsultaCadastro.VersaoServicoParaString(_cFgServico.VersaoNfeConsultaCadastro,
                    _cFgServico.cUF);

            var ws = CriarServico(ServicoNFe.NfeConsultaCadastro, proxyAddress);

            if (_cFgServico.VersaoNfeConsultaCadastro != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto ConsCad

            var pedConsulta = new ConsCad
            {
                versao = versaoServico,
                infCons = new infConsEnv
                {
                    UF = uf
                }
            };

            switch (tipoDocumento)
            {
                case ConsultaCadastroTipoDocumento.Ie:
                    pedConsulta.infCons.IE = documento;
                    break;
                case ConsultaCadastroTipoDocumento.Cnpj:
                    pedConsulta.infCons.CNPJ = documento;
                    break;
                case ConsultaCadastroTipoDocumento.Cpf:
                    pedConsulta.infCons.CPF = documento;
                    break;
                default: throw new ArgumentOutOfRangeException("tipoDocumento", tipoDocumento, null);
            }

            // Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedConsulta.ObterXmlString();
            //Validador.Valida(ServicoNFe.NfeConsultaCadastro, _cFgServico.VersaoNfeConsultaCadastro, xmlConsulta, cfgServico: _cFgServico);
            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-ped-cad.xml", xmlConsulta);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosConsulta);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeConsultaCadastro, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retConsCad().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-cad.xml", retornoXmlString);

            return new RetornoNfeConsultaCadastro(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(),
                retornoXmlString, retConsulta);
        }

        /// <summary>
        ///     Serviço destinado à distribuição de informações resumidas e documentos fiscais eletrônicos de interesse de um ator,
        ///     seja este pessoa física ou jurídica.
        /// </summary>
        /// <param name="ufAutor">Código da UF do Autor</param>
        /// <param name="documento">CNPJ/CPF do interessado no DF-e</param>
        /// <param name="ultNsu">Último NSU recebido pelo Interessado</param>
        /// <param name="nSu">Número Sequencial Único</param>
        /// <param name="chNfe">Chave eletronica da NF-e</param>
        /// <returns>Retorna um objeto da classe RetornoNfeDistDFeInt com os documentos de interesse do CNPJ/CPF pesquisado</returns>
        public async Task<RetornoNfeDistDFeInt> NfeDistDFeInteresseAsync(string ufAutor, string documento,
            string ultNsu = "0", string nSu = "0", string chNfe = "")
        {
            var versaoServico =
                ServicoNFe.NFeDistribuicaoDFe.VersaoServicoParaString(_cFgServico.VersaoNFeDistribuicaoDFe);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NFeDistribuicaoDFe);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto distDFeInt

            var pedDistDFeInt = new distDFeInt
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                cUFAutor = _cFgServico.cUF
            };

            if (documento.Length == 11) pedDistDFeInt.CPF = documento;
            if (documento.Length > 11) pedDistDFeInt.CNPJ = documento;

            if (string.IsNullOrEmpty(chNfe))
                pedDistDFeInt.distNSU = new distNSU
                {
                    ultNSU = ultNsu.PadLeft(15, '0')
                };

            if (!nSu.Equals("0"))
            {
                pedDistDFeInt.consNSU = new consNSU
                {
                    NSU = nSu.PadLeft(15, '0')
                };

                pedDistDFeInt.distNSU = null;
                pedDistDFeInt.consChNFe = null;
            }

            if (!string.IsNullOrEmpty(chNfe))
            {
                pedDistDFeInt.consChNFe = new consChNFe
                {
                    chNFe = chNfe
                };

                pedDistDFeInt.consNSU = null;
                pedDistDFeInt.distNSU = null;
            }

            // Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedDistDFeInt.ObterXmlString();

            Validador.Valida(ServicoNFe.NFeDistribuicaoDFe, _cFgServico.VersaoNFeDistribuicaoDFe, xmlConsulta,
                cfgServico: _cFgServico);

            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-ped-DistDFeInt.xml", xmlConsulta);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosConsulta);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeDistribuicaoDFe, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retDistDFeInt().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-distDFeInt.xml", retornoXmlString);

            // Obtém um retDistDFeInt de cada evento e salva em arquivo

            //if (retConsulta.loteDistDFeInt != null)
            //    foreach (var t in retConsulta.loteDistDFeInt)
            //    {
            //        var conteudo = Compressao.Unzip(t.XmlNfe);
            //        var chNFe = string.Empty;

            //        if (conteudo.StartsWith("<resNFe"))
            //        {
            //            var retConteudo = FuncoesXml.XmlStringParaClasse<resNFe>(conteudo);
            //            chNFe = retConteudo.chNFe;
            //        }
            //        else if (conteudo.StartsWith("<procEventoNFe"))
            //        {
            //            var procEventoNFeConteudo =
            //                FuncoesXml.XmlStringParaClasse<Classes.Servicos.DistribuicaoDFe.Schemas.procEventoNFe>(
            //                    conteudo);

            //            chNFe = procEventoNFeConteudo.retEvento.infEvento.chNFe;
            //        }
            //        else if (conteudo.StartsWith("<resEvento"))
            //        {
            //            var resEventoConteudo = FuncoesXml.XmlStringParaClasse<resEvento>(conteudo);
            //            chNFe = resEventoConteudo.chNFe;
            //        }
            //        else if (conteudo.StartsWith("<nfeProc"))
            //        {
            //            chNFe = t.NSU.ToString();
            //        }

            //        var schema = t.schema.Split('_');
            //        if (chNFe == string.Empty) chNFe = DateTime.Now.Ticks + "_SEMCHAVE";

            //        SalvarArquivoXml(chNFe + "-" + schema[0] + ".xml", conteudo);
            //    }

            return new RetornoNfeDistDFeInt(pedDistDFeInt.ObterXmlString(), retConsulta.ObterXmlString(),
                retornoXmlString, retConsulta);
        }

        /// <summary>
        ///     Serviço destinado à distribuição de informações resumidas e documentos fiscais eletrônicos de interesse de um ator,
        ///     seja este pessoa física ou jurídica.
        /// </summary>
        /// <param name="pedDistDFeInt">Detalhes do pedido de notas</param>
        /// <returns>Retorna um objeto da classe RetornoNfeDistDFeInt com os documentos de interesse do CNPJ/CPF pesquisado</returns>
        public async Task<RetornoNfeDistDFeInt> NfeDistDFeInteresseAsync(distDFeInt pedDistDFeInt)
        {
            var versaoServico =
                ServicoNFe.NFeDistribuicaoDFe.VersaoServicoParaString(_cFgServico.VersaoNFeDistribuicaoDFe);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NFeDistribuicaoDFe);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            pedDistDFeInt.versao = versaoServico;

            // Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedDistDFeInt.ObterXmlString();

            Validador.Valida(ServicoNFe.NFeDistribuicaoDFe, _cFgServico.VersaoNFeDistribuicaoDFe, xmlConsulta,
                cfgServico: _cFgServico);

            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            //SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-ped-DistDFeInt.xml", xmlConsulta);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosConsulta);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeDistribuicaoDFe, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retDistDFeInt().CarregarDeXmlString(retornoXmlString);

            return new RetornoNfeDistDFeInt(pedDistDFeInt.ObterXmlString(), retConsulta.ObterXmlString(),
                retornoXmlString, retConsulta);
        }

        /// <summary>
        ///     Consulta a Situação da NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da NFe</returns>
        public async Task<RetornoNfeDownload> NfeDownloadNfAsync(string cnpj, List<string> chaves,
            string nomeSaida = "")
        {
            //var versaoServico = ServicoNFe.NfeDownloadNF.VersaoServicoParaString(_cFgServico.VersaoNfeDownloadNF);

            // Cria o objeto wdsl para envio do pedido de Download

            var ws = CriarServico(ServicoNFe.NfeDownloadNF);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
                versaoDados = /*versaoServico*/ "1.00"
            };

            // Cria o objeto downloadNFe

            var pedDownload = new downloadNFe
            {
                //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
                versao = /*versaoServico*/ "1.00",
                CNPJ = cnpj,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaves
            };

            // Valida, Envia os dados e obtém a resposta

            var xmlDownload = pedDownload.ObterXmlString();

            Validador.Valida(ServicoNFe.NfeDownloadNF, _cFgServico.VersaoNfeDownloadNF, xmlDownload,
                cfgServico: _cFgServico);

            var dadosDownload = new XmlDocument();
            dadosDownload.LoadXml(xmlDownload);

            //if (nomeSaida == "") nomeSaida = cnpj;

            //SalvarArquivoXml(nomeSaida + "-ped-down.xml", xmlDownload);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosDownload);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeDownloadNF, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retDownload = new retDownloadNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(nomeSaida + "-down.xml", retornoXmlString);

            return new RetornoNfeDownload(pedDownload.ObterXmlString(), retDownload.ObterXmlString(), retornoXmlString,
                retDownload);
        }

        #region Adm CSC

        public async Task<RetornoAdmCscNFCe> AdmCscNfCeAsync(string raizCnpj,
            IdentificadorOperacaoCsc identificadorOperacaoCsc, string idCscASerRevogado = null,
            string codigoCscASerRevogado = null)
        {
            var versaoServico =
                ServicoNFe.NfceAdministracaoCSC.VersaoServicoParaString(_cFgServico.VersaoNfceAministracaoCSC);

            // Cria o objeto wdsl para envio do pedido de Download

            var ws = CriarServico(ServicoNFe.NfceAdministracaoCSC);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto downloadNFe

            var admCscNfCe = new admCscNFCe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                indOp = identificadorOperacaoCsc,
                raizCNPJ = raizCnpj
            };

            if (identificadorOperacaoCsc == IdentificadorOperacaoCsc.ioRevogaCscAtivo)
                admCscNfCe.dadosCsc = new dadosCsc
                {
                    codigoCsc = codigoCscASerRevogado,
                    idCsc = idCscASerRevogado
                };

            // Valida, Envia os dados e obtém a resposta

            var xmlAdmCscNfe = admCscNfCe.ObterXmlString();
            var dadosAdmnistracaoCsc = new XmlDocument();
            dadosAdmnistracaoCsc.LoadXml(xmlAdmCscNfe);

            //SalvarArquivoXml(raizCnpj + "-adm-csc.xml", xmlAdmCscNfe);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosAdmnistracaoCsc);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfceAdministracaoCSC, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retCsc = new retAdmCscNFCe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(raizCnpj + "-ret-adm-csc.xml", retornoXmlString);

            return new RetornoAdmCscNFCe(admCscNfCe.ObterXmlString(), retCsc.ObterXmlString(), retornoXmlString,
                retCsc);
        }

        #endregion

        #region Recepção

        /// <summary>
        ///     Envia uma ou mais NFe
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="nFes"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeRecepcao com com os dados do resultado da transmissão</returns>
        public async Task<RetornoNfeRecepcao> NfeRecepcaoAsync(long idLote, List<Classes.NFe> nFes)
        {
            var versaoServico = ServicoNFe.NfeRecepcao.VersaoServicoParaString(_cFgServico.VersaoNfeRecepcao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeRecepcao);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto enviNFe

            var pedEnvio = new enviNFe2(versaoServico, idLote, nFes);

            // Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();

            if (_cFgServico.cUF == Estado.PR)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
                xmlEnvio = xmlEnvio.Replace("<NFe>", "<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

            Validador.Valida(ServicoNFe.NfeRecepcao, _cFgServico.VersaoNfeRecepcao, xmlEnvio, cfgServico: _cFgServico);
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            //SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosEnvio);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeRecepcao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString);

            return new RetornoNfeRecepcao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString,
                retEnvio);
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeRetRecepcao com com os dados do processamento do lote</returns>
        public async Task<RetornoNfeRetRecepcao> NfeRetRecepcaoAsync(string recibo)
        {
            var versaoServico = ServicoNFe.NfeRetRecepcao.VersaoServicoParaString(_cFgServico.VersaoNfeRetRecepcao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeRetRecepcao);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto consReciNFe

            var pedRecibo = new consReciNFe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                nRec = recibo
            };

            // Envia os dados e obtém a resposta

            var xmlRecibo = pedRecibo.ObterXmlString();
            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlRecibo);

            //SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosRecibo);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NfeRetRecepcao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retConsReciNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString);

            return new RetornoNfeRetRecepcao(pedRecibo.ObterXmlString(), retRecibo.ObterXmlString(), retornoXmlString,
                retRecibo);
        }

        #endregion

        #region Autorização

        private static readonly XmlWriterSettings _xmlSettings = new XmlWriterSettings
        {
            Indent = false,
            OmitXmlDeclaration = true,
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            Encoding = new UTF8Encoding(false)
        };

        private static readonly XmlSerializerNamespaces _ns =
            new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName(string.Empty, "http://www.portalfiscal.inf.br/nfe")
            });

        private static readonly XmlSerializer _enviNFe3Serializer =
            XmlSerializer.FromTypes(new[] { typeof(enviNFe3) })[0];

        /// <summary>
        ///     Envia uma ou mais NFe
        /// </summary>
        /// <param name="idLote">ID do Lote</param>
        /// <param name="indSinc">Indicador de Sincronização</param>
        /// <param name="nFes">Lista de NFes a serem enviadas</param>
        /// <param name="compactarMensagem">Define se a mensagem será enviada para a SEFAZ compactada</param>
        /// <returns>Retorna um objeto da classe RetornoNFeAutorizacao com com os dados do resultado da transmissão</returns>
        public Task<RetornoNFeAutorizacao> NFeAutorizacaoAsync(long idLote, IndicadorSincronizacao indSinc,
            List<Classes.NFe> nFes, bool compactarMensagem = true)
        {
            if (_cFgServico.VersaoNFeAutorizacao != VersaoServico.ve400)
                return NFeAutorizacaoVersao310Async(idLote, indSinc, nFes, compactarMensagem);

            if (_cFgServico.VersaoNFeAutorizacao == VersaoServico.ve400)
                return NFeAutorizacao4Async(idLote, indSinc, nFes, compactarMensagem);

            throw new InvalidOperationException("Versão inválida");
        }

        private async Task<RetornoNFeAutorizacao> NFeAutorizacao4Async(long idLote, IndicadorSincronizacao indSinc,
            List<Classes.NFe> nFes, bool compactarMensagem)
        {
            var versaoServico = ServicoNFe.NFeAutorizacao.VersaoServicoParaString(_cFgServico.VersaoNFeAutorizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServicoAutorizacao(ServicoNFe.NFeAutorizacao);

            // Cria o objeto enviNFe

            var pedEnvio = new enviNFe4(versaoServico, idLote, indSinc, nFes);

            // Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();

            if (_cFgServico.cUF == Estado.PR)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
                xmlEnvio = xmlEnvio.Replace("<NFe>", "<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

            //var envio = new MemoryStream(Encoding.UTF8.GetBytes(xmlEnvio));

            Validador.Valida(ServicoNFe.NFeAutorizacao, _cFgServico.VersaoNFeAutorizacao, xmlEnvio,
                cfgServico: _cFgServico);

            //SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            XmlNode retorno;

            try
            {
                if (compactarMensagem)
                {
                    var xmlCompactado = Convert.ToBase64String(Compressao.Zip(DefaultHeaderUtf8 + xmlEnvio));
                    retorno = await ws.ExecuteZipAsync(xmlCompactado);

                    //retorno = await ws.ExecuteAsyncZipAsync(Compressao.ZipWithToBase64Transform(envio));
                    //envio.Dispose();
                }
                else
                {
                    var dadosEnvio = new XmlDocument();
                    dadosEnvio.LoadXml(xmlEnvio);
                    retorno = await ws.ExecuteAsync(dadosEnvio.FirstChild);
                }
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeAutorizacao, ex);
            }

            //SalvarArquivoXml(idLote + "-rec.xml", retorno.OuterXml);

            return new RetornoNFeAutorizacao(xmlEnvio, retorno.OuterXml, retorno.OuterXml,
                FuncoesXml.XmlNodeParaClasse<retEnviNFe>(retorno));
        }

        private async Task<RetornoNFeAutorizacao> NFeAutorizacaoVersao310Async(long idLote,
            IndicadorSincronizacao indSinc, List<Classes.NFe> nFes, bool compactarMensagem)
        {
            var versaoServico = ServicoNFe.NFeAutorizacao.VersaoServicoParaString(_cFgServico.VersaoNFeAutorizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServicoAutorizacao(ServicoNFe.NFeAutorizacao);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto enviNFe

            var pedEnvio = new enviNFe3(versaoServico, idLote, indSinc, nFes);

            // Valida, Envia os dados e obtém a resposta

            var xmlEnvio = new MemoryStream();

            using (var xmlWriter = XmlWriter.Create(xmlEnvio, _xmlSettings))
                _enviNFe3Serializer.Serialize(xmlWriter, pedEnvio, _ns);

            //if (_cFgServico.cUF == Estado.PR)
            //    //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
            //    xmlEnvio = xmlEnvio.Replace("<NFe>", "<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

            Validador.Valida(ServicoNFe.NFeAutorizacao, _cFgServico.VersaoNFeAutorizacao, xmlEnvio,
                cfgServico: _cFgServico);

            //SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            XmlNode retorno;

            try
            {
                //if (compactarMensagem)
                //{
                retorno = await ws.ExecuteZipAsync(Compressao.ZipWithToBase64Transform(xmlEnvio));
                //}
                //else
                //{
                //    var xmlEnvioNode = new XmlDocument();
                //    xmlEnvioNode.LoadXml(pedEnvio);
                //    retorno = await ws.ExecuteAsync(xmlEnvioNode);
                //}
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeAutorizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString);

            string ret;

            using (var r = new StreamReader(xmlEnvio))
            {
                ret = await r.ReadToEndAsync();
                xmlEnvio.Dispose();
            }

            return new RetornoNFeAutorizacao(ret, retornoXmlString, retornoXmlString, retEnvio);
        }

        /// <summary>
        ///     Envia uma ou mais NFe
        /// </summary>
        /// <param name="idLote">ID do Lote</param>
        /// <param name="indSinc">Indicador de Sincronização</param>
        /// <param name="nFes">Lista de NFes a serem enviadas</param>
        /// <param name="compactarMensagem">Define se a mensagem será enviada para a SEFAZ compactada</param>
        /// <returns>Retorna um objeto da classe RetornoNFeAutorizacao com com os dados do resultado da transmissão</returns>
        public async Task<RetornoNFeAutorizacao> NFeAutorizacaoComXmlAsync(long idLote, IndicadorSincronizacao indSinc,
            string nFes, bool compactarMensagem = false)
        {
            var versaoServico = ServicoNFe.NFeAutorizacao.VersaoServicoParaString(_cFgServico.VersaoNFeAutorizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServicoAutorizacao(ServicoNFe.NFeAutorizacao);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            // Cria o objeto enviNFe

            var pedEnvio = new enviNFe3(versaoServico, idLote, indSinc, null);

            // Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();

            if (_cFgServico.cUF == Estado.PR)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <NFe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/33
                xmlEnvio = xmlEnvio.Replace("<NFe>", "<NFe xmlns=\"http://www.portalfiscal.inf.br/nfe\">");

            Validador.Valida(ServicoNFe.NFeAutorizacao, _cFgServico.VersaoNFeAutorizacao, xmlEnvio,
                cfgServico: _cFgServico);

            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            //SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            XmlNode retorno;

            try
            {
                if (compactarMensagem)
                {
                    var xmlCompactado = Convert.ToBase64String(Compressao.Zip(xmlEnvio));
                    retorno = await ws.ExecuteZipAsync(xmlCompactado);
                }
                else
                {
                    retorno = await ws.ExecuteAsync(dadosEnvio);
                }
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeAutorizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString);

            return new RetornoNFeAutorizacao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString,
                retEnvio);
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoNFeRetAutorizacao com com os dados do processamento do lote</returns>
        public async Task<RetornoNFeRetAutorizacao> NFeRetAutorizacaoAsync(string recibo)
        {
            var versaoServico =
                ServicoNFe.NFeRetAutorizacao.VersaoServicoParaString(_cFgServico.VersaoNFeRetAutorizacao);

            // Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NFeRetAutorizacao);

            if (_cFgServico.VersaoNFeRetAutorizacao != VersaoServico.ve400)
                ws.nfeCabecMsg = new nfeCabecMsg
                {
                    cUF = _cFgServico.cUF,
                    versaoDados = versaoServico
                };

            // Cria o objeto consReciNFe

            var pedRecibo = new consReciNFe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                nRec = recibo
            };

            // Envia os dados e obtém a resposta

            var xmlRecibo = pedRecibo.ObterXmlString();
            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlRecibo);

            //SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo);

            XmlNode retorno;

            try
            {
                retorno = await ws.ExecuteAsync(dadosRecibo);
            }
            catch (WebException ex)
            {
                throw FabricaComunicacaoException.ObterException(ServicoNFe.NFeRetAutorizacao, ex);
            }

            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retConsReciNFe().CarregarDeXmlString(retornoXmlString);

            //SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString);

            return new RetornoNFeRetAutorizacao(xmlRecibo, retornoXmlString, retornoXmlString, retRecibo);
        }

        #endregion

        #region Implementação do padrão Dispose

        // Flag: Dispose já foi chamado?
        private bool _disposed;

        // Implementação protegida do padrão Dispose.
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                if (!_cFgServico.Certificado.ManterDadosEmCache)
                    _certificado.Reset();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServicosNFe()
        {
            Dispose(false);
        }

        #endregion
    }
}