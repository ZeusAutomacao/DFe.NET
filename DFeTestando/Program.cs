using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.CTeOS;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFeTestando
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DFeSoapConfig();
            config.DFeCabecalho = new DFeCabecalho(Estado.GO, VersaoServico.Versao300);
            config.NamespaceBody = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS";
            config.NamespaceHeader = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS";
            config.Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS";
            config.Url = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoos/cterecepcaoos.asmx";
            config.TimeOut = 50000;
            config.Certificado = new X509Certificate2(@"C:\Users\rober\Documents\Certificados\AGIL4 TECNOLOGIA LTDA  ME21025760000123.pfx", "agil4@123");
            config.TagTagCabecalho = new CteTagCabecalho();
            config.TagCorpo = new CteTagCorpo();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            CTeOS cteOs = CTeOS.LoadXmlArquivo(@"C:\Users\rober\Desktop\xmlcteos\21351378000100\setembro\Autorizar\Enviado\52170921351378000100670010000000081603356706-cte.xml");

            var request = new XmlDocument();
            request.LoadXml(cteOs.ObterXmlString());

            config.Xml = request;

            new CteRecepcaoOSS().Autorizar(config);
        }
    }
}
