namespace CTe.Utils.Extencoes
{
    public static class ExtCTe
    {
        public static string Chave(this Classes.CTe cte)
        {
            var chave = cte.infCte.Id.Substring(3, 44);
            return chave;
        }
    }
}