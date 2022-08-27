﻿/********************************************************************************/
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
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;
using NFe.Classes.Servicos.Autorizacao;

namespace NFe.Wsdl.Autorizacao
{
    public class NfeAutorizacao3 : NfeAutorizacao3Soap12Client, INfeServicoAutorizacao
    {
        public NfeAutorizacao3(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            //Timeout = timeOut;
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeAutorizacaoLoteAsync(nfeCabecMsg, nfeDadosMsg);
            return result.nfeAutorizacaoLoteResult;
        }

        public XmlNode Execute(enviNFe3 nfeDadosMsg)
        {
            throw new NotImplementedException();
        }

        //[SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        //[SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3/nfeAutorizacaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        //[WebMethod(MessageName = "nfeAutorizacaoLote")]
        //[return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")]
        //public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")] enviNFe3 nfeDadosMsg)
        //{
        //    var results = Invoke("nfeAutorizacaoLote", new object[] { nfeDadosMsg });
        //    return ((XmlNode)(results[0]));
        //}

        //public XmlNode Execute_([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")] XmlNode nfeDadosMsg)
        //{
        //    var results = Invoke("nfeAutorizacaoLote", new object[] { nfeDadosMsg });
        //    return ((XmlNode)(results[0]));
        //}

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            return ExecuteZipAsync(nfeDadosMsgZip).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteZipAsync(string nfeDadosMsgZip)
        {
            var result = await base.nfeAutorizacaoLoteZipAsync(this.nfeCabecMsg, nfeDadosMsgZip);
            return result.nfeAutorizacaoLoteResult;
        }

        //public XmlNode ExecuteZip_(string nfeDadosMsgZip)
        //{
        //    var results = Invoke("nfeAutorizacaoLoteZip", new object[] { nfeDadosMsgZip });
        //    return ((XmlNode)(results[0]));
        //}
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao3LoteRequest
    {

        [MessageHeader(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeAutorizacao3LoteRequest()
        {
        }

        public nfeAutorizacao3LoteRequest(nfeCabecMsg nfeCabecMsg, XmlNode nfeDadosMsg)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao3LoteResponse
    {

        [MessageHeader(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3", Order = 0)]
        public XmlNode nfeAutorizacaoLoteResult;

        public nfeAutorizacao3LoteResponse()
        {
        }

        public nfeAutorizacao3LoteResponse(nfeCabecMsg nfeCabecMsg, XmlNode nfeAutorizacaoLoteResult)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeAutorizacaoLoteResult = nfeAutorizacaoLoteResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao3LoteZipRequest
    {

        [MessageHeader(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3", Order = 0)]
        public string nfeDadosMsgZip;

        public nfeAutorizacao3LoteZipRequest()
        {
        }

        public nfeAutorizacao3LoteZipRequest(nfeCabecMsg nfeCabecMsg, string nfeDadosMsgZip)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3", ConfigurationName = "NfeAutorizacao3Soap12")]
    public interface NfeAutorizacao3Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3/nfeAutorizacaoLote", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeAutorizacao3LoteResponse> nfeAutorizacaoLoteAsync(nfeAutorizacao3LoteRequest request);

        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao3/nfeAutorizacaoLoteZip", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeAutorizacao3LoteResponse> nfeAutorizacaoLoteZipAsync(nfeAutorizacao3LoteZipRequest request);
    }

    public partial class NfeAutorizacao3Soap12Client : SoapBindingClient<NfeAutorizacao3Soap12>
    {
        public NfeAutorizacao3Soap12Client(string endpointAddressUri) : base(endpointAddressUri)
        {
        }

        public Task<nfeAutorizacao3LoteResponse> nfeAutorizacaoLoteAsync(nfeCabecMsg nfeCabecMsg, XmlNode nfeDadosMsg)
        {
            nfeAutorizacao3LoteRequest inValue = new nfeAutorizacao3LoteRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((NfeAutorizacao3Soap12)(this.Channel)).nfeAutorizacaoLoteAsync(inValue);
        }

        public Task<nfeAutorizacao3LoteResponse> nfeAutorizacaoLoteZipAsync(nfeCabecMsg nfeCabecMsg, string nfeDadosMsgZip)
        {
            nfeAutorizacao3LoteZipRequest inValue = new nfeAutorizacao3LoteZipRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            return ((NfeAutorizacao3Soap12)(this.Channel)).nfeAutorizacaoLoteZipAsync(inValue);
        }
    }
}
