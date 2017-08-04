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
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.MDFe.Servicos.Factory;
using DFe.DocumentosEletronicos.MDFe.Servicos.RecepcaoMDFe;
using DFe.Ext;
using MDFeEletronico = DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.MDFe;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.EnviarLoteMDFe
{
    public class MDFeEnviarLote
    {
        public CertificadoDigital CertificadoDigital { get; }
        private readonly DFeConfig _dfeConfig;

        public MDFeEnviarLote(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            CertificadoDigital = certificadoDigital;
            _dfeConfig = dfeConfig;
        }

        public event EventHandler<AntesDeEnviar> AntesDeEnviar; 

        public retEnviMDFe EnviarLote(long lote, MDFeEletronico mdfe)
        {
            var enviMDFe = ClassesFactory.CriaEnviMDFe(lote, mdfe, _dfeConfig);

            mdfe.InfMDFe.versao = _dfeConfig.VersaoServico;

            switch (mdfe.InfMDFe.versao)
            {
                case VersaoServico.Versao100:
                    mdfe.InfMDFe.infModal.versaoModal = versaoModal.Versao100;
                    mdfe.InfMDFe.ide.ProxydhEmi = mdfe.InfMDFe.ide.dhEmi.ParaDataHoraStringSemUtc();
                    break;
                case VersaoServico.Versao300:
                    mdfe.InfMDFe.infModal.versaoModal = versaoModal.Versao300;
                    mdfe.InfMDFe.ide.ProxydhEmi = mdfe.InfMDFe.ide.dhEmi.ParaDataHoraStringUtc();
                    break;
            }

            enviMDFe.MDFe.Assina(_dfeConfig, CertificadoDigital);

            enviMDFe.Valida(_dfeConfig);

            enviMDFe.SalvarXmlEmDisco(_dfeConfig);

            var webService = WsdlFactory.CriaWsdlMDFeRecepcao(_dfeConfig, CertificadoDigital);

            OnAntesDeEnviar(enviMDFe);

            var retornoXml = webService.mdfeRecepcaoLote(enviMDFe.CriaXmlRequestWs());

            var retorno = retEnviMDFe.LoadXml(retornoXml.OuterXml, enviMDFe);

            retorno.SalvarXmlEmDisco(_dfeConfig, enviMDFe.MDFe.InfMDFe.emit.CNPJ);

            return retorno;
        }

        protected virtual void OnAntesDeEnviar(enviMDFe enviMdfe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesDeEnviar(enviMdfe));
        }
    }
}