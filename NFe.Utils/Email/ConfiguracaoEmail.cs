namespace NFe.Utils.Email
{
    public class ConfiguracaoEmail
    {
        public string ServidorSmtp { get; set; }
        public int Porta { get; set; }
        public string EmailDeUsuario { get; set; }
        public string Senha { get; set; }
        public string Assunto { get; set; }
        public bool Ssl { get; set; }
        public string MensagemDoEmail { get; set; }
        public int Timeout { get; set; }
    }
}