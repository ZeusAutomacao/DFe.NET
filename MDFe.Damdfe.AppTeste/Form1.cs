using System;
using System.Drawing;
using System.Windows.Forms;


using MDFe.Classes.Retorno;
using MDFe.Damdfe.Fast;
using MDFe.Damdfe.Base;
using DFe.Utils;
using System.IO;

namespace MDFe.Damdfe.AppTeste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public byte[] ImageToByte(Image img)
        {
            if (img == null)
                return null;

            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }


        public DamdfeFrMDFe GetReport()
        {
            MDFeProcMDFe mdfe = null;
            try
            {
                var dlg = new OpenFileDialog
                {
                    Title = "Carrgar xml MDFeProc",
                    FileName = "",
                    DefaultExt = ".xml",
                    Filter = "Arquivo XML(.xml) | *.xml"
                };
                dlg.ShowDialog();
                string xml = dlg.FileName;
                if (!File.Exists(xml))
                    return null;

                mdfe = FuncoesXml.ArquivoXmlParaClasse<MDFe.Classes.Retorno.MDFeProcMDFe>(xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configurar impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                var rpt = new DamdfeFrMDFe(proc: mdfe,
                    config: new ConfiguracaoDamdfe()
                    {
                        Logomarca = ImageToByte(pcbLogotipo.Image),
                        DocumentoEncerrado = chbEncerrado.Checked,
                        DocumentoCancelado = chbCancelado.Checked,
                        Desenvolvedor = txtDesenvolvedor.Text,
                        QuebrarLinhasObservacao = chbQuebrarLinhaObservacao.Checked
                    },
                    arquivoRelatorio: txtArquivoFrx.Text);
                return rpt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Configurar impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var damdfe = GetReport();
            if (damdfe != null)
                damdfe.Visualizar(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var damdfe = GetReport();
            if (damdfe != null)
                damdfe.Imprimir(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var damdfe = GetReport();
            if (damdfe != null)
                damdfe.ExibirDesign();
        }

        private void chbCancelado_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCancelado.Checked)
                chbEncerrado.Checked = false;
        }

        private void chbEncerrado_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEncerrado.Checked)
                chbCancelado.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pcbLogotipo.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Carrgar imagem",
                FileName = "",
                DefaultExt = ".png",
                Filter = "Imagem (.png) | *.png"
            };
            dlg.ShowDialog();
            string img = dlg.FileName;
            if (!File.Exists(img))
                return;
        
            pcbLogotipo.Image = Bitmap.FromFile(img);        
        }
    }
}
