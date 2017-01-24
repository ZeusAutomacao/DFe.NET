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
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Informacoes.Detalhe.DeclaracaoImportacao
{
    public class DI
    {
        private decimal? _vAfrmm;

        /// <summary>
        ///     I19 - Número do Documento de Importação (DI, DSI, DIRE, ...)
        /// </summary>
        public string nDI { get; set; }

        /// <summary>
        ///     I20 - Data de Registro do documento.
        /// </summary>
        [XmlIgnore]
        public DateTime dDI { get; set; }

        /// <summary>
        /// Proxy para dDI no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dDI")]
        public string ProxydDI
        {
            get { return dDI.ParaDataString(); }
            set { dDI = DateTime.Parse(value); }
        }
        
        /// <summary>
        ///     I21 - Local de desembaraço
        /// </summary>
        public string xLocDesemb { get; set; }

        /// <summary>
        ///     I22 - Sigla da UF onde ocorreu o Desembaraço Aduaneiro
        /// </summary>
        public string UFDesemb { get; set; }

        /// <summary>
        ///     I23 - Data do Desembaraço Aduaneiro.
        /// </summary>
        [XmlIgnore]
        public DateTime dDesemb { get; set; }

        /// <summary>
        /// Proxy para dDesemb no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dDesemb")]
        public string ProxydDesemb
        {
            get { return dDesemb.ParaDataString(); }
            set { dDesemb = DateTime.Parse(value); }
        }

        /// <summary>
        ///     I23a - Via de transporte internacional informada na Declaração de Importação (DI)
        /// </summary>
        public TipoTransporteInternacional tpViaTransp { get; set; }

        /// <summary>
        ///     I23b - Valor da AFRMM - Adicional ao Frete para Renovação da Marinha Mercante
        /// </summary>
        public decimal? vAFRMM
        {
            get { return _vAfrmm.Arredondar(2); }
            set { _vAfrmm = value.Arredondar(2); }
        }

        /// <summary>
        ///     I23c - Forma de importação quanto a intermediação
        /// </summary>
        public TipoIntermediacao tpIntermedio { get; set; }

        /// <summary>
        ///     I23d - CNPJ do adquirente ou do encomendante
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     I23e - Sigla da UF do adquirente ou do encomendante
        /// </summary>
        public string UFTerceiro { get; set; }

        /// <summary>
        ///     I24 - Código do Exportador
        /// </summary>
        public string cExportador { get; set; }

        /// <summary>
        ///     I25 - Adições
        /// </summary>
        [XmlElement("adi")]
        public List<adi> adi { get; set; }

        public bool ShouldSerializevAFRMM()
        {
            return vAFRMM.HasValue;
        }
    }
}