namespace DFe.Classes.Assinatura
{
    public class KeyInfo
    {
        public KeyInfo()
        {
            X509Data = new X509Data();
        }

        /// <summary>
        ///     XS20 - Grupo X509
        /// </summary>
        public X509Data X509Data { get; set; }
    }
}