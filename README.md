API REST desenvolvida para atuar como backend de um serviço de webchat.

A arquitetura possui 3 entidades:

Usuário (5 endpoints):

GET - /api/Usuario -> retorna todos os usernames e ids presentes no sistema;

POST - /api/Usuario -> retorna o ID após a criação de um usuário e consome um JSON com informações do usuário;

PUT - /api/Usuario -> retorna o ID após a atualização de um usuário e consome um JSON;

GET - /api/Usuario/id -> retorna o usuário com o ID de parâmetro;

DELETE - /id -> deleta o usuário com o ID de parâmetro;

POST - /api/Usuario/login -> realiza a autenticação do usuário de acordo com o JSON consumido;

Conversa (3 endpoints):

GET - /api/Conversa/id -> retorna as conversas que um usuário de determinado ID está presente;

DELETE - /api/Conversa/id -> deleta a conversa com o determinado ID;

POST - /api/Conversa -> cria uma conversa a partir de um JSON consumido;

Mensagem(3 endpoints):

GET - /api/Mensagem/id -> retorna todas as mensagens que uma conversa de determinado ID possui;

DELETE - /api/Mensagem/id -> deleta a mensagem com determinado ID;

POST - /api/Mensagem -> envia uma mensagem, ou seja cria ela e adiciona em determinada conversa, além de utilizar um websocket para enviar a mensagem a outro usuário;

Nem todos os endpoints são acessíveis livremente, a maioria deles necessita de autenticação para retornar informações.

Outras features utilizadas:

Autenticação por criptografia assimétrica;

Uso de websockets para envio de mensagens em tempo real;


Versão em "produção" : https://api-simplechat.azurewebsites.net/swagger
