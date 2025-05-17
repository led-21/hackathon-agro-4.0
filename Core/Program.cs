using Core.Data;
using Core.Services;

AIService aIService = new();

var result = await aIService.AvaliarReceituario(AgronomicMockData.receituario, AgronomicMockData.bula);

MarkdownService.SalveAndOpen(result).Wait();