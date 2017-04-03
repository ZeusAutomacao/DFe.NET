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
using System.IO;

namespace GraphicsPrinter
{
    public class AdicionarImagem
    {
        private readonly Graphics _graphics;
        private readonly string _caminhoImagem;
        private readonly int _posicaoX;
        private readonly int _posicaoY;
        private readonly Image _imagem;

        public AdicionarImagem(Graphics graphics, string caminhoImagem, int posicaoX, int posicaoY)
        {
            _graphics = graphics;
            _caminhoImagem = caminhoImagem;
            _posicaoX = posicaoX;
            _posicaoY = posicaoY;
        }

        public AdicionarImagem(Graphics graphics, Image imagem, int posicaoX, int posicaoY)
        {
            _graphics = graphics;
            _imagem = imagem;
            _posicaoX = posicaoX;
            _posicaoY = posicaoY;
        }

        public AdicionarImagem(AdicionarImagem adicionarImagem, Image novaImagem)
        {
            _graphics = adicionarImagem.Graphics;
            _imagem = novaImagem;
            _posicaoX = adicionarImagem.PosicaoX;
            _posicaoY = adicionarImagem.PosicaoY;
        }

        public Image Logo {get { return _imagem; } }
        public Graphics Graphics { get { return _graphics; } }
        public int PosicaoX { get { return _posicaoX; } }
        public int PosicaoY { get { return _posicaoY; } }

        public void Desenhar()
        {
            try
            {
                Image imagemUtilizada = null;

                if (_imagem != null)
                {
                    imagemUtilizada = _imagem;
                }

                if (File.Exists(_caminhoImagem))
                    imagemUtilizada = Image.FromFile(_caminhoImagem);

                // ReSharper disable once AssignNullToNotNullAttribute
                _graphics.DrawImage(imagemUtilizada, new Point(_posicaoX, _posicaoY));
            }
            catch
            {
            }
        } 
    }
}