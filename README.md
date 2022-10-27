# Processo Seletivo CRM Educacional

Projeto elaborado visando construir uma aplicativo para o cadastro de inscrições de candidados à cursos.

## Requisitos

 - Cadastrar lead (candidato) com validação de CPF
 - Cadastrar novos cursos
 - Cadastrar uma nova inscrição, a inscrição é comporta por um candidato e um curso, um candidato pode ter mais de uma inscrição.

## Tecnologias utilizadas

 - C#
 - .NET
 - ASP.NET Core
 - Entity Framework
 - MS SQL Server
 - xUnit
 - FluentAssertions
 - Docker
 
## Orientações

Antes de iniciar o projeto, é necessário ter o [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) instalado em sua máquina.

Além disso, é necessário ter o [MS SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads).

Tenha instalado algum software para fazer requisições HTTP ao aplicativo criado. São alguns exemplos: Thunder Client (extensão do VS Code), [Insomnia](https://insomnia.rest/download) e [Postman](https://www.postman.com/).

Por fim, é necessário ter o git instalado e inicializado no diretório para clonar o repositório e rodar o projeto localmente.
 
## Como inicializar o projeto

Clone o projeto
```bash
git clone git@github.com:MaxRudim/Teste-Tecnico-CRMEducacional.git
```
Entre na pasta que você acabou de criar com o comando:
```bash
cd teste-tecnico
```
Restaure as dependências utilizando o comando:
```bash
dotnet restore
```
Inicialize o banco de dados atraves do comando
```docker
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SenhaSegura123." \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest

```
Entre na pasta do aplicativo:
```bash
cd CourseApplications
```
Suba o banco
```bash
dotnet ef database update
```
Inicialize a aplicação
```bash
dotnet run
```

## Rotas

O projeto possui as seguintes rotas: User `https://localhost:7125/candidate` e Post `https://localhost:7125/course` e `https://localhost:7125/subscription`. Note que o enderço local deve ser o mesmo quando se utiliza o comando dotnet run.

Antes de começar a navegar por elas, primeiramente será necessário criar um token. Para isso, faça uma requisição do tipo POST para a rota CANDIDATE passando o seguinte body em JSON:
```JSON
{
  "Email": "seuemailvalido@seuemail.com",
	"Password": "suasenhacommaisde6digitos",
	"Cpf": "seucpfvalido"
}
```

A partir disto, seu usuário foi criado no banco de dados e você poderá logar e pegar seu token, bastando fazer uma requisição do tipo POST para `https://localhost:7125/candidate/authentication` contendo o seguinto body em JSON:

```JSON
{
	"Cpf": "seucpfvalido",
	"Password": "suasenhacommaisde6digitos",
}
```

O retorno desta rota será um token que poderá ser utilizado para efetuar todas as requisições no aplicativo.

Caso queira encontrar todas as rotas e todas as tabelas utilizadas no aplicativo, entre no navegador, como o Google Chrome, por exemplo, e digite: `https://localhost:7125/swagger`.

## Veriicando os views

Para acessar os views do aplicativo, basta seguir a rota: `https://localhost:7125/candidate/view`. Caso tenha cadastrado algum candidato, o e-mail e o cpf dele aparecerá lá.

## Testando a aplicação

Este projeto foi realizado utilizando testes unitários. Para rodar os testes, basta entrar na pasta `CourseApplications.Test` e, sem estar com o aplicativo rodando (dê um ctrl + c no terminal caso o dotnet run esteja em execução), utilize o comando: `dotnet test`.

## Agradeço a oportunidade ;D