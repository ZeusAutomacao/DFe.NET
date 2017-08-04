using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace DFe.DocumentosEletronicos.NFe.Utils.Email
{
    public delegate void ErroAoEnviarEmail(Exception erro);

    public sealed class EmailBuilder
    {
        public event EventHandler AntesDeEnviarEmail;
        public event EventHandler DepoisDeEnviarEmail;
        public event ErroAoEnviarEmail ErroAoEnviarEmail = delegate { };
        private readonly ConfiguracaoEmail _configuracaoEmail;
        private readonly List<string> _destinatarios;
        private readonly List<string> _anexos;

        public EmailBuilder(ConfiguracaoEmail configuracaoEmail)
        {
            _configuracaoEmail = configuracaoEmail;
            _destinatarios = new List<string>();
            _anexos = new List<string>();
        }

        /// <summary>
        /// Adiciona um e-mail de destinatário
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public EmailBuilder AdicionarDestinatario(string email)
        {
            if (!EmailValido(email))
                throw new ArgumentException("E-mail do destinatário é inválido!");

            _destinatarios.Add(email);
            return this;
        }

        /// <summary>
        /// Adiciona um anexo. Informar o path do arquivo a ser anexado.
        /// </summary>
        /// <param name="pathArquivo"></param>
        /// <returns></returns>
        public EmailBuilder AdicionarAnexo(string pathArquivo)
        {
            _anexos.Add(pathArquivo);
            return this;
        }

        /// <summary>
        /// Envia um e-mail
        /// </summary>
        public void Enviar()
        {
            Verificacao();
            
            var mensagem = new MailMessage
            {
                From = new MailAddress(_configuracaoEmail.Email),
                Subject = _configuracaoEmail.Assunto ?? string.Empty, //Se nenhum assunto foi informado, enviar vazio
                Body = _configuracaoEmail.Mensagem ?? string.Empty, //Se nenhuma mensagem foi informada, enviar vazio
                IsBodyHtml = _configuracaoEmail.MensagemEmHtml 
                
            };
            _destinatarios.ForEach(mensagem.To.Add);

            _anexos.ForEach(a => { mensagem.Attachments.Add(new Attachment(a, MediaTypeNames.Application.Octet)); });

            var cliente = new SmtpClient(_configuracaoEmail.ServidorSmtp, _configuracaoEmail.Porta)
            {
                EnableSsl = _configuracaoEmail.Ssl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            cliente.SendCompleted += (sender, args) =>
            {
                if (args.Error != null)
                    ErroAoEnviarEmail(args.Error);
                else
                    OnDepoisDeEnviarEmail();
                mensagem.Dispose();
                cliente.Dispose();
            };

            var cred = new NetworkCredential(_configuracaoEmail.Email,
                _configuracaoEmail.Senha);
            cliente.Credentials = cred;

            cliente.Timeout = _configuracaoEmail.Timeout == 0 ? cliente.Timeout : _configuracaoEmail.Timeout;
            OnAntesDeEnviarEmail();
            if (_configuracaoEmail.Assincrono)
                cliente.SendAsync(mensagem, null);
            else
            {
                cliente.Send(mensagem);
                OnDepoisDeEnviarEmail();
                mensagem.Dispose();
                cliente.Dispose();
            }

        }

        /// <summary>
        /// Valida a configuração antes de enviar
        /// </summary>
        private void Verificacao()
        {
            if (_configuracaoEmail == null) throw new InvalidOperationException("Configure o envio de e-mail antes de usar esse recurso!");

            if (string.IsNullOrEmpty(_configuracaoEmail.Email))
                throw new ArgumentException("O e-mail do remetente não foi informado!");

            if(!EmailValido(_configuracaoEmail.Email))
                throw new ArgumentException("E-mail do remetente é inválido!");

            if (string.IsNullOrEmpty(_configuracaoEmail.Senha))
                throw new ArgumentException("A senha de e-mail do remetente não foi informada!");

            if (string.IsNullOrEmpty(_configuracaoEmail.ServidorSmtp))
                throw new ArgumentException("Servidor SMTP não foi informado!");
        }

        /// <summary>
        /// Checa se um e-mail é válido com base em sua estrutura
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailValido(string email)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return rg.IsMatch(email);
        }

        private void OnAntesDeEnviarEmail()
        {
            if (AntesDeEnviarEmail != null) AntesDeEnviarEmail.Invoke(this, EventArgs.Empty);
        }

        private void OnDepoisDeEnviarEmail()
        {
            if (DepoisDeEnviarEmail != null) DepoisDeEnviarEmail.Invoke(this, EventArgs.Empty);
        }
    }
}