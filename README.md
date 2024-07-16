# MicroserviceRabbitMq

Este é um microserviço simples que usa RabbitMQ para enviar mensagens. O serviço aceita requisições HTTP POST contendo a mensagem e informações do destinatário e então envia essa mensagem para uma fila do RabbitMQ.

## Configuração

### Requisitos

- Docker
- Docker Compose
- .NET 8

### Configuração do RabbitMQ

Adicione as seguintes configurações no arquivo `appsettings.json`:

```
{
  "RabbitMQSettings": {
    "HostName": "rabbitmq",
    "UserName": "guest",
    "Password": "guest"
  }
}
```

# Estrutura do Projeto

- `Program.cs`: Configuração principal do aplicativo.
- `Controllers/MessagesController.cs`: Controlador para lidar com as requisições HTTP.
- `Services/MessageService.cs`: Serviço que envia mensagens para o RabbitMQ.
- `Services/RabbitMQClientService.cs`: Serviço cliente do RabbitMQ.
- `Models/MessageRequest.cs`: Modelo da requisição de mensagem.
- `Models/RabbitMQSettings.cs`: Configurações do RabbitMQ.

## Como Executar

### Docker Compose

Execute o Docker Compose:

### Execute o Docker Compose:

	docker-compose up --build

## Enviar Mensagem para Fila RabbitMQ

Para enviar uma mensagem para uma fila RabbitMQ, faça uma requisição HTTP POST para o endpoint `http://localhost:80/messages` com o seguinte JSON:

```
{
    "queue": "Teste",
    "message": "Primeira mensagem",
    "recipientEmail": "",
    "recipientPhone": ""
}
```

## Exemplo com curl

Você pode usar o curl para enviar a requisição:

```
curl -X POST http://localhost:80/messages -H "Content-Type: application/json" -d '{
  "queue": "Teste",
  "message": "Primeira mensagem",
  "recipientEmail": "",
  "recipientPhone": ""
}'
```

## Verificação
Para verificar se a mensagem foi enviada com sucesso para a fila RabbitMQ, acesse a interface de gerenciamento do RabbitMQ em http://localhost:15672/ (usuário: guest, senha: guest).

## Contribuindo
Se você encontrar algum problema ou tiver sugestões, sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Licença
Este projeto está licenciado sob a MIT License.