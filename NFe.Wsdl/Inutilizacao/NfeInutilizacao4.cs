namespace NFe.Wsdl.Inutilizacao
{
    using NFe.Wsdl;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Services;
    using System.Web.Services.Protocols;

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "NFeInutilizacao4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
    public partial class NFeInutilizacao4 : SoapHttpClientProtocol, INfeServico
    {
        /// <remarks/>
        public NFeInutilizacao4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        public nfeCabecMsg nfeCabecMsg { get { return null; } set { } }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeInutilizacaoNF")]
        [return: System.Xml.Serialization.XmlElementAttribute("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
        public System.Xml.XmlNode Execute([System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")] System.Xml.XmlNode nfeDadosMsg)
        {
            object[] results = this.Invoke("nfeInutilizacaoNF", new object[] {
                        nfeDadosMsg});
            return ((System.Xml.XmlNode)(results[0]));
        }
    }
}