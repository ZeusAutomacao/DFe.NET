/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
        public static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpjCpf)
        {
            return CriarConsMDFeNaoEncInternal(cnpjCpf, MDFeConfiguracao.VersaoWebService);
        }

        public static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpjCpf, MDFeServicoConfiguracao configuracaoServico)
        {
            return CriarConsMDFeNaoEncInternal(cnpjCpf, configuracaoServico.VersaoWebService);
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

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave)
        {
            return CriarConsSitMDFeInternal(chave, MDFeConfiguracao.VersaoWebService);
        }

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave, MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriarConsSitMDFeInternal(chave, servicoConfiguracao.VersaoWebService);
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

        public static MDFeEnviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe)
        {
            return CriaEnviMDFeInternal(lote, mdfe, MDFeConfiguracao.VersaoWebService.VersaoLayout);
        }

        public static MDFeEnviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe, MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaEnviMDFeInternal(lote, mdfe, servicoConfiguracao.VersaoWebService.VersaoLayout);
        }

        public static MDFeConsReciMDFe CriaConsReciMDFe(string numeroRecibo)
        {
            return CriaConsReciMDFeInternal(numeroRecibo, MDFeConfiguracao.VersaoWebService);
        }

        public static MDFeConsReciMDFe CriaConsReciMDFe(string numeroRecibo, MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaConsReciMDFeInternal(numeroRecibo, servicoConfiguracao.VersaoWebService);
        }

        public static MDFeConsStatServMDFe CriaConsStatServMDFe()
        {
            return CriaConsStatServMDFe(MDFeConfiguracao.VersaoWebService);
        }

        public static MDFeConsStatServMDFe CriaConsStatServMDFe(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaConsStatServMDFe(servicoConfiguracao);
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

        #region Métodos internos

        private static MDFeCosMDFeNaoEnc CriarConsMDFeNaoEncInternal(string cnpjCpf, MDFeVersaoWebService versaoWebService)
        {
            var documentoUnico = cnpjCpf;

            var consMDFeNaoEnc = new MDFeCosMDFeNaoEnc
            {
                TpAmb = versaoWebService.TipoAmbiente,
                Versao = versaoWebService.VersaoLayout,
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

        private static MDFeConsSitMDFe CriarConsSitMDFeInternal(string chave, MDFeVersaoWebService versaoWebService)
        {
            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = versaoWebService.VersaoLayout,
                TpAmb = versaoWebService.TipoAmbiente,
                XServ = "CONSULTAR",
                ChMDFe = chave
            };

            return consSitMdfe;
        }

        private static MDFeEnviMDFe CriaEnviMDFeInternal(long lote, MDFeEletronico mdfe, Utils.Flags.VersaoServico versao)
        {
            var enviMdfe = new MDFeEnviMDFe
            {
                MDFe = mdfe,
                IdLote = lote.ToString(),
                Versao = versao,
            };

            return enviMdfe;
        }

        private static MDFeConsReciMDFe CriaConsReciMDFeInternal(string numeroRecibo, MDFeVersaoWebService versaoWebService)
        {
            var consReciMDFe = new MDFeConsReciMDFe
            {
                Versao = versaoWebService.VersaoLayout,
                TpAmb = versaoWebService.TipoAmbiente,
                NRec = numeroRecibo
            };

            return consReciMDFe;
        }
        private static MDFeConsStatServMDFe CriaConsStatServMDFe(MDFeVersaoWebService versaoWebService)
        {
            return new MDFeConsStatServMDFe
            {
                TpAmb = versaoWebService.TipoAmbiente,
                Versao = versaoWebService.VersaoLayout,
                XServ = "STATUS"
            };
        }

        #endregion
    }
}