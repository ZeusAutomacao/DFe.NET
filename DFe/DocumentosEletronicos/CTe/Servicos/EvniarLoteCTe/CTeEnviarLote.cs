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
using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Tipos;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Servicos.Factory;
using DFe.DocumentosEletronicos.CTe.Servicos.Recepcao;
using DFe.Ext;
using DFe.Flags;
using CTeEletronico = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.CTe;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe
{
    public class CTeEnviarLote
    {
        private readonly DFeConfig _dfeConfig;
        private readonly CertificadoDigital _certificadoDigital;

        public CTeEnviarLote(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            _dfeConfig = dfeConfig;
            _certificadoDigital = certificadoDigital;
        }

        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retEnviCte EnviarLote(int lote, List<CTeEletronico> cteEletronicosList)
        {
            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList, _dfeConfig);

            switch (_dfeConfig.VersaoServico)
            {
                case VersaoServico.Versao200:
                    foreach (var cte in cteEletronicosList)
                    {
                        cte.infCte.ide.ProxydhEmi = cte.infCte.ide.dhEmi.ParaDataHoraStringSemUtc();
                        cte.infCte.versao = VersaoServico.Versao200;
                        cte.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM200;
                    }
                    break;
                case VersaoServico.Versao300:
                    foreach (var cte in cteEletronicosList)
                    {
                        cte.infCte.ide.ProxydhEmi = cte.infCte.ide.dhEmi.ParaDataHoraStringUtc();
                        cte.infCte.versao = VersaoServico.Versao300;
                        cte.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_dfeConfig.TipoAmbiente == TipoAmbiente.Homologacao)
            {
                foreach (var cte in enviCte.CTe)
                {
                    const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                    cte.infCte.rem.xNome = razaoSocial;
                    cte.infCte.dest.xNome = razaoSocial;
                }
            }


            foreach (var cte in enviCte.CTe)
            {
                cte.Assina(_dfeConfig, _certificadoDigital);
                cte.ValidaSchema(_dfeConfig);
                cte.SalvarXmlEmDisco(_dfeConfig);
            }

            enviCte.ValidaSchema(_dfeConfig);
            enviCte.SalvarXmlEmDisco(_dfeConfig);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(_dfeConfig, _certificadoDigital);

            OnAntesDeEnviar(enviCte);

            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs(_dfeConfig));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);

            retorno.SalvarXmlEmDisco(_dfeConfig);

            return retorno;
        }

        protected virtual void OnAntesDeEnviar(enviCTe enviCTe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesEnviarRecepcao(enviCTe));
        }
    }
}