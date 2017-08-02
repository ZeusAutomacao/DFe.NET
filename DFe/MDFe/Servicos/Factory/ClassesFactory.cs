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

using System;
using DFe.MDFe.Classes.Extensoes;
using DFe.MDFe.Classes.Servicos.Autorizacao;
using DFe.MDFe.Classes.Servicos.ConsultaNaoEncerrados;
using DFe.MDFe.Classes.Servicos.ConsultaProtocolo;
using DFe.MDFe.Classes.Servicos.Evento.CorpoEvento;
using DFe.MDFe.Classes.Servicos.RetRecepcao;
using DFe.MDFe.Classes.Servicos.StatusServico;
using DFe.MDFe.Configuracoes;
using MDFeEletronico = DFe.MDFe.Classes.Informacoes.MDFe;

namespace DFe.MDFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static consMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpj)
        {
            var consMDFeNaoEnc = new consMDFeNaoEnc
            {
                CNPJ = cnpj,
                tpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                xServ = "CONSULTAR NÃO ENCERRADOS"
            };

            return consMDFeNaoEnc;
        }

        public static consSitMDFe CriarConsSitMDFe(string chave)
        {
            var consSitMdfe = new consSitMDFe
            {
                versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                tpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                xServ = "CONSULTAR",
                chMDFe = chave
            };

            return consSitMdfe;
        }

        public static evCancMDFe CriaEvCancMDFe(string protocolo, string justificativa)
        {
            var cancelamento = new evCancMDFe
            {
                descEvento = "Cancelamento",
                nProt = protocolo,
                xJust = justificativa
            };

            return cancelamento;
        }

        public static evEncMDFe CriaEvEncMDFe(long codigoIbgeCidade, string protocolo)
        {
            var encerramento = new evEncMDFe
            {
                cUF = MDFeConfiguracao.VersaoWebService.UfEmitente,
                dtEnc = DateTime.Now,
                descEvento = "Encerramento",
                cMun = codigoIbgeCidade,
                nProt = protocolo
            };

            return encerramento;
        }

        public static evIncCondutorMDFe CriaEvIncCondutorMDFe(string nome, string cpf)
        {
            var condutor = new condutor
            {
                xNome = nome,
                CPF = cpf
            };

            var incluirCodutor = new evIncCondutorMDFe
            {
                descEvento = "Inclusao Condutor",
                condutor = condutor
            };

            return incluirCodutor;
        }

        public static enviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe)
        {
            var enviMdfe = new enviMDFe
            {
                MDFe = mdfe,
                idLote = lote.ToString(),
                versao = MDFeConfiguracao.VersaoWebService.VersaoLayout
        };

            return enviMdfe;
        }

        public static consReciMDFe CriaConsReciMDFe(string numeroRecibo)
        {
            var consReciMDFe = new consReciMDFe
            {
                versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                tpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                nRec = numeroRecibo
            };

            return consReciMDFe;
        }

        public static consStatServMDFe CriaConsStatServMDFe()
        {
            return new consStatServMDFe
            {
                tpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                xServ = "STATUS"
            }; 
        }
    }
}