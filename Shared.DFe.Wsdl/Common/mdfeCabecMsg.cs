namespace CTe.CTeOSDocumento.Common
{
    public class mdfeCabecMsg
    {
        private string cUFField;
        private string versaoDadosField;

        public string cUF
        {
            get { return cUFField; }
            set { cUFField = value; }
        }

        public string versaoDados
        {
            get { return versaoDadosField; }
            set { versaoDadosField = value; }
        }
    }
}