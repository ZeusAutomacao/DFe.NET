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
using System.Text;
using System.Xml;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Validacao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.ManipuladorDeXml;
using DFe.DocumentosEletronicos.ManipulaPasta;

namespace DFe.DocumentosEletronicos.CTe.Classes.Extensoes
{
    public static class ExtConsReciCTe
    {
        public static void ValidarSchema(this consReciCTe consReciCTe, DFeConfig config)
        {
            var xmlValidacao = consReciCTe.ObterXmlString();

            switch (consReciCTe.versao)
            {
                case VersaoServico.Versao200:
                    Validador.Valida(xmlValidacao, "consReciCTe_v2.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consReciCTe_v3.00.xsd", config);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                   "a versão está inválida, somente é permitido " +
                                                   "versão 2.00 é 3.00");
            }
        }

        /// <summary>
        ///     Converte o objeto retconsReciCTe para uma string no formato XML
        /// </summary>
        /// <param name="consReciCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retconsReciCTe</returns>
        public static string ObterXmlString(this consReciCTe consReciCTe)
        {
            return FuncoesXml.ClasseParaXmlString(consReciCTe);
        }

        public static void SalvarXmlEmDisco(this consReciCTe consReciCTe, DFeConfig config)
        {
            if (config.NaoSalvarXml()) return;

            var caminhoXml = new ResolvePasta(config, DateTime.Now).PastaConsultaLoteEnvio();

            var arquivoSalvar = Path.Combine(caminhoXml, new StringBuilder(consReciCTe.nRec).Append("-ped-rec.xml").ToString());

            FuncoesXml.ClasseParaArquivoXml(consReciCTe, arquivoSalvar);
        }

        public static XmlDocument CriaRequestWs(this consReciCTe consReciCTe)
        {
            var request = new XmlDocument();
            request.LoadXml(consReciCTe.ObterXmlString());

            return request;
        }


        // Salvar Retorno de Envio de Recibo
        public static void SalvarXmlEmDisco(this retConsReciCTe retConsReciCTe, DFeConfig config)
        {
            if (config.NaoSalvarXml()) return;

            var caminhoXml = new ResolvePasta(config, DateTime.Now).PastaConsultaLoteRetorno();

            var arquivoSalvar = Path.Combine(caminhoXml, new StringBuilder(retConsReciCTe.nRec).Append("-rec.xml").ToString());

            FuncoesXml.ClasseParaArquivoXml(retConsReciCTe, arquivoSalvar);
        }
    }
}