using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Windows;
using System.Windows.Forms;
using BLL;
using Model;
using NFe;
using NFe.Classes;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Cobranca;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Observacoes;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Informacoes.Total;
using NFe.Classes.Informacoes.Transporte;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.Assinatura;
using NFe.Utils.Email;
using NFe.Utils.InformacoesSuplementares;
using NFe.Utils.NFe;
using NFe.Utils.Tributacao.Estadual;
//using RichTextBox = System.Windows.Controls.RichTextBox;
//using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
//using WebBrowser = System.Windows.Controls.WebBrowser;

namespace NFeMCInt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CarregarConfiguracao();
            //DataContext = _configuracoes;
        }
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private const string TituloErro = "Erro";
        private ConfiguracaoApp _configuracoes;
        private NFe.Classes.NFe _nfe;
        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private void Form1_Load(object sender, EventArgs e)
        {
            new SessionBLL().Connect(new SessionModel() { Server = "10.0.0.199", Database = "nova", User="root", Password = "beleza" });

            ClienteBLL cBll = new ClienteBLL();
            ClientesModel c = cBll.FrameworkGetOneModel(1000);
        }
        private void SalvarConfiguracao()
        {
            try
            {
                _configuracoes.SalvarParaAqruivo(_path + ArquivoConfiguracao);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    MessageBox.Show(string.Format("{0} \n\nDetalhes: {1}", ex.Message, ex.InnerException), "Erro",
                        MessageBoxButtons.OK);
            }
        }
        private void CarregarConfiguracao()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                _configuracoes = !File.Exists(path + ArquivoConfiguracao)
                    ? new ConfiguracaoApp()
                    : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoApp>(path + ArquivoConfiguracao);
                if (_configuracoes.CfgServico.TimeOut == 0)
                    _configuracoes.CfgServico.TimeOut = 3000; //mínimo
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region Cria e Envia NFe

                var numero = textBox1.Text;

                var lote = "6487";

                _nfe = GetNf(Convert.ToInt32(numero), _configuracoes.CfgServico.ModeloDocumento,
                    _configuracoes.CfgServico.VersaoNFeAutorizacao);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                //A URL do QR-Code deve ser gerada em um objeto nfe já assinado, pois na URL vai o DigestValue que é gerado por ocasião da assinatura
                _nfe.infNFeSupl = new infNFeSupl() { qrCode = _nfe.infNFeSupl.ObterUrlQrCode(_nfe, _configuracoes.ConfiguracaoCsc.CIdToken, _configuracoes.ConfiguracaoCsc.Csc) }; //Define a URL do QR-Code.
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<NFe.Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);
                //Para consumir o serviço de forma síncrona, use a linha abaixo:
                //var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Sincrono, new List<Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);

                TrataRetorno(retornoEnvio);

                #endregion
            }
            catch (Exception ex)
            {
                if (ex is SoapException | ex is InvalidOperationException | ex is WebException)
                {
                    //Faça o tratamento de contingência OffLine aqui. Em produção, acredito que tratar apenas as exceções SoapException e WebException sejam suficientes
                    //Ver https://msdn.microsoft.com/pt-br/library/system.web.services.protocols.soaphttpclientprotocol.invoke(v=vs.110).aspx
                    throw;
                }
                //if (!string.IsNullOrEmpty(ex.Message))
                //    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }


        #region Criar NFe

        protected virtual NFe.Classes.NFe GetNf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var nf = new NFe.Classes.NFe { infNFe = GetInf(numero, modelo, versao) };
            return nf;
        }

        protected virtual infNFe GetInf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var infNFe = new infNFe
            {
                versao = versao.VersaoServicoParaString(),
                ide = GetIdentificacao(numero, modelo, versao),
                emit = GetEmitente(),
                dest = GetDestinatario(versao, modelo),
                transp = GetTransporte()
            };

            for (var i = 0; i < 5; i++)
            {
                infNFe.det.Add(GetDetalhe(i, infNFe.emit.CRT, modelo));
            }

            infNFe.total = GetTotal(versao, infNFe.det);

            if (infNFe.ide.mod == ModeloDocumento.NFe & versao == VersaoServico.ve310)
                infNFe.cobr = GetCobranca(infNFe.total.ICMSTot); //V3.00 Somente
            if (infNFe.ide.mod == ModeloDocumento.NFCe)
                infNFe.pag = GetPagamento(infNFe.total.ICMSTot); //NFCe Somente  

            if (infNFe.ide.mod == ModeloDocumento.NFCe)
                infNFe.infAdic = new infAdic() { infCpl = "Troco: 10,00" }; //Susgestão para impressão do troco em NFCe

            return infNFe;
        }

        protected virtual ide GetIdentificacao(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var ide = new ide
            {
                cUF = Estado.SE,
                natOp = "VENDA",
                indPag = IndicadorPagamento.ipVista,
                mod = modelo,
                serie = 1,
                nNF = numero,
                tpNF = TipoNFe.tnSaida,
                cMunFG = 2802908,
                tpEmis = _configuracoes.CfgServico.tpEmis,
                tpImp = TipoImpressao.tiRetrato,
                cNF = "1234",
                tpAmb = _configuracoes.CfgServico.tpAmb,
                finNFe = FinalidadeNFe.fnNormal,
                verProc = "3.000"
            };

            if (ide.tpEmis != TipoEmissao.teNormal)
            {
                ide.dhCont =
                    DateTime.Now.ToString(versao == VersaoServico.ve310
                        ? "yyyy-MM-ddTHH:mm:sszzz"
                        : "yyyy-MM-ddTHH:mm:ss");
                ide.xJust = "TESTE DE CONTIGÊNCIA PARA NFe/NFCe";
            }

            #region V2.00

            if (versao == VersaoServico.ve200)
            {
                ide.dEmi = DateTime.Today.ToString("yyyy-MM-dd"); //Mude aqui para enviar a nfe vinculada ao EPEC, V2.00
                ide.dSaiEnt = DateTime.Today.ToString("yyyy-MM-dd");
            }

            #endregion

            #region V3.00

            if (versao != VersaoServico.ve310) return ide;
            ide.idDest = DestinoOperacao.doInterna;
            ide.dhEmi = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            //Mude aqui para enviar a nfe vinculada ao EPEC, V3.10
            if (ide.mod == ModeloDocumento.NFe)
                ide.dhSaiEnt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            else
                ide.tpImp = TipoImpressao.tiNFCe;
            ide.procEmi = ProcessoEmissao.peAplicativoContribuinte;
            ide.indFinal = ConsumidorFinal.cfConsumidorFinal; //NFCe: Tem que ser consumidor Final
            ide.indPres = PresencaComprador.pcPresencial; //NFCe: deve ser 1 ou 4

            #endregion

            return ide;
        }

        protected virtual emit GetEmitente()
        {
            var emit = _configuracoes.Emitente; // new emit
            //{
            //    //CPF = "80365027553",
            //    CNPJ = "32876302000114",
            //    xNome = "FIOLUX COMERCIAL LTDA",
            //    xFant = "FIOLUX COMERCIAL LTDA",
            //    IE = "270844821",
            //};
            emit.enderEmit = GetEnderecoEmitente();
            return emit;
        }

        protected virtual enderEmit GetEnderecoEmitente()
        {
            var enderEmit = _configuracoes.EnderecoEmitente; // new enderEmit
            //{
            //    xLgr = "RUA COMENDADOR FRANCISCO JOSE DA CUNHA",
            //    nro = "171",
            //    xCpl = "1 ANDAR",
            //    xBairro = "CENTRO",
            //    cMun = 2802908,
            //    xMun = "ITABAIANA",
            //    UF = "SE",
            //    CEP = 49500000,
            //    fone = 7934313234
            //};
            enderEmit.cPais = 1058;
            enderEmit.xPais = "BRASIL";
            return enderEmit;
        }

        protected virtual dest GetDestinatario(VersaoServico versao, ModeloDocumento modelo)
        {
            var dest = new dest(versao)
            {
                CNPJ = "99999999000191",
                //CPF = "99999999999",
            };
            if (modelo == ModeloDocumento.NFe)
            {
                dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"; //Obrigatório para NFe e opcional para NFCe
                dest.enderDest = GetEnderecoDestinatario(); //Obrigatório para NFe e opcional para NFCe
            }

            //if (versao == VersaoServico.ve200)
            //    dest.IE = "ISENTO";
            if (versao != VersaoServico.ve310) return dest;
            dest.indIEDest = indIEDest.NaoContribuinte; //NFCe: Tem que ser não contribuinte V3.00 Somente
            dest.email = "teste@gmail.com"; //V3.00 Somente
            return dest;
        }

        protected virtual enderDest GetEnderecoDestinatario()
        {
            var enderDest = new enderDest
            {
                xLgr = "RUA ...",
                nro = "S/N",
                xBairro = "CENTRO",
                cMun = 2802908,
                xMun = "ITABAIANA",
                UF = "SE",
                CEP = "49500000",
                cPais = 1058,
                xPais = "BRASIL"
            };
            return enderDest;
        }

        protected virtual det GetDetalhe(int i, CRT crt, ModeloDocumento modelo)
        {
            var det = new det
            {
                nItem = i + 1,
                prod = GetProduto(i + 1),
                imposto = new imposto
                {
                    vTotTrib = 0.17m,
                    ICMS = new ICMS
                    {
                        TipoICMS =
                            crt == CRT.SimplesNacional
                                ? InformarCSOSN(Csosnicms.Csosn102)
                                : InformarICMS(Csticms.Cst00, VersaoServico.ve310)
                    },
                    //Se você tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a classe NFe.Utils.Tributacao.Estadual.ICMSGeral para obter os dados básicos. Veja o método ObterIcmsBasico

                    //ICMSUFDest = new ICMSUFDest()
                    //{
                    //    pFCPUFDest = 0,
                    //    pICMSInter = 12,
                    //    pICMSInterPart = 0,
                    //    pICMSUFDest = 0,
                    //    vBCUFDest = 0,
                    //    vFCPUFDest = 0,
                    //    vICMSUFDest = 0,
                    //    vICMSUFRemet = 0
                    //},
                    COFINS =
                        new COFINS
                        {
                            TipoCOFINS = new COFINSOutr { CST = CSTCOFINS.cofins99, pCOFINS = 0, vBC = 0, vCOFINS = 0 }
                        },
                    PIS = new PIS { TipoPIS = new PISOutr { CST = CSTPIS.pis99, pPIS = 0, vBC = 0, vPIS = 0 } }
                }
            };

            if (modelo == ModeloDocumento.NFe) //NFCe não aceita grupo "IPI"
                det.imposto.IPI = new IPI()
                {
                    cEnq = 999,
                    TipoIPI = new IPITrib() { CST = CSTIPI.ipi00, pIPI = 5, vBC = 1, vIPI = 0.05m }
                };
            //det.impostoDevol = new impostoDevol() { IPI = new IPIDevolvido() { vIPIDevol = 10 }, pDevol = 100 };

            return det;
        }

        protected virtual prod GetProduto(int i)
        {
            var p = new prod
            {
                cProd = i.ToString().PadLeft(5, '0'),
                cEAN = "7770000000012",
                xProd = i == 1 ? "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : "ABRACADEIRA NYLON 6.6 BRANCA 91X92 " + i,
                NCM = "84159090",
                CFOP = 5102,
                uCom = "UNID",
                qCom = 1,
                vUnCom = 1,
                vProd = 1,
                vDesc = 0.10m,
                cEANTrib = "7770000000012",
                uTrib = "UNID",
                qTrib = 1,
                vUnTrib = 1,
                indTot = IndicadorTotal.ValorDoItemCompoeTotalNF,
                //NVE = {"AA0001", "AB0002", "AC0002"},
                //CEST = ?

                //ProdutoEspecifico = new arma
                //{
                //    tpArma = TipoArma.UsoPermitido,
                //    nSerie = "123456",
                //    nCano = "123456",
                //    descr = "TESTE DE ARMA"
                //}
            };
            return p;
        }

        protected virtual ICMSBasico InformarICMS(Csticms CST, VersaoServico versao)
        {
            var icms20 = new ICMS20
            {
                orig = OrigemMercadoria.OmNacional,
                CST = Csticms.Cst20,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1,
                pICMS = 17,
                vICMS = 0.17m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            if (versao == VersaoServico.ve310)
                icms20.vICMSDeson = 0.10m; //V3.00 ou maior Somente

            switch (CST)
            {
                case Csticms.Cst00:
                    return new ICMS00
                    {
                        CST = Csticms.Cst00,
                        modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                        orig = OrigemMercadoria.OmNacional,
                        pICMS = 17,
                        vBC = 1,
                        vICMS = 0.17m
                    };
                case Csticms.Cst20:
                    return icms20;
                    //Outros casos aqui
            }

            return new ICMS10();
        }

        protected virtual ICMSBasico ObterIcmsBasico(CRT crt)
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto ICMSGeral, como no exemplo abaixo.
            var icmsGeral = new ICMSGeral
            {
                orig = OrigemMercadoria.OmNacional,
                CST = Csticms.Cst20,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1,
                pICMS = 17,
                vICMS = 0.17m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            return icmsGeral.ObterICMSBasico(crt);
        }

        protected virtual ICMSBasico InformarCSOSN(Csosnicms CST)
        {
            switch (CST)
            {
                case Csosnicms.Csosn101:
                    return new ICMSSN101
                    {
                        CSOSN = Csosnicms.Csosn101,
                        orig = OrigemMercadoria.OmNacional
                    };
                case Csosnicms.Csosn102:
                    return new ICMSSN102
                    {
                        CSOSN = Csosnicms.Csosn102,
                        orig = OrigemMercadoria.OmNacional
                    };
                //Outros casos aqui
                default:
                    return new ICMSSN201();
            }
        }

        protected virtual total GetTotal(VersaoServico versao, List<det> produtos)
        {
            var icmsTot = new ICMSTot
            {
                vProd = produtos.Sum(p => p.prod.vProd),
                vNF = produtos.Sum(p => p.prod.vProd) - produtos.Sum(p => p.prod.vDesc ?? 0),
                vDesc = produtos.Sum(p => p.prod.vDesc ?? 0),
                vTotTrib = produtos.Sum(p => p.imposto.vTotTrib ?? 0),
            };
            if (versao == VersaoServico.ve310)
                icmsTot.vICMSDeson = 0;

            foreach (var produto in produtos)
            {
                if (produto.imposto.IPI != null && produto.imposto.IPI.TipoIPI.GetType() == typeof(IPITrib))
                    icmsTot.vIPI = icmsTot.vIPI + ((IPITrib)produto.imposto.IPI.TipoIPI).vIPI ?? 0;
                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS00))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS00)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS00)produto.imposto.ICMS.TipoICMS).vICMS;
                }
                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS20))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS20)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS20)produto.imposto.ICMS.TipoICMS).vICMS;
                }
                //Outros Ifs aqui, caso vá usar as classes ICMS00, ICMS10 para totalizar
            }

            var t = new total { ICMSTot = icmsTot };
            return t;
        }



        protected virtual transp GetTransporte()
        {
            //var volumes = new List<vol> {GetVolume(), GetVolume()};

            var t = new transp
            {
                modFrete = ModalidadeFrete.mfSemFrete //NFCe: Não pode ter frete
                //vol = volumes 
            };

            return t;
        }

        protected virtual vol GetVolume()
        {
            var v = new vol
            {
                esp = "teste de especia",
                lacres = new List<lacres> { new lacres { nLacre = "123456" } }
            };

            return v;
        }

        protected virtual cobr GetCobranca(ICMSTot icmsTot)
        {
            var valorParcela = Valor.Arredondar(icmsTot.vProd / 2, 2);
            var c = new cobr
            {
                fat = new fat { nFat = "12345678910", vLiq = icmsTot.vProd },
                dup = new List<dup>
                {
                    new dup {nDup = "12345678", vDup = valorParcela},
                    new dup {nDup = "987654321", vDup = icmsTot.vProd - valorParcela}
                }
            };

            return c;
        }

        protected virtual List<pag> GetPagamento(ICMSTot icmsTot)
        {
            var valorPagto = Valor.Arredondar(icmsTot.vProd / 2, 2);
            var p = new List<pag>
            {
                new pag {tPag = FormaPagamento.fpDinheiro, vPag = valorPagto},
                new pag {tPag = FormaPagamento.fpCheque, vPag = icmsTot.vProd - valorPagto}
            };
            return p;
        }

        #endregion

        private void TrataRetorno(RetornoBasico retornoBasico)
        {
            EnvioStr(richTextBox1, retornoBasico.EnvioStr);
            RetornoStr(richTextBox1, retornoBasico.RetornoStr);
            RetornoXml(webBrowser1, retornoBasico.RetornoStr);
            RetornoCompletoStr(richTextBox1, retornoBasico.RetornoCompletoStr);
            RetornoDados(retornoBasico.Retorno, richTextBox1);
        }
        #region Tratamento de retornos dos Serviços

        internal void RetornoDados<T>(T objeto, RichTextBox richTextBox) where T : class
        {
            richTextBox.Clear();

            foreach (var atributos in objeto.LerPropriedades())
            {
                richTextBox.AppendText(atributos.Key + " = " + atributos.Value + "\r");
            }
        }

        internal void RetornoCompletoStr(RichTextBox richTextBox, string retornoCompletoStr)
        {
            richTextBox.Clear();
            richTextBox.AppendText(retornoCompletoStr);
        }

        internal void EnvioStr(RichTextBox richTextBox, string envioStr)
        {
            richTextBox.Clear();
            richTextBox.AppendText(envioStr);
        }

        internal void RetornoXml(WebBrowser webBrowser, string retornoXmlString)
        {
            var stw = new StreamWriter(_path + @"\tmp.xml");
            stw.WriteLine(retornoXmlString);
            stw.Close();
            webBrowser.Navigate(_path + @"\tmp.xml");
        }

        internal void RetornoStr(RichTextBox richTextBox, string retornoXmlString)
        {
            richTextBox.Clear();
            richTextBox.AppendText(retornoXmlString);
        }

        #endregion
    }
}
