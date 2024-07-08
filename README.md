# Projeto Agenda Telefônica

Este projeto é uma aplicação WEB desenvolvida com .NET 8.0 C#, usando o Visual Studio, para gerenciar uma agenda telefônica.

## Funcionalidades

- **Cadastro de Contatos**: Inclusão de nome, idade e telefones associados.
- **Pesquisa de Contatos**: Busca por nome e número de telefone.
- **Edição de Contatos**: Permite alterar os dados do contato.
- **Exclusão de Contatos**: Remove contatos e registra a exclusão em um arquivo de log.
- **Estrutura de Dados**: Utilização correta das tabelas `Contato` e `Telefone` com chaves estrangeiras adequadas.

## Tecnologias Utilizadas

- .NET 8.0
- C#
- Entity Framework Core
- Razor Pages
- PostgreSQL

## Estrutura do Banco de Dados

### Tabela: Contato
- ID (NUMBER(14)) - PK
- NOME (VARCHAR(100))
- IDADE (NUMER(3))

### Tabela: Telefone
- IDCONTATO (NUMBER(14)) - PK - FK
- ID (NUMBER(14)) - PK
- NUMERO (VARCHAR(16))
