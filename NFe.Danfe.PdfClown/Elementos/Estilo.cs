using NFe.Danfe.PdfClown.Graphics;
using pcf = org.pdfclown.documents.contents.fonts;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Coleção de fontes e medidas a serem compartilhadas entre os elementos básicos.
    /// </summary>
    internal class Estilo
    {
        public float PaddingSuperior { get; set; }
        public float PaddingInferior { get; set; }
        public float PaddingHorizontal { get; set; }
        public float FonteTamanhoMinimo { get; set; }

        public pcf.Font FonteInternaRegular { get; set; }
        public pcf.Font FonteInternaNegrito { get; set; }
        public pcf.Font FonteInternaItalico { get; set; }

        public Fonte FonteCampoCabecalho { get; private set; }
        public Fonte FonteCampoConteudo { get; private set; }
        public Fonte FonteCampoConteudoNegrito { get; private set; }
        public Fonte FonteBlocoCabecalho { get; private set; }
        public Fonte FonteNumeroFolhas { get; private set; }

        public Estilo(pcf.Font fontRegular, pcf.Font fontBold, pcf.Font fontItalic, float tamanhoFonteCampoCabecalho = 6, float tamanhoFonteConteudo = 10)
        {
            PaddingHorizontal = 0.75F;
            PaddingSuperior = 0.65F;
            PaddingInferior = 0.3F;

            FonteInternaRegular = fontRegular;
            FonteInternaNegrito = fontBold;
            FonteInternaItalico = fontItalic;

            FonteCampoCabecalho = CriarFonteRegular(tamanhoFonteCampoCabecalho);
            FonteCampoConteudo = CriarFonteRegular(tamanhoFonteConteudo);
            FonteCampoConteudoNegrito = CriarFonteNegrito(tamanhoFonteConteudo);
            FonteBlocoCabecalho = CriarFonteRegular(7);
            FonteNumeroFolhas = CriarFonteNegrito(10F);
            FonteTamanhoMinimo = 5.75F;
        }

        public Fonte CriarFonteRegular(float emSize) => new Fonte(FonteInternaRegular, emSize);
        public Fonte CriarFonteNegrito(float emSize) => new Fonte(FonteInternaNegrito, emSize);
        public Fonte CriarFonteItalico(float emSize) => new Fonte(FonteInternaItalico, emSize);

    }
}
