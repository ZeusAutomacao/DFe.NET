using System;
using System.IO;
using System.Reflection;

namespace DFe.Utils
{
    public static class FrxFileHelper
    {
        public static byte[] TryGetFrxFile(string caminho)
        {
            try
            {
                if (!caminho.EndsWith(".frx"))
                {
                    caminho += ".frx";
                }

                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Erro no Zeus. Assembly de relatório nao encontrado"), caminho);
                var bytes = File.ReadAllBytes(path);
                return bytes.Length == 0 ? null : bytes;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
