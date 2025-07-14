# Sistema de Telenfermagem - Arquitetura SOA

## Sobre

Este projeto foi desenvolvido para aplicar o padrão arquitetural **SOA (Service Oriented Architecture)**.  
O principal objetivo é fornecer serviços para aplicações terceiras, implementados através de **Web Service** e **REST**.

## Objetivo da Aplicação

A aplicação tem como finalidade manipular e disponibilizar recursos para um sistema de telenfermagem.  
Dentro do sistema é possível:

- Realizar agendamentos de consultas médicas.
- Efetuar chamadas online em tempo real entre médicos e pacientes.

## Estrutura de Pastas

A estrutura do projeto está organizada da seguinte forma:

- **Controllers**  
  Responsável por receber as chamadas HTTP de todos os endpoints da aplicação.

- **Core/Entities**  
  Define os modelos de dados existentes na aplicação.

- **DTOS**  
  Modela como alguns tipos de serviços serão utilizados, determinando quais dados deverão ser passados e como as respostas serão estruturadas.

- **Enuns**  
  Contém definições globais de tipos de dados utilizados na aplicação.

- **Extensions**  
  Centraliza todos os tipos de serviços da aplicação, reduzindo a quantidade de declarações no `Program.cs`.

- **Infrastructure**  
  Declara as entidades e configurações relacionadas ao banco de dados.

- **Middleware**  
  Implementa tratativas globais de requisições.

- **Migrations**  
  Pasta gerada automaticamente pelo **Entity Framework** para aplicar as entidades no banco de dados.

- **Repositories**  
  Responsável por mapear as operações de manipulação de cada entidade no banco.

- **Services**  
  Contém as regras de negócio de cada fluxo da aplicação, além de outros serviços reutilizáveis, como validators e arquivos auxiliares.

- **Dockerfile**  
  Arquivo base para gerar uma imagem da aplicação e configurar o ambiente necessário para inicialização.

## Contatos

**Desenvolvedor:**  
Matheus Henrique Lourenço Bernardo

- **LinkedIn:**  
  [https://www.linkedin.com/in/matheus-bernardo-b20796196/]

- **Email:**  
  [mhlbernaroo0402@gmail.com]
