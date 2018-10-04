
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using SMDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using SMDFe.Servicos.Enderecos.Helper;
using SMDFe.Servicos.Factory;
using SMDFe.Utils.Configuracoes;
using SMDFe.Utils.Flags;
using SMDFe.Wsdl.Configuracao;
using SMDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;

namespace TestandoWsdl
{
    [XmlType(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SOAPEnvelope
    {
        [XmlAttribute(AttributeName = "soap12", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public string soapenva { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public string xsd { get; set; }

        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseHead<mdfeCabecMsg> head { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
        public SOAPEnvelope()
        {
            xmlns.Add("soap12", "http://www.w3.org/2003/05/soap-envelope");
        }
    }

    [XmlRoot(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class ResponseHead<T>
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")]
        public T mdfeCabecMsg { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class ResponseBody<T>
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")]
        public T mdfeDadosMsg { get; set; }
    }


    public class mdfeCabecMsg
    {

        private string cUFField;

        private string versaoDadosField;

        /// <remarks/>
        
        public string cUF
        {
            get
            {
                return this.cUFField;
            }
            set
            {
                this.cUFField = value;
            }
        }

        /// <remarks/>
        
        public string versaoDados
        {
            get
            {
                return this.versaoDadosField;
            }
            set
            {
                this.versaoDadosField = value;
            }
        }

       
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            

            var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(config.Empresa.Cnpj);
            consMDFeNaoEnc.TpAmb = TipoAmbiente.Homologacao; // Teste -- Remover Linha depois
            consMDFeNaoEnc.Versao = VersaoServico.Versao300; // Teste -- Remover Linha depois

            
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente = TipoAmbiente.Homologacao).MDFeConsNaoEnc; //Remover depois
            var versao = (MDFeConfiguracao.VersaoWebService.VersaoLayout = VersaoServico.Versao300).GetVersaoString(); //Remover depois

            var configuracaoWsdl = CriaConfiguracao(url, versao, config);



            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            var retornoXml = ws.mdfeConsNaoEnc(consMDFeNaoEnc.CriaRequestWs());
            var retorno = MDFeRetConsMDFeNao.LoadXmlString(retornoXml.OuterXml, consMDFeNaoEnc);

            //Serializacao(consMDFeNaoEnc);
        }

        private static void Serializacao(MDFeCosMDFeNaoEnc consMDFeNaoEnc)
        {
            var xmlNode = consMDFeNaoEnc.CriaRequestWs();

            var mdfeca = new mdfeCabecMsg()
            {
                cUF = "10",
                versaoDados = "300"
            };


            var se = new SOAPEnvelope()
            {
                head = new ResponseHead<mdfeCabecMsg>()
                {
                    mdfeCabecMsg = mdfeca
                },
                body = new ResponseBody<XmlNode>()
                {
                    mdfeDadosMsg = xmlNode
                }
            };
            

            var soapserializer = new XmlSerializer(typeof(SOAPEnvelope));


            var enXmlDocument = new XmlDocument();

            using (StreamWriter arqStream = new StreamWriter("soap.xml"))
            {
                using (XmlTextWriter soapwriter = new XmlTextWriter(arqStream))
                {
                    soapserializer.Serialize(soapwriter, se);
                    soapwriter.Close();
                    enXmlDocument.Load("soap.xml");
                }
            }

            // 
            Console.WriteLine("SOAP soapenvelopefortest.xml generated");
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, Configuracao confi)
        {
            var codigoEstado = confi.ConfigWebService.UfEmitente.GetCodigoIbgeEmString();
            var certificadoDigital = Program.ObterCertificado();

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = 1000
            };
        }

        private static X509Certificate2 ObterCertificado()
        {
            X509Certificate2Collection lcerts;
            X509Store lStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 xcertifaCertificate2 = null;
            Object[] certificadObjects = new object[10];
            int i = 0;

            // Abre o Store
            lStore.Open(OpenFlags.ReadOnly);

            // Lista os certificados
            lcerts = lStore.Certificates;

            foreach (X509Certificate2 cert in lcerts)
            {
                if (cert.HasPrivateKey && cert.NotAfter > DateTime.Now && cert.NotBefore < DateTime.Now)
                {
                    certificadObjects[i++] = cert;
                    
                }
            }
            lStore.Close();
            xcertifaCertificate2 = certificadObjects[0] as X509Certificate2;

            return xcertifaCertificate2;

        }




    }
}
