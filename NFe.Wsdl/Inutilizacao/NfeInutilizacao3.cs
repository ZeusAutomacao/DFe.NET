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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Inutilizacao
{
    public class NfeInutilizacao3 : NfeInutilizacao3Soap12Client, INfeServico
    {
        public NfeInutilizacao3(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeInutilizacaoNF3Async(nfeCabecMsg, nfeDadosMsg);
            return result.nfeInutilizacaoNF3Result;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3", ConfigurationName = "NfeInutilizacao3Soap12")]
    public interface NfeInutilizacao3Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3/nfeInutilizacaoNF", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeInutilizacaoNF3Response> nfeInutilizacaoNF3Async(nfeInutilizacaoNF3Request request);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacaoNF3Request
    {

        [MessageHeader(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeInutilizacaoNF3Request()
        {
        }

        public nfeInutilizacaoNF3Request(nfeCabecMsg nfeCabecMsg, XmlNode nfeDadosMsg)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacaoNF3Response
    {

        [MessageHeader(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3", Order = 0)]
        public XmlNode nfeInutilizacaoNF3Result;

        public nfeInutilizacaoNF3Response()
        {
        }

        public nfeInutilizacaoNF3Response(nfeCabecMsg nfeCabecMsg, XmlNode nfeInutilizacaoNF3Result)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeInutilizacaoNF3Result = nfeInutilizacaoNF3Result;
        }
    }
    
    public partial class NfeInutilizacao3Soap12Client : SoapBindingClient<NfeInutilizacao3Soap12>
    {
        public NfeInutilizacao3Soap12Client(string endpointAddressUri) : base(endpointAddressUri)
        {
        }

        public Task<nfeInutilizacaoNF3Response> nfeInutilizacaoNF3Async(nfeCabecMsg nfeCabecMsg, XmlNode nfeDadosMsg)
        {
            nfeInutilizacaoNF3Request inValue = new nfeInutilizacaoNF3Request();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((NfeInutilizacao3Soap12)(this.Channel)).nfeInutilizacaoNF3Async(inValue);
        }
    }
}