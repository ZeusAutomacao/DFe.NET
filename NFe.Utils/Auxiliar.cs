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
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils
{
    public class Auxiliar
    {
        public static string VersaoServicoParaString(ServicoNFe servicoNFe, VersaoServico? versaoServico)
        {
            switch (versaoServico)
            {
                case VersaoServico.ve100:
                    return "1.00";
                case VersaoServico.ve200:
                    switch (servicoNFe)
                    {
                        case ServicoNFe.NfeConsultaProtocolo:
                            return "2.01";
                    }
                    return "2.00";
                case VersaoServico.ve310:
                    return "3.10";
            }
            return "";
        }

        public static string tpAmbParaString(TipoAmbiente? tpAmb)
        {
            switch (tpAmb)
            {
                case TipoAmbiente.taHomologacao:
                    return "Homologação";
                case TipoAmbiente.taProducao:
                    return "Produção";
            }
            return "";
        }

        public static string VersaoServicoParaString(VersaoServico versao)
        {
            switch (versao)
            {
                case VersaoServico.ve100:
                    return "1.00";
                case VersaoServico.ve200:
                    return "2.00";
                case VersaoServico.ve310:
                    return "3.10";
            }
            return null;
        }

        public static string TipoEmissaoParaString(TipoEmissao tipoEmissao)
        {
            var s = Enum.GetName(typeof (TipoEmissao), tipoEmissao);
            return s != null ? s.Substring(2) : "";
        }
    }
}