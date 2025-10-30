using MDFe.Classes.Extencoes;
using MDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.RetRecepcao;
using MDFe.Classes.Informacoes.StatusServico;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Utils.Configuracoes;
using System;
using System.Collections.Generic;
using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpjCpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var documentoUnico = cnpjCpf;

            var consMDFeNaoEnc = new MDFeCosMDFeNaoEnc
            {
                TpAmb = config.VersaoWebService.TipoAmbiente,
                Versao = config.VersaoWebService.VersaoLayout,
                XServ = "CONSULTAR NÃO ENCERRADOS"
            };

            if (documentoUnico.Length == 11)
            {
                consMDFeNaoEnc.CPF = cnpjCpf;
            }

            if (documentoUnico.Length == 14)
            {
                consMDFeNaoEnc.CNPJ = cnpjCpf;
            }

            return consMDFeNaoEnc;
        }

        public static MDFeEvIncDFeMDFe CriaEvIncDFeMDFe(string protocolo, string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos)
        {
            return new MDFeEvIncDFeMDFe
            {
                NProt = protocolo,
                CMunCarrega = codigoMunicipioCarregamento,
                XMunCarrega = nomeMunicipioCarregamento,
                InfDoc = informacoesDocumentos
            };
        }

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = config.VersaoWebService.VersaoLayout,
                TpAmb = config.VersaoWebService.TipoAmbiente,
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

        public static MDFeEvEncMDFe CriaEvEncMDFe(Estado estadoEncerramento, long codigoMunicipioEncerramento, string protocolo)
        {
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = estadoEncerramento,
                DtEnc = DateTime.Now,
                DescEvento = "Encerramento",
                CMun = codigoMunicipioEncerramento,
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

        public static MDFeEnviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var enviMdfe = new MDFeEnviMDFe
            {
                MDFe = mdfe,
                IdLote = lote.ToString(),
                Versao = config.VersaoWebService.VersaoLayout
            };

            return enviMdfe;
        }

        public static MDFeConsReciMDFe CriaConsReciMDFe(string numeroRecibo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consReciMDFe = new MDFeConsReciMDFe
            {
                Versao = config.VersaoWebService.VersaoLayout,
                TpAmb = config.VersaoWebService.TipoAmbiente,
                NRec = numeroRecibo
            };

            return consReciMDFe;
        }

        public static MDFeConsStatServMDFe CriaConsStatServMDFe(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            return new MDFeConsStatServMDFe
            {
                TpAmb = config.VersaoWebService.TipoAmbiente,
                Versao = config.VersaoWebService.VersaoLayout,
                XServ = "STATUS"
            };
        }

        public static evPagtoOperMDFe CriaEvPagtoOperMDFe(string protocolo, infViagens infViagens, List<infPag> infPagamentos)
        {
            return new evPagtoOperMDFe
            {
                infViagens = infViagens,
                nProt = protocolo,
                infPag = infPagamentos
            };
        }
    }
}