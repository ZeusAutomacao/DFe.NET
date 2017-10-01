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
using System.IO;
using System.Xml;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.ManipuladorDeXml;
using DFe.DocumentosEletronicos.ManipulaPasta;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.StatusServico;
using DFe.DocumentosEletronicos.MDFe.Validacao;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Extensoes
{
    public static class ExtconsStatServMDFe
    {
        public static void ValidarSchema(this consStatServMDFe consStatServMDFe, DFeConfig dfeConfig)
        {
            var xmlValidacao = consStatServMDFe.GetXml();

            switch (dfeConfig.VersaoServico)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValidacao, "consStatServMDFe_v1.00.xsd", dfeConfig);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consStatServMDFe_v3.00.xsd", dfeConfig);
                    break;
            }
        }

        public static string GetXml(this consStatServMDFe consStatServMDFe)
        {
            var xml = FuncoesXml.ClasseParaXmlString(consStatServMDFe);

            return xml;
        }

        public static XmlDocument CriaRequestWs(this consStatServMDFe consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.GetXml());

            return request;
        }

        public static void SalvarXmlEmDisco(this consStatServMDFe consStatServMdFe, DFeConfig dfeConfig)
        {
            if (dfeConfig.NaoSalvarXml()) return;

            var caminhoXml = new ResolvePasta(dfeConfig, DateTime.Now).PastaConsultaStatusEnvio();

            var arquivoSalvar = Path.Combine(caminhoXml, "-pedido-status-servico.xml");

            FuncoesXml.ClasseParaArquivoXml(consStatServMdFe, arquivoSalvar);
        }
    }
}