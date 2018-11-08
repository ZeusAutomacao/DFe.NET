
using System;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DFe.Utils;
using SMDFe.Classes.Informacoes.Evento;
using SMDFe.Classes.Retorno;
using SMDFe.Classes.Servicos.Autorizacao;


using MDFeEletronico = SMDFe.Classes.Informacoes.MDFe;

namespace SMDFe.Tests.Dao
{
    public class MDfeEletronicaDaoFalsa
    {
        private MDFeEletronico _mdFeEletronico;
        private MDFeEnviMDFe _enviMdFe;
        private MDFeEventoMDFe _evento;



        public MDfeEletronicaDaoFalsa()
        {

        }


        private void CarregarMdfeArquivo()
        {
            var caminhoXml = "mdfe-teste.xml";
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
            var caminhoXml = "envi-mdfe-teste.xml";

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


        private void CarregarEnviMdfeEventoIncluir(string caminhoXml)
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
                CarregarEnviMdfeEventoIncluir(evento);
            }

            return _evento;
        }

        private static string ArquivoEvento(int tipoEvento)
        {
            var evento = "";
            switch (tipoEvento)
            {
                case 1:
                    evento = "evento-incluir.xml";
                    break;
                case 2:
                    evento = "evento-cancelar.xml";
                    break;
                case 3:
                    evento = "evento-encerrar.xml";
                    break;
                default:
                    throw new ArgumentException("Argumento Inválido");                    
            }

            return evento;
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
