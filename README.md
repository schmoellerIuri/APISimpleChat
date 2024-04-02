# API REST para Serviço de Webchat

Esta API REST foi desenvolvida para atuar como o backend de um serviço de webchat. A arquitetura da API é composta por três entidades principais: Usuário, Conversa e Mensagem.

## Endpoints

### Usuário (5 endpoints):

- **GET** - `/api/Usuario`:
  Retorna todos os usernames e IDs presentes no sistema.

- **POST** - `/api/Usuario`:
  Retorna o ID após a criação de um usuário e consome um JSON com informações do usuário.

- **PUT** - `/api/Usuario`:
  Retorna o ID após a atualização de um usuário e consome um JSON com informações do usuário.

- **GET** - `/api/Usuario/id`:
  Retorna o usuário com o ID fornecido como parâmetro.

- **DELETE** - `/api/Usuario/id`:
  Deleta o usuário com o ID fornecido como parâmetro.

- **POST** - `/api/Usuario/login`:
  Realiza a autenticação do usuário conforme o JSON consumido.

### Conversa (3 endpoints):

- **GET** - `/api/Conversa/id`:
  Retorna as conversas em que um usuário de determinado ID está presente.

- **DELETE** - `/api/Conversa/id`:
  Deleta a conversa com o ID determinado.

- **POST** - `/api/Conversa`:
  Cria uma conversa a partir do JSON consumido.

### Mensagem (3 endpoints):

- **GET** - `/api/Mensagem/id`:
  Retorna todas as mensagens em uma conversa com o ID especificado.

- **DELETE** - `/api/Mensagem/id`:
  Deleta a mensagem com o ID especificado.

- **POST** - `/api/Mensagem`:
  Envia uma mensagem, criando-a e adicionando-a a uma determinada conversa. Utiliza um websocket para enviar a mensagem em tempo real para outro usuário.

**Observação:** Nem todos os endpoints são acessíveis livremente; a maioria deles exige autenticação para retornar informações.

## Recursos Adicionais

- Autenticação por criptografia assimétrica.
- Uso de websockets para envio de mensagens em tempo real.

## Versão em Produção

Uma versão em "produção" da API está disponível em [Swagger](https://api-simplechat.azurewebsites.net/swagger).
