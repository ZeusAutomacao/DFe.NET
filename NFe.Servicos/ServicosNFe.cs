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
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Classes.Servicos.Consulta;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Classes.Servicos.Download;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Inutilizacao;
using NFe.Classes.Servicos.Recepcao;
using NFe.Classes.Servicos.Recepcao.Retorno;
using NFe.Classes.Servicos.Status;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.Assinatura;
using NFe.Utils.Autorizacao;
using NFe.Utils.Consulta;
using NFe.Utils.ConsultaCadastro;
using NFe.Utils.Download;
using NFe.Utils.Evento;
using NFe.Utils.Inutilizacao;
using NFe.Utils.NFe;
using NFe.Utils.Recepcao;
using NFe.Utils.Status;
using NFe.Utils.Validacao;
using NFe.Wsdl;
using NFe.Wsdl.Autorizacao;
using NFe.Wsdl.ConsultaProtocolo;
using NFe.Wsdl.Download;
using NFe.Wsdl.Evento;
using NFe.Wsdl.Inutilizacao;
using NFe.Wsdl.Recepcao;
using NFe.Wsdl.Status;

namespace NFe.Servicos
{
    public class ServicosNFe
    {
        private readonly X509Certificate _certificado;
        private readonly ConfiguracaoServico _cFgServico;
        private readonly string _path;

        /// <summary>
        ///     Cria uma instância da Classe responsável pelos serviços relacionados à NFe
        /// </summary>
        /// <param name="cFgServico"></param>
        public ServicosNFe(ConfiguracaoServico cFgServico)
        {
            _cFgServico = cFgServico;
            _certificado = CertificadoDigital.BuscaCertificado(_cFgServico.SerialCertificado);
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void SalvarArquivoXml(string nomeArquivo, string xmlString)
        {
            if (!_cFgServico.SalvarXmlServicos) return;
            var dir = String.IsNullOrEmpty(_cFgServico.DiretorioSalvarXml) ? _path : _cFgServico.DiretorioSalvarXml;
            var stw = new StreamWriter(dir + @"\" + nomeArquivo);
            stw.WriteLine(xmlString);
            stw.Close();
        }

        private INfeServico CriarServico(ServicoNFe servico, TipoRecepcaoEvento tipoRecepcaoEvento)
        {
            var url = DefinicaoWsdl.ObterUrl(servico, tipoRecepcaoEvento, _cFgServico);
            switch (servico)
            {
                case ServicoNFe.NfeStatusServico:
                    if (_cFgServico.cUF == Estado.PR & _cFgServico.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeStatusServico3(url, _certificado, _cFgServico.TimeOut);
                    }
                    if (_cFgServico.cUF == Estado.BA & _cFgServico.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeStatusServico(url, _certificado, _cFgServico.TimeOut);
                    }
                    return new NfeStatusServico2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeConsultaProtocolo:
                    if (_cFgServico.cUF == Estado.PR & _cFgServico.VersaoNfeConsultaProtocolo == VersaoServico.ve310)
                    {
                        return new NfeConsulta3(url, _certificado, _cFgServico.TimeOut);
                    }
                    if (_cFgServico.cUF == Estado.BA & _cFgServico.VersaoNfeConsultaProtocolo == VersaoServico.ve310)
                    {
                        return new NfeConsulta(url, _certificado, _cFgServico.TimeOut);
                    }
                    return new NfeConsulta2(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoNFe.NfeRecepcao:
                    return new NfeRecepcao2(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoNFe.NfeRetRecepcao:
                    return new NfeRetRecepcao2(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoNFe.NFeAutorizacao:
                    if (_cFgServico.cUF == Estado.PR & _cFgServico.VersaoNFeAutorizacao == VersaoServico.ve310)
                        return new NfeAutorizacao3(url, _certificado, _cFgServico.TimeOut);
                    return new NfeAutorizacao(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoNFe.NFeRetAutorizacao:
                    if (_cFgServico.cUF == Estado.PR & _cFgServico.VersaoNFeAutorizacao == VersaoServico.ve310)
                        return new NfeRetAutorizacao3(url, _certificado, _cFgServico.TimeOut);
                    return new NfeRetAutorizacao(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeInutilizacao:
                    if (_cFgServico.cUF == Estado.PR & _cFgServico.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeInutilizacao3(url, _certificado, _cFgServico.TimeOut);
                    }
                    if (_cFgServico.cUF == Estado.BA & _cFgServico.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeInutilizacao(url, _certificado, _cFgServico.TimeOut);
                    }
                    return new NfeInutilizacao2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.RecepcaoEvento:
                    if (_cFgServico.cUF == Estado.SP & _cFgServico.VersaoRecepcaoEvento == VersaoServico.ve310 &
                        _cFgServico.ModeloDocumento == ModeloDocumento.NFCe & tipoRecepcaoEvento == TipoRecepcaoEvento.Epec) 
                        return new RecepcaoEPEC(url, _certificado, _cFgServico.TimeOut);
                    return new RecepcaoEvento(url, _certificado, _cFgServico.TimeOut);
                
                case ServicoNFe.NfeConsultaCadastro:
                    switch (_cFgServico.cUF)
                    {
                        case Estado.CE:
                            return new Wsdl.ConsultaCadastro.CE.CadConsultaCadastro2(url, _certificado, _cFgServico.TimeOut);
                    }
                    return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro2(url, _certificado, _cFgServico.TimeOut);

                case ServicoNFe.NfeDownloadNF:
                    return new NfeDownloadNF(url, _certificado, _cFgServico.TimeOut);                    

            }

            return null;
        }

        /// <summary>
        ///     Consulta o status do Serviço de NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeStatusServico com os dados status do serviço</returns>
        public RetornoNfeStatusServico NfeStatusServico()
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeStatusServico, _cFgServico.VersaoNfeStatusServico);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeStatusServico, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consStatServ

            var pedStatus = new consStatServ
            {
                cUF = _cFgServico.cUF,
                tpAmb = _cFgServico.tpAmb,
                versao = versaoServico
            };

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlStatus = pedStatus.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeStatusServico, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeStatusServico, xmlStatus);
            var dadosStatus = new XmlDocument();
            dadosStatus.LoadXml(xmlStatus);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-ped-sta.xml", xmlStatus);

            var retorno = ws.Execute(dadosStatus);
            var retornoXmlString = retorno.OuterXml;
            var retConsStatServ = new retConsStatServ().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-sta.xml", retornoXmlString);

            return new RetornoNfeStatusServico(pedStatus.ObterXmlString(), retConsStatServ.ObterXmlString(), retornoXmlString, retConsStatServ);

            #endregion
        }

        /// <summary>
        ///     Consulta a Situação da NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da NFe</returns>
        public RetornoNfeConsultaProtocolo NfeConsultaProtocolo(string chave)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeConsultaProtocolo, _cFgServico.VersaoNfeConsultaProtocolo);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeConsultaProtocolo, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consSitNFe

            var pedConsulta = new consSitNFe
            {
                versao = versaoServico,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chave
            };

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedConsulta.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeConsultaProtocolo, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeConsultaProtocolo, xmlConsulta);
            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            SalvarArquivoXml(chave + "-ped-sit.xml", xmlConsulta);

            var retorno = ws.Execute(dadosConsulta);
            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retConsSitNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(chave + "-sit.xml", retornoXmlString);

            return new RetornoNfeConsultaProtocolo(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

            #endregion
        }

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
        /// <returns>Retorna um objeto da classe RetornoNfeInutilizacao com o retorno do serviço NfeInutilizacao</returns>
        public RetornoNfeInutilizacao NfeInutilizacao(string cnpj, int ano, ModeloDocumento modelo, int serie, int numeroInicial, int numeroFinal, string justificativa)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeInutilizacao, _cFgServico.VersaoNfeInutilizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeInutilizacao, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto inutNFe

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

            var numId = String.Concat((int) pedInutilizacao.infInut.cUF, pedInutilizacao.infInut.ano,
                pedInutilizacao.infInut.CNPJ, (int) pedInutilizacao.infInut.mod, pedInutilizacao.infInut.serie.ToString().PadLeft(3, '0'),
                pedInutilizacao.infInut.nNFIni.ToString().PadLeft(9, '0'), pedInutilizacao.infInut.nNFFin.ToString().PadLeft(9, '0'));
            pedInutilizacao.infInut.Id = "ID" + numId;

            pedInutilizacao.Assina();

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlInutilizacao = pedInutilizacao.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeInutilizacao, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeInutilizacao, xmlInutilizacao);
            var dadosInutilizacao = new XmlDocument();
            dadosInutilizacao.LoadXml(xmlInutilizacao);

            SalvarArquivoXml(numId + "-ped-inu.xml", xmlInutilizacao);

            var retorno = ws.Execute(dadosInutilizacao);
            var retornoXmlString = retorno.OuterXml;
            var retInutNFe = new retInutNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(numId + "-inu.xml", retornoXmlString);

            return new RetornoNfeInutilizacao(pedInutilizacao.ObterXmlString(), retInutNFe.ObterXmlString(), retornoXmlString, retInutNFe);

            #endregion
        }

        /// <summary>
        ///     Envia um evento genérico
        /// </summary>
        /// <param name="idlote"></param>
        /// <param name="eventos"></param>
        /// <param name="tipoRecepcaoEvento"></param>
        /// <returns>Retorna um objeto da classe RetornoRecepcaoEvento com o retorno do serviço RecepcaoEvento</returns>
        private RetornoRecepcaoEvento RecepcaoEvento(int idlote, List<evento> eventos, TipoRecepcaoEvento tipoRecepcaoEvento)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.RecepcaoEvento, _cFgServico.VersaoRecepcaoEvento);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.RecepcaoEvento, tipoRecepcaoEvento);

            ws.nfeCabecMsg = new nfeCabecMsg
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
            Validador.Valida(ServicoNFe.RecepcaoEvento, tipoRecepcaoEvento, _cFgServico.VersaoRecepcaoEvento, xmlEvento);
            var dadosEvento = new XmlDocument();
            dadosEvento.LoadXml(xmlEvento);

            SalvarArquivoXml(idlote + "-ped-eve.xml", xmlEvento);

            var retorno = ws.Execute(dadosEvento);
            var retornoXmlString = retorno.OuterXml;
            var retEnvEvento = new retEnvEvento().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(idlote + "-eve.xml", retornoXmlString);

            #region Obtém um procEventoNFe de cada evento e salva em arquivo

            var listprocEventoNFe = new List<procEventoNFe>();

            foreach (var evento in eventos)
            {
                var eve = evento;
                var query = (from retevento in retEnvEvento.retEvento
                    where retevento.infEvento.chNFe == eve.infEvento.chNFe && retevento.infEvento.tpEvento == eve.infEvento.tpEvento
                    select retevento).SingleOrDefault();

                var procevento = new procEventoNFe {evento = eve, versao = eve.versao, retEvento = new List<retEvento> {query}};
                listprocEventoNFe.Add(procevento);
                if (!_cFgServico.SalvarXmlServicos) continue;
                var proceventoXmlString = procevento.ObterXmlString();
                SalvarArquivoXml(procevento.evento.infEvento.Id.Substring(2) + "-procEventoNFe.xml", proceventoXmlString);
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
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.RecepcaoEvento, _cFgServico.VersaoRecepcaoEvento);
            var detEvento = new detEvento {nProt = protocoloAutorizacao, versao = versaoServico, xJust = justificativa};
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

            var evento = new evento {versao = versaoServico, infEvento = infEvento};

            var retorno = RecepcaoEvento(idlote, new List<evento> {evento}, TipoRecepcaoEvento.Cancelmento);
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
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.RecepcaoEvento, _cFgServico.VersaoRecepcaoEvento);
            var detEvento = new detEvento {versao = versaoServico, xCorrecao = correcao};
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

            var evento = new evento {versao = versaoServico, infEvento = infEvento};

            var retorno = RecepcaoEvento(idlote, new List<evento> {evento}, TipoRecepcaoEvento.CartaCorrecao);
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
        public RetornoRecepcaoEvento RecepcaoEventoEpec(int idlote, int sequenciaEvento, Classes.NFe nfe, string veraplic)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.RecepcaoEvento, _cFgServico.VersaoRecepcaoEvento);

            if (String.IsNullOrEmpty(nfe.infNFe.Id))
                nfe.Assina().Valida();

            var detevento = new detEvento
            {
                versao = versaoServico,
                cOrgaoAutor = nfe.infNFe.ide.cUF,
                tpAutor = TipoAutor.taEmpresaEmitente,
                verAplic = veraplic,
                dhEmi = !String.IsNullOrEmpty(nfe.infNFe.ide.dhEmi) ? nfe.infNFe.ide.dhEmi : Convert.ToDateTime(nfe.infNFe.ide.dEmi).ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpNF = nfe.infNFe.ide.tpNF,
                IE = nfe.infNFe.emit.IE,
                dest = new dest
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
                dhEvento = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                tpEvento = 110140,
                nSeqEvento = sequenciaEvento,
                verEvento = versaoServico,
                detEvento = detevento
            };

            var evento = new evento {versao = versaoServico, infEvento = infEvento};

            var retorno = RecepcaoEvento(idlote, new List<evento> {evento}, TipoRecepcaoEvento.Epec);
            return retorno;
        }

        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        ///     <para>O documento pode ser: CPF ou CNPJ. O serviço avaliará o tamanho da string passada e determinará se a coonsulta será por CPF ou por CNPJ</para>
        /// </summary>
        /// <param name="uf"></param>
        /// <param name="documento"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaCadastro com o retorno do serviço NfeConsultaCadastro</returns>
        public RetornoNfeConsultaCadastro NfeConsultaCadastro(string uf, string documento)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeConsultaCadastro, _cFgServico.VersaoNfeConsultaCadastro);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeConsultaCadastro, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto ConsCad

            var pedConsulta = new ConsCad
            {
                versao = versaoServico,
                infCons = new infConsEnv {UF = uf}
            };

            if (documento.Length == 11)
                pedConsulta.infCons.CPF = documento;
            if (documento.Length > 11)
                pedConsulta.infCons.CNPJ = documento;

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlConsulta = pedConsulta.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeConsultaCadastro, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeConsultaCadastro, xmlConsulta);
            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-ped-cad.xml", xmlConsulta);

            var retorno = ws.Execute(dadosConsulta);
            var retornoXmlString = retorno.OuterXml;
            var retConsulta = new retConsCad().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(DateTime.Now.ToString("yyyyMMddHHmmss") + "-cad.xml", retornoXmlString);

            return new RetornoNfeConsultaCadastro(pedConsulta.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

            #endregion
        }

        #region Recepção

        /// <summary>
        ///     Envia uma ou mais NFe
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="nFes"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeRecepcao com com os dados do resultado da transmissão</returns>
        public RetornoNfeRecepcao NfeRecepcao(int idLote, List<Classes.NFe> nFes)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeRecepcao, _cFgServico.VersaoNfeRecepcao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeRecepcao, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto enviNFe

            var pedEnvio = new enviNFe2(versaoServico, idLote, nFes);

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeRecepcao, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeRecepcao, xmlEnvio);
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            var retorno = ws.Execute(dadosEnvio);
            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString);

            return new RetornoNfeRecepcao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString, retEnvio);

            #endregion
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoNfeRetRecepcao com com os dados do processamento do lote</returns>
        public RetornoNfeRetRecepcao NfeRetRecepcao(string recibo)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeRetRecepcao, _cFgServico.VersaoNfeRetRecepcao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NfeRetRecepcao, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consReciNFe

            var pedRecibo = new consReciNFe
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

            SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo);

            var retorno = ws.Execute(dadosRecibo);
            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retConsReciNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString);

            return new RetornoNfeRetRecepcao(pedRecibo.ObterXmlString(), retRecibo.ObterXmlString(), retornoXmlString, retRecibo);

            #endregion
        }

        #endregion

        #region Autorização

        /// <summary>
        ///     Envia uma ou mais NFe
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="indSinc"></param>
        /// <param name="nFes"></param>
        /// <returns>Retorna um objeto da classe RetornoNFeAutorizacao com com os dados do resultado da transmissão</returns>
        public RetornoNFeAutorizacao NFeAutorizacao(int idLote, IndicadorSincronizacao indSinc, List<Classes.NFe> nFes)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NFeAutorizacao,
                _cFgServico.VersaoNFeAutorizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NFeAutorizacao, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto enviNFe

            var pedEnvio = new enviNFe3(versaoServico, idLote, indSinc, nFes);

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlEnvio = pedEnvio.ObterXmlString();
            Validador.Valida(ServicoNFe.NFeAutorizacao, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNFeAutorizacao, xmlEnvio);
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xmlEnvio);

            SalvarArquivoXml(idLote + "-env-lot.xml", xmlEnvio);

            var retorno = ws.Execute(dadosEnvio);
            var retornoXmlString = retorno.OuterXml;
            var retEnvio = new retEnviNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(idLote + "-rec.xml", retornoXmlString);

            return new RetornoNFeAutorizacao(pedEnvio.ObterXmlString(), retEnvio.ObterXmlString(), retornoXmlString, retEnvio);

            #endregion
        }

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns>Retorna um objeto da classe RetornoNFeRetAutorizacao com com os dados do processamento do lote</returns>
        public RetornoNFeRetAutorizacao NFeRetAutorizacao(string recibo)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NFeRetAutorizacao,
                _cFgServico.VersaoNFeRetAutorizacao);

            #region Cria o objeto wdsl para consulta

            var ws = CriarServico(ServicoNFe.NFeRetAutorizacao, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                versaoDados = versaoServico
            };

            #endregion

            #region Cria o objeto consReciNFe

            var pedRecibo = new consReciNFe
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

            SalvarArquivoXml(recibo + "-ped-rec.xml", xmlRecibo);

            var retorno = ws.Execute(dadosRecibo);
            var retornoXmlString = retorno.OuterXml;
            var retRecibo = new retConsReciNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(recibo + "-pro-rec.xml", retornoXmlString);

            return new RetornoNFeRetAutorizacao(pedRecibo.ObterXmlString(), retRecibo.ObterXmlString(), retornoXmlString, retRecibo);

            #endregion
        }

        #endregion

        /// <summary>
        ///     Consulta a Situação da NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da NFe</returns>
        public RetornoNfeDownload NfeDownloadNf(string cnpj, List<string> chaves)
        {
            var versaoServico = Auxiliar.VersaoServicoParaString(ServicoNFe.NfeDownloadNF, _cFgServico.VersaoNfeDownloadNF);

            #region Cria o objeto wdsl para envio do pedido de Download

            var ws = CriarServico(ServicoNFe.NfeDownloadNF, TipoRecepcaoEvento.Nenhum);

            ws.nfeCabecMsg = new nfeCabecMsg
            {
                cUF = _cFgServico.cUF,
                //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
                versaoDados = /*versaoServico*/ "1.00" 
            };

            #endregion

            #region Cria o objeto downloadNFe

            var pedDownload = new downloadNFe
            {
                //Embora em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#GO esse serviço está nas versões 2.00 e 3.10, ele rejeita se mandar a versão diferente de 1.00. Testado no Ambiente Nacional - (AN)
                versao = /*versaoServico*/ "1.00",
                CNPJ = cnpj,
                tpAmb = _cFgServico.tpAmb,
                chNFe = chaves
            };

            #endregion

            #region Valida, Envia os dados e obtém a resposta

            var xmlDownload = pedDownload.ObterXmlString();
            Validador.Valida(ServicoNFe.NfeDownloadNF, TipoRecepcaoEvento.Nenhum, _cFgServico.VersaoNfeDownloadNF, xmlDownload);
            var dadosDownload = new XmlDocument();
            dadosDownload.LoadXml(xmlDownload);

            SalvarArquivoXml(cnpj + "-ped-down.xml", xmlDownload);

            var retorno = ws.Execute(dadosDownload);
            var retornoXmlString = retorno.OuterXml;
            var retDownload = new retDownloadNFe().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(cnpj + "-down.xml", retornoXmlString);

            return new RetornoNfeDownload(pedDownload.ObterXmlString(), retDownload.ObterXmlString(), retornoXmlString, retDownload);

            #endregion
        }
    }
}