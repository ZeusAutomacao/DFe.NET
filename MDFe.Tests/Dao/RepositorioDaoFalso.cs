using System;
using System.IO;
using System.Reflection;
using System.Xml;
using DFe.Utils;
using MDFe.Classes.Informacoes.Evento;

namespace MDFe.Tests.Dao
{
    public class RepositorioDaoFalso
    {
        private MDFeEventoMDFe _evento;



        public RepositorioDaoFalso()
        {

        }

        private void CarregarEnviMdfeEvento(string caminhoXml)
        {
            try
            {
                var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeEventoMDFe>(caminhoXml);
                _evento = proc;
            }
            catch
            {
                throw new Exception("Não foi...");
            }

        }

        public XmlDocument GetXmlEsperado(string xml)
        {
            var xmlSolicitado = new XmlDocument();
            xmlSolicitado.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" + xml);
            return xmlSolicitado;
        }
    }
}
