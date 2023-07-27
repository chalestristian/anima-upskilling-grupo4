|![foto-diego](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/b33cd5e8-192f-4d62-9590-7cf34ed7fb88)|![foto-matheus](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/55c07fdd-8f13-42c2-a589-3c98ceafce5b)|![foto-tales](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/235053be-7cd8-4ea7-b095-8d850f0358a8)|![foto-william](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/f3304da1-7b60-41eb-a8cd-fc12e3a53fb8)|
|----------------|--------------|----------------|--------------|
| DIEGO PEREIRA CAMPOS | DIEGOMATHEUS CHAVES FERREIRA | THALES CRISTIAN EUGENIO | WILLIAM CLEISSON DE CARVALHO |

# Objetivo do Projeto

Este projeto é um trabalho realizado em grupo para conclusão de treinamento Upskiliing Gama Academy + Ânima. O objetivo era desenvolver várias aplicações usando variadas versões de frameworks do Dotnet, simulando aplicações modernas e legadas, comunicando entre si. A arquitetura abaixo é uma arquitetura proposta pelo grupo para conseguir aplicar todos os conceitos e ferramentas aprendidas durante o treinamento.

# Arquitetura da comunicação entre os serviços
![arquitetura_aplicacao](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/5b548ce1-8ddb-40f7-8379-d91cf3713a12)

# Como gerenciamos a entrega do projeto (JIRA)
![jira](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/327e7d7e-9820-47db-990d-1dfb11d7cea9)

# Como executar o projeto

O projeto está todo configurado em containers para subir as aplicações de uma vez no ambiente de desenvolvimento.

O banco de Dados Postgres contém as informações necessárias para todas as aplicações.

Para subir o container atualizado já realizando o build basta executar:
`docker-compose up -d --build`

A API já possui as migrations para criar a estrutura inicial do banco de dados e dados iniciais nas tabelas.

Para rodar as migrations acesse o container da api e execute:
`dotnet ef database update`

Para logar na API é necessário um token que é gerado pelo Identity Server na porta 5200. Segue abaixo um exemplo de requisição de token com CURL:

``
curl --location 'http://localhost:5200/connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=client_credentials' \
--data-urlencode 'client_id=<CLIENT_ID>' \
--data-urlencode 'client_secret=<CLIENT_SECRET>' \
--data-urlencode 'scope=<SCOPO>'
``

#API
A API poderá ser acessada como:
http://localhost:5100

![foto-api](https://github.com/williamcc89/anima-upskilling-grupo4/assets/2452619/ad1b5d8b-701d-4945-897b-31bdfac8f767)

# Banco de Dados

O banco de dados pode ser acessado utilizando estes dados:

`Host: localhost`

`Port: 54321`

`Usuário: postgres`

`Senha: postgres`

`Banco: UpskillingGrupo4Final`
