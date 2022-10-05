using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        /*
         *  Usando o Condiguration que nem no
         *  Startup (Microsoft.Extensions.Configuration),
         *  para poder pegar as configurações de conexão
         */
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Gerando o e-mail
        public void EnviarEmail(string[] destinatario,
            string assunto, int usuarioId, string code)
        {
            // Gerando a mensagem para o usuário
            Mensagem mensagem = new Mensagem(destinatario,
                assunto, usuarioId, code);

            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);

            // Enviar e-mail
            Enviar(mensagemDeEmail);
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            // Instância um MimeMessage, para montarmos o nosso e-mail
            var mensagemCorpoEmail = new MimeMessage();

            // From recebe o remetente
            mensagemCorpoEmail.From.Add(new MailboxAddress(
                _configuration.GetValue<string>("EmailSettings:From")));

            // From recebe o destinatário
            mensagemCorpoEmail.To.AddRange(mensagem.Destinatario);

            // O subject o assunto do e-mail
            mensagemCorpoEmail.Subject = mensagem.Assunto;

            /*
             * Body recebe o conteúdo em forma de texto para que o e-mail entenda.
             * Esse texto é tipo um mime que o e-mail entende.
             */
            mensagemCorpoEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemCorpoEmail;
        }

        private void Enviar(MimeMessage sendMensagem)
        {
            // Usando um ciente Smtp para fazer o envio
            using (var client = new SmtpClient())
            {
                /*
                 * Vamos tentar abrir uma conexão para enviar um e-mail
                 * e cada não de certo, vamos jogar uma mensagem de erro.
                 * E independente do que acontecer, vamos fechar a nossa
                 * conexão para não ficar consumindo recursos.
                 */
                try
                {
                    // Conectando ao provedor de e-mail
                    // Conexão foi denifida no appsettings.json
                    // True para conexão SSL, no caso tanto faz
                    client.Connect(
                        _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);

                    // Removendo a autenticação XOUATH2, porque não é o nosso foco
                    client.AuthenticationMechanisms.Remove("XOUATH2");

                    // Passando a nossa autenticação
                    client.Authenticate(
                        _configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

                    // Mandar e-mail
                    client.Send(sendMensagem);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

    }
}
