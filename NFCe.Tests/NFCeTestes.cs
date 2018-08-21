using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Utils;
using NFe.Utils.Assinatura;
using NFe.Utils.InformacoesSuplementares;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;
using NFeClasses = global::NFe.Classes;

namespace NFCe.Testes
{
    [TestClass]
    public class NFCeTestes
    {
        public ConfiguracaoServico _conf { get; set; }
        public ServicosNFe CreateInstance4()
        {
            _conf = new ConfiguracaoServico();
            //var cert = new ConfiguracaoCertificado
            //{
            //    TipoCertificado = TipoCertificado.A1Arquivo,
            //    Arquivo = @"D:\Works\CERTIFICADO MORIMO A1.pfx",
            //    Senha = "morimo1458"
            //};
            var cert = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1Arquivo,
                Arquivo = @"D:\works\nfe\certificates\CERTIFICADO MORIMO A1.pfx",
                Senha = "morimo1458"
            };
            _conf.Certificado = cert;
            _conf.ModeloDocumento = DFe.Classes.Flags.ModeloDocumento.NFCe; //  Condition

            _conf.tpAmb = NFeClasses.Informacoes.Identificacao.Tipos.TipoAmbiente.taProducao;
            _conf.cUF = DFe.Classes.Entidades.Estado.SP;
            _conf.tpEmis = NFeClasses.Informacoes.Identificacao.Tipos.TipoEmissao.teNormal;
            _conf.TimeOut = 120000;
            _conf.DiretorioSalvarXml = @"D:\works\nfce";
            _conf.SalvarXmlServicos = true;
            _conf.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Tls12;
            _conf.DiretorioSchemas = @"C:\Works\nfe\nfe-products-api\schemas";
            _conf.VersaoNFeAutorizacao = VersaoServico.ve400;
            _conf.VersaoNfeDownloadNF = VersaoServico.ve400;
            _conf.VersaoNfeStatusServico = VersaoServico.ve400;
            _conf.VersaoNFeRetAutorizacao = VersaoServico.ve400;

            return new ServicosNFe(_conf);
        }
        public NFeClasses.nfeProc CreateObject()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NFe.Classes.nfeProc));
            StreamReader reader = new StreamReader(@"C:\Users\staff\Downloads\35180708765239000164550050000053271382938770-nfe.xml");
            var nfe = (NFe.Classes.nfeProc)serializer.Deserialize(reader);
            reader.Close();

            return nfe;
        }

        public void SetValuesForMoriMo(NFe.Classes.NFe nfe)
        {
            nfe.infNFe.emit.CNPJ = "28456297000113";
            //nfe.infNFe.emit.xNome = "MORI MO SUSHI MOEMA LTDA EPP";
            //nfe.infNFe.emit.xFant = "MORI MO SUSHI";
            nfe.infNFe.emit.IE = "118425254110";
            //nfe.infNFe.emit.IM = "ISENTO";
        }

        [TestMethod]
        public void ServicosNFe_WhenNfeNFeAutorizacao4_ReturnsxMotivoSuccess()
        {
            var servico = CreateInstance4();
            var nfeProc = CreateObject();

            var nfe = nfeProc.NFe;

            var list = new List<NFe.Classes.NFe>();
            list.Add(nfeProc.NFe);

            nfe.infNFe.ide.mod = DFe.Classes.Flags.ModeloDocumento.NFCe;
            nfe.infNFe.ide.tpImp = NFeClasses.Informacoes.Identificacao.Tipos.TipoImpressao.tiNFCe;
            nfe.infNFe.ide.indPres = NFeClasses.Informacoes.Identificacao.Tipos.PresencaComprador.pcPresencial;
            nfe.infNFe.dest = null;
            nfe.infNFe.det[0].prod.xProd = "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

            nfe.Signature = null;
            SetValuesForMoriMo(nfe);

            nfe.infNFe.ide.dhEmi = DateTimeOffset.UtcNow;
            nfe.infNFe.ide.dhSaiEnt = null;

            CriarChaveDeAcesso(nfe);
            var a = new X509Certificate2(@"D:\works\nfe\certificates\CERTIFICADO MORIMO A1.pfx", "morimo1458", X509KeyStorageFlags.Exportable);
            //var a = new X509Certificate2(@"D:\Works\ProductInvoices\A1_NFE_08765239000164_cer2017.pfx", "cer2017");

            var signature = Assinador.ObterAssinaturac<NFe.Classes.NFe>(nfe, nfe.infNFe.Id, a);

            nfe.Signature = signature;

            nfe.infNFeSupl = new NFeClasses.infNFeSupl
            {
                urlChave = nfe.infNFe.Id,
                qrCode = ExtinfNFeSupl.ObterUrlQrCode(new NFeClasses.infNFeSupl(), nfe, VersaoQrCode.QrCodeVersao1, "000001", "442e0b0a-5297-458e-9bc4-6711ffac869c")
            };


            var issuedNfe = Assinador.SerializeToString(nfe);
            File.WriteAllText($@"D:\works\nfce\test123.xml", issuedNfe);

            var procNfe = new NFe.Classes.nfeProc
            {
                NFe = nfe
            };

            var result = servico.NFeAutorizacao(1, IndicadorSincronizacao.Sincrono, list);

            Assert.IsTrue("Lote recebido com sucesso" == result.Retorno.xMotivo.ToString());
        }

        public static void CriarChaveDeAcesso(NFeClasses.NFe nfe)
        {
            var tamanhocNf = 9;
            var versao = (decimal.Parse(nfe.infNFe.versao, CultureInfo.InvariantCulture));
            if (versao >= 2) tamanhocNf = 8;
            nfe.infNFe.ide.cNF = Convert.ToInt32(nfe.infNFe.ide.cNF).ToString().PadLeft(tamanhocNf, '0');

            var modeloDocumentoFiscal = nfe.infNFe.ide.mod;
            var tipoEmissao = (int)nfe.infNFe.ide.tpEmis;
            var codigoNumerico = int.Parse(nfe.infNFe.ide.cNF);
            var estado = nfe.infNFe.ide.cUF;
            var dataEHoraEmissao = nfe.infNFe.ide.dhEmi;
            var cnpj = nfe.infNFe.emit.CNPJ;
            var numeroDocumento = nfe.infNFe.ide.nNF;
            var serie = nfe.infNFe.ide.serie;

            var dadosChave = ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal, serie, numeroDocumento, tipoEmissao, codigoNumerico);

            nfe.infNFe.Id = "NFe" + dadosChave.Chave;
            nfe.infNFe.ide.cDV = Convert.ToInt16(dadosChave.DigitoVerificador);
        }

        private static DadosChaveFiscal ObterChave(Estado ufEmitente, DateTimeOffset dataEmissao, string cnpjEmitente, ModeloDocumento modelo, int serie, long numero, int tipoEmissao, int cNf)
        {
            var chave = new StringBuilder();

            chave.Append(((int)ufEmitente).ToString("D2"))
                .Append(dataEmissao.DateTime.Date.ToString("yyMM"))
                .Append(cnpjEmitente)
                .Append(((int)modelo).ToString("D2"))
                .Append(serie.ToString("D3"))
                .Append(numero.ToString("D9"))
                .Append(tipoEmissao.ToString())
                .Append(cNf.ToString("D8"));

            var digitoVerificador = ObterDigitoVerificador(chave.ToString());

            chave.Append(digitoVerificador);

            return new DadosChaveFiscal(chave.ToString(), byte.Parse(digitoVerificador));
        }

        private static string ObterDigitoVerificador(string chave)
        {
            var soma = 0; // Vai guardar a Soma
            var mod = -1; // Vai guardar o Resto da divisão
            var dv = -1; // Vai guardar o DigitoVerificador
            var peso = 2; // vai guardar o peso de multiplicação

            //percorrendo cada caractere da chave da direita para esquerda para fazer os cálculos com o peso
            for (var i = chave.Length - 1; i != -1; i--)
            {
                var ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * peso;
                //sempre que for 9 voltamos o peso a 2
                if (peso < 9)
                    peso += 1;
                else
                    peso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv.ToString();
        }

        //public void Create_Mdfe_Example()
        //{
        //    var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
        //    var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, null/*Logomarca em byte[]*/), "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX");
        //    danfe.Visualizar();
        //}
    }
}
