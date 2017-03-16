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
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Classes.Servicos.Recepcao;
using CTeDLL.Classes.Servicos.Recepcao.Retorno;
using CTeDLL.Classes.Servicos.Status;
using CTeDLL.Servicos.Inutilizacao;
using DFe.Classes.Extencoes;
using CTeEletronica = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Factory
{
    public class ClassesFactory
    {
        public static consStatServCte CriaConsStatServCte()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consStatServCte
            {
                versao = configuracaoServico.VersaoLayout,
                tpAmb = configuracaoServico.tpAmb
            };
        }

        public static consSitCTe CriarconsSitCTe(string chave)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consSitCTe
            {
                tpAmb = configuracaoServico.tpAmb,
                versao = configuracaoServico.VersaoLayout,
                chCTe = chave
            };
        }

        public static inutCTe CriaInutCTe(ConfigInutiliza configInutiliza)
        {
            if (configInutiliza == null) throw new ArgumentNullException("Preciso de uma configuração de inutilização");

            var configuracaoServico = ConfiguracaoServico.Instancia;

            var id = new StringBuilder("ID");
            id.Append(configuracaoServico.cUF.GetCodigoIbgeEmString());
            id.Append(configInutiliza.Cnpj);
            id.Append((byte) configInutiliza.ModeloDocumento);
            id.Append(configInutiliza.Serie.ToString("D3"));
            id.Append(configInutiliza.NumeroInicial.ToString("D9"));
            id.Append(configInutiliza.NumeroFinal.ToString("D9"));

            return new inutCTe
            {
                versao = configuracaoServico.VersaoLayout,
                infInut = new infInutEnv
                {
                    Id = id.ToString(),
                    tpAmb = configuracaoServico.tpAmb,
                    cUF = configuracaoServico.cUF,
                    CNPJ = configInutiliza.Cnpj,
                    ano = configInutiliza.Ano,
                    nCTIni = configInutiliza.NumeroInicial,
                    nCTFin = configInutiliza.NumeroInicial,
                    mod = configInutiliza.ModeloDocumento,
                    serie = configInutiliza.Serie,
                    xJust = configInutiliza.Justificativa,
                }
            };
        }

        public static consReciCTe CriaConsReciCTe(string recibo)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consReciCTe
            {
                tpAmb = configuracaoServico.tpAmb,
                versao = configuracaoServico.VersaoLayout,
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

        public static enviCTe CriaEnviCTe(int lote, List<CTeEletronica> cteEletronicoList)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new enviCTe(configuracaoServico.VersaoLayout, lote, cteEletronicoList);
        }
    }
}