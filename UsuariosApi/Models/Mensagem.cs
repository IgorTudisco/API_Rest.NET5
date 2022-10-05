using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Models
{
    // Class responsável por construir a mensagem
    public class Mensagem
    {
        /*
         * Usando os pacotes Mailkit e Mimekit, para podemos
         * usar um tipo especial para o nosso destinatário
         */
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario,
            string assunto, int usuarioId, string codigo)
        {
            // Criando a instância da lista de tipo especial para e-mail.
            Destinatario = new List<MailboxAddress>();

            // Adicionando e transformando a minha lista de destinatário em tipo especial
            Destinatario.AddRange(
                destinatario.Select(d => new MailboxAddress(d)));

            Assunto = assunto;

            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }

    }
}
