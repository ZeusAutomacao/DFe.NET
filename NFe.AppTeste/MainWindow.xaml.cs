/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
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
using NFe.Utils.Email;
using NFe.Utils.InformacoesSuplementares;
using NFe.Utils.NFe;
using NFe.Utils.Tributacao.Estadual;
using RichTextBox = System.Windows.Controls.RichTextBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using WebBrowser = System.Windows.Controls.WebBrowser;
using System.Windows.Media.Imaging;
using NFe.Danfe.Nativo.NFCe;
using NFe.Utils;
using NFe.Utils.Excecoes;
using NFe.Utils.Tributacao.Federal;
using Image = System.Drawing.Image;

namespace NFe.AppTeste
{
    /// <summary>
    ///     Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow
    {
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private const string TituloErro = "Erro";
        private ConfiguracaoApp _configuracoes;
        private Classes.NFe _nfe;
        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainWindow()
        {
            InitializeComponent();
            CarregarConfiguracao();
            DataContext = _configuracoes;
        }

        private void CarregaDadosCertificado()
        {
            try
            {
                var cert = CertificadoDigitalUtils.ListareObterDoRepositorio();
                _configuracoes.CfgServico.Certificado.Serial = cert.SerialNumber;
                var be = TxtCertificado.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                if (be != null) be.UpdateTarget();

                //TxtValidade.Text = "Validade: " + cert.GetExpirationDateString();
            }
            catch (Exception ex)
            {
                Funcoes.Mensagem(ex.Message, TituloErro, MessageBoxButton.OK);
            }
        }

        private void btnCertificado_Click(object sender, RoutedEventArgs e)
        {
            CarregaDadosCertificado();
        }

        private void BtnStatusServico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Status do serviço
                //Exemplo com using para chamar o método Dispose da classe.
                //Usar dessa forma, especialmente, quando for usar certificado A3 com a senha salva.
                // se usar cache você pode por um id no certificado e salvar mais de um certificado digital também na memoria com o zeus
                //_configuracoes.CfgServico.Certificado.CacheId = "1";
                using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico))
                {
                    var retornoStatus = servicoNFe.NfeStatusServico();
                    TrataRetorno(retornoStatus);
                }

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarConfiguracao();
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
                    Funcoes.Mensagem(string.Format("{0} \n\nDetalhes: {1}", ex.Message, ex.InnerException), "Erro",
                        MessageBoxButton.OK);
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

                #region Carrega a logo no controle logoEmitente

                if (_configuracoes.ConfiguracaoDanfeNfce.Logomarca != null && _configuracoes.ConfiguracaoDanfeNfce.Logomarca.Length > 0)
                    using (var stream = new MemoryStream(_configuracoes.ConfiguracaoDanfeNfce.Logomarca))
                    {
                        LogoEmitente.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }

                #endregion
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaChave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Situação NFe

                var chave = Funcoes.InpuBox(this, "Consultar NFe pela Chave", "Chave da NFe:");
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaRecibo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Situação NFe

                var recibo = Funcoes.InpuBox(this, "Consultar Recibo", "Número do recibo:");
                if (string.IsNullOrEmpty(recibo)) throw new Exception("O recibo deve ser informado!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NFeRetAutorizacao(recibo);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Situação NFe pelo XML

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrWhiteSpace(arquivoXml))
                    return;

                var nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                var chave = nfe.infNFe.Id.Substring(3);

                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave da NFe não foi encontrada no arquivo!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void GeranNfe(VersaoServico versaoServico, ModeloDocumento modelo)
        {
            try
            {
                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (string.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                _nfe = ObterNfeValidada(versaoServico, modelo, Convert.ToInt32(numero), _configuracoes.ConfiguracaoCsc);

                ExibeNfe();

                var dlg = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = _nfe.infNFe.Id.Substring(3),
                    DefaultExt = ".xml",
                    Filter = "Arquivo XML (.xml)|*.xml"
                };
                var result = dlg.ShowDialog();
                if (result != true) return;
                var arquivoXml = dlg.FileName;
                _nfe.SalvarArquivoXml(arquivoXml);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private Classes.NFe ObterNfeValidada(VersaoServico versaoServico, ModeloDocumento modelo, int numero, ConfiguracaoCsc configuracaoCsc)
        {
            var nfe = GetNf(numero, modelo, versaoServico);

            nfe.Assina();

            if (nfe.infNFe.ide.mod == ModeloDocumento.NFCe)
            {
                nfe.infNFeSupl = new infNFeSupl();
                if (versaoServico == VersaoServico.Versao400)
                    nfe.infNFeSupl.urlChave = nfe.infNFeSupl.ObterUrlConsulta(nfe, _configuracoes.ConfiguracaoDanfeNfce.VersaoQrCode);
                nfe.infNFeSupl.qrCode = nfe.infNFeSupl.ObterUrlQrCode(nfe, _configuracoes.ConfiguracaoDanfeNfce.VersaoQrCode, configuracaoCsc.CIdToken, configuracaoCsc.Csc);
            }

            nfe.Valida();

            return nfe;
        }

        private void BtnCriareEnviar3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Cria e Envia NFe

                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (string.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (string.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                _nfe = ObterNfeValidada(_configuracoes.CfgServico.VersaoNFeAutorizacao, _configuracoes.CfgServico.ModeloDocumento, Convert.ToInt32(numero), _configuracoes.ConfiguracaoCsc);

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Sincrono, new List<Classes.NFe> { _nfe }, false/*Envia a mensagem compactada para a SEFAZ*/);
                //Para consumir o serviço de forma síncrona, use a linha abaixo:
                //var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Sincrono, new List<Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);

                TrataRetorno(retornoEnvio);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                //Faça o tratamento de contingência OffLine aqui.
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultarReciboLote3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Recibo de lote

                var recibo = Funcoes.InpuBox(this, "Consultar processamento de lote de NF-e", "Número do recibo:");
                if (string.IsNullOrEmpty(recibo)) throw new Exception("O número do recibo deve ser informado!");
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoRecibo = servicoNFe.NFeRetAutorizacao(recibo);

                TrataRetorno(retornoRecibo);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void TrataRetorno(RetornoBasico retornoBasico)
        {
            EnvioStr(RtbEnvioStr, retornoBasico.EnvioStr);
            RetornoStr(RtbRetornoStr, retornoBasico.RetornoStr);
            RetornoXml(WebXmlRetorno, retornoBasico.RetornoStr);
            RetornoCompletoStr(RtbRetornoCompletoStr, retornoBasico.RetornoCompletoStr);
            RetornoDados(retornoBasico.Retorno, RtbDadosRetorno);
        }

        private void BtnGerarNfe3_Click(object sender, RoutedEventArgs e)
        {
            GeranNfe(_configuracoes.CfgServico.VersaoNFeAutorizacao, _configuracoes.CfgServico.ModeloDocumento);
        }

        private void BtnInutiliza_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Inutiliza Numeração

                var ano = Funcoes.InpuBox(this, "Inutilizar Numeração", "Ano - dois dígitos somente. Ex: 17");
                if (string.IsNullOrEmpty(ano)) throw new Exception("O Ano deve ser informado!");
                if (ano.Length > 2) throw new Exception("O Ano deve ter dois números apenas!");

                var modelostr = Funcoes.InpuBox(this, "Inutilizar Numeração", "Modelo");
                if (string.IsNullOrEmpty(modelostr)) throw new Exception("O Modelo deve ser informado!");

                var modelo = (ModeloDocumento)Convert.ToInt16(modelostr);

                var serie = Funcoes.InpuBox(this, "Inutilizar Numeração", "Série");
                if (string.IsNullOrEmpty(serie)) throw new Exception("A série deve ser informada!");

                var numeroInicial = Funcoes.InpuBox(this, "Inutilizar Numeração", "Número Inicial");
                if (string.IsNullOrEmpty(numeroInicial)) throw new Exception("O Número Inicial deve ser informado!");

                var numeroFinal = Funcoes.InpuBox(this, "Inutilizar Numeração", "Número Final");
                if (string.IsNullOrEmpty(numeroFinal)) throw new Exception("O Número Final deve ser informado!");

                var justificativa = Funcoes.InpuBox(this, "Inutilizar Numeração", "Justificativa da inutilização");
                if (string.IsNullOrEmpty(justificativa)) throw new Exception("A Justificativa deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeInutilizacao(_configuracoes.Emitente.CNPJ, Convert.ToInt16(ano),
                    modelo, Convert.ToInt16(serie), Convert.ToInt32(numeroInicial),
                    Convert.ToInt32(numeroFinal), justificativa);

                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCartaCorrecao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carta de correção

                var idlote = Funcoes.InpuBox(this, "Carta de correção", "Identificador de controle do Lote de envio:");
                if (string.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Carta de correção", "Número sequencial do evento:");
                if (string.IsNullOrEmpty(sequenciaEvento))
                    throw new Exception("O número sequencial deve ser informado!");

                var chave = Funcoes.InpuBox(this, "Carta de correção", "Chave da NFe:");
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var correcao = Funcoes.InpuBox(this, "Carta de correção", "Correção");
                if (string.IsNullOrEmpty(correcao)) throw new Exception("A Correção deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = string.IsNullOrEmpty(_configuracoes.Emitente.CNPJ)
                    ? _configuracoes.Emitente.CPF
                    : _configuracoes.Emitente.CNPJ;
                if (string.IsNullOrEmpty(cpfcnpj))
                    throw new Exception("O CPF ou CNPJ do emitente deve ser informado!");

                var retornoCartaCorrecao = servicoNFe.RecepcaoEventoCartaCorrecao(Convert.ToInt32(idlote),
                    Convert.ToInt16(sequenciaEvento), chave, correcao, cpfcnpj);

                TrataRetorno(retornoCartaCorrecao);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCancelarNFe_Click(object sender, RoutedEventArgs e)
        {
            const string titulo = "Cancelar NFe";

            try
            {
                #region Cancelar NFe

                var idlote = Funcoes.InpuBox(this, titulo, "Identificador de controle do Lote de envio:");
                if (string.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, titulo, "Número sequencial do evento:");
                if (string.IsNullOrEmpty(sequenciaEvento))
                    throw new Exception("O número sequencial deve ser informado!");

                var protocolo = Funcoes.InpuBox(this, titulo, "Protocolo de Autorização da NFe:");
                if (string.IsNullOrEmpty(protocolo)) throw new Exception("O protocolo deve ser informado!");

                var chave = Funcoes.InpuBox(this, titulo, "Chave da NFe:");
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var justificativa = Funcoes.InpuBox(this, titulo, "Justificativa do cancelamento");
                if (string.IsNullOrEmpty(justificativa)) throw new Exception("A justificativa deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = string.IsNullOrEmpty(_configuracoes.Emitente.CNPJ)
                    ? _configuracoes.Emitente.CPF
                    : _configuracoes.Emitente.CNPJ;
                var retornoCancelamento = servicoNFe.RecepcaoEventoCancelamento(Convert.ToInt32(idlote),
                    Convert.ToInt16(sequenciaEvento), protocolo, chave, justificativa, cpfcnpj);

                TrataRetorno(retornoCancelamento);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnEnviaEpec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                 * Atenção:
                 * O campo dhEmi da nfe a ser vinculada ao EPEC deve ser exatamente igual ao informado m detevento do EPEC, assim como os demais dados, como emitente, destinatário, etc. 
                 * Vide a rejeição código 467 no manual do EPEC
                */

                #region Enviar EPEC

                var idlote = Funcoes.InpuBox(this, "Enviar EPEC", "Identificador de controle do Lote de envio:");
                if (string.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Enviar EPEC", "Número sequencial do evento:");
                if (string.IsNullOrEmpty(sequenciaEvento))
                    throw new Exception("O número sequencial deve ser informado!");

                var numeronota = Funcoes.InpuBox(this, "Enviar EPEC", "Número da Nota:");
                if (string.IsNullOrEmpty(numeronota)) throw new Exception("O Número da Nota deve ser informado!");

                _nfe = ObterNfeValidada(_configuracoes.CfgServico.VersaoNFeAutorizacao,
                    _configuracoes.CfgServico.ModeloDocumento, Convert.ToInt32(numeronota),
                    _configuracoes.ConfiguracaoCsc);

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEpec = servicoNFe.RecepcaoEventoEpec(Convert.ToInt32(idlote),
                    Convert.ToInt16(sequenciaEvento), _nfe, Assembly.GetExecutingAssembly().GetName().Version.ToString());
                TrataRetorno(retornoEpec);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaEpec_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Atenção:
             * Depois de enviar a EPEC, leva um tempo para a consulta retornar os dados, pois o ambiente AN precisa replicar os dados para o serviço de consulta dos demais estados
             * Se uma NFe já foi conciliada para a EPEC, então o serviço retornará os dados de autorização da nfe e a EPEC virá no grupo procEventoNFe(eventos)
            */
            BtnConsultaChave_Click(sender, e);
            //Nada mais que uma consulta ao serviço NfeConsultaProtocolo, passando a chave gerada pelo EPEC
        }

        private void BtnDiretorioSchema_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new FolderBrowserDialog();
                dlg.ShowDialog();
                TxtDiretorioSchema.Text = dlg.SelectedPath;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnImportarXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarregaArquivoNfe();
                ExibeNfe();

                //Ler os impostos de um XML carregado para o objeto do tipo NFe
                /*
                //ICMS
                var icms = _nfe.infNFe.det[0].imposto.ICMS.TipoICMS as ICMSSN500;
                if (icms != null)
                    Funcoes.Mensagem(icms.CSOSN.ToString(), "CSOSN", MessageBoxButton.OK);

                //IPI
                var ipi = _nfe.infNFe.det[0].imposto.IPI.TipoIPI as IPITrib;
                if (ipi != null)
                {
                    Funcoes.Mensagem(ipi.CST.ToString(), "CST", MessageBoxButton.OK);
                    Funcoes.Mensagem(ipi.pIPI.ToString(), "pIPI", MessageBoxButton.OK);
                    Funcoes.Mensagem(ipi.vIPI.ToString(), "vIPI", MessageBoxButton.OK);
                }

                //PIS
                var pis = _nfe.infNFe.det[0].imposto.PIS.TipoPIS as PISOutr;
                if (pis != null)
                {
                    Funcoes.Mensagem(pis.CST.ToString(), "CST", MessageBoxButton.OK);
                    Funcoes.Mensagem(pis.pPIS.ToString(), "pPIS", MessageBoxButton.OK);
                    Funcoes.Mensagem(pis.vPIS.ToString(), "vPIS", MessageBoxButton.OK);
                }
                */
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void ExibeNfe()
        {
            _nfe.SalvarArquivoXml(_path + @"\tmp.xml");
            WebXmlNfe.Navigate(_path + @"\tmp.xml");
            TabItemNfe.IsSelected = true;
        }

        private void CarregaArquivoNfe()
        {
            var arquivoXml = Funcoes.BuscarArquivoXml();

            if (!string.IsNullOrWhiteSpace(arquivoXml))
                _nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
        }

        private void BtnValida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_nfe == null)
                {
                    CarregaArquivoNfe();
                }

                if (_nfe == null) return;
                _nfe.Valida();
                Funcoes.Mensagem(string.Format("NFe número {0} validada com sucesso!", _nfe.infNFe.ide.nNF), "Atenção",
                    MessageBoxButton.OK);
                ExibeNfe();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnAssina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_nfe == null)
                {
                    CarregaArquivoNfe();
                }

                if (_nfe == null) return;
                _nfe.Assina();
                Funcoes.Mensagem(string.Format("NFe número {0} assinada com sucesso!", _nfe.infNFe.ide.nNF), "Atenção",
                    MessageBoxButton.OK);
                ExibeNfe();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnDiretorioXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new FolderBrowserDialog();
                dlg.ShowDialog();
                TxtDiretorioXml.Text = dlg.SelectedPath;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void CbxSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            if (CbxSalvarXml.IsChecked == true)
                BtnDiretorioXml_Click(sender, e);
        }

        private void BtnAdicionaNfeproc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var arquivoXml = Funcoes.BuscarArquivoXml();

                if (string.IsNullOrWhiteSpace(arquivoXml))
                    return;

                var nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                var chave = nfe.infNFe.Id.Substring(3);

                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave da NFe não foi encontrada no arquivo!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                var nfeproc = new nfeProc
                {
                    NFe = nfe,
                    protNFe = retornoConsulta.Retorno.protNFe,
                    versao = retornoConsulta.Retorno.versao
                };
                if (nfeproc.protNFe != null)
                {
                    var novoArquivo = Path.GetDirectoryName(arquivoXml) + @"\" + nfeproc.protNFe.infProt.chNFe +
                                      "-procNfe.xml";
                    FuncoesXml.ClasseParaArquivoXml(nfeproc, novoArquivo);
                    Funcoes.Mensagem("Arquivo salvo em " + novoArquivo, "Atenção", MessageBoxButton.OK);
                }
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCarregaXmlEnvia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (string.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                BtnImportarXml_Click(sender, e);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<Classes.NFe> { _nfe }, true/*Envia a mensagem compactada para a SEFAZ*/);

                TrataRetorno(retornoEnvio);
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaCadastro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Cadastro

                var uf = Funcoes.InpuBox(this, "Consultar Cadastro", "UF do Documento a ser Consultado:");
                if (string.IsNullOrEmpty(uf)) throw new Exception("A UF deve ser informada!");
                if (uf.Length != 2) throw new Exception("UF deve conter 2 caracteres!");

                var tipoDocumento = Funcoes.InpuBox(this, "Consultar Cadastro", "Tipo de documento a ser consultado: (0 - IE; 1 - CNPJ; 2 - CPF):");
                if (string.IsNullOrEmpty(tipoDocumento)) throw new Exception("O Tipo de documento deve ser informado!");
                if (tipoDocumento.Length != 1) throw new Exception("O Tipo de documento deve conter um apenas um número!");
                if (!tipoDocumento.All(char.IsDigit)) throw new Exception("O Tipo de documento deve ser um número inteiro");
                var intTipoDocumento = int.Parse(tipoDocumento);
                if (!(intTipoDocumento >= 0 && intTipoDocumento <= 2)) throw new Exception("Tipos válidos: (0 - IE; 1 - CNPJ; 2 - CPF)");

                var documento = Funcoes.InpuBox(this, "Consultar Cadastro", "Documento(IE/CNPJ/CPF):");
                if (string.IsNullOrEmpty(documento)) throw new Exception("O Documento(IE/CNPJ/CPF) deve ser informado!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaCadastro(uf, (ConsultaCadastroTipoDocumento)intTipoDocumento, documento);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        #region Criar NFe

        protected virtual Classes.NFe GetNf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var nf = new Classes.NFe { infNFe = GetInf(numero, modelo, versao) };
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

            if (infNFe.ide.mod == ModeloDocumento.NFe & (versao == VersaoServico.Versao310 || versao == VersaoServico.Versao400))
                infNFe.cobr = GetCobranca(infNFe.total.ICMSTot); //V3.00 e 4.00 Somente
            if (infNFe.ide.mod == ModeloDocumento.NFCe || (infNFe.ide.mod == ModeloDocumento.NFe & versao == VersaoServico.Versao400))
                infNFe.pag = GetPagamento(infNFe.total.ICMSTot, versao); //NFCe Somente  

            if (infNFe.ide.mod == ModeloDocumento.NFCe & versao != VersaoServico.Versao400)
                infNFe.infAdic = new infAdic() { infCpl = "Troco: 10,00" }; //Susgestão para impressão do troco em NFCe

            return infNFe;
        }

        protected virtual ide GetIdentificacao(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var ide = new ide
            {
                cUF = _configuracoes.EnderecoEmitente.UF,
                natOp = "VENDA",
                mod = modelo,
                serie = 1,
                nNF = numero,
                tpNF = TipoNFe.tnSaida,
                cMunFG = _configuracoes.EnderecoEmitente.cMun,
                tpEmis = _configuracoes.CfgServico.tpEmis,
                tpImp = TipoImpressao.tiRetrato,
                cNF = "1234",
                tpAmb = _configuracoes.CfgServico.tpAmb,
                finNFe = FinalidadeNFe.fnNormal,
                verProc = "3.000",
                indIntermed = IndicadorIntermediador.iiSemIntermediador
            };

            if (ide.tpEmis != TipoEmissao.teNormal)
            {
                ide.dhCont = DateTime.Now;
                ide.xJust = "TESTE DE CONTIGÊNCIA PARA NFe/NFCe";
            }

            #region V2.00

            if (versao == VersaoServico.Versao200)
            {
                ide.dEmi = DateTime.Today; //Mude aqui para enviar a nfe vinculada ao EPEC, V2.00
                ide.dSaiEnt = DateTime.Today;
            }

            #endregion

            #region V3.00

            if (versao == VersaoServico.Versao200) return ide;

            if (versao == VersaoServico.Versao310)
            {
                ide.indPag = IndicadorPagamento.ipVista;
            }


            ide.idDest = DestinoOperacao.doInterna;
            ide.dhEmi = DateTime.Now;
            //Mude aqui para enviar a nfe vinculada ao EPEC, V3.10
            if (ide.mod == ModeloDocumento.NFe)
                ide.dhSaiEnt = DateTime.Now;
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
            //    //CPF = "12345678912",
            //    CNPJ = "12345678000189",
            //    xNome = "RAZAO SOCIAL LTDA",
            //    xFant = "FANTASIA LTRA",
            //    IE = "123456789",
            //};
            emit.enderEmit = GetEnderecoEmitente();
            return emit;
        }

        protected virtual enderEmit GetEnderecoEmitente()
        {
            var enderEmit = _configuracoes.EnderecoEmitente; // new enderEmit
            //{
            //    xLgr = "RUA TESTE DE ENREREÇO",
            //    nro = "123",
            //    xCpl = "1 ANDAR",
            //    xBairro = "CENTRO",
            //    cMun = 2802908,
            //    xMun = "ITABAIANA",
            //    UF = "SE",
            //    CEP = 49500000,
            //    fone = 79123456789
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
            dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"; //Obrigatório para NFe e opcional para NFCe
            dest.enderDest = GetEnderecoDestinatario(); //Obrigatório para NFe e opcional para NFCe
            
            //if (versao == VersaoServico.Versao200)
            //    dest.IE = "ISENTO";
            if (versao == VersaoServico.Versao200) return dest;

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
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do ICMS
                        //TipoICMS = ObterIcmsBasico(crt),

                        //Caso você resolva utilizar método ObterIcmsBasico(), comente esta proxima linha
                        TipoICMS =
                            crt == CRT.SimplesNacional
                                ? InformarCSOSN(Csosnicms.Csosn102)
                                : InformarICMS(Csticms.Cst00, VersaoServico.Versao310)
                    },

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

                    COFINS = new COFINS
                    {
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do COFINS
                        //TipoCOFINS = ObterCofinsBasico(),

                        //Caso você resolva utilizar método ObterCofinsBasico(), comente esta proxima linha
                        TipoCOFINS = new COFINSOutr { CST = CSTCOFINS.cofins99, pCOFINS = 0, vBC = 0, vCOFINS = 0 }
                    },

                    PIS = new PIS
                    {
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do PIS
                        //TipoPIS = ObterPisBasico(),

                        //Caso você resolva utilizar método ObterPisBasico(), comente esta proxima linha
                        TipoPIS = new PISOutr { CST = CSTPIS.pis99, pPIS = 0, vBC = 0, vPIS = 0 }
                    }
                }
            };

            if (modelo == ModeloDocumento.NFe) //NFCe não aceita grupo "IPI"
            {
                det.imposto.IPI = new IPI()
                {
                    cEnq = 999,

                    //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do IPI
                    //TipoIPI = ObterIPIBasico(),

                    //Caso você resolva utilizar método ObterIPIBasico(), comente esta proxima linha
                    TipoIPI = new IPITrib() { CST = CSTIPI.ipi00, pIPI = 5, vBC = 1, vIPI = 0.05m }
                };
            }

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
                vUnCom = 1.1m,
                vProd = 1.1m,
                vDesc = 0.10m,
                cEANTrib = "7770000000012",
                uTrib = "UNID",
                qTrib = 1,
                vUnTrib = 1.1m,
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
                vBC = 1.1m,
                pICMS = 18,
                vICMS = 0.20m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            if (versao == VersaoServico.Versao310)
                icms20.vICMSDeson = 0.10m; //V3.00 ou maior Somente

            switch (CST)
            {
                case Csticms.Cst00:
                    return new ICMS00
                    {
                        CST = Csticms.Cst00,
                        modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                        orig = OrigemMercadoria.OmNacional,
                        pICMS = 18,
                        vBC = 1.1m,
                        vICMS = 0.20m
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
                CST = Csticms.Cst00,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1.1m,
                pICMS = 18,
                vICMS = 0.20m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            return icmsGeral.ObterICMSBasico(crt);
        }

        private PISBasico ObterPisBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto PISGeral, como no exemplo abaixo.
            var pisGeral = new PISGeral()
            {
                CST = CSTPIS.pis01,
                vBC = 1.1m,
                pPIS = 1.65m,
                vPIS = 0.01m,
                vAliqProd = 0
            };

            return pisGeral.ObterPISBasico();
        }

        private COFINSBasico ObterCofinsBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto COFINSGeral, como no exemplo abaixo.
            var cofinsGeral = new COFINSGeral()
            {
                CST = CSTCOFINS.cofins01,
                vBC = 1.1m,
                pCOFINS = 1.65m,
                vCOFINS = 0.01m,
                vAliqProd = 0
            };

            return cofinsGeral.ObterCOFINSBasico();
        }

        private IPIBasico ObterIPIBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto IPIGeral, como no exemplo abaixo.
            var ipiGeral = new IPIGeral()
            {
                CST = CSTIPI.ipi01,
                vBC = 1.1m,
                pIPI = 5m,
                vIPI = 0.05m
            };

            return ipiGeral.ObterIPIBasico();
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
                vDesc = produtos.Sum(p => p.prod.vDesc ?? 0),
                vTotTrib = produtos.Sum(p => p.imposto.vTotTrib ?? 0),
            };

            if (versao == VersaoServico.Versao310 || versao == VersaoServico.Versao400)
                icmsTot.vICMSDeson = 0;

            if (versao == VersaoServico.Versao400)
            {
                icmsTot.vFCPUFDest = 0;
                icmsTot.vICMSUFDest = 0;
                icmsTot.vICMSUFRemet = 0;
                icmsTot.vFCP = 0;
                icmsTot.vFCPST = 0;
                icmsTot.vFCPSTRet = 0;
                icmsTot.vIPIDevol = 0;
            }

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

            //** Regra de validação W16-10 que rege sobre o Total da NF **//
            icmsTot.vNF =
                icmsTot.vProd
                - icmsTot.vDesc
                - icmsTot.vICMSDeson.GetValueOrDefault()
                + icmsTot.vST
                + icmsTot.vFCPST.GetValueOrDefault()
                + icmsTot.vFrete
                + icmsTot.vSeg
                + icmsTot.vOutro
                + icmsTot.vII
                + icmsTot.vIPI
                + icmsTot.vIPIDevol.GetValueOrDefault();

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
                esp = "teste de espécie",
                lacres = new List<lacres> { new lacres { nLacre = "123456" } }
            };

            return v;
        }

        protected virtual cobr GetCobranca(ICMSTot icmsTot)
        {
            var valorParcela = (icmsTot.vNF / 2).Arredondar(2);
            var c = new cobr
            {
                fat = new fat { nFat = "12345678910", vLiq = icmsTot.vNF, vOrig = icmsTot.vNF, vDesc = 0m },
                dup = new List<dup>
                {
                    new dup {nDup = "001", dVenc = DateTime.Now.AddDays(30), vDup = valorParcela},
                    new dup {nDup = "002", dVenc = DateTime.Now.AddDays(60), vDup = icmsTot.vNF - valorParcela}
                }
            };

            return c;
        }

        protected virtual List<pag> GetPagamento(ICMSTot icmsTot, VersaoServico versao)
        {
            var valorPagto = (icmsTot.vNF / 2).Arredondar(2);

            if (versao != VersaoServico.Versao400) // difernte de versão 4 retorna isso
            {
                var p = new List<pag>
                {
                    new pag {tPag = FormaPagamento.fpDinheiro, vPag = valorPagto},
                    new pag {tPag = FormaPagamento.fpCheque, vPag = icmsTot.vNF - valorPagto}
                };
                return p;
            }


            // igual a versão 4 retorna isso
            var p4 = new List<pag>
            {
                //new pag {detPag = new detPag {tPag = FormaPagamento.fpDinheiro, vPag = valorPagto}},
                //new pag {detPag = new detPag {tPag = FormaPagamento.fpCheque, vPag = icmsTot.vNF - valorPagto}}
                new pag
                {
                    detPag = new List<detPag>
                    {
                        new detPag {tPag = FormaPagamento.fpCreditoLoja, vPag = valorPagto},
                        new detPag {tPag = FormaPagamento.fpCreditoLoja, vPag = icmsTot.vNF - valorPagto}
                    }
                }
            };


            return p4;
        }

        #endregion

        #region Tratamento de retornos dos Serviços

        internal void RetornoDados<T>(T objeto, RichTextBox richTextBox) where T : class
        {
            richTextBox.Document.Blocks.Clear();

            foreach (var atributos in objeto.LerPropriedades())
            {
                richTextBox.AppendText(atributos.Key + " = " + atributos.Value + "\r");
            }
        }

        internal void RetornoCompletoStr(RichTextBox richTextBox, string retornoCompletoStr)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(retornoCompletoStr);
        }

        internal void EnvioStr(RichTextBox richTextBox, string envioStr)
        {
            richTextBox.Document.Blocks.Clear();
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
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(retornoXmlString);
        }

        #endregion


        private void BtnArquivoCertificado_Click(object sender, RoutedEventArgs e)
        {
            if (_configuracoes.CfgServico.Certificado.TipoCertificado == TipoCertificado.A1ByteArray)
            {
                var caminhoArquivo = Funcoes.BuscarArquivoCertificado();
                if (!string.IsNullOrWhiteSpace(caminhoArquivo))
                {
                    _configuracoes.CfgServico.Certificado.ArrayBytesArquivo = File.ReadAllBytes(caminhoArquivo);
                    _configuracoes.CfgServico.Certificado.Arquivo = null;
                }
                TxtArquivoCertificado.Text = caminhoArquivo;
            }
            else if (_configuracoes.CfgServico.Certificado.TipoCertificado == TipoCertificado.A1Arquivo)
            {
                _configuracoes.CfgServico.Certificado.Arquivo = Funcoes.BuscarArquivoCertificado();
                TxtArquivoCertificado.Text = _configuracoes.CfgServico.Certificado.Arquivo;
            }
        }

        private void BtnAdminCsc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Administração do CSC

                //indOp
                var indOp = Funcoes.InpuBox(this, "Identificador do tipo de operação:", "1 - Consulta CSC Ativos;\n2 - Solicita novo CSC;\n3 - Revoga CSC Ativo");
                if (string.IsNullOrEmpty(indOp)) throw new Exception("O Identificador do tipo de operação deve ser informado!");
                int n1;
                var inteiro = int.TryParse(indOp, out n1);
                if (!inteiro)
                    throw new Exception(string.Format("{0} não é um número inteiro!", indOp));
                if (n1 < 1 | n1 > 3)
                    throw new Exception("O Identificador do tipo de operação deve ser um número inteiro entre 1 e 3!");

                //raizCnpj
                var raizCnpj = Funcoes.InpuBox(this, "Administração do CSC", "Raiz do CNPJ do contribuinte que está efetuando a consulta (oito primeiros dígitos do CNPJ):");
                if (string.IsNullOrEmpty(raizCnpj)) throw new Exception("A Raiz do CNPJ do contribuinte deve ser informada!");
                long l;
                var longo = long.TryParse(raizCnpj, out l);
                if (!longo)
                    throw new Exception("A Raiz do CNPJ do contribuinte deve conter apenas números!");
                if (raizCnpj.Length != 8)
                    throw new Exception("A Raiz do CNPJ do contribuinte deve conter 8 caracteres!");

                var idCsc = "";
                var codigoCsc = "";
                if (int.Parse(indOp) == (int)IdentificadorOperacaoCsc.ioRevogaCscAtivo)
                {
                    //idCsc
                    idCsc = Funcoes.InpuBox(this, "Administração do CSC", "Número identificador do CSC a ser revogado:");
                    if (string.IsNullOrEmpty(idCsc)) throw new Exception("O Número identificador do CSC deve ser informado!");
                    int n2;
                    var inteiro2 = int.TryParse(idCsc, out n2);
                    if (!inteiro2)
                        throw new Exception("O Número identificador do CSC deve conter apenas números!");
                    if (idCsc.Length != 6)
                        throw new Exception("O Número identificador do CSC deve conter 6 caracteres!");

                    //codigoCsc
                    codigoCsc = Funcoes.InpuBox(this, "Administração do CSC", "Código alfanumérico do CSC a ser revogado: ");
                    if (string.IsNullOrEmpty(codigoCsc)) throw new Exception("O Código alfanumérico do CSC deve ser informado!");
                }

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoCsc = servicoNFe.AdmCscNFCe(raizCnpj, (IdentificadorOperacaoCsc)int.Parse(indOp), idCsc, codigoCsc);
                TrataRetorno(retornoCsc);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var arquivoXml = Funcoes.BuscarArquivoXml();

                if (string.IsNullOrEmpty(arquivoXml)) throw new ArgumentException("Opa\nSelecione um arquivo XML!");

                var arquivoPdf = Funcoes.BuscarArquivoPdf();

                if (string.IsNullOrEmpty(arquivoPdf)) throw new ArgumentException("Opa\nSelecione um arquivo PDF!");

                var emailDoDestinatario = Funcoes.InpuBox(this, "E-mail do destinatario",
                    "Digite o e-mail do destinatario por favor\nObrigado");

                if (string.IsNullOrEmpty(emailDoDestinatario))
                    throw new ArgumentException("Opá\nDigite o e-mail do destinatario");

                var emailBuilder = new EmailBuilder(_configuracoes.ConfiguracaoEmail)
                    .AdicionarDestinatario(emailDoDestinatario)
                    .AdicionarAnexo(arquivoXml)
                    .AdicionarAnexo(arquivoPdf);

                emailBuilder.AntesDeEnviarEmail += EventoAntesDeEnviarEmail;
                emailBuilder.DepoisDeEnviarEmail += EventoDepoisDeEnviarEmail;
                emailBuilder.ErroAoEnviarEmail += erro => Funcoes.Mensagem(erro.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                emailBuilder.Enviar();
            }
            catch (ArgumentException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EventoDepoisDeEnviarEmail(object sender, EventArgs e)
        {
            Funcoes.Mensagem("Evento executado depois de enviar o e-mail", "Evento", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EventoAntesDeEnviarEmail(object sender, EventArgs e)
        {
            Funcoes.Mensagem("Evento executado antes de enviar o e-mail\nO ATRIBUTO TIMEOUT SE DEIXADO COM 0 ELE PEGA O PADRÃO! EQUIVALENTE A 100000 millisegundos ou 100 segundos", "Evento", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_NFeDistribuicaoDFe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region NFeDistribuicaoDFe

                var cnpj = Funcoes.InpuBox(this, "Consulta NFeDistribuicaoDFe", "CNPJ do destinatário da NFe:");
                if (string.IsNullOrEmpty(cnpj)) throw new Exception("O CNPJ deve ser informado!");
                if (cnpj.Length != 14) throw new Exception("O CNPJ deve conter 14 caracteres!");

                var nsu = Funcoes.InpuBox(this, "Consulta NFeDistribuicaoDFe", "Ultimo NSU Retornado");
                if (string.IsNullOrEmpty(nsu))
                    nsu = "0";

                if (int.Parse(nsu) < 0) throw new Exception("NSU deve ser maior ou igual a 0");

                string chnfe = "";
                if (string.IsNullOrEmpty(nsu) || int.Parse(nsu) <= 0)
                    chnfe = Funcoes.InpuBox(this, "Consulta NFeDistribuicaoDFe", "Chave Eletrônica NFe");

                if ((string.IsNullOrEmpty(nsu) || int.Parse(nsu) < 0) && string.IsNullOrEmpty(chnfe))
                    throw new Exception("Último NSU ou Chave Eletrônica devem ser informados");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoNFeDistDFe = servicoNFe.NfeDistDFeInteresse(_configuracoes.EnderecoEmitente.UF.ToString(), cnpj, ultNSU: nsu, chNFE: chnfe);

                TrataRetorno(retornoNFeDistDFe);

            #endregion
        }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnManifestacaoDestinatario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region ManifestacaoDestinatario
                /* Justificativa = null*/

                string justificativa = null;

                var idlote = Funcoes.InpuBox(this, "Manifestação Destinatário", "Identificador de controle do Lote de envio:");
                if (string.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Manifestação Destinatário", "Número sequencial do evento:");
                if (string.IsNullOrEmpty(sequenciaEvento)) throw new Exception("O número sequencial deve ser informado!");

                var chave = Funcoes.InpuBox(this, "Manifestação Destinatário", "Chave da NFe:");
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var codigoEvento = Funcoes.InpuBox(this, "Manifestação Destinatário", "Código do Evento da Manifestação:");
                if (string.IsNullOrEmpty(codigoEvento)) throw new Exception("O Código do Evento da Manifestação deve ser informado!");
                if (codigoEvento.Length != 6) throw new Exception("O Código do Evento da Manifestação deve conter 6 caracteres!");

                var cnpj = Funcoes.InpuBox(this, "Manifestação Destinatário", "CNPJ do destinatário da NFe:");
                if (string.IsNullOrEmpty(cnpj)) throw new Exception("O CNPJ deve ser informado!");
                if (cnpj.Length != 14) throw new Exception("O CNPJ deve conter 14 caracteres!");

                var tipoEvento = (NFeTipoEvento)int.Parse(codigoEvento);

                if (tipoEvento == NFeTipoEvento.TeMdOperacaoNaoRealizada)
                {
                    justificativa = Funcoes.InpuBox(this, "Manifestação Destinatário", "Justificativa para a Operação Não Realizada");
                    if (string.IsNullOrEmpty(justificativa)) throw new Exception("A justificativa deve ser informada!");
                }

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoNFeDistDFe = servicoNFe.RecepcaoEventoManifestacaoDestinatario(int.Parse(idlote), int.Parse(sequenciaEvento), chave, (NFeTipoEvento)int.Parse(codigoEvento), cnpj, justificativa);

                TrataRetorno(retornoNFeDistDFe);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCupom_Click(object sender, RoutedEventArgs e)
        {
            string arquivoXml = Funcoes.BuscarArquivoXml();
            try
            {
                nfeProc proc = null;
                Classes.NFe nfe = null;
                string arquivo = string.Empty;

                try
                {
                    proc = new nfeProc().CarregarDeArquivoXml(arquivoXml);
                    arquivo = proc.ObterXmlString();
                }
                catch (Exception)
                {
                    nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                    arquivo = nfe.ObterXmlString();
                }



                DanfeNativoNfce impr = new DanfeNativoNfce(arquivo,
                    _configuracoes.ConfiguracaoDanfeNfce,
                    _configuracoes.ConfiguracaoCsc.CIdToken,
                    _configuracoes.ConfiguracaoCsc.Csc,
                    0 /*troco*//*, "Arial Black"*/);

                SaveFileDialog fileDialog = new SaveFileDialog();

                fileDialog.ShowDialog();

                if (string.IsNullOrEmpty(fileDialog.FileName))
                    throw new ArgumentException("Não foi selecionado nem uma pasta");



                //impr.Imprimir(salvarArquivoPdfEm: fileDialog.FileName.Replace(".pdf", "") + ".pdf");
                impr.GerarJPEG(fileDialog.FileName.Replace(".jpeg", "") + ".jpeg");

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Funcoes.Mensagem(ex.Message, TituloErro, MessageBoxButton.OK);
                }
            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            var arquivo = Funcoes.BuscarImagem();
            if (string.IsNullOrEmpty(arquivo)) return;
            var imagem = Image.FromFile(arquivo);
            LogoEmitente.Source = new BitmapImage(new Uri(arquivo));

            _configuracoes.ConfiguracaoDanfeNfce.Logomarca = new byte[0];
            using (var stream = new MemoryStream())
            {
                imagem.Save(stream, ImageFormat.Png);
                stream.Close();
                _configuracoes.ConfiguracaoDanfeNfce.Logomarca = stream.ToArray();
            }
        }

        private void btnRemoveLogo_Click(object sender, RoutedEventArgs e)
        {
            LogoEmitente.Source = null;
            _configuracoes.ConfiguracaoDanfeNfce.Logomarca = null;
        }

        private void BtnCancelarNFeSubstituicao_OnClick(object sender, RoutedEventArgs e)
        {
            const string titulo = "Cancelar NFe por substutuição";
            try
            {
                #region Cancelar NFe

                var idlote = Funcoes.InpuBox(this, titulo, "Identificador de controle do Lote de envio:");
                if (string.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, titulo, "Número sequencial do evento:");
                if (string.IsNullOrEmpty(sequenciaEvento))
                    throw new Exception("O número sequencial deve ser informado!");

                var protocolo = Funcoes.InpuBox(this, titulo, "Protocolo de Autorização da NFe substituída:");
                if (string.IsNullOrEmpty(protocolo)) throw new Exception("O protocolo deve ser informado!");

                var chave = Funcoes.InpuBox(this, titulo, "Chave da NFe substituída:");
                if (string.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var justificativa = Funcoes.InpuBox(this, titulo, "Justificativa do cancelamento");
                if (string.IsNullOrEmpty(justificativa)) throw new Exception("A justificativa deve ser informada!");

                var chaveNfeSubstituta = Funcoes.InpuBox(this, titulo, "Chave da NFe substituta:");
                if (string.IsNullOrEmpty(chaveNfeSubstituta)) throw new Exception("A Chave da NFe substituta deve ser informada!");
                if (chaveNfeSubstituta.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = string.IsNullOrEmpty(_configuracoes.Emitente.CNPJ)
                    ? _configuracoes.Emitente.CPF
                    : _configuracoes.Emitente.CNPJ;
                var retornoCancelamento = servicoNFe.RecepcaoEventoCancelamentoPorSubstituicao(Convert.ToInt32(idlote),
                    Convert.ToInt16(sequenciaEvento), protocolo, chave, justificativa, cpfcnpj,
                    _configuracoes.EnderecoEmitente.UF, Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    chaveNfeSubstituta);

                TrataRetorno(retornoCancelamento);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        
    }
}