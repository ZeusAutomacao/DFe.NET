using DFe.Utils;
using DFe.Utils.Assinatura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Utils;
using NFe.Wsdl.Autorizacao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NFeClasses = global::NFe.Classes;

namespace NFe.Testes
{
    [TestClass]
    public class WsdlBindingTest
    {

        public ServicosNFe CreateInstance()
        {
            var conf = new ConfiguracaoServico();
            var cert = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1Arquivo,
                Arquivo = @"D:\Works\ProductInvoices\A1_NFE_08765239000164_cer2017.pfx",
                Senha = "cer2017"
            };

            conf.Certificado = cert;
            conf.tpAmb = NFeClasses.Informacoes.Identificacao.Tipos.TipoAmbiente.taHomologacao;
            conf.cUF = DFe.Classes.Entidades.Estado.SP;
            conf.tpEmis = NFeClasses.Informacoes.Identificacao.Tipos.TipoEmissao.teNormal;
            conf.TimeOut = 120000;
            conf.DiretorioSalvarXml = @"D:\Works\";
            conf.SalvarXmlServicos = true;
            conf.ModeloDocumento = DFe.Classes.Flags.ModeloDocumento.NFe;
            conf.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Ssl3;
            conf.DiretorioSchemas = @"D:\Works\Schemas\";
            conf.VersaoNFeAutorizacao = VersaoServico.ve310;
            conf.VersaoNfeDownloadNF = VersaoServico.ve310;
            conf.VersaoNfeStatusServico = VersaoServico.ve310;
            conf.VersaoNFeRetAutorizacao = VersaoServico.ve310;

            return new ServicosNFe(conf);
        }

        private static X509Certificate2 ObterDeArquivo(string arquivo, string senha)
        {
            if (!File.Exists(arquivo))
            {
                throw new Exception(String.Format("Certificado digital {0} não encontrado!", arquivo));
            }

            var certificado = new X509Certificate2(arquivo, senha, X509KeyStorageFlags.MachineKeySet);
            return certificado;
        }


        [TestMethod]
        public void teste()
        {
            var cert = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1Arquivo,
                Arquivo = @"D:\Works\ProductInvoices\A1_NFE_08765239000164_cer2017.pfx",
                Senha = "cer2017"
            };

            var certificado = WsdlBindingTest.ObterDeArquivo(cert.Arquivo, cert.Senha);
            var ws = new NfeAutorizacao("https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx", certificado, 1000);

            Assert.IsTrue(true);

        }

    }
}
