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
using System.ComponentModel;
using NFe.Utils.Annotations;
using System.Xml.Serialization;

namespace NFe.Utils
{
    public class ConfiguracaoCertificado : INotifyPropertyChanged
    {
        private string _serial;
        private string _arquivo;

        /// <summary>
        ///     Nº de série do certificado digital
        /// </summary>
        public string Serial
        {
            get { return _serial; }
            set
            {
                if (value == _serial) return;
                _serial = value;
                if (!string.IsNullOrEmpty(value))
                    Arquivo = null;
                OnPropertyChanged("Serial");
            }
        }

        /// <summary>
        ///     Arquivo do certificado digital
        /// </summary>
        public string Arquivo
        {
            get { return _arquivo; }
            set
            {
                if (value == _arquivo) return;
                _arquivo = value;
                if (!string.IsNullOrEmpty(value))
                    Serial = null;
                OnPropertyChanged("Arquivo");
            }
        }

        /// <summary>
        ///     Senha do certificado digital
        /// </summary>
        public string Senha { get; set; }
        
        [XmlIgnore]
        public System.Security.Cryptography.X509Certificates.X509Certificate2 CertificadoCarregado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
