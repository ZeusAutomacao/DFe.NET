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
using System.Security.Cryptography.X509Certificates;
using CTe.Classes.Informacoes.Tipos;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTe.Classes
{
    public sealed class ConfiguracaoServico : IDisposable
    {
        private static volatile ConfiguracaoServico _instancia;
        private static readonly object SyncRoot = new object();
        private string _diretorioSchemas;

        public ConfiguracaoServico()
        {
            ConfiguracaoCertificado = new ConfiguracaoCertificado();
            TipoEmissao = tpEmis.teNormal;
        }

        /// <summary>
        ///     Configurações relativas ao Certificado Digital
        /// </summary>
        public ConfiguracaoCertificado ConfiguracaoCertificado { get; set; }

        private X509Certificate2 _certificado = null;
        public X509Certificate2 X509Certificate2
        {
            get
            {
                if (this._certificado != null)
                    if (!this.ConfiguracaoCertificado.ManterDadosEmCache)
                        this._certificado.Reset();
                _certificado = ObterCertificado();
                return _certificado;
            }
        }

        private X509Certificate2 ObterCertificado()
        {
            return CertificadoDigital.ObterCertificado(ConfiguracaoCertificado);
        }

        public void Dispose()
        {
            if (!this.ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }

        ~ConfiguracaoServico()
        {
            if (!this.ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }

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
        ///     Versão do layout
        /// </summary>
        public versao VersaoLayout { get; set; }

        public bool IsAdicionaQrCode { get; set; }

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
        public bool IsSalvarXml { get; set; }

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

        public tpEmis TipoEmissao { get; set; }

        public bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }
    }
}