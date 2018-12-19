﻿/********************************************************************************/
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
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CTe.Utils.Recepcao;
using DFe.Classes.Flags;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Recepcao
{
    public class ServicoCTeRecepcao
    {
        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList)
        {
            var instanciaConfiguracao = ConfiguracaoServico.Instancia;

            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList);

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao)
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
                cte.infCte.ide.tpEmis = instanciaConfiguracao.TipoEmissao;
                cte.Assina();
                cte.ValidaSchema();
                cte.SalvarXmlEmDisco();
            }

            enviCte.ValidaSchema();
            enviCte.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteRecepcao();

            OnAntesDeEnviar(enviCte);

            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs());

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }

        protected virtual void OnAntesDeEnviar(enviCTe enviCTe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesEnviarRecepcao(enviCTe));
        }
    }
}