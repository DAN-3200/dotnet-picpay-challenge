# PicPay Challenge - API financeira simplificada

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

## Descrição

Este projeto é uma API backend em C# com ASP.NET Core para um sistema financeiro simplificado, inspirando-se no desafio técnico do PicPay. A aplicação oferece operações como cadastro de usuários e transferência entre contas, com validações de negócio e persistência em PostgreSQL.

## Tecnologias

- C# / .NET 10
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker Compose
- OpenTelemetry
- Scalar (documentação interativa)

## Estrutura do projeto

```text
main/
├── appsettings.json.example
├── Src/
│   ├── Application/
│   │   ├── Dtos/
│   │   ├── Ports/
│   │   └── Usecase/
│   ├── Domain/
│   │   └── Entity/
│   └── Infrastructure/
│       ├── Adapters/
│       ├── Http/
│       ├── Persistence/
│       └── Telemetry/
├── Migrations/
└── Properties/
```

## Pré-requisitos

- .NET SDK 10.0+
- Docker Desktop
- Git

## Executando o projeto

### 1) Subir o banco de dados com Docker

```bash
docker compose up -d database
```

### 2) Criar o arquivo de configuração local

Copie o exemplo para um arquivo real de configuração:

```powershell
Copy-Item .\main\appsettings.example.json .\main\appsettings.json
```

O arquivo de exemplo já vem com uma configuração compatível com o Docker Compose local:

```json
{
  "ConnectionStrings": {
    "URL": "Host=localhost;Port=5600;Database=picpay;Username=admin;Password=4321"
  },
  "OpenTelemetry": {
    "ServiceName": "PicPay.Api",
    "Endpoint": "http://localhost:4317"
  }
}
```

### 3) Restaurar dependências e executar

```bash
cd main
dotnet restore
dotnet run
```

A API ficará disponível em:

- HTTP: http://localhost:5082
- Documentação interativa: http://localhost:5082/scalar/v1

## Observações

- O projeto usa migrations do Entity Framework Core.
- O OpenTelemetry está configurado para exportar para o endpoint local padrão em http://localhost:4317.
- O arquivo [main/appsettings.json.example](main/appsettings.json.example) deve ser usado como base para configurações locais e não deve conter segredos reais em ambientes compartilhados.

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

