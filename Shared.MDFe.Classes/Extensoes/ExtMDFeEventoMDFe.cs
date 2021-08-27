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

using DFe.Utils;
using DFe.Utils.Assinatura;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using System.IO;
using System.Xml;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEventoMDFe
    {
        public static void ValidarSchema(this MDFeEventoMDFe evento)
        {
            var xmlValido = evento.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValido, "eventoMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValido, "eventoMDFe_v3.00.xsd");
                    break;
            }

            var tipoEvento = evento.InfEvento.DetEvento.EventoContainer.GetType();

            if (tipoEvento == typeof(MDFeEvCancMDFe))
            {
                var objetoXml = (MDFeEvCancMDFe)evento.InfEvento.DetEvento.EventoContainer;
                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvEncMDFe))
            {
                var objetoXml = (MDFeEvEncMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvIncCondutorMDFe))
            {
                var objetoXml = (MDFeEvIncCondutorMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(MDFeEvIncDFeMDFe))
            {
                var objetoXml = (MDFeEvIncDFeMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }

            if (tipoEvento == typeof(evPagtoOperMDFe))
            {
                var objetoXml = (evPagtoOperMDFe)evento.InfEvento.DetEvento.EventoContainer;

                objetoXml.ValidaSchema();
            }
        }

        public static XmlDocument CriaXmlRequestWs(this MDFeEventoMDFe evento)
        {
            var xmlRequest = new XmlDocument();
            xmlRequest.LoadXml(evento.XmlString());

            return xmlRequest;
        }

        public static string XmlString(this MDFeEventoMDFe evento)
        {
            return FuncoesXml.ClasseParaXmlString(evento);
        }

        public static void Assinar(this MDFeEventoMDFe evento)
        {
            evento.Signature = AssinaturaDigital.Assina(evento, evento.InfEvento.Id,
                MDFeConfiguracao.X509Certificate2);
        }

        public static void SalvarXmlEmDisco(this MDFeEventoMDFe evento, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, chave + "-ped-eve.xml");

            FuncoesXml.ClasseParaArquivoXml(evento, arquivoSalvar);
        }

    }
}