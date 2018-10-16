
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using DFe.Classes.Extensoes;
using DFe.Classes.Flags;

using SMDFe.Classes.Extencoes;

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
        public ResponseBody<CosMDFeNaoEnc> body { get; set; }

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

    public class CosMDFeNaoEnc
    {
        [XmlElement(ElementName = "consMDFeNaoEnc", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public Atributes atributos { get; set; }
    }

    [XmlRoot(ElementName = "consMDFeNaoEnc", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class Atributes
    {
        public Atributes()
        {
            XServ = "CONSULTAR NÃO ENCERRADOS";
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
    }

    class Program
    {


        static void Main(string[] args)
        {

            TesteWs();
            Console.ReadKey();

            //var config = new ConfiguracaoDao().BuscarConfiguracao();
            //var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(config.Empresa.Cnpj);
            //consMDFeNaoEnc.TpAmb = TipoAmbiente.Homologacao; // Teste -- Remover Linha depois
            //consMDFeNaoEnc.Versao = VersaoServico.Versao300; // Teste -- Remover Linha depois


            //Serializacao(config);
            //SerializacaoBody(config);
        }

        private static void TesteWs()
        {
            try
            {
                var config = new ConfiguracaoDao().BuscarConfiguracao();


                var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(config.Empresa.Cnpj);
                consMDFeNaoEnc.TpAmb = TipoAmbiente.Homologacao;
                consMDFeNaoEnc.Versao = VersaoServico.Versao300;
                consMDFeNaoEnc.ValidarSchema();



                var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente = TipoAmbiente.Homologacao)
                    .MDFeConsNaoEnc; //Remover depois
                var versao = VersaoServico.Versao300.GetVersaoString();



                var configuracaoWsdl = CriaConfiguracao(url, versao, config);


                var ws = new MDFeConsNaoEnc(configuracaoWsdl);


                var retornoXml = ws.mdfeConsNaoEnc(consMDFeNaoEnc.CriaRequestWs());
                
                var retorno = MDFeRetConsMDFeNao.LoadXmlString(retornoXml.OuterXml, consMDFeNaoEnc);
                
                Console.WriteLine("Teste Aprovado");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private static void Serializacao(Configuracao configuracao)
        {
            var con = new CosMDFeNaoEnc()
            {
                atributos = new Atributes()
                {
                    Versao = VersaoServico.Versao300,
                    TpAmb = TipoAmbiente.Homologacao,
                    CNPJ = configuracao.Empresa.Cnpj
                }
            };

            var mdfeca = new mdfeCabecMsg()
            {
                cUF = "10",
                versaoDados = VersaoServico.Versao300.GetVersaoString()
            };

            var se = new SOAPEnvelope()
            {
                head = new ResponseHead<mdfeCabecMsg>()
                {
                    mdfeCabecMsg = mdfeca
                },
                body = new ResponseBody<CosMDFeNaoEnc>()
                {
                    mdfeDadosMsg = con
                }
            };


            var soapserializer = new XmlSerializer(typeof(SOAPEnvelope));


            var enXmlDocument = new XmlDocument();

            using (var sww = new StreamWriter("soap.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(sww,
                    new XmlWriterSettings() { Indent = false }))
                {
                    soapserializer.Serialize(writer, se);
                    writer.Close();

                }
            }

            Console.ReadKey();


        }

        private static void SerializacaoBody(Configuracao configuracao)
        {
            var atributos = new Atributes()
            {
                Versao = VersaoServico.Versao300,
                TpAmb = TipoAmbiente.Homologacao,
                CNPJ = configuracao.Empresa.Cnpj
            };


            var mdfeca = new mdfeCabecMsg()
            {
                cUF = "10",
                versaoDados = VersaoServico.Versao300.GetVersaoString()
            };



            var soapserializer = new XmlSerializer(typeof(Atributes));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://www.portalfiscal.inf.br/mdfe");

            using (var sww = new StreamWriter("soap.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(sww,
                    new XmlWriterSettings() { Indent = false }))
                {
                    soapserializer.Serialize(writer, atributos, ns);
                    writer.Close();

                }
            }


            Console.ReadKey();


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
