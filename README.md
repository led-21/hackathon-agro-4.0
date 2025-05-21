# hackathon-agro-4.0
HACKATHON AGRO 4.0 - CREA MS

## Arquitetura Geral

### Camada de Configuração e Segurança
- **Azure Key Vault**: Armazena e recupera segredos como chaves de API.
- **Autenticação**: Usa `DefaultAzureCredential` para acesso seguro.

### Camada de IA Generativa
- **Semantic Kernel**: Orquestra chamadas para **Azure OpenAI**.
- **Modelo GPT-4o-mini**: Processa linguagem natural para análise de receituários.
- **Plugins**: Inclui `EmailNotificationPlugin` para notificações automáticas.

### Camada de Busca e Vetorização
- **Azure AI Search**: Recupera bulas de produtos fitossanitários.
- **Vetorização**: Usa embeddings para melhorar a precisão da busca.

### Camada de Aplicação
- **Avaliação de Receituário**: Compara receituário e bula, identificando inconsistências.
- **Notificação**: Envia alertas por e-mail em caso de desconformidade.

## 🔄 Fluxo de Execução

### Inicialização
1. Recupera segredos do **Azure Key Vault**.
2. Configura **Semantic Kernel** e **Azure AI Search**.

### Processamento
1. O usuário fornece um **receituário**.
2. O sistema busca a **bula** correspondente.
3. O **GPT-4o-mini** analisa inconsistências.

### Resposta
- **Se houver problemas**: O sistema gera um relatório e envia uma **notificação**.
- **Caso contrário**: Informa que está tudo correto.

🛠 Tecnologias Utilizadas
| **Categoria**     | **Tecnologia**                     |
|-------------------|----------------------------------|
| **Cloud**        | Azure                             |
| **IA Generativa** | Azure OpenAI (GPT-4o-mini)      |
| **Orquestração**  | Semantic Kernel                 |
| **Armazenamento** | Azure AI Search                 |
| **Autenticação**  | DefaultAzureCredential          |
| **Comunicação**   | Simulação de envio de e-mail    |


# Exemplo output
## Análise Comparativa: Receituário vs Bula Oficial

## Comparação de Dados Técnicos

| Parâmetro                       | Receituário                                        | Bula Oficial                                                 | Conformidade         |
|---------------------------------|---------------------------------------------------|-------------------------------------------------------------|----------------------|
| *Produto/Princípios Ativos*     | Tiametoxam 141 g/L + Lambda-cialotrina 106 g/L   | TIAMETOXAM 141 g/L + LAMBDA-CIALOTRINA 106 g/L             | ✅ Conforme           |
| *Cultura*                       | Soja (Glycine max)                                | Soja                                                        | ✅ Conforme           |
| *Praga-alvo*                    | Percevejo-marrom (Euschistus heros)              | Percevejo-marrom (Euschistus heros)                         | ✅ Conforme           |
| *Dose*                          | 200 mL/ha                                         | 200 mL/ha                                                   | ✅ Conforme           |
| *Volume de Calda*               | 150 L/ha                                          | 150 L/ha                                                    | ✅ Conforme           |
| *Modo de Aplicação*             | Pulverização foliar com trator                     | Pulverização foliar                                         | ✅ Conforme           |
| *Época de Aplicação*            | Início do estágio R3                              | Início do estágio R3 de soja                                | ✅ Conforme           |
| *Intervalo de Segurança*         | 7 dias                                            | 30 dias                                                     | ⚠ Não conforme       |
| *Número Máximo de Aplicações*    | 2 por ciclo                                       | 3 por ciclo                                                 | ⚠ Não conforme       |
| *Uso de EPI*                    | Uso obrigatório conforme legislação vigente       | Uso obrigatório conforme legislação vigente                  | ✅ Conforme           |

## Análise de Discrepâncias

1. **Intervalo de Segurança**: 
   - O receituário indica um intervalo de segurança de 7 dias, enquanto a bula oficial especifica 30 dias para a soja. Essa é uma discrepância significativa, pois o intervalo de segurança é importante para garantir que o produto não cause efeitos indesejados na saúde humana ou no meio ambiente após a aplicação.
   
   **Correção Sugerida**: O intervalo de segurança no receituário deve ser alterado para refletir a bula oficial, 30 dias antes da colheita para soja.

2. **Número Máximo de Aplicações**: 
   - O receituário prescreve um máximo de 2 aplicações por ciclo, enquanto a bula oficial permite até 3 aplicações. Essa diferença pode levar à subutilização do produto e, potencialmente, a uma eficácia reduzida no controle da praga.

   **Correção Sugerida**: O receituário deve ser modificado para acomodar o número máximo de 3 aplicações conforme indicado na bula.
   
## Conclusão

O receituário apresenta boa conformidade com a bula, mas há inconsistências no intervalo de segurança e no número máximo de aplicações. As correções necessárias são cruciais para garantir a segurança no uso do produto e maximizar sua eficácia.

Por favor, proceda com as correções e, com base nas não conformidades, notificarei o responsável.Notificações de não conformidade foram enviadas para o responsável. As seguintes correções devem ser feitas no receituário:

1. **Intervalo de Segurança**: Alterar de 7 dias para 30 dias conforme a bula oficial.
2. **Número Máximo de Aplicações**: Alterar de 2 para 3 por ciclo conforme a bula oficial.
