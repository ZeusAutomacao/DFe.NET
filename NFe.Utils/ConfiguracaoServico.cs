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
using System.IO;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils
{
    public sealed class ConfiguracaoServico
    {
        private static volatile ConfiguracaoServico _instancia;
        private static readonly object SyncRoot = new object();
        private string _diretorioSchemas;
        private bool _salvarXmlServicos;

        private ConfiguracaoServico()
        {
        }

        /// <summary>
        ///     Nº de série do certificado digital
        /// </summary>
        public string SerialCertificado { get; set; }

        /// <summary>
        ///     Tempo máximo de espera pela resposta do webservice, em milisegundos
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        ///     Estado de destino do webservice
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     Tipo de ambiente do webservice (Produção, Homologação)
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Tipo de Emissão da NF-e
        /// </summary>
        public TipoEmissao tpEmis { get; set; }

        /// <summary>
        ///     Tipo de documento que está sendo referenciado nos webservices
        /// </summary>
        public ModeloDocumento ModeloDocumento { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento
        /// </summary>
        public VersaoServico VersaoRecepcaoEvento { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRecepcao
        /// </summary>
        public VersaoServico VersaoNfeRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRetRecepcao
        /// </summary>
        public VersaoServico VersaoNfeRetRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaCadastro
        /// </summary>
        public VersaoServico VersaoNfeConsultaCadastro { get; set; }

        /// <summary>
        ///     Versão do serviço NfeInutilizacao
        /// </summary>
        public VersaoServico VersaoNfeInutilizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaProtocolo
        /// </summary>
        public VersaoServico VersaoNfeConsultaProtocolo { get; set; }

        /// <summary>
        ///     Versão do serviço NfeStatusServico
        /// </summary>
        public VersaoServico VersaoNfeStatusServico { get; set; }

        /// <summary>
        ///     Versão do serviço NFeAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeRetAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeRetAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeDistribuicaoDFe
        /// </summary>
        public VersaoServico VersaoNFeDistribuicaoDFe { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaDest
        /// </summary>
        public VersaoServico VersaoNfeConsultaDest { get; set; }

        /// <summary>
        ///     Versão do serviço NfeDownloadNF
        /// </summary>
        public VersaoServico VersaoNfeDownloadNF { get; set; }

        /// <summary>
        ///     Diretório onde estão aramazenados os schemas para validação
        /// </summary>
        public string DiretorioSchemas
        {
            get { return _diretorioSchemas; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !Directory.Exists(value))
                    throw new Exception("Diretório " + value + " não encontrado!");
                _diretorioSchemas = value;
            }
        }

        /// <summary>
        ///     Informar se a biblioteca deve salvar o xml de envio e de retorno
        /// </summary>
        public bool SalvarXmlServicos
        {
            get { return _salvarXmlServicos; }
            set
            {
                if (!value)
                    DiretorioSalvarXml = "";
                _salvarXmlServicos = value;
            }
        }

        /// <summary>
        ///     Diretório onde os xmls de envio/retorno devem ser salvos
        /// </summary>
        public string DiretorioSalvarXml { get; set; }

        /// <summary>
        ///     Instância do Singleton de ConfiguracaoServico
        /// </summary>
        public static ConfiguracaoServico Instancia
        {
            get
            {
                if (_instancia != null) return _instancia;
                lock (SyncRoot)
                {
                    if (_instancia != null) return _instancia;
                    _instancia = new ConfiguracaoServico();
                }

                return _instancia;
            }
        }
    }
}