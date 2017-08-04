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
using DFe.DocumentosEletronicos.NFe.Classes;
using DFe.DocumentosEletronicos.NFe.Classes.Informacoes.Identificacao.Tipos;
using DFe.Entidades;
using DFe.Ext;

namespace DFe.DocumentosEletronicos.NFe.Utils.InformacoesSuplementares
{
    internal class EnderecoConsultaPublicaNfce
    {
        public EnderecoConsultaPublicaNfce(Estado estado, TipoAmbiente tipoAmbiente, TipoUrlConsultaPublica tipoUrlConsultaPublica, string url)
        {
            TipoAmbiente = tipoAmbiente;
            Estado = estado;
            TipoUrlConsultaPublica = tipoUrlConsultaPublica;
            Url = url;
        }

        public TipoAmbiente TipoAmbiente { get; protected set; }
        public Estado Estado { get; protected set; }
        public TipoUrlConsultaPublica TipoUrlConsultaPublica { get; protected set; }
        public string Url { get; protected set; }
    }

    /// <summary>
    ///     Classe reponsável pelas regras de obtenção do endereço de consulta da NFCe pelo site e via QR-Code
    /// </summary>
    public static class ExtinfNFeSupl
    {
        private static readonly List<EnderecoConsultaPublicaNfce> EndQrCodeNfce;

        static ExtinfNFeSupl()
        {
            EndQrCodeNfce = CarregarUrls();
        }

        private static List<EnderecoConsultaPublicaNfce> CarregarUrls()
        {
            var endQrCodeNfce = new List<EnderecoConsultaPublicaNfce>
            {
                new EnderecoConsultaPublicaNfce(Estado.AC, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://hml.sefaznet.ac.gov.br/nfce/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.AC, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.sefaznet.ac.gov.br/nfce/"),
                new EnderecoConsultaPublicaNfce(Estado.AC, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://hml.sefaznet.ac.gov.br/nfce/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.AC, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://hml.sefaznet.ac.gov.br/nfce/"),

                new EnderecoConsultaPublicaNfce(Estado.AL, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.AL, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.sefaz.al.gov.br/consultaNFCe.htm"),
                new EnderecoConsultaPublicaNfce(Estado.AL, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.AL, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.sefaz.al.gov.br/consultaNFCe.htm"),

                new EnderecoConsultaPublicaNfce(Estado.AM, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://sistemas.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.AM, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://sistemas.sefaz.am.gov.br/nfceweb/formConsulta.do"),
                new EnderecoConsultaPublicaNfce(Estado.AM, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://homnfce.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.AM, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://homnfce.sefaz.am.gov.br/nfceweb/formConsulta.do"),

                new EnderecoConsultaPublicaNfce(Estado.AP, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://www.sefaz.ap.gov.br/nfce/nfcep.php"),
                new EnderecoConsultaPublicaNfce(Estado.AP, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://www.sefaz.ap.gov.br/sate/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1261"),
                new EnderecoConsultaPublicaNfce(Estado.AP, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "https://www.sefaz.ap.gov.br/nfcehml/nfce.php"),
                new EnderecoConsultaPublicaNfce(Estado.AP, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "https://www.sefaz.ap.gov.br/sate1/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1261"),

                new EnderecoConsultaPublicaNfce(Estado.BA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://nfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.BA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://nfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.BA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://hnfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.BA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://nfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"),

                new EnderecoConsultaPublicaNfce(Estado.DF, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.DF, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://dec.fazenda.df.gov.br/nfce"),
                new EnderecoConsultaPublicaNfce(Estado.DF, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.DF, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://dec.fazenda.df.gov.br/nfce"),

                new EnderecoConsultaPublicaNfce(Estado.GO, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://nfe.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.GO, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.go.gov.br/post/ver/214278/consumid"),
                new EnderecoConsultaPublicaNfce(Estado.GO, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://homolog.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.GO, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.go.gov.br/post/ver/214413/consulta-nfc-e-homologacao"),

                new EnderecoConsultaPublicaNfce(Estado.ES, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://app.sefaz.es.gov.br/ConsultaNFCe/qrcode.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.ES, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://app.sefaz.es.gov.br/ConsultaNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.ES, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://homologacao.sefaz.es.gov.br/ConsultaNFCe/qrcode.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.ES, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://homologacao.sefaz.es.gov.br/ConsultaNFCe"),

                new EnderecoConsultaPublicaNfce(Estado.MA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.MA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.sefaz.ma.gov.br/portal/consultaNFe.do"),
                new EnderecoConsultaPublicaNfce(Estado.MA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.MA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultaNFe.do"),

                new EnderecoConsultaPublicaNfce(Estado.MS, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.dfe.ms.gov.br/nfce/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.MS, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.dfe.ms.gov.br/nfce/chavedeacesso"),
                new EnderecoConsultaPublicaNfce(Estado.MS, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www.dfe.ms.gov.br/nfce/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.MS, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.dfe.ms.gov.br/nfce/chavedeacesso"),

                new EnderecoConsultaPublicaNfce(Estado.MT, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoConsultaPublicaNfce(Estado.MT, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoConsultaPublicaNfce(Estado.MT, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),
                new EnderecoConsultaPublicaNfce(Estado.MT, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"),

                new EnderecoConsultaPublicaNfce(Estado.PA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"),
                new EnderecoConsultaPublicaNfce(Estado.PA, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://appnfc.sefa.pa.gov.br/portal/view/consultas/nfce/consultanfce.seam"),
                new EnderecoConsultaPublicaNfce(Estado.PA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"),
                new EnderecoConsultaPublicaNfce(Estado.PA, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/consultanfce.seam"),

                new EnderecoConsultaPublicaNfce(Estado.PB, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://www5.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoConsultaPublicaNfce(Estado.PB, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://www5.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoConsultaPublicaNfce(Estado.PB, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "https://www6.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),
                new EnderecoConsultaPublicaNfce(Estado.PB, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "https://www6.receita.pb.gov.br/atf/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1410"),

                new EnderecoConsultaPublicaNfce(Estado.PE, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://nfce.sefaz.pe.gov.br/nfce-web/consultarNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.PE, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.sefaz.pe.gov.br/nfce-web/entradaConsNfce"),
                new EnderecoConsultaPublicaNfce(Estado.PE, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://nfcehomolog.sefaz.pe.gov.br/nfce-web/consultarNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.PE, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://nfcehomolog.sefaz.pe.gov.br/nfce-web/entradaConsNfce"),

                new EnderecoConsultaPublicaNfce(Estado.PI, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://webas.sefaz.pi.gov.br/nfceweb/consultarNFCe.jsf"),
                new EnderecoConsultaPublicaNfce(Estado.PI, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://webas.sefaz.pi.gov.br/nfceweb/consultarNFCe.jsf"),
                new EnderecoConsultaPublicaNfce(Estado.PI, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"),
                new EnderecoConsultaPublicaNfce(Estado.PI, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"),

                new EnderecoConsultaPublicaNfce(Estado.PR, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.PR, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.fazenda.pr.gov.br/"),
                new EnderecoConsultaPublicaNfce(Estado.PR, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe"),
                new EnderecoConsultaPublicaNfce(Estado.PR, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.fazenda.pr.gov.br/"),

                new EnderecoConsultaPublicaNfce(Estado.RJ, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
                new EnderecoConsultaPublicaNfce(Estado.RJ, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.fazenda.rj.gov.br/consulta"),
                new EnderecoConsultaPublicaNfce(Estado.RJ, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode"),
                new EnderecoConsultaPublicaNfce(Estado.RJ, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.fazenda.rj.gov.br/consulta"),

                new EnderecoConsultaPublicaNfce(Estado.RN, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RN, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.set.rn.gov.br/portalDFE/NFCe/ConsultaNFCe.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RN, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://hom.nfce.set.rn.gov.br/consultarNFCe.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RN, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://nfce.set.rn.gov.br/portalDFE/NFCe/ConsultaNFCe.aspx"),

                new EnderecoConsultaPublicaNfce(Estado.RO, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.RO, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.sefin.ro.gov.br"),
                new EnderecoConsultaPublicaNfce(Estado.RO, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.RO, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.sefin.ro.gov.br/consultaAmbHomologacao.jsp"),

                new EnderecoConsultaPublicaNfce(Estado.RR, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://www.sefaz.rr.gov.br/nfce/servlet/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.RR, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://www.sefaz.rr.gov.br/nfce/servlet/wp_consulta_nfce"),
                new EnderecoConsultaPublicaNfce(Estado.RR, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://200.174.88.103:8080/nfce/servlet/qrcode"),
                new EnderecoConsultaPublicaNfce(Estado.RR, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://200.174.88.103:8080/nfce/servlet/wp_consulta_nfce"),

                new EnderecoConsultaPublicaNfce(Estado.RS, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RS, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://www.sefaz.rs.gov.br/NFE/NFE-NFC.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RS, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.RS, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "https://www.sefaz.rs.gov.br/NFE/NFE-NFC.aspx"),

                new EnderecoConsultaPublicaNfce(Estado.SE, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "http://www.nfce.se.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.SE, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "http://www.nfce.se.gov.br/portal"),
                new EnderecoConsultaPublicaNfce(Estado.SE, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "http://www.hom.nfe.se.gov.br/portal/consultarNFCe.jsp"),
                new EnderecoConsultaPublicaNfce(Estado.SE, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "http://www.hom.nfe.se.gov.br/portal"),

                new EnderecoConsultaPublicaNfce(Estado.SP, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlQrCode, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.SP, TipoAmbiente.taProducao, TipoUrlConsultaPublica.UrlConsulta, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaPublica.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.SP, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlQrCode, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"),
                new EnderecoConsultaPublicaNfce(Estado.SP, TipoAmbiente.taHomologacao, TipoUrlConsultaPublica.UrlConsulta, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaPublica.aspx"),

            };

            return endQrCodeNfce;
        }

        /// <summary>
        ///     Obtém a URL para uso no DANFE da NFCe
        /// </summary>
        /// <param name="infNFeSupl"></param>
        /// <param name="tipoAmbiente"></param>
        /// <param name="estado"></param>
        /// <param name="tipoUrlConsultaPublica"></param>
        /// <returns></returns>
        public static string ObterUrl(this infNFeSupl infNFeSupl, TipoAmbiente tipoAmbiente, Estado estado, TipoUrlConsultaPublica tipoUrlConsultaPublica)
        {
            var query = from qr in EndQrCodeNfce where qr.TipoAmbiente == tipoAmbiente && qr.Estado == estado && qr.TipoUrlConsultaPublica == tipoUrlConsultaPublica select qr.Url;
            var listaRetorno = query as IList<string> ?? query.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(string.Format("Não foi possível obter o {0}, para o Estado {1}, ambiente: {2}", EnumExt.Descricao(tipoUrlConsultaPublica), estado, EnumExt.Descricao(tipoAmbiente)));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrl obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }

        /// <summary>
        ///     Obtém a URL para uso no QR-Code
        /// </summary>
        /// <param name="infNFeSupl"></param>
        /// <param name="nfe"></param>
        /// <param name="cIdToken"></param>
        /// <param name="csc"></param>
        /// <returns></returns>
        public static string ObterUrlQrCode(this infNFeSupl infNFeSupl, Classes.NFe nfe, string cIdToken, string csc)
        {
            //Passo 1: Converter o valor da Data e Hora de Emissão da NFC-e (dhEmi) para HEXA;
            var dhEmi = ObterHexDeString(nfe.infNFe.ide.ProxyDhEmi);

            //Passo 2: Converter o valor do Digest Value da NFC-e (digVal) para HEXA;
            //Ao se efetuar a assinatura digital da NFCe emitida em contingência off-line, o campo digest value constante da XMl Signature deve obrigatoriamente ser idêntico ao encontrado quando da geração do digest value para a montagem QR Code.
            //Ver página 18 do Manual de Padrões Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2
            if (nfe.Signature == null)
                throw new Exception("Não é possível obter a URL do QR-Code de uma NFCe não assinada!");
            var digVal = ObterHexDeString(nfe.Signature.SignedInfo.Reference.DigestValue);

            //Na hipótese do consumidor não se identificar, não existirá o parâmetro cDest no QR Code;
            var cDest = "";
            if (nfe.infNFe.dest != null)
                cDest = "&cDest=" + nfe.infNFe.dest.CPF + nfe.infNFe.dest.CNPJ + nfe.infNFe.dest.idEstrangeiro;

            //Passo 3: Substituir os valores (“dhEmi” e “digVal”) nos parâmetros;
            var dadosBase = "chNFe=" + nfe.infNFe.Id.Substring(3) + "&nVersao=100&tpAmb=" + ((int)nfe.infNFe.ide.tpAmb) + cDest + "&dhEmi=" + dhEmi + "&vNF=" +
                            nfe.infNFe.total.ICMSTot.vNF.ToString("0.00").Replace(',', '.') + "&vICMS=" + nfe.infNFe.total.ICMSTot.vICMS.ToString("0.00").Replace(',', '.') + "&digVal=" + digVal + "&cIdToken=" + cIdToken;

            //Passo 4: Adicionar, ao final dos parâmetros, o CSC (CSC do contribuinte disponibilizado pela SEFAZ do Estado onde a empresa esta localizada):
            var dadosParaSh1 = dadosBase + csc;

            //Passo 5: Aplicar o algoritmo SHA-1 sobre todos os parâmetros concatenados. Asaída do algoritmo SHA-1 deve ser em HEXADECIMAL.
            var sha1ComCsc = ObterHexSha1DeString(dadosParaSh1);

            //Passo 6: Adicione o resultado sem o CSC e gere a imagem do QR Code: 1º parte (endereço da consulta) +2º parte (tabela 3 com indicação SIM na última coluna).
            return ObterUrl(infNFeSupl, nfe.infNFe.ide.tpAmb, nfe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlQrCode) + "?" + dadosBase + "&cHashQRCode=" + sha1ComCsc;
        }

        /// <summary>
        /// Obtém uma string SHA1, no formato hexadecimal da string passada no parâmeto        
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ObterHexSha1DeString(string s)
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
        private static string ObterHexDeByteArray(byte[] bytes)
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
        private static string ObterHexDeString(string s)
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