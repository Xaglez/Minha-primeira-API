# Minha Primeira API

Este projeto é uma API RESTful desenvolvida com **ASP.NET Core**, conectada a um **banco de dados SQL Server**, com operações CRUD completas e testes via **Swagger**. Foi criada como prática para consolidar conhecimentos em desenvolvimento backend com C#.

## 🔧 Tecnologias Utilizadas

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- REST API

## 📌 Funcionalidades

- ✅ Cadastro de usuários
- ✅ Consulta de dados
- ✅ Atualização de registros
- ✅ Exclusão de dados
- ✅ Documentação interativa com Swagger

## 🚀 Como executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/Xaglez/Minha-primeira-API.git
````

2. Clone o repositório:

````bash
cd Minha-primeira-API
````
3. Configure a connection string em appsettings.json com os dados do seu SGBD:
   
````json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=NOME_DO_BANCO;Trusted_Connection=True;TrustServerCertificate=True;"
}
````
4. Execute as migrações (se estiver usando EF Core):
````bash
dotnet ef database update
````
5. Rode o projeto:
````bash
dotnet run
````
6. Acesse o Swagger no navegador:
````bash
https://localhost:PORT/swagger
````

🛠 Em desenvolvimento
Este projeto está em constante aprimoramento. Próximas implementações planejadas:

🔒 Autenticação com JWT

🧪 Validações e tratamento de erros personalizados

🧾 Logs com Serilog

📁 Organização por camadas (Services, DTOs, etc.)

🤝 Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests.

Desenvolvido por Paulo Vinícius Rosa Gonçalves
Estudante de Engenharia de Software | Foco em desenvolvimento backend com C# e ASP.NET
