using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Core.KernelFunctions
{
    class EmailNotificationPlugin
    {
        //Simula a Notificação por Email
        [KernelFunction, Description("Notificaçao por em e-mail em caso de não conformidade do receituario agronomico")]
        public static string SendEmailNotification(
            [Description("Nome do agronomo que preencheu o receituario")]string nome,
            [Description("Nao conformidade encontrados no receituario")] string desconformidades)
        {
            string email = "exemplo@email.com";
            string subject = "Notificação de Receituário Agronômico";
            string body = $"""
                Assunto: Notificação de Desconformidade no Receituário Agronômico
                Prezado(a) {nome},
                Informamos que o receituário agronômico referente a [informação específica, como número do receituário ou data] foi identificado como estando em desconformidade com as normas estabelecidas.
                Detalhes da desconformidade:
                - {desconformidades}
                - [Norma ou regulamento relacionado]
                Solicitamos que as devidas correções sejam realizadas o mais breve possível para garantir a conformidade com as regulamentações vigentes. Em caso de dúvidas, favor entrar em contato conosco pelo [e-mail ou telefone de suporte].
                Agradecemos sua atenção e colaboração.
                Atenciosamente,
                Fiscalizaçao
                """;

            // Simula o envio de um e-mail
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Enviando e-mail para: {email}");
            Console.WriteLine($"Assunto: {subject}");
            Console.WriteLine($"Corpo: {body}");
            Console.ForegroundColor = ConsoleColor.White;
            return "E-mail enviado com sucesso!";
        }
    }
}
