namespace DFe.Wsdl.Common
{
    public class mdfeCabMsg
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