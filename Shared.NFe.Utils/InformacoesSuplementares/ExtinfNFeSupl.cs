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
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Utils.InformacoesSuplementares
{
    internal class EnderecoConsultaPublicaNfce
    {
        public EnderecoConsultaPublicaNfce(TipoAmbiente tipoAmbiente, Estado estado, TipoUrlConsultaPublica tipoUrlConsultaPublica, VersaoServico versaoServico, VersaoQrCode versaoQrCode, string url)
        {
            TipoAmbiente = tipoAmbiente;
            Estado = estado;
            TipoUrlConsultaPublica = tipoUrlConsultaPublica;
            Url = url;
            VersaoServico = versaoServico;
            VersaoQrCode = versaoQrCode;
        }

        public TipoAmbiente TipoAmbiente { get; private set; }
        public Estado Estado { get; private set; }
        public TipoUrlConsultaPublica TipoUrlConsultaPublica { get; private set; }
        public string Url { get; private set; }
        public VersaoServico VersaoServico { get; private set; }
        public VersaoQrCode VersaoQrCode { get; private set; }
    }

    internal class TupleList<T1, T2, T3> : List<Tuple<T1, T2, T3>>
    {
        internal void Add(T1 item, T2 item2, T3 item3)
        {
            Add(new Tuple<T1, T2, T3>(item, item2, item3));
        }
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
            //Revisão das URLs segundo portal do ENCAT em 31/07/2018
            var endQrCodeNfce = new List<EnderecoConsultaPublicaNfce>();

            var adicionarUrls = new Action<TipoAmbiente, TipoUrlConsultaPublica, VersaoQrCode[], TupleList<Estado, VersaoServico[], string>>(
                (tipoAmbiente, tipoUrl, versoesQrCode, enderecos) =>
                {
                    foreach (var endereco in enderecos)
                        foreach (var versaoQrCode in versoesQrCode)
                            foreach (var versaoServico in endereco.Item2)
                                endQrCodeNfce.Add(new EnderecoConsultaPublicaNfce(tipoAmbiente, endereco.Item1, tipoUrl, versaoServico, versaoQrCode, endereco.Item3));
                });

            var versao3 = new[] { VersaoServico.Versao310 };
            var versao4 = new[] { VersaoServico.Versao400 };
            var versao3E4 = new[] { VersaoServico.Versao310, VersaoServico.Versao400 };

            #region URL por UF utilizada QR code: Ambiente de Produção

            //Embora no site do ENCAT http://nfce.encat.org/desenvolvedor/qrcode/ não faça separação por versão do QR-Code, mas pela versão do layout,
            //as urls que estavam marcadas no site para NFCe 3.1 foram colocadas aqui para o QR-Code versão 1, independente da versão do layout. A SEFAZ SRVRS está assim em 08/08/2018
            var urlsQrCodeProducaoQrCode1E2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.AC, versao3E4, "http://www.sefaznet.ac.gov.br/nfce/qrcode?"},
                {Estado.AL, versao3E4, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"},
                {Estado.AP, versao3E4, "https://www.sefaz.ap.gov.br/nfce/nfcep.php"},
                {Estado.AM, versao3E4, "http://sistemas.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp?"}, //No portal do ENCAT está: sistemas.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp?chNFe=1315….
                {Estado.BA, versao3E4, "http://nfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"},
                {Estado.CE, versao3E4, "http://nfce.sefaz.ce.gov.br/pages/ShowNFCe.html"}, //Não utilizado para emissão via ws, mas utilizado para emissão com integrador
                {Estado.GO, versao3E4, "http://nfe.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"},
                {Estado.MA, versao3E4, "http://nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"},
                {Estado.MT, versao3E4, "http://www.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.MS, versao3E4, "http://www.dfe.ms.gov.br/nfce/qrcode?"},
                {Estado.PA, versao3E4, "https://appnfc.sefa.pa.gov.br/portal/view/consultas/nfce/nfceForm.seam"},
                {Estado.PB, versao3E4, "http://www.receita.pb.gov.br/nfce"},
                {Estado.PE, versao3E4, "http://nfce.sefaz.pe.gov.br/nfce/consulta"},
                {Estado.PI, versao3E4, "http://www.sefaz.pi.gov.br/nfce/qrcode"},
                {Estado.RJ, versao3E4, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode?"},
                {Estado.RN, versao3E4, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"},
                {Estado.RS, versao3E4, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"},
                {Estado.RO, versao3E4, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"},
                {Estado.RR, versao3E4, "https://www.sefaz.rr.gov.br/nfce/servlet/qrcode"},
                {Estado.MG, versao3E4, "https://nfce.fazenda.mg.gov.br/portalnfce/sistema/qrcode.xhtml"}
            };
            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao1, VersaoQrCode.QrCodeVersao2 }, urlsQrCodeProducaoQrCode1E2);

            var urlsQrCodeProducaoQrCode1 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.DF, versao3E4, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"},
                {Estado.PR, versao3E4, "http://www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe?"},
                {Estado.SP, versao3E4, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"},
                {Estado.SE, versao3E4, "http://www.nfce.se.gov.br/portal/consultarNFCe.jsp?"},
                {Estado.TO, versao3E4, "http://apps.sefaz.to.gov.br/portal-nfce/qrcodeNFCe"}
            };
            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao1 }, urlsQrCodeProducaoQrCode1);

            var urlsQrCodeProducaoQrCode2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.DF, versao4, "http://www.fazenda.df.gov.br/nfce/qrcode?"},
                {Estado.ES, versao4, "http://app.sefaz.es.gov.br/ConsultaNFCe?"},
                {Estado.PR, versao4, "http://www.fazenda.pr.gov.br/nfce/qrcode?"},
                {Estado.SP, versao4, "https://www.nfce.fazenda.sp.gov.br/qrcode"},
                {Estado.SE, versao4, "http://www.nfce.se.gov.br/nfce/qrcode?"},
                {Estado.TO, versao4, "http://apps.sefaz.to.gov.br/portal-nfce/qrcodeNFCe"},
                {Estado.SC, versao4, "https://sat.sef.sc.gov.br/nfce/consulta"}
            };
            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao2 }, urlsQrCodeProducaoQrCode2);

            #endregion

            #region URL por UF utilizada QR code: Ambiente de Homologação

            var urlsQrCodeHomologacaoQrCode1E2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.AC, versao3E4, "http://www.hml.sefaznet.ac.gov.br/nfce/qrcode?"},
                {Estado.AL, versao3E4, "http://nfce.sefaz.al.gov.br/QRCode/consultarNFCe.jsp"},
                {Estado.AP, versao3E4, "https://www.sefaz.ap.gov.br/nfcehml/nfce.php"},
                {Estado.AM, versao3E4, "https://sistemas.sefaz.am.gov.br/nfceweb-hom/consultarNFCe.jsp?"},
                {Estado.BA, versao3E4, "http://hnfe.sefaz.ba.gov.br/servicos/nfce/modulos/geral/NFCEC_consulta_chave_acesso.aspx"},
                {Estado.CE, versao3E4, "http://nfceh.sefaz.ce.gov.br/pages/ShowNFCe.html"},
                {Estado.DF, versao3E4, "http://dec.fazenda.df.gov.br/ConsultarNFCe.aspx"},
                {Estado.ES, versao3E4, "http://homologacao.sefaz.es.gov.br/ConsultaNFCe/qrcode.aspx?"},
                {Estado.GO, versao3E4, "http://homolog.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"},
                {Estado.MA, versao3E4, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"},
                {Estado.MT, versao3E4, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.MS, versao3E4, "http://www.dfe.ms.gov.br/nfce/qrcode?"},
                {Estado.PA, versao3E4, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/nfceForm.seam"},
                {Estado.PB, versao3E4, "http://www.receita.pb.gov.br/nfcehom"},
                {Estado.PE, versao3E4, "http://nfcehomolog.sefaz.pe.gov.br/nfce-web/consultarNFCe"},
                {Estado.PI, versao3E4, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"},
                {Estado.RJ, versao3E4, "http://www4.fazenda.rj.gov.br/consultaNFCe/QRCode?"},
                {Estado.RN, versao3E4, "http://hom.nfce.set.rn.gov.br/consultarNFCe.aspx"},
                {Estado.RS, versao3E4, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"},
                {Estado.RO, versao3E4, "http://www.nfce.sefin.ro.gov.br/consultanfce/consulta.jsp"},
                {Estado.RR, versao3E4, "http://200.174.88.103:8080/nfce/servlet/qrcode"},
                {Estado.TO, versao3E4, "http://apps.sefaz.to.gov.br/portal-nfce-homologacao/qrcodeNFCe"},
                {Estado.MG, versao3E4, "https://nfce.fazenda.mg.gov.br/portalnfce/sistema/qrcode.xhtml"}
            };
            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao1, VersaoQrCode.QrCodeVersao2 }, urlsQrCodeHomologacaoQrCode1E2);

            var urlsQrCodeHomologacaoQrCode1 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.PR, versao3E4, "http://www.dfeportal.fazenda.pr.gov.br/dfe-portal/rest/servico/consultaNFCe?"},
                {Estado.SE, versao3E4, "http://www.hom.nfe.se.gov.br/portal/consultarNFCe.jsp?"},
                {Estado.SP, versao3E4, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx"},
            };
            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao1 }, urlsQrCodeHomologacaoQrCode1);


            var urlsQrCodeHomologacaoQrCode2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.PR, versao4, "http://www.fazenda.pr.gov.br/nfce/qrcode?"},
                {Estado.SE, versao4, "http://www.hom.nfe.se.gov.br/nfce/qrcode?"},
                {Estado.SP, versao4, "https://www.homologacao.nfce.fazenda.sp.gov.br/qrcode"},
                {Estado.SC, versao4, "https://hom.sat.sef.sc.gov.br/nfce/consulta"},
            };
            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlQrCode, new[] { VersaoQrCode.QrCodeVersao2 }, urlsQrCodeHomologacaoQrCode2);

            #endregion

            #region URL por UF utilizada para consulta chave

            #region Urls de consulta para Qr Code versão 1.0

            #region PRODUÇÃO

            var urlsConsultaProducao1 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.AC, versao3E4, "www.sefaznet.ac.gov.br/nfce/consulta"},
                {Estado.AL, versao3E4, "http://nfce.sefaz.al.gov.br/consultaNFCe.htm"},
                {Estado.AP, versao3E4, "https://www.sefaz.ap.gov.br/sate/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1261"},
                {Estado.AM, versao3E4, "sistemas.sefaz.am.gov.br/nfceweb/formConsulta.do"},
                {Estado.BA, versao3E4, "nfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"},
                {Estado.DF, versao3E4, "http://dec.fazenda.df.gov.br/NFCE/"},
                {Estado.ES, versao3E4, "http://app.sefaz.es.gov.br/ConsultaNFCe"},
                {Estado.MA, versao3E4, "http://www.nfce.sefaz.ma.gov.br/portal/consultaNFe.do?method=preFilterCupom&"},
                {Estado.MT, versao3E4, "http://www.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.MS, versao3E4, "http://www.dfe.ms.gov.br/nfce"},
                {Estado.PA, versao3E4, "https://appnfc.sefa.pa.gov.br/portal/view/consultas/nfce/consultanfce.seam"},
                {Estado.PB, versao3E4, "www.receita.pb.gov.br/nfce"},
                {Estado.PR, versao3E4, "http://www.fazenda.pr.gov.br"},
                {Estado.PI, versao3E4, "http://webas.sefaz.pi.gov.br/nfceweb/consultarNFCe.jsf"},
                {Estado.RJ, versao3E4, "www.nfce.fazenda.rj.gov.br/consulta"},
                {Estado.RN, versao3E4, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"},
                {Estado.RS, versao3E4, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"},
                {Estado.RO, versao3E4, "http://www.nfce.sefin.ro.gov.br"},
                {Estado.RR, versao3E4, "https://www.sefaz.rr.gov.br/nfce/servlet/wp_consulta_nfce"},
                {Estado.SP, versao3E4, "https://www.nfce.fazenda.sp.gov.br/NFCeConsultaPublica"},
                {Estado.SE, versao3E4, "http://www.nfce.se.gov.br/portal/portalNoticias.jsp"},
                {Estado.TO, versao3E4, "http://apps.sefaz.to.gov.br/portal-nfce/consultarNFCe.jsf"},
                {Estado.GO, versao4, "http://www.nfce.go.gov.br/post/ver/214344/consulta-nfce"},
                {Estado.PE, versao4, "http://nfce.sefaz.pe.gov.br/nfce-web/consultarNFCe"},
                {Estado.MG, versao3E4, "http://nfce.fazenda.mg.gov.br/portalnfce"}
            };

            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao1 }, urlsConsultaProducao1);

            #endregion

            #region HOMOLOGAÇÃO

            var urlsConsultaHomologacao1 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.AC, versao3E4, "http://hml.sefaznet.ac.gov.br/nfce/consulta"},
                {Estado.AL, versao3E4, "http://nfce.sefaz.al.gov.br/consultaNFCe.htm"},
                {Estado.AP, versao3E4, "https://www.sefaz.ap.gov.br/sate1/seg/SEGf_AcessarFuncao.jsp?cdFuncao=FIS_1261"},
                {Estado.AM, versao3E4, "http://homnfce.sefaz.am.gov.br/nfceweb/consultarNFCe.jsp?"},
                {Estado.BA, versao3E4, "http://hnfe.sefaz.ba.gov.br/servicos/nfce/default.aspx"},
                {Estado.CE, versao3E4, "http://nfceh.sefaz.ce.gov.br/pages/consultaNota.jsf"},
                {Estado.DF, versao3E4, "http://dec.fazenda.df.gov.br/NFCE/"},
                {Estado.ES, versao3E4, "http://homologacao.sefaz.es.gov.br/ConsultaNFCe"},
                {Estado.MA, versao3E4, "http://www.hom.nfce.sefaz.ma.gov.br/portal/consultarNFCe.jsp"},
                {Estado.MT, versao3E4, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.MS, versao3E4, "http://www.dfe.ms.gov.br/nfce"},
                {Estado.PA, versao3E4, "https://appnfc.sefa.pa.gov.br/portal-homologacao/view/consultas/nfce/consultanfce.seam"},
                {Estado.PB, versao3E4, "http://www.receita.pb.gov.br/nfcehom"},
                {Estado.PR, versao3E4, "http://www.fazenda.pr.gov.br"},
                {Estado.PI, versao3E4, "http://webas.sefaz.pi.gov.br/nfceweb-homologacao/consultarNFCe.jsf"},
                {Estado.RJ, versao3E4, "www.nfce.fazenda.rj.gov.br/consulta"},
                {Estado.RN, versao3E4, "http://nfce.set.rn.gov.br/consultarNFCe.aspx"},
                {Estado.RS, versao3E4, "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx"},
                {Estado.RO, versao3E4, "http://www.nfce.sefin.ro.gov.br"},
                {Estado.RR, versao3E4, "http://200.174.88.103:8080/nfce/servlet/wp_consulta_nfce"},
                {Estado.SP, versao3E4, "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica"},
                {Estado.SE, versao3E4, "http://www.hom.nfe.se.gov.br/portal/portalNoticias.jsp"},
                {Estado.TO, versao3E4, "http://apps.sefaz.to.gov.br/portal-nfce-homologacao/consultarNFCe.jsf"},
                {Estado.GO, versao4, "http://www.nfce.go.gov.br/post/ver/214413/consulta-nfc-e-homologacao?" },
                {Estado.PE, versao4, "http://nfcehomolog.sefaz.pe.gov.br/nfce-web/consultarNFCe" },
                {Estado.MG, versao3E4, "http://hnfce.fazenda.mg.gov.br/portalnfce"}
            };

            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao1 }, urlsConsultaHomologacao1);

            #endregion

            #endregion

            #region Urls de consulta para Qr Code versão 2.0

            #region HOMOLOGAÇÃO e PRODUÇÃO

            var urlsConsultaHomologacaoEProducao2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.AC, versao3E4, "www.sefaznet.ac.gov.br/nfce/consulta"},
                {Estado.AL, versao3E4, "www.sefaz.al.gov.br/nfce/consulta"},
                {Estado.AP, versao3E4, "www.sefaz.ap.gov.br/nfce/consulta"},
                {Estado.AM, versao3E4, "www.sefaz.am.gov.br/nfce/consulta"},
                {Estado.CE, versao3E4, "www.sefaz.ce.gov.br/nfce/consulta"},
                {Estado.DF, versao3E4, "www.fazenda.df.gov.br/nfce/consulta"},
                {Estado.ES, versao3E4, "www.sefaz.es.gov.br/nfce/consulta"},
                {Estado.MA, versao3E4, "www.sefaz.ma.gov.br/nfce/consulta"},
                {Estado.MS, versao3E4, "http://www.dfe.ms.gov.br/nfce"},
                {Estado.PA, versao3E4, "www.sefa.pa.gov.br/nfce/consulta"},
                {Estado.PR, versao3E4, "http://www.fazenda.pr.gov.br/nfce/consulta"},
                {Estado.PE, versao3E4, "nfce.sefaz.pe.gov.br/nfce/consulta"},
                {Estado.PI, versao3E4, "www.sefaz.pi.gov.br/nfce/consulta"},
                {Estado.RJ, versao3E4, "www.fazenda.rj.gov.br/nfce/consulta"},
                {Estado.RN, versao3E4, "www.set.rn.gov.br/nfce/consulta"},
                {Estado.RS, versao3E4, "www.sefaz.rs.gov.br/nfce/consulta"},
                {Estado.RO, versao3E4, "www.sefin.ro.gov.br/nfce/consulta"},
                {Estado.RR, versao3E4, "www.sefaz.rr.gov.br/nfce/consulta"}
            };

            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao2 }, urlsConsultaHomologacaoEProducao2);
            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao2 }, urlsConsultaHomologacaoEProducao2);

            #endregion

            #region PRODUÇÃO

            var urlsConsultaProducao2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.BA, versao3E4, "www.sefaz.ba.gov.br/nfce/consulta"},
                {Estado.MT, versao3E4, "http://www.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.PB, versao3E4, "www.receita.pb.gov.br/nfce/consulta"},
                {Estado.SP, versao3E4, "https://www.nfce.fazenda.sp.gov.br/consulta"},
                {Estado.SE, versao3E4, "http://www.nfce.se.gov.br/nfce/consulta"},
                {Estado.GO, versao3E4, "www.sefaz.go.gov.br/nfce/consulta"},
                {Estado.MG, versao3E4, "http://nfce.fazenda.mg.gov.br/portalnfce"},
                {Estado.TO, versao3E4, "www.sefaz.to.gov.br/nfce/consulta"},
                {Estado.SC, versao3E4, "https://sat.sef.sc.gov.br/nfce/consulta" }
            };

            adicionarUrls(TipoAmbiente.Producao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao2 }, urlsConsultaProducao2);

            #endregion

            #region HOMOLOGAÇÃO

            var urlsConsultaHomologacao2 = new TupleList<Estado, VersaoServico[], string>
            {
                {Estado.BA, versao3E4, "http://hinternet.sefaz.ba.gov.br/nfce/consulta"},
                {Estado.MT, versao3E4, "http://homologacao.sefaz.mt.gov.br/nfce/consultanfce"},
                {Estado.PB, versao3E4, "www.receita.pb.gov.br/nfcehom"},
                {Estado.SP, versao3E4, "https://www.homologacao.nfce.fazenda.sp.gov.br/consulta"},
                {Estado.SE, versao3E4, "http://www.hom.nfe.se.gov.br/nfce/consulta"},
                {Estado.GO, versao3E4, "http://homolog.sefaz.go.gov.br/nfeweb/sites/nfce/danfeNFCe"},
                {Estado.MG, versao3E4, "http://hnfce.fazenda.mg.gov.br/portalnfce"},
                {Estado.TO, versao3E4, "http://homologacao.sefaz.to.gov.br/nfce/consulta.jsf" },
                {Estado.SC, versao3E4, "https://hom.sat.sef.sc.gov.br/nfce/consulta" }
            };

            adicionarUrls(TipoAmbiente.Homologacao, TipoUrlConsultaPublica.UrlConsulta, new[] { VersaoQrCode.QrCodeVersao2 }, urlsConsultaHomologacao2);

            #endregion

            #endregion

            #endregion

            return endQrCodeNfce;
        }

        /// <summary>
        ///     Obtém a URL para uso no DANFE da NFCe
        /// </summary>
        public static string ObterUrl(this infNFeSupl infNFeSupl, TipoAmbiente tipoAmbiente, Estado estado, TipoUrlConsultaPublica tipoUrlConsultaPublica, VersaoServico versaoServico, VersaoQrCode versaoQrCode)
        {
            var query = from qr in EndQrCodeNfce
                        where qr.TipoAmbiente == tipoAmbiente && qr.Estado == estado &&
                              qr.TipoUrlConsultaPublica == tipoUrlConsultaPublica && qr.VersaoServico == versaoServico &&
                              qr.VersaoQrCode == versaoQrCode
                        select qr.Url;
            var listaRetorno = query as IList<string> ?? query.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(string.Format("Não foi possível obter o {0}, para o Estado {1}, ambiente: {2}", tipoUrlConsultaPublica.Descricao(), estado, tipoAmbiente.Descricao()));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrl obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }

        /// <summary>
        ///     Obtém a URL de consulta pela chave
        /// </summary>
        public static string ObterUrlConsulta(this infNFeSupl infNFeSupl, Classes.NFe nfe, VersaoQrCode versaoQrCode)
        {
            var versaoServico = Conversao.StringParaVersaoServico(nfe.infNFe.versao);
            return ObterUrl(infNFeSupl, nfe.infNFe.ide.tpAmb, nfe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlConsulta, versaoServico, versaoQrCode);
        }

        /// <summary>
        ///     Obtém a URL para montagem do QR-Code
        /// </summary>
        public static string ObterUrlQrCode(this infNFeSupl infNFeSupl, Classes.NFe nfe, VersaoQrCode versaoQrCode, string cIdToken, string csc)
        {
            Func<string, string> msgErro = parametro => $"O {parametro} não foi informado!";

            if (string.IsNullOrEmpty(cIdToken))
                throw new ArgumentNullException(nameof(cIdToken), msgErro("token"));

            if (string.IsNullOrEmpty(csc))
                throw new ArgumentNullException(nameof(cIdToken), msgErro("CSC"));

            var versaoServico = Conversao.StringParaVersaoServico(nfe.infNFe.versao);
            switch (versaoQrCode)
            {
                case VersaoQrCode.QrCodeVersao1:
                    return ObterUrlQrCode1(infNFeSupl, nfe, cIdToken, csc, versaoServico);
                case VersaoQrCode.QrCodeVersao2:
                    return ObterUrlQrCode2(infNFeSupl, nfe, cIdToken, csc, versaoServico);
                default:
                    throw new ArgumentOutOfRangeException("versaoQrCode", versaoQrCode, null);
            }
        }

        /// <summary>
        /// Obtém a URL para uso no QR-Code, versão 1.0 - leiaute 3.10
        /// </summary>
        private static string ObterUrlQrCode1(infNFeSupl infNFeSupl, Classes.NFe nfe, string cIdToken, string csc, VersaoServico versaoServico)
        {
            //Passo 1: Converter o valor da Data e Hora de Emissão da NFC-e (dhEmi) para HEXA;
            var dhEmi = Conversao.ObterHexDeString(nfe.infNFe.ide.ProxyDhEmi);

            //Passo 2: Converter o valor do Digest Value da NFC-e (digVal) para HEXA;
            //Ao se efetuar a assinatura digital da NFCe emitida em contingência off-line, o campo digest value constante da XMl Signature deve obrigatoriamente ser idêntico ao encontrado quando da geração do digest value para a montagem QR Code.
            //Ver página 18 do Manual de Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2
            if (nfe.Signature == null)
                throw new Exception("Não é possível obter a URL do QR-Code de uma NFCe não assinada!");
            var digVal = Conversao.ObterHexDeString(nfe.Signature.SignedInfo.Reference.DigestValue);

            //Na hipótese do consumidor não se identificar, não existirá o parâmetro cDest no QR Code;
            var cDest = "";
            if (nfe.infNFe.dest != null)
                cDest = "&cDest=" + nfe.infNFe.dest.CPF + nfe.infNFe.dest.CNPJ + nfe.infNFe.dest.idEstrangeiro;

            //Passo 3: Substituir os valores (“dhEmi” e “digVal”) nos parâmetros;
            var dadosBase =
                "chNFe=" + nfe.infNFe.Id.Substring(3) +
                "&nVersao=100" +
                "&tpAmb=" + (int)nfe.infNFe.ide.tpAmb +
                cDest +
                "&dhEmi=" + dhEmi +
                "&vNF=" + nfe.infNFe.total.ICMSTot.vNF.ToString("0.00").Replace(',', '.') +
                "&vICMS=" + nfe.infNFe.total.ICMSTot.vICMS.ToString("0.00").Replace(',', '.') +
                "&digVal=" + digVal +
                "&cIdToken=" + cIdToken;

            //Passo 4: Adicionar, ao final dos parâmetros, o CSC (CSC do contribuinte disponibilizado pela SEFAZ do Estado onde a empresa esta localizada):
            var dadosParaSh1 = dadosBase + csc;

            //Passo 5: Aplicar o algoritmo SHA-1 sobre todos os parâmetros concatenados. A saída do algoritmo SHA-1 deve ser em HEXADECIMAL.
            var sha1ComCsc = Conversao.ObterHexSha1DeString(dadosParaSh1);

            //Passo 6: Adicione o resultado sem o CSC e gere a imagem do QR Code: 1º parte (endereço da consulta) +2º parte (tabela 3 com indicação SIM na última coluna).
            const string interrogacao = "?";
            var url = ObterUrl(infNFeSupl, nfe.infNFe.ide.tpAmb, nfe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlQrCode, versaoServico, VersaoQrCode.QrCodeVersao1);
            if (!url.EndsWith(interrogacao))
                url += interrogacao;
            return url + dadosBase + "&cHashQRCode=" + sha1ComCsc;
        }

        /// <summary>
        /// Obtém a URL para uso no QR-Code, versão 2.0 - leiaute 4.00
        /// </summary>
        private static string ObterUrlQrCode2(infNFeSupl infNFeSupl, Classes.NFe nfe, string cIdToken, string csc, VersaoServico versaoServico)
        {
            #region 1ª parte

            var url = ObterUrlQrCode2ComParametro(infNFeSupl, nfe.infNFe.ide.tpAmb, nfe.infNFe.ide.cUF, versaoServico);

            #endregion

            #region 2ª parte

            const string pipe = "|";

            //Chave de Acesso da NFC-e 
            var chave = nfe.infNFe.Id.Substring(3);

            //Identificação do Ambiente (1 – Produção, 2 – Homologação) 
            var ambiente = (int)nfe.infNFe.ide.tpAmb;

            //Identificador do CSC (Código de Segurança do Contribuinte no Banco de Dados da SEFAZ). Informar sem os zeros não significativos
            var idCsc = Convert.ToInt32(cIdToken);

            string dadosBase;

            if (nfe.infNFe.ide.tpEmis == TipoEmissao.teOffLine)
            {
                var diaEmi = nfe.infNFe.ide.dhEmi.Day.ToString("D2");
                var valorNfce = nfe.infNFe.total.ICMSTot.vNF.ToString("0.00").Replace(',', '.');
                var digVal = Conversao.ObterHexDeString(nfe.Signature.SignedInfo.Reference.DigestValue);
                dadosBase = string.Concat(chave, pipe, (int)VersaoQrCode.QrCodeVersao2, pipe, ambiente, pipe, diaEmi, pipe, valorNfce, pipe, digVal, pipe, idCsc);
            }
            else
            {
                dadosBase = string.Concat(chave, pipe, (int)VersaoQrCode.QrCodeVersao2, pipe, ambiente, pipe, idCsc);
            }

            var dadosSha1 = string.Concat(dadosBase, csc);
            var sh1 = Conversao.ObterHexSha1DeString(dadosSha1);

            return string.Concat(url, dadosBase, pipe, sh1);

            #endregion
        }

        /// <summary>
        /// Obtém a URL para o QR-Code versão 2.0 com o tratamento de parâmetro query no final da url
        /// </summary>
        /// <returns></returns>
        public static string ObterUrlQrCode2ComParametro(this infNFeSupl infNFeSupl, TipoAmbiente tipoAmbiente, Estado estado, VersaoServico versaoServico)
        {
            const string interrogacao = "?";
            const string parametro = "p=";

            var url = ObterUrl(infNFeSupl, tipoAmbiente, estado, TipoUrlConsultaPublica.UrlQrCode, versaoServico, VersaoQrCode.QrCodeVersao2);

            if (!url.EndsWith(interrogacao))
                url += interrogacao;
            if (!url.EndsWith(parametro))
                url += parametro;
            return url;
        }
    }
}
