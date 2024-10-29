using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace NFe.Danfe.Base.Fontes
{
    public static class Fonte
    {
        /// <summary>
        /// Obtém um objeto <see cref="FontFamily"/> a partir de um array de bytes. Útil para carregar uma fonte a partir de um arquivo de recurso
        /// </summary>
        /// <returns></returns>
        public static FontFamily CarregarDeByteArray(byte[] fonte, out PrivateFontCollection colecaoDeFonte)
        {
            var handle = GCHandle.Alloc(fonte, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(fonte, 0);
                colecaoDeFonte = new PrivateFontCollection();
                colecaoDeFonte.AddMemoryFont(ptr, fonte.Length);
                return colecaoDeFonte.Families[0];
            }
            finally
            {
                handle.Free();
            }
        }
    }
}