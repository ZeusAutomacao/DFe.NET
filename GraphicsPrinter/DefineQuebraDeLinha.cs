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
using System.Collections.Generic;
using System.Drawing;

namespace GraphicsPrinter
{
    public class DefineQuebraDeLinha
    {
        private readonly AdicionarTexto _adicionarTexto;
        private readonly ComprimentoMaximo _comprimentoMaximo;
        private readonly int _larguraDoTexto;

        public DefineQuebraDeLinha(AdicionarTexto adicionarTexto, ComprimentoMaximo comprimentoMaximo, int larguraDoTexto)
        {
            _adicionarTexto = adicionarTexto;
            _comprimentoMaximo = comprimentoMaximo;
            _larguraDoTexto = larguraDoTexto;
        }

        public AdicionarTexto DesenharComQuebras(Graphics graphics)
        {
            if (_larguraDoTexto <= _comprimentoMaximo.GetComprimentoMaximo()) return _adicionarTexto;

            string linha = _adicionarTexto.Texto.Replace("\n", " ").Replace("\r", "");
            string[] palavras = linha.Split(' ');
            string linhaFormat = string.Empty;

            Dictionary<int, string> partesDaLinha = new Dictionary<int, string>();
            string parte = string.Empty;

            int parteContador = 0;

            foreach (string palavra in palavras)
            {
                Medida medidaParte = MedidasLinha.GetMedidas(parte, _adicionarTexto.Fonte);
                Medida medidaPalavra = MedidasLinha.GetMedidas(palavra, _adicionarTexto.Fonte);
                int larguraLinha = medidaParte.Largura + medidaPalavra.Largura;

                if (larguraLinha < _comprimentoMaximo.GetComprimentoMaximo())
                {
                    parte += string.IsNullOrEmpty(parte) ? palavra : " " + palavra;
                }
                else
                {
                    partesDaLinha.Add(parteContador, parte);
                    parte = palavra;
                    parteContador++;
                }
            }
            partesDaLinha.Add(parteContador, parte);

            int qtdQuebras = 0;
            foreach (KeyValuePair<int, string> item in partesDaLinha)
            {
                linhaFormat += item.Value;
                if (qtdQuebras >= partesDaLinha.Count - 1) continue;

                linhaFormat += "\n";
                qtdQuebras++;
            }

            SolidBrush br = CriaSolidBrushParaQuebrarLinhas();
            return new AdicionarTexto(graphics, linhaFormat, _adicionarTexto.TamanhoFonte, br);
        }

        private static SolidBrush CriaSolidBrushParaQuebrarLinhas()
        {
            SolidBrush br = new SolidBrush(SystemColors.ControlText) {Color = Color.Black};
            return br;
        }
    }
}