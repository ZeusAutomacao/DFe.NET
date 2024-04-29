using NFe.Danfe.PdfClown.Graphics;

namespace NFe.Danfe.PdfClown.Elementos
{
    /// <summary>
    /// Elemento básico no DANFE.
    /// </summary>
    internal abstract class ElementoBase : DrawableBase
    {
        public Estilo Estilo { get; protected set; }
        public virtual bool PossuiContono => true;

        public ElementoBase(Estilo estilo)
        {
            Estilo = estilo ?? throw new ArgumentNullException(nameof(estilo));
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            if (PossuiContono)
                gfx.StrokeRectangle(BoundingBox, 0.25f);
        }
    }
}
