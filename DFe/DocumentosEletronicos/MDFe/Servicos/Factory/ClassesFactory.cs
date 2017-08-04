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
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.ConsultaNaoEncerrados;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.ConsultaProtocolo;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Evento.CorpoEvento;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.RetRecepcao;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.StatusServico;
using MDFeEletronico = DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.MDFe;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static consMDFeNaoEnc CriarConsMDFeNaoEnc(string cnpj, DFeConfig dfeConfig)
        {
            var consMDFeNaoEnc = new consMDFeNaoEnc
            {
                CNPJ = cnpj,
                tpAmb = dfeConfig.TipoAmbiente,
                versao = dfeConfig.VersaoServico,
                xServ = "CONSULTAR NÃO ENCERRADOS"
            };

            return consMDFeNaoEnc;
        }

        public static consSitMDFe CriarConsSitMDFe(string chave, DFeConfig dfeConfig)
        {
            var consSitMdfe = new consSitMDFe
            {
                versao = dfeConfig.VersaoServico,
                tpAmb = dfeConfig.TipoAmbiente,
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

        public static evEncMDFe CriaEvEncMDFe(long codigoIbgeCidade, string protocolo, DFeConfig dfeConfig)
        {
            var encerramento = new evEncMDFe
            {
                cUF = dfeConfig.EstadoUf,
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

        public static enviMDFe CriaEnviMDFe(long lote, MDFeEletronico mdfe, DFeConfig dfeConfig)
        {
            var enviMdfe = new enviMDFe
            {
                MDFe = mdfe,
                idLote = lote.ToString(),
                versao = dfeConfig.VersaoServico
            };

            return enviMdfe;
        }

        public static consReciMDFe CriaConsReciMDFe(string numeroRecibo, DFeConfig dfeConfig)
        {
            var consReciMDFe = new consReciMDFe
            {
                versao = dfeConfig.VersaoServico,
                tpAmb = dfeConfig.TipoAmbiente,
                nRec = numeroRecibo
            };

            return consReciMDFe;
        }

        public static consStatServMDFe CriaConsStatServMDFe(DFeConfig dfeConfig)
        {
            return new consStatServMDFe
            {
                tpAmb = dfeConfig.TipoAmbiente,
                versao = dfeConfig.VersaoServico,
                xServ = "STATUS"
            }; 
        }
    }
}