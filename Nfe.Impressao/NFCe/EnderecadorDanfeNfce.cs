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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;

namespace NFe.Impressao.NFCe
{
    internal class EnderecoNfceDanfe
    {
        public EnderecoNfceDanfe(Estado estado, TipoAmbiente tipoAmbiente, TipoUrlDanfeNfce tipoUrlDanfeNfce, string url)
        {
            TipoAmbiente = tipoAmbiente;
            Estado = estado;
            TipoUrlDanfeNfce = tipoUrlDanfeNfce;
            Url = url;
        }

        public TipoAmbiente TipoAmbiente { get; protected set; }
        public Estado Estado { get; protected set; }
        public TipoUrlDanfeNfce TipoUrlDanfeNfce { get; protected set; }
        public string Url { get; protected set; }
    }

    /// <summary>
    ///     Classe reponsável pelas regras de obtenção do endereço de consulta da NFCe pelo site e via QR-Code
    /// </summary>
    public static class EnderecadorDanfeNfce
    {
        private static readonly List<EnderecoNfceDanfe> EndQrCodeNfce;

        static EnderecadorDanfeNfce()
        {
            EndQrCodeNfce = CarregarUrls();
        }

        private static List<EnderecoNfceDanfe> CarregarUrls()
        {
            var endQrCodeNfce = new List<EnderecoNfceDanfe>
            {
                new EnderecoNfceDanfe(Estado.AC, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www.sefaznet.ac.gov.br/nfce/qrcode"),
                new EnderecoNfceDanfe(Estado.AC, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.sefaznet.ac.gov.br/nfce/"),
                new EnderecoNfceDanfe(Estado.AC, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://hml.sefaznet.ac.gov.br/nfce/qrcode"),
                new EnderecoNfceDanfe(Estado.AC, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://hml.sefaznet.ac.gov.br/nfce/"),

                new EnderecoNfceDanfe(Estado.AM, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://sistemas.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.AM, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://sistemas.sefaz.am.gov.br/nfceweb/formConsulta.do"),
                new EnderecoNfceDanfe(Estado.AM, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://homnfce.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.AM, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://homnfce.sefaz.am.gov.br/nfceweb/formConsulta.do"),

                new EnderecoNfceDanfe(Estado.BA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://nfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"),
                new EnderecoNfceDanfe(Estado.BA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://nfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"),
                new EnderecoNfceDanfe(Estado.BA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://hnfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"),
                new EnderecoNfceDanfe(Estado.BA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://nfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"),

                new EnderecoNfceDanfe(Estado.DF, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"),
                new EnderecoNfceDanfe(Estado.DF, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://dec.fazenda.df.gov.br/nfce"),
                new EnderecoNfceDanfe(Estado.DF, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"),
                new EnderecoNfceDanfe(Estado.DF, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://dec.fazenda.df.gov.br/nfce"),

                new EnderecoNfceDanfe(Estado.MA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.MA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.nfce.sefaz.ma.gov.br/portal/consultaNFe.do"),
                new EnderecoNfceDanfe(Estado.MA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.MA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultaNFe.do"),

                new EnderecoNfceDanfe(Estado.MT, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoNfceDanfe(Estado.MT, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoNfceDanfe(Estado.MT, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoNfceDanfe(Estado.MT, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),

                new EnderecoNfceDanfe(Estado.PA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"),
                new EnderecoNfceDanfe(Estado.PA, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "https://appnfc.sefa.pa.gov.br/portal/view/consultas/nfce/consultanfce.seam"),
                new EnderecoNfceDanfe(Estado.PA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"),
                new EnderecoNfceDanfe(Estado.PA, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/consultanfce.seam"),

                new EnderecoNfceDanfe(Estado.PB, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "https://www5.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoNfceDanfe(Estado.PB, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "https://www5.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoNfceDanfe(Estado.PB, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "https://www6.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoNfceDanfe(Estado.PB, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "https://www6.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),

                new EnderecoNfceDanfe(Estado.PI, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://webas.sefaz.pi.gov.br/nfceweb/consultarNFCe.jsf"),
                new EnderecoNfceDanfe(Estado.PI, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://webas.sefaz.pi.gov.br/nfceweb/consultarNFCe.jsf"),
                new EnderecoNfceDanfe(Estado.PI, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"),
                new EnderecoNfceDanfe(Estado.PI, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"),

                new EnderecoNfceDanfe(Estado.PR, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe"),
                new EnderecoNfceDanfe(Estado.PR, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.fazenda.pr.gov.br/"),
                new EnderecoNfceDanfe(Estado.PR, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe"),
                new EnderecoNfceDanfe(Estado.PR, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://www.fazenda.pr.gov.br/"),

                new EnderecoNfceDanfe(Estado.RJ, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
                new EnderecoNfceDanfe(Estado.RJ, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://nfce.fazenda.rj.gov.br/consulta"),
                new EnderecoNfceDanfe(Estado.RJ, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
                new EnderecoNfceDanfe(Estado.RJ, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://nfce.fazenda.rj.gov.br/consulta"),

                new EnderecoNfceDanfe(Estado.RN, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"),
                new EnderecoNfceDanfe(Estado.RN, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"),
                new EnderecoNfceDanfe(Estado.RN, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://nfce.set.rn.gov.br/portalDFE/NFCe/ConsultaNFCe.aspx"),
                new EnderecoNfceDanfe(Estado.RN, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"),

                new EnderecoNfceDanfe(Estado.RO, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
                new EnderecoNfceDanfe(Estado.RO, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.nfce.sefin.ro.gov.br"),
                new EnderecoNfceDanfe(Estado.RO, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
                new EnderecoNfceDanfe(Estado.RO, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://www.nfce.sefin.ro.gov.br/consultaAmbHomologacao.jsp"),

                new EnderecoNfceDanfe(Estado.RS, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
                new EnderecoNfceDanfe(Estado.RS, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "https://www.sefaz.rs.gov.br/NFE/NFE-NFC.aspx"),
                new EnderecoNfceDanfe(Estado.RS, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
                new EnderecoNfceDanfe(Estado.RS, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "https://www.sefaz.rs.gov.br/NFE/NFE-NFC.aspx"),

                new EnderecoNfceDanfe(Estado.RR, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "https://www.sefaz.rr.gov.br/nfce/servlet/wp_consulta_nfce"),
                new EnderecoNfceDanfe(Estado.RR, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://200.174.88.103:8080/nfce/servlet/wp_consulta_nfce"),

                new EnderecoNfceDanfe(Estado.SE, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "http://www.nfe.se.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.SE, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "http://www.nfe.se.gov.br/portal/portalNoticias.jsp?jsp=barra-menu/servicos/consultaDANFENFCe.htm"),
                new EnderecoNfceDanfe(Estado.SE, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "http://www.hom.nfe.se.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoNfceDanfe(Estado.SE, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "http://www.hom.nfe.se.gov.br/portal/portalNoticias.jsp?jsp=barra-menu/servicos/consultaDANFENFCe.htm"),

                new EnderecoNfceDanfe(Estado.SP, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlQrCode, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
                new EnderecoNfceDanfe(Estado.SP, TipoAmbiente.taProducao, TipoUrlDanfeNfce.UrlConsulta, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaPublica.aspx"),
                new EnderecoNfceDanfe(Estado.SP, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlQrCode, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
                new EnderecoNfceDanfe(Estado.SP, TipoAmbiente.taHomologacao, TipoUrlDanfeNfce.UrlConsulta, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaPublica.aspx"),

            };

            return endQrCodeNfce;
        }

        /// <summary>
        ///     Obtém a URL para uso no DANFE da NFCe
        /// </summary>
        /// <param name="tipoAmbiente"></param>
        /// <param name="estado"></param>
        /// <param name="tipoUrlDanfeNfce"></param>
        /// <returns></returns>
        public static string ObterUrl(TipoAmbiente tipoAmbiente, Estado estado, TipoUrlDanfeNfce tipoUrlDanfeNfce)
        {
            var query = from qr in EndQrCodeNfce where qr.TipoAmbiente == tipoAmbiente && qr.Estado == estado && qr.TipoUrlDanfeNfce == tipoUrlDanfeNfce select qr.Url;
            var listaRetorno = query as IList<string> ?? query.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(string.Format("Não foi possível obter o {0}, para o Estado {1}, ambiente: {2}", tipoUrlDanfeNfce.Descricao(), estado, tipoAmbiente.Descricao()));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrl obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }

        /// <summary>
        ///     Obtém a URL para uso no QR-Code
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="configuracaoDanfeNfce"></param>
        /// <returns></returns>
        public static string ObterUrlQrCode(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce)
        {
            //Passo 1: Converter o valor da Data e Hora de Emissão da NFC-e (dhEmi) para HEXA;
            var dhEmi = StringParaHex(proc.NFe.infNFe.ide.dhEmi);

            //Passo 2: Converter o valor do Digest Value da NFC-e (digVal) para HEXA;
            //Ao se efetuar a assinatura digital da NFCe emitida em contingência off-line, o campo digest value constante da XMl Signature deve obrigatoriamente ser idêntico ao encontrado quando da geração do digest value para a montagem QR Code.
            //Ver página 18 do Manual de Padrões Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2
            var digVal = StringParaHex(proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teNormal ? proc.protNFe.infProt.digVal : proc.NFe.Signature.SignedInfo.Reference.DigestValue);

            //Na hipótese do consumidor não se identificar, não existirá o parâmetro cDest no QR Code;
            var cDest = "";
            if (proc.NFe.infNFe.dest != null)
                cDest = "&cDest=" + proc.NFe.infNFe.dest.CPF + proc.NFe.infNFe.dest.CNPJ + proc.NFe.infNFe.dest.idEstrangeiro;

            //Passo 3: Substituir os valores (“dhEmi” e “digVal”) nos parâmetros;
            var dadosBase = "chNFe=" + proc.NFe.infNFe.Id.Substring(3) + "&nVersao=100&tpAmb=" + ((int) proc.NFe.infNFe.ide.tpAmb) + cDest + "&dhEmi=" + dhEmi + "&vNF=" +
                            proc.NFe.infNFe.total.ICMSTot.vNF.ToString("0.00").Replace(',', '.') + "&vICMS=" + proc.NFe.infNFe.total.ICMSTot.vICMS.ToString("0.00").Replace(',', '.') + "&digVal=" + digVal + "&cIdToken=" +
                            configuracaoDanfeNfce.cIdToken;

            //Passo 4: Adicionar, ao final dos parâmetros, o CSC (CSC do contribuinte disponibilizado pela SEFAZ do Estado onde a empresa esta localizada):
            var dadosParaSh1 = dadosBase + configuracaoDanfeNfce.CSC;

            //Passo 5: Aplicar o algoritmo SHA-1 sobre todos os parâmetros concatenados. Asaída do algoritmo SHA-1 deve ser em HEXADECIMAL.
            var sha1ComCsc = ObterHashSha1(dadosParaSh1);

            //Passo 6: Adicione o resultado sem o CSC e gere a imagem do QR Code: 1º parte (endereço da consulta) +2º parte (tabela 3 com indicação SIM na última coluna).
            return ObterUrl(proc.NFe.infNFe.ide.tpAmb, proc.NFe.infNFe.ide.cUF, TipoUrlDanfeNfce.UrlQrCode) + "?" + dadosBase + "&cHashQRCode=" + sha1ComCsc;
        }

        private static string ObterHashSha1(string data)
        {
            var sha1 = SHA1.Create();
            var hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            var returnValue = new StringBuilder();

            foreach (var t in hashData)
                returnValue.Append(t.ToString());

            return returnValue.ToString();
        }

        private static string StringParaHex(string asciiString)
        {
            var hex = "";
            foreach (var c in asciiString)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
    }
}
