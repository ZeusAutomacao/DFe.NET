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

using System;
using System.IO;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.Validacao;
using DFe.Classes.Entidades;
using DFe.Utils;

namespace CTe.Utils.CTe
{
    public static class ExtEnvCte
    {
        public static void ValidaSchema(this enviCTe enviCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var xmlValidacao = enviCTe.ObterXmlString();

            switch (enviCTe.versao)
            {
                case versao.ve200:
                    Validador.Valida(xmlValidacao, "enviCTe_v2.00.xsd", configuracaoServico);
                    break;
                case versao.ve300:
                    Validador.Valida(xmlValidacao, "enviCTe_v3.00.xsd", configuracaoServico);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Converte o objeto enviCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedEnvio"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto enviCTe</returns>
        public static string ObterXmlString(this enviCTe pedEnvio)
        {
            return FuncoesXml.ClasseParaXmlString(pedEnvio);
        }

        public static void SalvarXmlEmDisco(this enviCTe enviCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, enviCte.idLote + "-env-lot.xml");

            FuncoesXml.ClasseParaArquivoXml(enviCte, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this enviCTe enviCTe, ConfiguracaoServico configuracaoServico = null)
        {
            var request = new XmlDocument();

            var xml = enviCTe.ObterXmlString();
            
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.cUF == Estado.PR
                || instanciaServico.cUF == Estado.MT)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <CTe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/456
                xml = xml.Replace("<CTe>", "<CTe xmlns=\"http://www.portalfiscal.inf.br/cte\">");

            request.LoadXml(xml);

            return request;
        }
    }
}