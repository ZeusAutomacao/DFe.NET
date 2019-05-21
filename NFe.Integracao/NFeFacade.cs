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
using System.Linq;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Servicos.Tipos;
using NFe.Classes.Servicos.Status;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.NFe;
using NFe.Danfe.Nativo.NFCe;
using NFe.Danfe.Base.NFCe;

namespace NFe.Integracao
{
    public class NFeFacade
    {
        public NFeFacade()
        {
            CarregarConfiguracoes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public retConsStatServ ConsultarStatusServico()
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeStatusServico().Retorno;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numLote"></param>
        /// <param name="nfe"></param>
        /// <returns></returns>
        public RetornoNFeAutorizacao EnviarNFe(Int32 numLote, Classes.NFe nfe)
        {
            nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NFeAutorizacao(numLote, IndicadorSincronizacao.Assincrono, new List<Classes.NFe> { nfe });
        }

        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        ///     <para>O documento pode ser: CPF ou CNPJ. O serviço avaliará o tamanho da string passada e determinará se a coonsulta será por CPF ou por CNPJ</para>
        /// </summary>
        /// <param name="uf">Sigla da UF consultada, informar 'SU' para SUFRAMA.</param>
        /// <param name="tipoDocumento">Tipo do documento</param>
        /// <param name="documento">CPF ou CNPJ</param>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaCadastro com o retorno do serviço NfeConsultaCadastro</returns>
        public RetornoNfeConsultaCadastro ConsultaCadastro(string uf,ConsultaCadastroTipoDocumento tipoDocumento,
            string documento)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeConsultaCadastro(uf,tipoDocumento, documento);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns></returns>
        public RetornoNFeRetAutorizacao ConsultarReciboDeEnvio(string recibo)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NFeRetAutorizacao(recibo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cnpjEmitente"></param>
        /// <param name="numeroLote"></param>
        /// <param name="sequenciaEvento"></param>
        /// <param name="chaveAcesso"></param>
        /// <param name="protocolo"></param>
        /// <param name="justificativa"></param>
        /// <returns></returns>
        public RetornoRecepcaoEvento CancelarNFe(string cnpjEmitente, int numeroLote, short sequenciaEvento, string chaveAcesso,
            string protocolo, string justificativa)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.RecepcaoEventoCancelamento(numeroLote, sequenciaEvento, protocolo, chaveAcesso, justificativa, cnpjEmitente);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="cnpj"></param>
        /// <param name="justificativa"></param>
        /// <param name="numeroInicial"></param>
        /// <param name="numeroFinal"></param>
        /// <param name="serie"></param>
        /// <returns></returns>
        public RetornoNfeInutilizacao InutilizarNumeracao(int ano, string cnpj, string justificativa,
            int numeroInicial, int numeroFinal, int serie)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeInutilizacao(cnpj, Convert.ToInt16(ano.ToString().Substring(2, 2)), ConfiguracaoServico.Instancia.ModeloDocumento, Convert.ToInt16(serie), Convert.ToInt32(numeroInicial), Convert.ToInt32(numeroFinal), justificativa);
        }
        /// <summary>
        /// 
        /// </summary>
        private void CarregarConfiguracoes()
        {
            #region Set file config

            ConfiguracaoServico.Instancia.Certificado.TipoCertificado = TipoCertificado.A1Arquivo;
            ConfiguracaoServico.Instancia.Certificado.Arquivo = Properties.Settings.Default.certificado_arquivo;
            ConfiguracaoServico.Instancia.Certificado.Senha = Properties.Settings.Default.certificado_senha;
            ConfiguracaoServico.Instancia.DiretorioSalvarXml = Properties.Settings.Default.diretorio_xml;
            //-------------------
            Estado uf;
            Enum.TryParse(Properties.Settings.Default.estado_emitente, out uf);
            ConfiguracaoServico.Instancia.cUF = uf;
            //-------------------
            ConfiguracaoServico.Instancia.DiretorioSchemas = Properties.Settings.Default.diretorio_schemas;
            ModeloDocumento mod;
            Enum.TryParse(Properties.Settings.Default.modelo_documento, out mod);
            ConfiguracaoServico.Instancia.ModeloDocumento = mod;
            //-------------------
            ConfiguracaoServico.Instancia.SalvarXmlServicos = Properties.Settings.Default.salvar_xml_servicos == "1";
            //-------------------
            int time;
            int.TryParse(Properties.Settings.Default.time_out, out time);
            ConfiguracaoServico.Instancia.TimeOut = time;
            //-------------------
            TipoAmbiente tamb;
            Enum.TryParse(Properties.Settings.Default.tipo_ambiente, out tamb);
            ConfiguracaoServico.Instancia.tpAmb = tamb;
            //-------------------
            TipoEmissao temiss;
            Enum.TryParse(Properties.Settings.Default.tipo_emissao, out temiss);
            ConfiguracaoServico.Instancia.tpEmis = temiss;
            //-------------------------------------------------------------------------------

            var versaoNFe = Properties.Settings.Default.versao_NFe;

            ConfiguracaoServico.Instancia.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Tls;
            ConfiguracaoServico.Instancia.VersaoNfceAministracaoCSC = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNFeAutorizacao = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaCadastro = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaDest = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaProtocolo = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNFeDistribuicaoDFe = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeDownloadNF = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeInutilizacao = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeRecepcao = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNFeRetAutorizacao = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeRetRecepcao = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoNfeStatusServico = versaoNFe;
            ConfiguracaoServico.Instancia.VersaoRecepcaoEventoCceCancelamento = versaoNFe;

            #endregion

        }
        /// <summary>
        /// Verificar se todas as variáveis de configuração possuem valor
        /// </summary>
        /// <returns></returns>
        public bool IsConfigured()
        {
            var list = new List<string>();
            list.Add(Properties.Settings.Default.certificado_arquivo);
            list.Add(Properties.Settings.Default.certificado_senha);
            list.Add(Properties.Settings.Default.diretorio_xml);
            list.Add(Properties.Settings.Default.diretorio_schemas);
            list.Add(Properties.Settings.Default.estado_emitente);
            list.Add(Properties.Settings.Default.modelo_documento);
            list.Add(Properties.Settings.Default.salvar_xml_servicos);
            list.Add(Properties.Settings.Default.time_out);
            list.Add(Properties.Settings.Default.tipo_ambiente);
            list.Add(Properties.Settings.Default.tipo_emissao);
            //Se pelo menos uma variável não estiver com valor, false
            return !list.Any(string.IsNullOrWhiteSpace);

        }

        /// <summary>
        /// Imprime em um JPEG o NFC-e relacionado a um xml.
        /// </summary>
        /// <param name="pathXmlNFCe">Path do NFC-e a imprimir</param>
        /// <param name="pathJpeg">Path onde gerar o jpeg</param>
        public void ImprimirNFCe(string pathXmlNFCe, string pathJpeg, string idToken, string csc)
        {
            var nfe = new Classes.NFe().CarregarDeArquivoXml(pathXmlNFCe);
            var arquivo = nfe.ObterXmlString();

            var configuracaoDanfeNFCe = new ConfiguracaoDanfeNfce(Danfe.Base.NfceDetalheVendaNormal.UmaLinha, Danfe.Base.NfceDetalheVendaContigencia.UmaLinha);
            DanfeNativoNfce impr = new DanfeNativoNfce(arquivo, configuracaoDanfeNFCe, idToken, csc);

            impr.GerarJPEG(pathJpeg);
        }

        /// <summary>
        /// Alterar dados de configuração
        /// </summary>
        /// <param name="pathcertificado">Caminho do certifiacdo</param>
        /// <param name="certificadosenha">Senha do certifiacdo</param>
        /// <param name="pathxml">Caminho de saida para o arquivo XML</param>
        /// <param name="pathchema">Caminho dos schemas XML</param>
        /// <param name="emitente">UF do emitente</param>
        /// <param name="modelodocumento">Modelo do documento {65|55}</param>
        /// <param name="salvarxmlservico">1 - Salva arquivo xml</param>
        /// <param name="timeout">Tempo de resposta estimado em milisegundos </param>
        /// <param name="tmpamb">{P - Produção | H - Homologação}</param>
        /// <param name="tmpemissao">Tipo de emissão 1 (Normal), 2 (FS-IA), 3 (SCAN), 4 (EPEC), 5 (FS-DA), 6 (SVC-AN), 7 (SVC-RS) ou 9 (Offline)</param>
        public static void SetConfiguracoes(string pathcertificado, string certificadosenha, string pathxml,
            string pathchema, string emitente, string modelodocumento, string salvarxmlservico, string timeout,
            string tmpamb, string tmpemissao, string versaoNFe)
        {

            Properties.Settings.Default.salvar_xml_servicos = salvarxmlservico == "1" ? "1" : "0";
            Properties.Settings.Default.certificado_arquivo = ConvertToLower(pathcertificado);
            Properties.Settings.Default.certificado_senha = ConvertToLower(certificadosenha);
            Properties.Settings.Default.diretorio_xml = ConvertToLower(pathxml);
            Properties.Settings.Default.diretorio_schemas = ConvertToLower(pathchema);
            Properties.Settings.Default.estado_emitente = ConvertToLower(emitente);
            Properties.Settings.Default.modelo_documento = ConvertToLower(modelodocumento);
            Properties.Settings.Default.salvar_xml_servicos = ConvertToLower(salvarxmlservico);
            Properties.Settings.Default.time_out = string.IsNullOrWhiteSpace(timeout) ? "5000" : timeout;
            Properties.Settings.Default.tipo_ambiente = ConvertToLower(tmpamb);
            Properties.Settings.Default.tipo_emissao = ConvertToLower(tmpemissao);
            Properties.Settings.Default.versao_NFe = versaoNFe == "3.10" ? VersaoServico.Versao310 : VersaoServico.Versao400;

            //Salvar configurações do usuario
            Properties.Settings.Default.Save();

        }

        public static IEnumerable<string> GetConfiguracao()
        {
            #region Get Enums
            ModeloDocumento mod;
            Enum.TryParse(Properties.Settings.Default.modelo_documento, out mod);
            TipoAmbiente tamb;
            Enum.TryParse(Properties.Settings.Default.tipo_ambiente, out tamb);
            TipoEmissao temiss;
            Enum.TryParse(Properties.Settings.Default.tipo_emissao, out temiss);
            var isSave = Properties.Settings.Default.salvar_xml_servicos == "1" ? "Sim" : "Não";
            #endregion

            var list = new List<string>
            {
                "Salvar arquivo XML: " + isSave,
                "Caminho do certificado digital (.pfx): " + Properties.Settings.Default.certificado_arquivo,
                "Senha certificado digital: **********",
                "Caminho do arquivo XML: " + Properties.Settings.Default.diretorio_xml,
                "Caminho dos arquivos .xsd: " + Properties.Settings.Default.diretorio_schemas,
                "Estado Emitente: " + Properties.Settings.Default.estado_emitente,
                "Modelo do documento: " + mod,
                "Tempo de resposta esperado: " + Properties.Settings.Default.time_out,
                "Ambiente: " + tamb.Descricao(),
                "Tipo de emissão: " + temiss.Descricao()
            };

            return list;

        }

        /// <summary>
        /// Converte entrada de dados para minuscula
        /// </summary>
        /// <param name="str">uma string</param>
        /// <returns></returns>
        private static string ConvertToLower(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str.Trim().ToLower();
        }      

    }
}