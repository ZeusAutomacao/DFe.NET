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

using System.Xml;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.MDFe.Classes.Flags;
using DFe.MDFe.Classes.Servicos.Evento;
using DFe.MDFe.Classes.Servicos.Evento.CorpoEvento;
using DFe.MDFe.Validacao;
using DFe.Utils;
using DFe.Utils.Assinatura;
using CertificadoDigital = DFe.CertificadosDigitais.CertificadoDigital;

namespace DFe.MDFe.Classes.Extensoes
{
    public static class ExteventoMDFe
    {
        public static void ValidarSchema(this eventoMDFe evento, DFeConfig dfeConfig)
        {
            var xmlValido = evento.XmlString();

            switch (dfeConfig.VersaoServico)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValido, "eventoMDFe_v1.00.xsd", dfeConfig);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValido, "eventoMDFe_v3.00.xsd", dfeConfig);
                    break;
            }

            var tipoEvento = evento.infEvento.detEvento.EventoContainer.GetType();

            if (tipoEvento == typeof (evCancMDFe))
            {
                var objetoXml = (evCancMDFe) evento.infEvento.detEvento.EventoContainer;
                objetoXml.ValidaSchema(dfeConfig);
            }

            if (tipoEvento == typeof (evEncMDFe))
            {
                var objetoXml = (evEncMDFe)evento.infEvento.detEvento.EventoContainer;

                objetoXml.ValidaSchema(dfeConfig);
            }

            if (tipoEvento == typeof (evIncCondutorMDFe))
            {
                var objetoXml = (evIncCondutorMDFe)evento.infEvento.detEvento.EventoContainer;

                objetoXml.ValidaSchema(dfeConfig);
            }


        }

        public static XmlDocument CriaXmlRequestWs(this eventoMDFe evento)
        {
            var xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(evento.XmlString());

            return xmlRequest;
        }

        public static string XmlString(this eventoMDFe evento)
        {
            return FuncoesXml.ClasseParaXmlString(evento);
        }

        public static void Assinar(this eventoMDFe evento, CertificadoDigital certificadoDigital)
        {
            evento.Signature = AssinaturaDigital.Assina(evento, evento.infEvento.Id,
                certificadoDigital);
        }

        public static void SalvarXmlEmDisco(this eventoMDFe evento, string chave, DFeConfig dfeConfig)
        {
            if (dfeConfig.NaoSalvarXml()) return;

            var caminhoXml = dfeConfig.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-ped-eve.xml";

            FuncoesXml.ClasseParaArquivoXml(evento, arquivoSalvar);
        }

    }
}