/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
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

using CTe.Classes;
using CTe.Classes.Servicos.DistribuicaoDFe;
using CTe.Utils.Validacao;
using DFe.Utils;
using System;

namespace CTe.Utils.DistribuicaoDFe
{
    public static class ExtdistDFeInt
    {

        /// <summary>
        /// Recebe um objeto ExtdistDFeInt e devolve a string no formato XML
        /// </summary>
        /// <param name="pedDistDFeInt">Objeto do Tipo distDFeInt</param>
        /// <returns>string com XML no do objeto distDFeInt</returns>
        public static string ObterXmlString(this distDFeInt pedDistDFeInt)
        {
            return FuncoesXml.ClasseParaXmlString(pedDistDFeInt);
        }

        public static void ValidaSchema(this distDFeInt pedDistDFeInt, ConfiguracaoServico configuracaoServico = null)
        {
            var xmlValidacao = pedDistDFeInt.ObterXmlString();
            

            if (pedDistDFeInt.versao.Equals("1.00"))
            {
                Validador.Valida(xmlValidacao, "distDFeInt_v1.00.xsd", configuracaoServico);
            }
            else if (pedDistDFeInt.versao.Equals("1.10"))
            {
                Validador.Valida(xmlValidacao, "distDFeInt_v1.10.xsd", configuracaoServico);
            }
            else
            {
                throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                    "a versão está inválida, somente é permitido " +
                                                    "versão 1.00 é 1.10");
            }
        }


        public static void SalvarXmlEmDisco(this distDFeInt pedDistDFeInt, string arquivoSalvar, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var arquivoXml = instanciaServico.DiretorioSalvarXml + arquivoSalvar;

            FuncoesXml.ClasseParaArquivoXml(pedDistDFeInt, arquivoXml);
        }

    }
}
