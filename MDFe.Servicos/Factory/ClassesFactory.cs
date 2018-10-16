﻿/********************************************************************************/
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
using System;
using MDFe.Classes.Extencoes;
using MDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.RetRecepcao;
using MDFe.Classes.Informacoes.StatusServico;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Utils.Configuracoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

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
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                XServ = "CONSULTAR NÃO ENCERRADOS"
            };

            return consMDFeNaoEnc;
        }

        public static MDFeConsSitMDFe CriarConsSitMDFe(string chave)
        {
            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
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
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout
        };

            return enviMdfe;
        }

        public static MDFeConsReciMDFe CriaConsReciMDFe(string numeroRecibo)
        {
            var consReciMDFe = new MDFeConsReciMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                NRec = numeroRecibo
            };

            return consReciMDFe;
        }

        public static MDFeConsStatServMDFe CriaConsStatServMDFe()
        {
            return new MDFeConsStatServMDFe
            {
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                XServ = "STATUS"
            }; 
        }
    }
}