using Markdig;
using System.Diagnostics;

namespace Core.Services
{
    class MarkdownService
    {
        public static async Task SaveAndOpen(string markdown)
        {
            // Tenta converter e salvar o Markdown como HTML
            try
            {
                string solutionRoot = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(solutionRoot, "output.html");

                await MarkdownService.ConvertAndSaveMarkdownToHtmlAsync(markdown, outputPath);
                Console.WriteLine($"Markdown convertido e salvo com sucesso em: {outputPath}");

                // Criar um processo para abrir o arquivo com o aplicativo padrão
                ProcessStartInfo startInfo = new()
                {
                    FileName = outputPath,
                    UseShellExecute = true
                };

                Process.Start(startInfo);

                startInfo = new()
                {
                    FileName = "output.md",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao converter Markdown para HTML: {ex.Message}");
            }
        }

        // Método para converter Markdown para HTML e salvar em arquivo
        public static async Task ConvertAndSaveMarkdownToHtmlAsync(string markdownContent, string outputFilePath)
        {
            if (string.IsNullOrWhiteSpace(markdownContent))
            {
                throw new ArgumentException("O conteúdo Markdown não pode ser vazio ou nulo.");
            }

            // Converter Markdown para HTML usando a biblioteca Markdig
            var pipeline = new MarkdownPipelineBuilder()
                                .UseGridTables() // Extensão para tabelas no formato grid
                                .UsePipeTables() // Extensão para tabelas com pipes
                                .UseGridTables() // Extensão para tabelas no formato grid
                                .UseAdvancedExtensions()
                                .Build();

            string htmlContent = Markdig.Markdown.ToHtml(markdownContent, pipeline);

            // Adicionar estrutura HTML básica se necessário
            if (!htmlContent.Contains("<html>", StringComparison.OrdinalIgnoreCase))
            {
                htmlContent = $@"<!DOCTYPE html>
                                  <html>
                                        <head>
                                            <meta charset=""utf-8"">
                                            <title>Documento Convertido</title>
                                        </head>
                                        <body>
                                            {htmlContent}
                                        </body>
                                  </html>";
            }

            // Salvar o conteúdo HTML no arquivo
            await File.WriteAllTextAsync(outputFilePath, htmlContent);
            // Salvar o conteúdo no formato Markdown
            await File.WriteAllTextAsync("output.md", markdownContent);
        }

    }
}
