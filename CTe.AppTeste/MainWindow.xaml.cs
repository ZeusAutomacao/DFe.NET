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
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace CTe.AppTeste
{
    public partial class MainWindow
    {
        private readonly CTeTesteModel _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new CTeTesteModel();
            DataContext = _model;
            _model.SucessoSync += Sucesso;
        }

        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private void Sucesso(object sender, RetornoEEnvio e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                XmlTemp(e.Envio, "envio-tmp.xml");
                XmlTemp(e.Retorno, "retorno-tmp.xml");
                WebXmlEnvio.Navigate(_path + @"\envio-tmp.xml");
                WebXmlRetorno.Navigate(_path + @"\retorno-tmp.xml");
            }));
        }

        private void XmlTemp(string xml, string nomeXml)
        {
            var stw = new StreamWriter(_path + @"\" + nomeXml);
            stw.WriteLine(xml);
            stw.Close();
        }

        private void Certificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterSerialCertificado();
        }

        private void ArquivoCertificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterCertificadoArquivo();
        }

        private void BuscarDiretorioSchema_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSchema();
        }

        private void BuscarDiretorioSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSalvarXml();
        }

        private void SalvarConfiguracoesXml_Click(object sender, RoutedEventArgs e)
        {
            _model.SalvarConfiguracoesXml();
        }

        private void ConsultarStatusServico(object sender, RoutedEventArgs e)
        {
            _model.ConsultarStatusServico2();
        }

        private void InutilizacaoDeNumeracao_Click(object sender, RoutedEventArgs e)
        {
            _model.InutilizacaoDeNumeracao();
        }

        private void ConsultaPorProtocolo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorProtocolo();
        }

        private void EventoCancelarCTe_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoCancelarCTe();
        }

        private void EventoDesacordoCTe_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoDesacordoCTe();
        }

        private void CartaCorrecao_Click(object sender, RoutedEventArgs e)
        {
            _model.CartaCorrecao();
        }

        private void CriarEnviarCTe2_Click(object sender, RoutedEventArgs e)
        {
            _model.CriarEnviarCTe2e3();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _model.CarregarConfiguracoes();
        }

        private void ConsultaPorNumeroRecibo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorNumeroRecibo();
        }

        private void CriarEnviarAutomaticoCTe2_Click(object sender, RoutedEventArgs e)
        {
            _model.CriarEnviarCTeConsultaReciboAutomatico2e3();
        }

        private void CTeDistribuicaoDFe_Click(object sender, RoutedEventArgs e)
        {
            _model.DistribuicaoDFe();
        }

        private void EmitirCteOs_Click(object sender, RoutedEventArgs e)
        {
            _model.EmitirCteOs();
        }

        private void LoadXmlCte_Click(object sender, RoutedEventArgs e)
        {
            string xml = string.Empty;

            var janelaArquivo = new OpenFileDialog
            {
                Filter = "Xml (*.xml)|*.xml"
            };
            if (janelaArquivo.ShowDialog() == true)
            {
                xml = janelaArquivo.FileName;
            }

            if (string.IsNullOrWhiteSpace(xml))
            {
                MessageBox.Show("Selecione um xml");
            }

            _model.LoadXmlCTe(xml);
        }
    }
}
