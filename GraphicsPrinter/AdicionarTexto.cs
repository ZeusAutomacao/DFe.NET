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

namespace GraphicsPrinter
{
    public class AdicionarTexto
    {
        public static FontFamily FontPadrao { get; set; }
        private readonly Graphics _graphics;
        private float _pontoX;
        private float _pontoY;
        private readonly SolidBrush _br;

        public AdicionarTexto(Graphics graphics, string texto, int tamanhoFonte, SolidBrush br = null, Font font = null)
        {
            _graphics = graphics;
            Texto = texto;
            TamanhoFonte = tamanhoFonte;
            _br = br;

            if (font != null)
            {
                Fonte = font;
            }

            if (font == null)
            {
                Fonte = new Font(FontPadrao, TamanhoFonte);
            }
            
            Medida = MedidasLinha.GetMedidas(this);
        }

        public void Desenhar(float pontoX, float pontoY)
        {
            _pontoX = pontoX;
            _pontoY = pontoY;

            if (_br != null)
            {
                _graphics.DrawString(Texto, Fonte, _br, new PointF(_pontoX, _pontoY));
                return;
            }

            _graphics.DrawString(Texto, Fonte, Brushes.Black, new PointF(_pontoX, _pontoY));
        }

        public Medida Medida { get; private set; }
        public string Texto { get; private set; }
        public Font Fonte { get; private set; }
        public int TamanhoFonte { get; private set; }
    }
}