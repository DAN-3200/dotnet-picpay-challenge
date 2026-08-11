# Estratégia de testes

Os testes seguem **AAA (Arrange, Act, Assert)**: cada cenário prepara as dependências e os dados, executa uma única ação de negócio e valida um resultado observável.

| Camada | Escopo | Isolamento |
| --- | --- | --- |
| Unitária | Regras dos use cases | Mocks de portas (`IUserRepo`, `IPaymentRepo`, `IHttpServices`) |
| Integração | Repositórios EF Core e migrations | PostgreSQL real em Testcontainers |

## Executar

O Docker Desktop precisa estar em execução. A fixture sobe `postgres:16-alpine`, aplica as migrations e remove o banco entre cenários para que os testes não dependam de dados locais nem do `docker-compose`.

```powershell
dotnet test PicPay.slnx
```

## Próximos cenários prioritários

1. Transferência recusada para pagador logista, saldo insuficiente, pagador/destinatário inexistentes e mesma conta.
2. Depósito válido e valores inválidos.
3. Reembolso duplicado e transação inexistente.
4. Testes HTTP dos controllers com `WebApplicationFactory`, substituindo apenas o serviço de autorização externo.
