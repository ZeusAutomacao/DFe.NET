// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


namespace NFe.Danfe.Html.Dominio
{
    public class Volume
    {
        #region Propriedades

        public decimal? Quantidade { get; }

        public string Especie { get; }

        public decimal PesoBruto { get; }

        public decimal PesoLiq { get; }

        public string Marca { get; }

        public string NumeroVolume { get; }

        public Volume(decimal? quantidade, string especie, decimal pesoBruto, decimal pesoLiq, string marca,string numeroVolume)
        {
            Quantidade = quantidade;
            Especie = especie;
            PesoBruto = pesoBruto;
            PesoLiq = pesoLiq;
            Marca = marca;
            NumeroVolume = numeroVolume;
        }

        #endregion
    }
}