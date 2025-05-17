using Core.Data;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Chat;
using System.Diagnostics;

namespace Core.Services
{
    class AIService
    {
        public async Task<string> Run(IChatCompletionService chatCompletionService, string receituario, string bula)
        {
            ChatHistory chatHistory = [];

            chatHistory.AddSystemMessage("Você é um assistente de inteligência artificial especializado em análise de receituários agronômicos e bulas de produtos fitossanitários." +
                "Seu objetivo é comparar o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                "Se houver alguma inconsistência ou não conformidade, forneça uma explicação detalhada e sugira correções." +
                "Se tudo estiver correto, informe que não há inconsistências ou não conformidades." +
                $"Exemplo de resposta: {AgronomicMockData.exemplo}");

            chatHistory.AddUserMessage("Compare o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                $"Receituário:\n{receituario}" +
                $"Bula:\n{bula}");

            var response = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
            var result = "";

            await foreach (var chunk in response)
            {
                Console.Write(chunk.Content);
                result += chunk.Content;
            }

            Console.WriteLine();

            // Tenta converter e salvar o Markdown como HTML
            try
            {
                string solutionRoot = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(solutionRoot, "output.html");

                await MarkdownService.ConvertAndSaveMarkdownToHtmlAsync(result, outputPath);
                Console.WriteLine($"Markdown convertido e salvo com sucesso em: {outputPath}");

                // Criar um processo para abrir o arquivo com o aplicativo padrão
                ProcessStartInfo startInfo = new()
                {
                    FileName = outputPath,
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao converter Markdown para HTML: {ex.Message}");
            }

            return result;
        }
    }
}
