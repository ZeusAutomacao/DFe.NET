
using System;
using System.Xml;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DFe.Utils;
using SMDFe.Classes.Informacoes.Evento;
using SMDFe.Classes.Retorno;
using SMDFe.Classes.Servicos.Autorizacao;


using MDFeEletronico = SMDFe.Classes.Informacoes.MDFe;

namespace SMDFe.Tests.Dao
{
    public class RepositorioDaoFalso
    {
        private MDFeEletronico _mdFeEletronico;
        private MDFeEnviMDFe _enviMdFe;
        private MDFeEventoMDFe _evento;



        public RepositorioDaoFalso()
        {

        }


        private void CarregarMdfeArquivo()
        {
            var caminhoXml = @"Arquivos\mdfe-teste.xml";
            try
            {
                _mdFeEletronico = MDFeEletronico.LoadXmlArquivo(caminhoXml);
            }
            catch
            {
                var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoXml);
                _mdFeEletronico = proc.MDFe;
            }

        }

        private void CarregarEnviMdfeArquivo()
        {
            var caminhoXml = @"Arquivos\envi-mdfe-teste.xml";

            try
            {
                _enviMdFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoXml);
            }
            catch
            {
                var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeEnviMDFe>(caminhoXml);
                _enviMdFe = proc;
            }

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


        public MDFeEletronico GetMdFeEletronica()
        {
            if (_mdFeEletronico == null)
            {
                _mdFeEletronico = new MDFeEletronico();
               
                CarregarMdfeArquivo();
            }

            return _mdFeEletronico;
        }

        public MDFeEventoMDFe GetEvento(int tipoEvento)
        {
            if (_evento == null)
            {
                _evento = new MDFeEventoMDFe();
                var evento = ArquivoEvento(tipoEvento);
                CarregarEnviMdfeEvento(evento);
            }

            return _evento;
        }

        private static string ArquivoEvento(int tipoEvento)
        {
            var evento = "";
            switch (tipoEvento)
            {
                case 1:
                    evento = @"Arquivos\evento-incluir.xml";
                    break;
                case 2:
                    evento = @"Arquivos\evento-cancelar.xml";
                    break;
                case 3:
                    evento = @"Arquivos\evento-encerrar.xml";
                    break;
                default:
                    throw new ArgumentException("Argumento Inválido");                    
            }

            return evento;
        }

        public XmlDocument GetXmlEsperado(string xml)
        {
            var xmlSolicitado = new XmlDocument();
            xmlSolicitado.Load(@"Arquivos\"+xml);
            return xmlSolicitado;
        }

        public MDFeEnviMDFe GetEnviMdFe()
        {
            if (_enviMdFe == null)
            {
                _enviMdFe = new MDFeEnviMDFe();
                CarregarEnviMdfeArquivo();
            }
            return _enviMdFe;
        }
    }
}
