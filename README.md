# Como executar o projeto

O projeto está todo configurado em containers para subir as aplicações de uma vez no ambiente de desenvolvimento.

O banco de Dados Postgres contém as informações necessárias para todas as aplicações.

Para subir o container atualizado já realizando o build basta executar:
`docker-compose up -d --build`

A API já possui as migrations para criar a estrutura inicial do banco de dados e dados iniciais nas tabelas.

Para rodar as migrations acesse o container da api e execute:
`dotnet ef database update`

Para logar na API é necessário uma AppKey e uma SecretKey, estes dados ficam na tabela Aplicacao de acordo com a aplicação que vai consumir a API.

A API poderá ser acessada como:
http://localhost:5100

O banco de dados, se precisar acessar pelo Dbeaver para consultar os dados, utilize com os dados:

`Host: localhost`

`Port: 54321`

`Usuário: postgres`

`Senha: postgres`

`Banco: UpskillingGrupo4Final`

