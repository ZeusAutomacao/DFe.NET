using System;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaProtocolo;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.RetRecepcao;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Autorizacao;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;

namespace MDFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpj)
        {
            var consMDFeNaoEnc = new MDFeCosMDFeNaoEnc
            {
                CNPJ = cnpj,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc,
                XServ = "CONSULTAR NÃO ENCERRADOS"
            };

            return consMDFeNaoEnc;
        }

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave)
        {
            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsulta,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                XServ = "CONSULTAR",
                ChMDFe = chave
            };

            return consSitMdfe;
        }

        public static MDFeEvCancMDFe CriaEvCancMDFe(string protocolo, string justificativa)
        {
            var cancelamento = new MDFeEvCancMDFe
            {
                DescEvento = "Cancelamento",
                NProt = protocolo,
                XJust = justificativa
            };

            return cancelamento;
        }

        public static MDFeEvEncMDFe CriaEvEncMDFe(MDFeEletronico mdfe, string protocolo)
        {
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = mdfe.UFEmitente(),
                DtEnc = DateTime.Now,
                DescEvento = "Encerramento",
                CMun = mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = protocolo
            };

            return encerramento;
        }

        public static MDFeEvIncCondutorMDFe CriaEvIncCondutorMDFe(string nome, string cpf)
        {
            var condutor = new MDFeCondutorIncluir
            {
                XNome = nome,
                CPF = cpf
            };

            var incluirCodutor = new MDFeEvIncCondutorMDFe
            {
                DescEvento = "Inclusao Condutor",
                Condutor = condutor
            };

            return incluirCodutor;
        }

        public static MDFeEnviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe)
        {
            var enviMdfe = new MDFeEnviMDFe
            {
                MDFe = mdfe,
                IdLote = lote.ToString(),
                Versao = VersaoServico.Versao100
        };

            return enviMdfe;
        }

        public static MDFeConsReciMDFe CriaConsReciMDFe(string numeroRecibo)
        {
            var consReciMDFe = new MDFeConsReciMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRetRecepcao,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                NRec = numeroRecibo
            };

            return consReciMDFe;
        }
    }
}