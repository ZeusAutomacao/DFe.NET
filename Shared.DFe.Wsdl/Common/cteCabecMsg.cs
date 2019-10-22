namespace CTe.CTeOSDocumento.Common
{
    /// <summary>
    /// Classe para os campos contidos no cabeçalho do Envelope SOAP
    /// </summary>
    public class cteCabecMsg
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