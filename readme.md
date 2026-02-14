# PicPay Desafio técnico

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)


## Descrição

Sistema de operações financeiras desenvolvido em C# com .NET e ASP.NET, utilizando PostgreSQL como banco de dados. A aplicação permite realizar depósitos e transferências entre usuários, garantindo controle de saldo e validação das regras de negócio.

O projeto foi implementado com base no desafio técnico do PicPay [(picpay-desafio-backend)](https://github.com/PicPay/picpay-desafio-backend), com foco em organização arquitetural, separação de responsabilidades e boas práticas no desenvolvimento de APIs backend.

## Tecnologias

- C# .NET 10
- ASP.NET
- PostgreSQL
- Docker (opcional)

## Estrutura

```text
src
├───Application                  # Orquestra casos de uso (camada de aplicação)
│   ├───Dtos                     # Modelos de entrada/saída entre camadas
│   ├───Ports                    # Contratos (interfaces) ex: IUserRepository
│   └───Usecase                  # Implementação dos casos de uso (regras de fluxo)
│
├───Domain                       # Núcleo do negócio (regra pura e invariantes)
│   └───Entity                   # Entidades de domínio com comportamento
│
└───Infrastructure               # Implementações técnicas externas ao domínio
    ├───Adapters                 # Implementações das Ports (ex: EF, serviços externos)
    │
    ├───Http                     # Camada de exposição da aplicação
    │   ├───Controllers          # Endpoints que chamam os UseCases
    │   └───Middlewares          # Camada intermediaria entre requisições (ex: Tratamento de errors)
    │
    └───Persistence              # Infraestrutura de banco de dados
        ├───Repository           # Repositórios concretos (implementam Ports)
        └───Schemas              # Modelos de persistência (mapeamento EF Core)
```

## Instalação e execução

### Pre requesitos
- .NET SDK 10.0+
- PostgreSQL (Local ou Docker)
- Git

```bash
# Clone repository
git clone https://github.com/DAN-3200/dotnet-picpay-challenge.git
cd dotnet-picpay-challenge/main

# Configure a conexão com PostgreSQL no appsettings.json
"ConnectionStrings": {
  "URL": "Host=localhost;Port=<...>;Database=<...>;Username=<...>;Password=<...>;"
}

# Execute a aplicação
dotnet run --project .
```

## Documentação

Documentação disponível a partir do Scalar UI [http://localhost:5015/scalar/v1](http://localhost:5015/scalar/v1)

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](./LICENSE) para mais detalhes.
