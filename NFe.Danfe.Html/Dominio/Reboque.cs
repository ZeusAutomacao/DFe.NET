namespace NFe.Danfe.Html.Dominio
{
    public class Reboque
    {
        public string Placa { get; }

        public string Rntc { get; }
        public string Uf { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Reboque(string placa, string rntc, string uf)
        {
            Placa = placa;
            Rntc = rntc;
            Uf = uf;    
        }
    }
}
