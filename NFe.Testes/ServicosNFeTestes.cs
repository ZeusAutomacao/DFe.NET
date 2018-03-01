using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using DFe.Utils;
using NFeClasses = global::NFe.Classes;
using System.IO;
using System.IdentityModel.Tokens;
using System.Xml.Serialization;


namespace NFe.Testes
{
    [TestClass]
    public class ServicosNFeTestes
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

        public NFeClasses.nfeProc CreateObject()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.nfeProc));
            StreamReader reader = new StreamReader("C:\\Users\\staff\\Desktop\\teste.xml");
            var nfe = (Classes.nfeProc)serializer.Deserialize(reader);
            reader.Close();

            return nfe;
        }

        [TestMethod]
        public void ServicosNFe_WhenNfeNFeAutorizacao_ReturnsxMotivoSuccess()
        {

            var servico = CreateInstance();
            var nfe = CreateObject();

            var list = new List<Classes.NFe>();
            list.Add(nfe.NFe);

            var result = servico.NFeAutorizacao(1, IndicadorSincronizacao.Sincrono, list);

            Assert.IsTrue("Lote recebido com sucesso" == result.Retorno.xMotivo.ToString());


        }

        [TestMethod]
        public void ServicosNFe_WhenNFeRetAutorizacao_ReturnsLoteOk()
        {
            var servico = CreateInstance();

            var result = servico.NFeRetAutorizacao("351000117302663");
            
            Assert.IsTrue("Lote processado" == result.Retorno.xMotivo.ToString());
        }


        [TestMethod]
        public void teste()
        {
            var servico = CreateInstance();

            var result = servico.NfeDistDFeInteresse("87554774", "67390111000122");

            Assert.IsTrue("Lote processado" == result.Retorno.xMotivo.ToString());
        }

    }
}
