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

using System.Drawing;
using System.Drawing.Text;
using System.Xml.Serialization;
using NFe.Danfe.Base.Fontes;
using NFe.Danfe.Base.Properties;

namespace NFe.Danfe.Base.NFCe
{
    public class ConfiguracaoDanfeNfce: ConfiguracaoDanfe
    {
        public ConfiguracaoDanfeNfce(NfceDetalheVendaNormal detalheVendaNormal,
            NfceDetalheVendaContigencia detalheVendaContigencia, byte[] logomarca = null,
            bool imprimeDescontoItem = false, float margemEsquerda = 4.5F, float margemDireita = 4.5F, 
            NfceModoImpressao modoImpressao = NfceModoImpressao.MultiplasPaginas, bool documentoCancelado = false)
        {
            DocumentoCancelado = documentoCancelado;
            DetalheVendaNormal = detalheVendaNormal;
            DetalheVendaContigencia = detalheVendaContigencia;
            Logomarca = logomarca;
            ImprimeDescontoItem = imprimeDescontoItem;
            MargemEsquerda = margemEsquerda;
            MargemDireita = margemDireita;
            ModoImpressao = modoImpressao;
            CarregarFontePadraoNfceNativa();
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        private ConfiguracaoDanfeNfce()
        {
            DocumentoCancelado = false;
        }

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NFCes emitidos em ambiente Normal
        /// </summary>
        public NfceDetalheVendaNormal DetalheVendaNormal { get; set; }

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NFCes emitidos em ambiente de Homologação
        /// Nesse modo a informação do detalhe é obrigatória. Vide Manual de Padrões Padrões Técnicos do DANFE-NFC-e e QR Code, versão 3.2
        /// </summary>
        public NfceDetalheVendaContigencia DetalheVendaContigencia { get; set; }

        /// <summary>
        /// Determina se o desconto do item será impresso no DANTE, quando houver
        /// </summary>
        public bool ImprimeDescontoItem { get; set; }

        /// <summary>
        /// Margem esquerda de impressão em milímetros
        /// </summary>
        public float MargemEsquerda { get; set; }

        /// <summary>
        /// Margem direita de impressão em milímetros
        /// </summary>
        public float MargemDireita { get; set; }

        /// <summary>
        /// Determina o modo de impressão do DANFE da NFCe.
        /// 
        /// </summary>
        public NfceModoImpressao ModoImpressao { get; set; }

        public string FontPadraoNfceNativa { get; set; }

        public FontFamily CarregarFontePadraoNfceNativa(string font = null)
        {
            if (font != null)
            {
                FontPadraoNfceNativa = font;
                return new FontFamily(font);
            }


            PrivateFontCollection colecaoDeFontes; //todo dispose na coleção
            var openSans = Fonte.CarregarDeByteArray(Resources.OpenSans_CondBold, out colecaoDeFontes);

            return openSans;
        }
    }
}
