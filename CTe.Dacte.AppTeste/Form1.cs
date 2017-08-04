﻿using CTe.Dacte.Base;
using CTe.Dacte.Fast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DFe.CTe.Classes;
using DFe.ManipuladorDeXml;
using DFe.Utils;

namespace CTe.Dacte.AppTeste
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

        private void button3_Click(object sender, EventArgs e)
        {
            pcbLogotipo.Image = null;
        }

        public DacteFrCte GetReport()
        {
            cteProc cte = null;
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

                cte = FuncoesXml.ArquivoXmlParaClasse<cteProc>(xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configurar impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                var rpt = new DacteFrCte(cte,
                    new ConfiguracaoDacte()
                    {
                        Logomarca = ImageToByte(pcbLogotipo.Image),
                        DocumentoCancelado = chbCancelado.Checked,
                        Desenvolvedor = txtDesenvolvedor.Text
                    },
                    arquivoRelatorio: txtArquivo.Text);
                return rpt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configurar impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dacte = GetReport();
            if (dacte != null)
                dacte.Visualizar(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dacte = GetReport();
            if (dacte != null)
                dacte.Imprimir(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var dacte = GetReport();
            if (dacte != null)
                dacte.ExibirDesign();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Carrgar relatório",
                FileName = "",
                DefaultExt = ".frx",
                Filter = "Relatório em Fast Reports (.frx) | *.frx"
            };
            dlg.ShowDialog();
            string frx = dlg.FileName;
            if (File.Exists(frx))
                txtArquivo.Text = frx;
        }

    }
}
