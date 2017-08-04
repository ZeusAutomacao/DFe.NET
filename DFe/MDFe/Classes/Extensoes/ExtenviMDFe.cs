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
using System.Linq;
using System.Text;
using System.Xml;
using DFe.Configuracao;
using DFe.ManipuladorDeXml;
using DFe.MDFe.Classes.Flags;
using DFe.MDFe.Classes.Servicos.Autorizacao;
using DFe.MDFe.Validacao;
using DFe.ResolvePastas;
using DFe.Utils;

namespace DFe.MDFe.Classes.Extensoes
{
    public static class ExtenviMDFe
    {
        public static void Valida(this enviMDFe enviMDFe, DFeConfig dfeConfig)
        {
            if (enviMDFe == null) throw new ArgumentException("Erro de assinatura, EnviMDFe esta null");

            var xmlMdfe = FuncoesXml.ClasseParaXmlString(enviMDFe);

            switch (dfeConfig.VersaoServico)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlMdfe, "enviMDFe_v1.00.xsd", dfeConfig);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlMdfe, "enviMDFe_v3.00.xsd", dfeConfig);
                    break;
            }

            enviMDFe.MDFe.Valida(dfeConfig);
        }

        public static XmlDocument CriaXmlRequestWs(this enviMDFe enviMDFe)
        {
            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(enviMDFe.XmlString());

            return dadosEnvio;
        }

        public static string XmlString(this enviMDFe enviMDFe)
        {
            var xmlString = FuncoesXml.ClasseParaXmlString(enviMDFe);

            return xmlString;
        }

        public static void SalvarXmlEmDisco(this enviMDFe enviMDFe, DFeConfig dfeConfig)
        {
            if (dfeConfig.NaoSalvarXml()) return;

            var caminhoXml = new ResolvePasta(dfeConfig, enviMDFe.MDFe.InfMDFe.ide.dhEmi).PastaEnviar();

            var arquivoSalvar = caminhoXml + @"\" + enviMDFe.MDFe.Chave() + "-mdfe.xml";

            FuncoesXml.ClasseParaArquivoXml(enviMDFe, arquivoSalvar);

            enviMDFe.MDFe.SalvarXmlEmDisco(dfeConfig);
        }
    }
}