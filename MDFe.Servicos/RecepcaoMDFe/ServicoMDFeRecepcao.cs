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
using DFe.Utils;
using MDFe.Classes.Extencoes;
using MDFe.Classes.Flags;
using MDFe.Classes.Retorno.MDFeRecepcao;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.RecepcaoMDFe
{
    public class ServicoMDFeRecepcao
    {
        public event EventHandler<AntesDeEnviar> AntesDeEnviar;
        public event EventHandler<string> GerouChave; 

        public MDFeRetEnviMDFe MDFeRecepcao(long lote, MDFeEletronico mdfe)
        {
            var enviMDFe = ClassesFactory.CriaEnviMDFe(lote, mdfe);

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao100;
                    mdfe.InfMDFe.Ide.ProxyDhIniViagem = mdfe.InfMDFe.Ide.DhIniViagem.ParaDataHoraStringSemUtc();
                    break;
                case VersaoServico.Versao300:
                    mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao300;
                    mdfe.InfMDFe.Ide.ProxyDhIniViagem = mdfe.InfMDFe.Ide.DhIniViagem.ParaDataHoraStringUtc();
                    break;
            }

            enviMDFe.MDFe.Assina(GerouChave, this);

            if (MDFeConfiguracao.IsAdicionaQrCode && MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.infMDFeSupl = mdfe.QrCode(MDFeConfiguracao.X509Certificate2);
            }

            enviMDFe.Valida();
            enviMDFe.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlMDFeRecepcao();

            OnAntesDeEnviar(enviMDFe);

            var retornoXml = webService.mdfeRecepcaoLote(enviMDFe.CriaXmlRequestWs());

            var retorno = MDFeRetEnviMDFe.LoadXml(retornoXml.OuterXml, enviMDFe);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }

        protected virtual void OnAntesDeEnviar(MDFeEnviMDFe enviMdfe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesDeEnviar(enviMdfe));
        }
    }
}