using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Utils;
using NFe.Utils.Assinatura;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using NFeClasses = global::NFe.Classes;

namespace NFe.Testes
{
    [TestClass]
    public class NFCeTestes
    {

        public ConfiguracaoServico _conf{ get; set; }
        public ServicosNFe CreateInstance4()
        {
            _conf = new ConfiguracaoServico();
            var cert = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1Arquivo,
                Arquivo = @"D:\Works\CERTIFICADO MORIMO A1.pfx",
                Senha = "morimo1458"
            };

            _conf.Certificado = cert;
            _conf.ModeloDocumento = DFe.Classes.Flags.ModeloDocumento.NFCe; //  Condition

            _conf.tpAmb = NFeClasses.Informacoes.Identificacao.Tipos.TipoAmbiente.taHomologacao;
            _conf.cUF = DFe.Classes.Entidades.Estado.SP;
            _conf.tpEmis = NFeClasses.Informacoes.Identificacao.Tipos.TipoEmissao.teNormal;
            _conf.TimeOut = 120000;
            _conf.DiretorioSalvarXml = @"D:\Works\NFCE";
            _conf.SalvarXmlServicos = true;
            _conf.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Tls12;
            _conf.DiretorioSchemas = @"D:\Works\nfe\nfe-products-api\schemas";
            _conf.VersaoNFeAutorizacao = VersaoServico.ve400;
            _conf.VersaoNfeDownloadNF = VersaoServico.ve400;
            _conf.VersaoNfeStatusServico = VersaoServico.ve400;
            _conf.VersaoNFeRetAutorizacao = VersaoServico.ve400;

            return new ServicosNFe(_conf);
        }
        public NFeClasses.nfeProc CreateObject()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.nfeProc));
            StreamReader reader = new StreamReader(@"D:\Works\NFCE\35180708765239000164550050000053271382938770-nfe.xml");
            var nfe = (Classes.nfeProc)serializer.Deserialize(reader);
            reader.Close();

            return nfe;
        }

        [TestMethod]
        public void ServicosNFe_WhenNfeNFeAutorizacao4_ReturnsxMotivoSuccess()
        {
            var servico = CreateInstance4();
            var nfe = CreateObject();

            var list = new List<Classes.NFe>();
            list.Add(nfe.NFe);

            nfe.NFe.infNFe.ide.mod = DFe.Classes.Flags.ModeloDocumento.NFCe;

            var a = new X509Certificate2(@"D:\Works\CERTIFICADO MORIMO A1.pfx", "morimo1458");

            Assinador.ObterAssinatura<NFeClasses.NFe>(nfe.NFe, "teste", a);

            var result = servico.NFeAutorizacao(1, IndicadorSincronizacao.Sincrono, list);

            Assert.IsTrue("Lote recebido com sucesso" == result.Retorno.xMotivo.ToString());
        }

        //public void Create_Mdfe_Example()
        //{
        //    var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
        //    var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, null/*Logomarca em byte[]*/), "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX");
        //    danfe.Visualizar();
        //}
    }
}
