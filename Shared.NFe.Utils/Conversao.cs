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
using System.Security.Cryptography;
using System.Text;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils
{
    public static class Conversao
    {
        public static string VersaoServicoParaString(this ServicoNFe servicoNFe, VersaoServico? versaoServico)
        {

            if (servicoNFe == ServicoNFe.NfeConsultaCadastro && versaoServico != VersaoServico.Versao100)
            {
                return "2.00";
            }

            if (servicoNFe == ServicoNFe.RecepcaoEventoCancelmento
                || servicoNFe == ServicoNFe.RecepcaoEventoCartaCorrecao
                || servicoNFe == ServicoNFe.RecepcaoEventoManifestacaoDestinatario
                || servicoNFe == ServicoNFe.RecepcaoEventoEpec)
            {
                return "1.00";
            }

            switch (versaoServico)
            {
                case VersaoServico.Versao100:
                    switch (servicoNFe)
                    {
                        case ServicoNFe.NFeDistribuicaoDFe:
                            return "1.01";
                    }
                    return "1.00";
                case VersaoServico.Versao200:
                    switch (servicoNFe)
                    {
                        case ServicoNFe.NfeConsultaProtocolo:
                            return "2.01";
                    }
                    return "2.00";
                case VersaoServico.Versao310:
                    return "3.10";
                case VersaoServico.Versao400:
                    return "4.00";
            }
            return "";
        }

        public static string TpAmbParaString(this TipoAmbiente tpAmb)
        {
            switch (tpAmb)
            {
                case TipoAmbiente.Homologacao:
                    return "Homologação";
                case TipoAmbiente.Producao:
                    return "Produção";
                default:
                    throw new ArgumentOutOfRangeException("tpAmb", tpAmb, null);
            }
        }

        public static string VersaoServicoParaString(this VersaoServico versao)
        {
            switch (versao)
            {
                case VersaoServico.Versao100:
                    return "1.00";
                case VersaoServico.Versao200:
                    return "2.00";
                case VersaoServico.Versao310:
                    return "3.10";
                case VersaoServico.Versao400:
                    return "4.00";
            }
            return null;
        }

        public static VersaoServico StringParaVersaoServico(string versaoServico)
        {
            switch (versaoServico)
            {
                case "1.00":
                    return VersaoServico.Versao100;
                case "2.00":
                    return VersaoServico.Versao200;
                case "3.10":
                    return VersaoServico.Versao310;
                case "4.00":
                    return VersaoServico.Versao400;
                default:
                    throw new ArgumentOutOfRangeException("versaoServico", versaoServico, null);
            }
        }

        public static string TipoEmissaoParaString(this TipoEmissao tipoEmissao)
        {
            var s = Enum.GetName(typeof (TipoEmissao), tipoEmissao);
            return s != null ? s.Substring(2) : "";
        }

        public static string CrtParaString(this CRT crt)
        {
            switch (crt)
            {
                case CRT.SimplesNacional:
                    return "Simples Nacional";
                case CRT.SimplesNacionalExcessoSublimite:
                    return "Simples Nacional - sublimite excedido";
                case CRT.RegimeNormal:
                    return "Normal";
                default:
                    throw new ArgumentOutOfRangeException("crt", crt, null);
            }
        }

        public static string ModeloDocumentoParaString(this ModeloDocumento modelo)
        {
            switch (modelo)
            {
                case ModeloDocumento.NFe:
                    return "NF-e";
                case ModeloDocumento.NFCe:
                    return "NFC-e";
                case ModeloDocumento.MDFe:
                    return "MDF-e";
            }
            return null;
        }

        public static int ModeloDocumentoParaInt(this ModeloDocumento modelo)
        {
            return (int) modelo;
        }

        public static string CsticmsParaString(this Csticms csticms)
        {
            switch (csticms)
            {
                case Csticms.Cst00:
                    return "00";
                case Csticms.Cst10:
                case Csticms.CstPart10:
                    return "10";
                case Csticms.Cst20:
                    return "20";
                case Csticms.Cst30:
                    return "30";
                case Csticms.Cst40:
                    return "40";
                case Csticms.Cst41:
                case Csticms.CstRep41:
                    return "41";
                case Csticms.Cst50:
                    return "50";
                case Csticms.Cst51:
                    return "51";
                case Csticms.Cst60:
                case Csticms.CstRep60:
                    return "60";
                case Csticms.Cst70:
                    return "70";
                case Csticms.Cst90:
                case Csticms.CstPart90:
                    return "90";
                default:
                    throw new ArgumentOutOfRangeException("csticms", csticms, null);
            }
        }

        public static string CsosnicmsParaString(this Csosnicms csosnicms)
        {
            return ((int) csosnicms).ToString();
        }

        public static string OrigemMercadoriaParaString(this OrigemMercadoria origemMercadoria)
        {
            return ((int)origemMercadoria).ToString();
        }

        /// <summary>
        /// Obtém uma <see cref="string"/> SHA1, no formato hexadecimal da <see cref="string"/> passada no parâmero        
        /// </summary>
        public static string ObterHexSha1DeString(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(bytes);

            return ObterHexDeByteArray(hashBytes);
        }

        /// <summary>
        /// Obtém uma string Hexadecimal do array de bytes passado no parâmetro
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ObterHexDeByteArray(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Obtém uma string Hexadecimal de uma string passada no parâmetro
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ObterHexDeString(string s)
        {
            var hex = "";
            foreach (var c in s)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
    }
}