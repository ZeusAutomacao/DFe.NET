namespace NFe.Utils.Email
{
    /// <summary>
    /// Clase com configuração de e-mail
    /// </summary>
    public class ConfiguracaoEmail
    {
        public ConfiguracaoEmail(string email, string senha, string assunto, string mensagem, string servidorSmtp, int porta, bool ssl = true, bool mensagemHtml = false, int timeout = 100000, bool assincrono = true)
        {
            Email = email;
            Senha = senha;
            Assunto = assunto;
            Mensagem = mensagem;
            ServidorSmtp = servidorSmtp;
            Porta = porta;
            Ssl = ssl;
            MensagemEmHtml = mensagemHtml;
            Timeout = timeout;
            Assincrono = assincrono;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        private ConfiguracaoEmail()
        {
            
        }

        /// <summary>
        /// Endereço de e-mail do remetente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha de e-mail do remetente
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Asunto do e-mail
        /// </summary>
        public string Assunto { get; set; }

        /// <summary>
        /// Mensagem do e-mail (corpo)
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Endereço do Servidor SMTP
        /// </summary>
        public string ServidorSmtp { get; set; }

        /// <summary>
        /// Porta do Servidor SMTP
        /// </summary>
        public int Porta { get; set; }

        /// <summary>
        /// Usar conexão segura SSL
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// Determina se o corpo do email será em HTML
        /// </summary>
        public bool MensagemEmHtml { get; set; }

        /// <summary>
        /// Tempo em milissegundos que a aplicação deve aguardar o envio do e-mail 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Determina se o envio de e-mail será assíncrono (a aplicação é liberada logo após o comando de envio ser executado).
        /// Se for marcado como false, a aplicação irá esperar pelo envio
        /// </summary>
        public bool Assincrono { get; set; }
    }
}