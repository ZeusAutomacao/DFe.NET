namespace GraphicsPrinter
{
    public class ComprimentoMaximo
    {
        private readonly int _valor;

        public ComprimentoMaximo(int comprimentoMaximo)
        {
            _valor = comprimentoMaximo;
        }

        public int GetComprimentoMaximo()
        {
            return _valor;
        }
    }
}