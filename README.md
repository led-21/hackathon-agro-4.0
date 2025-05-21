# hackathon-agro-4.0
HACKATHON AGRO 4.0 - CREA MS

## Inovação
[Link para a inovaçao da solução](INOVACAO.md)

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
# Análise Comparativa: Receituário vs Bula Oficial

## Comparação de Dados Técnicos

| Parâmetro | Receituário | Bula Oficial | Conformidade |
|-----------|-------------|--------------|--------------|
| *Produto/Princípios Ativos* | Tiametoxam 141 g/L + Lambda-cialotrina 106 g/L | TIAMETHOXAM 141 g/L + LAMBDA-CYHALOTHRIN 106 g/L | ✅ Conforme |
| *Cultura* | Soja (Glycine max) | Soja | ✅ Conforme |
| *Praga-alvo* | Percevejo-marrom (Euschistus heros) | Percevejo-marrom (Euschistus heros) | ✅ Conforme |
| *Dose* | 100 mL/ha | 200 mL/ha (mínimo) | ⚠ Não conforme |
| *Volume de Calda* | 2150 L/ha | 200 L/ha (padrão para soja) | ⚠ Não conforme |
| *Método de Aplicação* | Pulverização foliar com trator | Pulverização foliar | ✅ Conforme |
| *Época de Aplicação* | Início do estágio R3 | Varia conforme a praga e cultura | ✅ Conformidade Geral |
| *Intervalo de Segurança* | 7 dias | 30 dias para soja | ⚠ Não conforme |
| *Número Máximo de Aplicações* | 2 por ciclo | 2 a 3 aplicações (dependendo da praga) | ✅ Conforme |
| *Uso de EPI* | Uso obrigatório conforme legislação vigente | Uso obrigatório conforme legislação vigente | ✅ Conforme |

## Análise de Discrepâncias

1. **Dose**: O receituário prescreve 100 mL/ha, enquanto a bula indica uma dose mínima de 200 mL/ha para essa praga e cultura. É essencial que a dose prescrita seja adequada para garantir a eficácia do produto.
2. **Volume de Calda**: O receituário especifica 2150 L/ha, o que está significativamente acima do volume recomendado na bula, que é de 200 L/ha.
3. **Intervalo de Segurança**: O receituário indica um intervalo de segurança de 7 dias, mas na bula para soja, o intervalo é de 30 dias, o que é importante para evitar contaminações e garantir a segurança alimentar.

## Conclusão

O receituário apresenta algumas não conformidades críticas em relação às recomendações da bula oficial. As principais não conformidades estão nas doses prescritas, volume de calda e intervalo de segurança, que podem comprometer tanto a eficácia do controle da praga quanto a segurança ao consumidor.

### Sugestões de Correção:
- **Ajustar a Dose**: Alterar a dose prescrita para no mínimo 200 mL/ha, conforme indicado na bula.
- **Reduzir o Volume de Calda**: Ajustar o volume de calda para o máximo de 200 L/ha, a menos que haja justificativa técnica para o uso de um volume maior.
- **Revisar o Intervalo de Segurança**: Modificar o intervalo de segurança para 30 dias antes da colheita, de acordo com a bula.

Diante das não conformidades encontradas, irei notificar o responsável. 

### Notificação
A notificação sobre as não conformidades encontradas no receituário foi enviada com sucesso ao Engenheiro Agrônomo João da Silva. As correções sugeridas foram destacadas para garantir que sejam feitas as devidas adequações para assegurar a conformidade com as diretrizes da bula oficial. 
