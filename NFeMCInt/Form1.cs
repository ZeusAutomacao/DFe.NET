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
using System.Data;
using NFe.Danfe.Fast.NFe;
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
        private DataSet _nfa;

        private void Form1_Load(object sender, EventArgs e)
        {
            new SessionBLL().Connect(new SessionModel() { Server = "10.0.0.199", Database = "nova", User = "root", Password = "beleza" });

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

                _nfa = new NFeBLL().GetNfaDataTable(numero);

                _nfe = GetNf(Convert.ToInt32(numero), _configuracoes.CfgServico.ModeloDocumento,
                    _configuracoes.CfgServico.VersaoNFeAutorizacao);
                //_nfe.SalvarArquivoXml(_configuracoes.CfgServico.DiretorioSalvarXml + "\\Teste.xml");
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                //A URL do QR-Code deve ser gerada em um objeto nfe já assinado, pois na URL vai o DigestValue que é gerado por ocasião da assinatura
                //_nfe.infNFeSupl = new infNFeSupl() { qrCode = _nfe.infNFeSupl.ObterUrlQrCode(_nfe, _configuracoes.ConfiguracaoCsc.CIdToken, _configuracoes.ConfiguracaoCsc.Csc) }; //Define a URL do QR-Code.
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);


                richTextBox1.Clear();

                //Assincrono
                //var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<NFe.Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);
                //Para consumir o serviço de forma síncrona, use a linha abaixo:
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Sincrono, new List<NFe.Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);

                TrataRetorno(retornoEnvio);

                System.Threading.Thread.Sleep(3000);

                var retornoRecibo = servicoNFe.NFeRetAutorizacao(retornoEnvio.Retorno.infRec.nRec);

                TrataRetorno(retornoRecibo);

                richTextBox1.Text = retornoRecibo.RetornoCompletoStr;

                textBox4.Text = retornoRecibo.Retorno.protNFe[0].infProt.cStat.ToString();
                textBox5.Text = retornoRecibo.Retorno.protNFe[0].infProt.xMotivo;

                if (retornoRecibo.Retorno.protNFe[0].infProt.cStat == 100)
                {

                    var nfeproc = new nfeProc
                    {
                        NFe = _nfe,
                        protNFe = retornoRecibo.Retorno.protNFe[0],
                        versao = retornoRecibo.Retorno.versao
                    };
                    if (nfeproc.protNFe != null)
                    {
                        var novoArquivo = Path.GetDirectoryName(_configuracoes.CfgServico.DiretorioSalvarXml) + @"\" + nfeproc.protNFe.infProt.chNFe +
                                          "-procNfe.xml";
                        FuncoesXml.ClasseParaArquivoXml(nfeproc, novoArquivo);
                    }


                    Imprimir(nfeproc.ObterXmlString());
                    //var retornoDownload = servicoNFe.NfeDownloadNf("64877996000109", 
                    //    new List<string>() { _nfa.Tables["nfe_cab"].Rows[0]["chnfe"].ToString() });

                    ////Se desejar consultar mais de uma chave, use o serviço como indicado abaixo. É permitido consultar até 10 nfes de uma vez.
                    ////Leia atentamente as informações do consumo deste serviço constantes no manual
                    ////var retornoDownload = servicoNFe.NfeDownloadNf(cnpj, new List<string>() { "28150707703290000189550010000009441000029953", "28150707703290000189550010000009431000029948" });

                    //TrataRetorno(retornoDownload);
                }



                #endregion
            }
            catch (Exception ex)
            {

                richTextBox1.Text = ex.Message;
                if (ex.InnerException != null)
                    richTextBox1.Text += ex.InnerException.Message;

                if (ex is SoapException | ex is InvalidOperationException | ex is WebException)
                {
                    //Faça o tratamento de contingência OffLine aqui. Em produção, acredito que tratar apenas as exceções SoapException e WebException sejam suficientes
                    //Ver https://msdn.microsoft.com/pt-br/library/system.web.services.protocols.soaphttpclientprotocol.invoke(v=vs.110).aspx
                    //throw;
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

            for (int i = 0; i < _nfa.Tables["nfe_item"].Rows.Count; i++)
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
                cUF = Estado.SP,
                natOp = _nfa.Tables["nfe_cab"].Rows[0]["ide_natop"].ToString(),
                indPag = IndicadorPagamento.ipVista,
                mod = modelo,
                serie = 1,
                nNF = numero,
                tpNF = Convert.ToInt32(_nfa.Tables["nfe_cab"].Rows[0]["ide_tpnf"].ToString()) == 1 ? TipoNFe.tnSaida : TipoNFe.tnEntrada,
                cMunFG = Convert.ToInt64(_nfa.Tables["nfe_emitente"].Rows[0]["cmun"].ToString()),
                tpEmis = _configuracoes.CfgServico.tpEmis,
                tpImp = TipoImpressao.tiRetrato,
                cNF = _nfa.Tables["nfe_cab"].Rows[0]["ide_cnf"].ToString(),
                tpAmb = _configuracoes.CfgServico.tpAmb,
                finNFe = FinalidadeNFe.fnNormal,
                verProc = "3.000",
                idDest = (DestinoOperacao)Convert.ToInt32(_nfa.Tables["nfe_cab"].Rows[0]["ide_iddest"].ToString())
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
            ide.dhEmi = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            //Mude aqui para enviar a nfe vinculada ao EPEC, V3.10
            if (ide.mod == ModeloDocumento.NFe)
                ide.dhSaiEnt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            else
                ide.tpImp = TipoImpressao.tiNFCe;
            ide.procEmi = ProcessoEmissao.peAplicativoContribuinte;
            ide.indFinal = ConsumidorFinal.cfNao; //NFCe: Tem que ser consumidor Final
            ide.indPres = PresencaComprador.pcNao; //NFCe: deve ser 1 ou 4


            #endregion

            return ide;
        }

        protected virtual emit GetEmitente()
        {
            var emit = new emit//_configuracoes.Emitente; // new emit
            {
                //CPF = "80365027553",
                CNPJ = _nfa.Tables["nfe_emitente"].Rows[0]["cnpj"].ToString(),
                xNome = _nfa.Tables["nfe_emitente"].Rows[0]["nome"].ToString(),
                xFant = _nfa.Tables["nfe_emitente"].Rows[0]["fant"].ToString(),
                IE = _nfa.Tables["nfe_emitente"].Rows[0]["ie"].ToString(),
                IM = _nfa.Tables["nfe_emitente"].Rows[0]["im"].ToString(),
                CRT = CRT.RegimeNormal,
                CNAE = _nfa.Tables["nfe_emitente"].Rows[0]["cnae"].ToString()
            };
            emit.enderEmit = GetEnderecoEmitente();
            return emit;
        }

        protected virtual enderEmit GetEnderecoEmitente()
        {
            var enderEmit = new enderEmit //_configuracoes.EnderecoEmitente; // new enderEmit
            {
                xLgr = _nfa.Tables["nfe_emitente"].Rows[0]["ender"].ToString(),
                nro = _nfa.Tables["nfe_emitente"].Rows[0]["nro"].ToString(),
                xCpl = _nfa.Tables["nfe_emitente"].Rows[0]["cpl"].ToString(),
                xBairro = _nfa.Tables["nfe_emitente"].Rows[0]["bairro"].ToString(),
                cMun = Convert.ToInt64(_nfa.Tables["nfe_emitente"].Rows[0]["cmun"].ToString()),
                xMun = _nfa.Tables["nfe_emitente"].Rows[0]["cmun"].ToString(),
                UF = _nfa.Tables["nfe_emitente"].Rows[0]["uf"].ToString(),
                CEP = _nfa.Tables["nfe_emitente"].Rows[0]["cep"].ToString(),
                fone = Convert.ToInt64(_nfa.Tables["nfe_emitente"].Rows[0]["fone"].ToString())
            };
            enderEmit.cPais = Convert.ToInt32(_nfa.Tables["nfe_emitente"].Rows[0]["cpais"].ToString());
            enderEmit.xPais = _nfa.Tables["nfe_emitente"].Rows[0]["pais"].ToString();
            return enderEmit;
        }

        protected virtual dest GetDestinatario(VersaoServico versao, ModeloDocumento modelo)
        {
            var dest = new dest(versao)
            {
                CNPJ = _nfa.Tables["nfe_cab"].Rows[0]["dest_cnpj"].ToString(),
                CPF = _nfa.Tables["nfe_cab"].Rows[0]["dest_cpf"].ToString(),
            };
            if (modelo == ModeloDocumento.NFe)
            {
                dest.xNome = _nfa.Tables["nfe_cab"].Rows[0]["dest_xnome"].ToString(); //Obrigatório para NFe e opcional para NFCe
                dest.enderDest = GetEnderecoDestinatario(); //Obrigatório para NFe e opcional para NFCe
                dest.IE = _nfa.Tables["nfe_cab"].Rows[0]["dest_ie"].ToString();

                //Verificando se está no ambiente de Homologação
                if (_configuracoes.CfgServico.tpAmb == TipoAmbiente.taHomologacao)
                {
                    dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                    //dest.IE = "";
                    //dest.CNPJ = "99999999000191";
                }

            }

            //if (versao == VersaoServico.ve200)
            //    dest.IE = "ISENTO";
            if (versao != VersaoServico.ve310) return dest;
            dest.indIEDest = (indIEDest)Convert.ToInt32(_nfa.Tables["nfe_cab"].Rows[0]["indiedest"].ToString()); //NFCe: Tem que ser não contribuinte V3.00 Somente
            dest.email = _nfa.Tables["nfe_cab"].Rows[0]["dest_email"].ToString(); //V3.00 Somente
            return dest;
        }

        protected virtual enderDest GetEnderecoDestinatario()
        {

            var enderDest = new enderDest
            {
                xLgr = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_xlgr"].ToString(),
                nro = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_nro"].ToString(),
                xCpl = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_xcpl"].ToString(),
                xBairro = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_xbairro"].ToString(),
                cMun = Convert.ToInt64(_nfa.Tables["nfe_cab"].Rows[0]["enderdest_cmun"].ToString()),
                xMun = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_xmun"].ToString(),
                UF = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_uf"].ToString(),
                CEP = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_cep"].ToString(),
                cPais = Convert.ToInt32(_nfa.Tables["nfe_cab"].Rows[0]["enderdest_cpais"].ToString()),
                xPais = _nfa.Tables["nfe_cab"].Rows[0]["enderdest_xpais"].ToString()
            };

            if (enderDest.xPais == "" && enderDest.cPais == 1058)
                enderDest.xPais = "Brasil";

            return enderDest;
        }

        protected virtual det GetDetalhe(int i, CRT crt, ModeloDocumento modelo)
        {
            ICMSGeral ic = new NFe.Utils.Tributacao.Estadual.ICMSGeral();
            var detalhe = new det();

            detalhe.nItem = i + 1;
            detalhe.prod = new prod()
            {
                cProd = _nfa.Tables["nfe_item"].Rows[i]["prod_cprod"].ToString(),
                cEAN = _nfa.Tables["nfe_item"].Rows[i]["prod_cean"].ToString(),
                xProd = _nfa.Tables["nfe_item"].Rows[i]["prod_xprod"].ToString(),
                NCM = _nfa.Tables["nfe_item"].Rows[i]["prod_ncm"].ToString(),
                //EXTIPI = _nfa.Tables["nfe_item"].Rows[i]["prod_extipi"].ToString(),
                CFOP = Convert.ToInt32(_nfa.Tables["nfe_item"].Rows[i]["prod_cfop"].ToString()),
                uCom = _nfa.Tables["nfe_item"].Rows[i]["prod_ucom"].ToString(),
                qCom = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_qcom"].ToString()),
                vUnCom = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vuncom"].ToString()),
                vProd = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vprod"].ToString()),
                cEANTrib = _nfa.Tables["nfe_item"].Rows[i]["prod_ceantrib"].ToString(),
                uTrib = _nfa.Tables["nfe_item"].Rows[i]["prod_utrib"].ToString(),
                qTrib = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_qtrib"].ToString()),
                vUnTrib = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vuntrib"].ToString()),
                vFrete = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vfrete"].ToString()),
                vSeg = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vseg"].ToString()),
                vDesc = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_vdesc"].ToString()),
                vOutro = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["prod_voutro"].ToString()),
                indTot = IndicadorTotal.ValorDoItemCompoeTotalNF,
                //CEST = _nfa.Tables["nfe_item"].Rows[i]["prod_cest"].ToString(),
                //xPed = _nfa.Tables["nfe_item"].Rows[i]["xped"].ToString(),
                //nItemPed = Convert.ToInt32(_nfa.Tables["nfe_item"].Rows[i]["nitemped"].ToString()),

            };

            string CSTstring = _nfa.Tables["nfe_item"].Rows[i]["icms_cst"].ToString();
            string modBCString = _nfa.Tables["nfe_item"].Rows[i]["icms_modbc"].ToString();
            string modBCSSTtring = _nfa.Tables["nfe_item"].Rows[i]["icms_modbcst"].ToString();
            string origString = _nfa.Tables["nfe_item"].Rows[i]["icms_orig"].ToString();

            DeterminacaoBaseIcms detBC = (DeterminacaoBaseIcms)int.Parse(modBCString);
            DeterminacaoBaseIcmsSt detBCST = (DeterminacaoBaseIcmsSt)int.Parse(modBCSSTtring);
            OrigemMercadoria origem = (OrigemMercadoria)int.Parse(origString);
            Csticms cs = new Csticms();

            switch (CSTstring)
            {
                case "00":
                    cs = Csticms.Cst00;
                    break;
                case "10":
                    cs = Csticms.Cst10;
                    break;
                case "40":
                    cs = Csticms.Cst40;
                    break;
                case "41":
                    cs = Csticms.Cst41;
                    break;
                case "50":
                    cs = Csticms.Cst50;
                    break;
                case "90":
                    cs = Csticms.Cst90;
                    break;
                default:
                    break;
            }

            ICMSBasico icms = new ICMS10();

            switch (cs)
            {
                case Csticms.Cst00:
                    icms = new ICMS00()
                    {
                        CST = cs,
                        vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vbc"].ToString()),
                        pICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_picms"].ToString()),
                        vICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vicms"].ToString()),
                        modBC = detBC,
                        orig = origem
                    };

                    break;
                case Csticms.Cst10:
                    icms = new ICMS10()
                    {
                        CST = cs,
                        vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vbc"].ToString()),
                        pICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_picms"].ToString()),
                        vICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vicms"].ToString()),
                        pMVAST = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_pmvast"].ToString()),
                        pRedBCST = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_predbcst"].ToString()),
                        vBCST = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vbcst"].ToString()),
                        pICMSST = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_picmsst"].ToString()),
                        vICMSST = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vicmsst"].ToString()),
                        modBC = detBC,
                        modBCST = detBCST,
                        orig = origem

                    };
                    break;
                case Csticms.CstPart10:
                    break;
                case Csticms.Cst20:
                    icms = new ICMS20();
                    break;
                case Csticms.Cst30:
                    break;
                case Csticms.Cst40:
                    icms = new ICMS40()
                    {
                        CST = cs,
                        orig = origem,
                        motDesICMS = MotivoDesoneracaoIcms.MdiOutros,
                        vICMSDeson = 0
                    };
                    break;
                case Csticms.Cst41:
                    icms = new ICMS40()
                    {
                        CST = cs,
                        orig = origem,

                    };
                    break;
                case Csticms.CstRep41:
                    break;
                case Csticms.Cst50:
                    icms = new ICMS51()
                    {
                        CST = cs,
                        vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vbc"].ToString()),
                        pICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_picms"].ToString()),
                        vICMS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["icms_vicms"].ToString()),
                        modBC = detBC,
                        orig = origem,

                    };
                    break;
                case Csticms.Cst51:
                    break;
                case Csticms.Cst60:
                    break;
                case Csticms.Cst70:
                    break;
                case Csticms.Cst90:
                    break;
                case Csticms.CstPart90:
                    break;
                default:
                    break;
            }
            CSTPIS cstpis = new CSTPIS();
            switch (_nfa.Tables["nfe_item"].Rows[i]["cofins_cst"].ToString())
            {
                case "01":
                    cstpis = CSTPIS.pis01;
                    break;
                case "49":
                    cstpis = CSTPIS.pis49;
                    break;
                case "50":
                    cstpis = CSTPIS.pis50;
                    break;
                default:
                    cstpis = (CSTPIS)Convert.ToInt32(_nfa.Tables["nfe_item"].Rows[i]["pis_cst"].ToString());
                    break;
            }
            PIS pis = new PIS()
            {
                TipoPIS = new PISAliq()
                {
                    CST = cstpis,
                    pPIS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["pis_ppis"].ToString()),
                    vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["pis_vbc"].ToString()),
                    vPIS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["pis_vpis"].ToString()),
                }
            };
            CSTCOFINS cstcofins = new CSTCOFINS();
            switch (_nfa.Tables["nfe_item"].Rows[i]["cofins_cst"].ToString())
            {
                case "01":
                    cstcofins = CSTCOFINS.cofins01;
                    break;
                case "49":
                    cstcofins = CSTCOFINS.cofins49;
                    break;
                case "50":
                    cstcofins = CSTCOFINS.cofins50;
                    break;
                default:
                    cstcofins = (CSTCOFINS)Convert.ToInt32(_nfa.Tables["nfe_item"].Rows[i]["cofins_cst"].ToString());
                    break;
            }
            COFINS cofins = new COFINS()
            {
                TipoCOFINS = new COFINSAliq()
                {
                    CST = cstcofins,
                    pCOFINS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["cofins_pcofins"].ToString()),
                    vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["cofins_vbc"].ToString()),
                    vCOFINS = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["cofins_vcofins"].ToString()),
                }
            };

            IPI ipi = new IPI()
            {
                cEnq = 150,
                //qSelo = 0,
                //cSelo = "",
                TipoIPI = new IPITrib()
                {
                    CST = (CSTIPI.ipi99),// Convert.ToInt32(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_cst"].ToString()),
                    pIPI = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_pipi"].ToString()),
                    vBC = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_vbc"].ToString()),
                    vIPI = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_vipi"].ToString()),
                    //qUnid = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_qunid"].ToString()),
                    //vUnid = Convert.ToDecimal(_nfa.Tables["nfe_item"].Rows[i]["ipitrib_vunid"].ToString())
                }
            };
            imposto imp = new imposto();

            //detalhe.impostoDevol = new NFe.Classes.Informacoes.Detalhe.impostoDevol() { };
            //detalhe.infAdProd = "";
            imp.ICMS = new ICMS() { TipoICMS = icms };
            imp.IPI = ipi;
            imp.PIS = pis;
            imp.COFINS = cofins;
            detalhe.imposto = imp;

            return detalhe;
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
            ICMSTot total = new ICMSTot()
            {
                vBC = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vbc"].ToString()),
                vICMS = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vicms"].ToString()),
                vBCST = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vbcst"].ToString()),
                vST = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vbc"].ToString()),
                vProd = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vprod"].ToString()),
                vFrete = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vfrete"].ToString()),
                vSeg = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vseg"].ToString()),
                vDesc = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vdesc"].ToString()),
                vII = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vii"].ToString()),
                vIPI = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vipi"].ToString()),
                vPIS = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vpis"].ToString()),
                vCOFINS = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vcofins"].ToString()),
                vOutro = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_voutro"].ToString()),
                vNF = Convert.ToDecimal(_nfa.Tables["nfe_cab"].Rows[0]["icmstot_vnf"].ToString()),


            };




            //var icmsTot = new ICMSTot
            //{
            //    vProd = produtos.Sum(p => p.prod.vProd),
            //    vNF = produtos.Sum(p => p.prod.vProd) - produtos.Sum(p => p.prod.vDesc ?? 0),
            //    vDesc = produtos.Sum(p => p.prod.vDesc ?? 0),
            //    vTotTrib = produtos.Sum(p => p.imposto.vTotTrib ?? 0),
            //};
            if (versao == VersaoServico.ve310)
                total.vICMSDeson = 0;

            //foreach (var produto in produtos)
            //{
            //    if (produto.imposto.IPI != null && produto.imposto.IPI.TipoIPI.GetType() == typeof(IPITrib))
            //        icmsTot.vIPI = icmsTot.vIPI + ((IPITrib)produto.imposto.IPI.TipoIPI).vIPI ?? 0;
            //    if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS00))
            //    {
            //        icmsTot.vBC = icmsTot.vBC + ((ICMS00)produto.imposto.ICMS.TipoICMS).vBC;
            //        icmsTot.vICMS = icmsTot.vICMS + ((ICMS00)produto.imposto.ICMS.TipoICMS).vICMS;
            //    }
            //    if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS20))
            //    {
            //        icmsTot.vBC = icmsTot.vBC + ((ICMS20)produto.imposto.ICMS.TipoICMS).vBC;
            //        icmsTot.vICMS = icmsTot.vICMS + ((ICMS20)produto.imposto.ICMS.TipoICMS).vICMS;
            //    }
            //    if (produto.imposto.COFINS.GetType() == typeof(COFINS))
            //    {
            //        icmsTot.vCOFINS = ((COFINSBasico)produto.imposto.COFINS.TipoCOFINS).;
            //        icmsTot.vICMS = icmsTot.vICMS + ((ICMS20)produto.imposto.ICMS.TipoICMS).vICMS;

            //        //Outros Ifs aqui, caso vá usar as classes ICMS00, ICMS10 para totalizar
            //    }


            //}
            var t = new total { ICMSTot = total };
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
            richTextBox1.Clear();
            //webBrowser1.


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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                #region Cancelar NFe

                var idlote = "64877";
                _nfa = new NFeBLL().GetNfaDataTable(textBox2.Text);

                var sequenciaEvento = 1;// _nfa.Tables["nfe_cab"].Rows[0]["cab_serial"].ToString();

                var protocolo = _nfa.Tables["nfe_cab"].Rows[0]["nprot"].ToString();

                var chave = _nfa.Tables["nfe_cab"].Rows[0]["chnfe"].ToString();

                var justificativa = "ERRO INTERNO DE SISTEMA....";

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = string.IsNullOrEmpty(_configuracoes.Emitente.CNPJ)
                    ? _configuracoes.Emitente.CPF
                    : _configuracoes.Emitente.CNPJ;
                var retornoCancelamento = servicoNFe.RecepcaoEventoCancelamento(Convert.ToInt32(idlote),
                    Convert.ToInt32(sequenciaEvento), protocolo, chave, justificativa, cpfcnpj);
                TrataRetorno(retornoCancelamento);

                richTextBox1.Text = retornoCancelamento.RetornoCompletoStr;

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                #region Consulta Recibo de lote

                _nfa = new NFeBLL().GetNfaDataTable(textBox3.Text);

                var recibo = _nfa.Tables["nfe_cab"].Rows[0]["nrec"].ToString();
                if (string.IsNullOrEmpty(recibo)) throw new Exception("O número do recibo deve ser informado!");
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoRecibo = servicoNFe.NFeRetAutorizacao(recibo);

                TrataRetorno(retornoRecibo);

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                #region Consulta Situação NFe
                _nfa = new NFeBLL().GetNfaDataTable(textBox3.Text);
                var chave = _nfa.Tables["nfe_cab"].Rows[0]["chnfe"].ToString();
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _nfa = new NFeBLL().GetNfaDataTable(textBox1.Text);
            Imprimir(_nfa.Tables["nfe_cab"].Rows[0]["xml"].ToString());
        }

        private void Imprimir(string xml)
        {
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = xml;// Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;
                var proc = new nfeProc().CarregarDeXmlString(arquivoXml);
                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                    throw new Exception("O XML informado não é um NFe!");

                #endregion

                #region Abre a visualização do relatório para impressão
                var danfe = new DanfeFrNfe(proc, new ConfiguracaoDanfeNfe());
                danfe.Visualizar();
                //danfe.Imprimir();
                //danfe.ExibirDesign();
                //danfe.ExportarPdf(@"d:\teste.pdf");

                #endregion

            }
            catch (Exception ex)
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Exemplo com using para chamar o método Dispose da classe.
            //Usar dessa forma, especialmente, quando for usar certificado A3 com a senha salva.
            using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico))
            {
                var retornoStatus = servicoNFe.NfeStatusServico();
                TrataRetorno(retornoStatus);
            }
        }
    }
}
