using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Core.KernelFunctions
{
    class EmailNotificationPlugin
    {
        //Simula a Notificação por Email
        [KernelFunction, Description("Notificaçao por em e-mail em caso de não conformidade do receituario agronomico")]
        public static string SendEmailNotification(
            [Description("Nome do agronomo que preencheu o receituario")]string nome)
        {
            string email = "exemplo@email.com";
            string subject = "Notificação de Receituário Agronômico";
            string body = $"""
                Assunto: Notificação de Desconformidade no Receituário Agronômico
                Prezado(a) {nome},
                Informamos que o receituário agronômico referente a [informação específica, como número do receituário ou data] foi identificado como estando em desconformidade com as normas estabelecidas.
                Detalhes da desconformidade:
                - [Descrição específica do problema encontrado]
                - [Norma ou regulamento relacionado]
                Solicitamos que as devidas correções sejam realizadas o mais breve possível para garantir a conformidade com as regulamentações vigentes. Em caso de dúvidas, favor entrar em contato conosco pelo [e-mail ou telefone de suporte].
                Agradecemos sua atenção e colaboração.
                Atenciosamente,
                [Seu Nome ou Nome da Empresa]
                [Cargo ou Departamento]
                [Contato]
                
                """;

            // Simula o envio de um e-mail
            Console.WriteLine($"Enviando e-mail para: {email}");
            Console.WriteLine($"Assunto: {subject}");
            Console.WriteLine($"Corpo: {body}");
            return "E-mail enviado com sucesso!";
        }
    }
}
