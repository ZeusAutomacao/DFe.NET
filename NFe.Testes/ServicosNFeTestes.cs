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

        //consulta NfeConsultaCadastro 1.0 hom não disponivel binding ok
        [TestMethod]
        public void ServicosNFe_WhenNfeConsultaCadastroBindingOK_ReturnExceptionService()
        {
            var servico = CreateInstance();
            var message = default(string);
            var esperado = "Serviço NfeConsultaCadastro, versão 1.00, não disponível para a UF SP, no ambiente de Homologação para emissão tipo Normal, documento: NF-e!";

            try
            {
                var result = servico.NfeConsultaCadastro("SP", NFeClasses.Servicos.ConsultaCadastro.ConsultaCadastroTipoDocumento.Cnpj, null);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            Assert.IsTrue(esperado == message);
        }

        //consulta NfeConsultaProtocolo 1.0 hom não disponivel
        [TestMethod]
        public void ServicosNFe_WhenNfeConsultaProtocoloBindingOK_ReturnExceptionService()
        {
            var servico = CreateInstance();
            var message = default(string);
            var esperado = "Serviço NfeConsultaProtocolo, versão 1.00, não disponível para a UF SP, no ambiente de Homologação para emissão tipo Normal, documento: NF-e!";

            try
            {
                var result = servico.NfeConsultaProtocolo("35171203744425000101550020000530201111012127");
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            Assert.IsTrue(esperado == message);
        }


        [TestMethod]
        public void ServicosNFe_WhenNfeDistDFeInteresseBindingOK_ReturnExceptionService()
        {
            var servico = CreateInstance();
            var message = default(string);

            try
            {
                var result = servico.NfeDistDFeInteresse("87554774", "67390111000122");
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }


            Assert.IsTrue("One or more errors occurred." == message);
        }

        [TestMethod]
        public void ServicosNFe_WhenRecepcaoEventoEPECBindingOK_ReturnExceptionService()
        {
            var servico = CreateInstance();
            var message = default(string);
            var esperado = "Serviço RecepcaoEventoEpec, versão 1.00, não disponível para a UF SP, no ambiente de Homologação para emissão tipo Normal, documento: NF-e!";

            try
            {
                var result = servico.RecepcaoEvento(1, new List<NFeClasses.Servicos.Evento.evento>(), ServicoNFe.RecepcaoEventoEpec);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            Assert.IsTrue(esperado == message);
        }

        [TestMethod]
        public void ServicosNFe_WhenNfeStatusServico_ReturnsLoteOk()
        {
            var servico = CreateInstance();

            var result = servico.NfeStatusServico();

            Assert.IsTrue("Serviço em Operação" == result.Retorno.xMotivo.ToString());
        }



    }
}
