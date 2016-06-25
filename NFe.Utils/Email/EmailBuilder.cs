using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace NFe.Utils.Email
{
    public sealed class EmailBuilder
    {
        public event EventHandler AntesDeEnviarEmail;
        public event EventHandler DepoisDeEnviarEmail;
        private readonly ConfiguracaoEmail _configuracaoEmail;
        private readonly List<string> _destinatarios;
        private readonly List<string> _anexos;

        public EmailBuilder(ConfiguracaoEmail configuracaoEmail)
        {
            _configuracaoEmail = configuracaoEmail;
            _destinatarios = new List<string>();
            _anexos = new List<string>();
        }

        public EmailBuilder AddDestinatario(string email)
        {
            if (EmailNaoEValido(email))
                throw new ArgumentException("E-mail do destinatário e inválido");

            _destinatarios.Add(email);
            return this;
        }

        public EmailBuilder AddAnexo(string anexo)
        {
            _anexos.Add(anexo);
            return this;
        }

        public void Enviar()
        {
            Verificacao();


            var mailMessage = new MailMessage { From = new MailAddress(_configuracaoEmail.EmailDeUsuario) };

            _destinatarios.ForEach(mailMessage.To.Add);

            ColocaValoresPadoresEmAssuntoOuMensagem();

            mailMessage.Subject = _configuracaoEmail.Assunto;
            mailMessage.Body = _configuracaoEmail.MensagemDoEmail;

            _anexos.ForEach(a => { mailMessage.Attachments.Add(new Attachment(a, MediaTypeNames.Application.Octet)); });

            var smtpClient = new SmtpClient(_configuracaoEmail.ServidorSmtp, _configuracaoEmail.Porta)
            {
                EnableSsl = _configuracaoEmail.Ssl,
                UseDefaultCredentials = true
            };
            var cred = new NetworkCredential(_configuracaoEmail.EmailDeUsuario,
                _configuracaoEmail.Senha);
            smtpClient.Credentials = cred;

            smtpClient.Timeout = _configuracaoEmail.Timeout == 0 ? smtpClient.Timeout : _configuracaoEmail.Timeout;
            OnAntesDeEnviarEmail();
            smtpClient.Send(mailMessage);
            OnDepoisDeEnviarEmail();
        }

        private void ColocaValoresPadoresEmAssuntoOuMensagem()
        {
            if (string.IsNullOrEmpty(_configuracaoEmail.Assunto))
                _configuracaoEmail.Assunto = string.Empty;

            if (string.IsNullOrEmpty(_configuracaoEmail.MensagemDoEmail))
                _configuracaoEmail.MensagemDoEmail = string.Empty;
        }

        private void Verificacao()
        {
            if (_configuracaoEmail == null) throw new InvalidOperationException("Porfavor configurar o envio de e-mail.");

            if (string.IsNullOrEmpty(_configuracaoEmail.EmailDeUsuario))
                throw new ArgumentException("Porfavor digitar um email de usuário");

            if(EmailNaoEValido(_configuracaoEmail.EmailDeUsuario))
                throw new ArgumentException("E-mail do usuário e inválido");

            if (string.IsNullOrEmpty(_configuracaoEmail.Senha))
                throw new ArgumentException("Porfavor digitar uma senha de usuário");

            if (string.IsNullOrEmpty(_configuracaoEmail.ServidorSmtp))
                throw new ArgumentException("Porfavor digitar um servidor smtp");
        }

        private bool EmailNaoEValido(string email)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return !rg.IsMatch(email);
        }

        private void OnAntesDeEnviarEmail()
        {
            AntesDeEnviarEmail?.Invoke(this, EventArgs.Empty);
        }

        private void OnDepoisDeEnviarEmail()
        {
            DepoisDeEnviarEmail?.Invoke(this, EventArgs.Empty);
        }
    }
}