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
using System.Text;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Inutilizacao;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Classes.Servicos.Status;
using CTe.Servicos.Inutilizacao;
using DFe.Classes.Extensoes;
using CTeEletronica = CTe.Classes.CTe;

namespace CTe.Servicos.Factory
{
    public class ClassesFactory
    {
        public static consStatServCte CriaConsStatServCte(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            return new consStatServCte
            {
                versao = configServico.VersaoLayout,
                tpAmb = configServico.tpAmb
            };
        }

        public static consSitCTe CriarconsSitCTe(string chave, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            return new consSitCTe
            {
                tpAmb = configServico.tpAmb,
                versao = configServico.VersaoLayout,
                chCTe = chave
            };
        }

        public static inutCTe CriaInutCTe(ConfigInutiliza configInutiliza, ConfiguracaoServico configuracaoServico = null)
        {
            if (configInutiliza == null) throw new ArgumentNullException("Preciso de uma configuração de inutilização");

            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var id = new StringBuilder("ID");
            id.Append(configServico.cUF.GetCodigoIbgeEmString());
            id.Append(configInutiliza.Cnpj);
            id.Append((byte) configInutiliza.ModeloDocumento);
            id.Append(configInutiliza.Serie.ToString("D3"));
            id.Append(configInutiliza.NumeroInicial.ToString("D9"));
            id.Append(configInutiliza.NumeroFinal.ToString("D9"));

            return new inutCTe
            {
                versao = configServico.VersaoLayout,
                infInut = new infInutEnv
                {
                    Id = id.ToString(),
                    tpAmb = configServico.tpAmb,
                    cUF = configServico.cUF,
                    CNPJ = configInutiliza.Cnpj,
                    ano = configInutiliza.Ano,
                    nCTIni = configInutiliza.NumeroInicial,
                    nCTFin = configInutiliza.NumeroFinal,
                    mod = configInutiliza.ModeloDocumento,
                    serie = configInutiliza.Serie,
                    xJust = configInutiliza.Justificativa,
                }
            };
        }

        public static consReciCTe CriaConsReciCTe(string recibo, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            return new consReciCTe
            {
                tpAmb = configServico.tpAmb,
                versao = configServico.VersaoLayout,
                nRec = recibo
            };
        }

        public static evCancCTe CriaEvCancCTe(string justificativa, string numeroProtocolo)
        {
            return new evCancCTe
            {
                nProt = numeroProtocolo,
                xJust = justificativa
            };
        }

        public static evCCeCTe CriaEvCCeCTe(List<infCorrecao> infCorrecaos)
        {
            return new evCCeCTe
            {
                infCorrecao = infCorrecaos
            };
        }

        public static evPrestDesacordo CriaEvPrestDesacordo(string indicadorDesacordo, string observacao)
        {
            return new evPrestDesacordo
            {
                indDesacordoOper = indicadorDesacordo,
                xObs = observacao
            };
        }

        public static enviCTe CriaEnviCTe(int lote, List<CTeEletronica> cteEletronicoList, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            
            return new enviCTe(configServico.VersaoLayout, lote, cteEletronicoList);
        }
    }
}