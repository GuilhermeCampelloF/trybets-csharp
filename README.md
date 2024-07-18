## TRYBETS

O projeto TryBets foi desenvolvido durante o curso de formação full-stack pela Trybe, na eletiva C#.

A aplicação consiste no back-end de um site de apostas previamente implementado, no qual foram realizadas as divisões de certas funcionalidades em diferentes microsserviços, a depender de suas especificidades.

- A entidade `Users` é responsável por armazenar os dados das pessoas usuárias;
- A entidade `Teams` armazena os possíveis times que participarão de partidas;
- A entidade `Matches` armazena cada uma das partidas com informações de data e horário, time A e time B (times que disputarão a partida), valor apostado em cada um dos times, se a partida foi finalizada e qual o time vencedor;
- A entidade `Bets` por sua vez armazena informações das apostas realizadas com dados da pessoa usuária, a partida, o time apostado e o valor apostado;

Esta `API` tem responsabilidade apenas de realizar o fluxo inicial de cadastrar novas pessoas usuárias, permitir que as mesmas se autentiquem, fornecer informações de times e partidas, realizar as apostas e atualizar as `odds` que são dinâmicas baseadas no valor apostado em cada time. Este site de apostas possui as `odds` (razão de ganho em uma aposta) atualizadas dinamicamente e não possui fins lucrativos, ou seja, o valor das apostas são inteiramente devolvidos às pessoas usuárias que apostaram.

Segue abaixo o diagrama entidade-relacionamento:
  
![trybets-der](https://github.com/user-attachments/assets/89a9702f-6f01-42c7-905b-6355c27ba478)

O banco de dados SQL Server dos microsserviços é o mesmo da aplicação monolítica, logo nenhuma alteração foi realizada nas models.

## REQUISITOS:
- Docker/Docker Compose
- .NET SDK

É possível testar todos os arquivos `Dockerfile` de uma vez executando o seguinte comando na raiz do projeto:

```shell
docker compose -f "docker-compose.microservices.yml" up -d --build
```

Para conectar-se ao sistema de gerenciamento de banco de dados, utilize as seguintes credenciais:

- `Server`: localhost
- `User`: sa
- `Password`: TryBets123456!
- `Trust server certificate`: true

⚠️ **Será necessário criar as migrations do seu banco de dados na aplicação monolítica e rodar a aplicação ao menos uma vez para que o seeder alimente a tabela Teams e Matches**

## 📖 HABILIDADES TRABALHADAS 📖

- Entendimento de uma arquitetura de microsserviços;
- Interpretação de um código fonte já implementado;
- Separação de responsabilidades de uma aplicação monolítica;
- Criação de imagens docker de aplicações web;
- Autenticação JWT;

## IMPLEMENTAÇÕES REALIZADAS

<details>
<summary><strong>Microsserviço TryBets.Users</strong></summary>

- `TryBets.Users`: responsável pelo cadastro e login de pessoas usuárias
    - `Fonte:` /src/TryBets.Users
    - `Porta`: 5501
    - `Rotas`:
        - POST /user/signup
        - POST /user/login
</details>

<details>
<summary><strong>Microsserviço TryBets.Matches</strong></summary>

- `TryBets.Matches`: responsável pela visualização de times e partidas
    - `Fonte:` /src/TryBets.Matches
    - `Porta`: 5502
    - `Rotas`:
        - GET /team
        - GET /match/{finished}
</details>

<details>
<summary><strong>Microsserviço TryBets.Bets</strong></summary>

- `TryBets.Bets`: responsável pelo cadastro e visualização de apostas
    - `Fonte:` /src/TryBets.Bets
    - `Porta`: 5503
    - `Rotas`:
        - POST /bet
        - GET /bet/{BetId}
</details>

<details>
<summary><strong>Microsserviço TryBets.Odds</strong></summary>

- `TryBets.Odds`: responsável pela atualização das odds de cada partida. Este microsserviço é novo e não é acessível ao site. Ele será utilizado pelo microsserviço TryBets.Bets e será chamado por este toda vez que uma nova aposta for cadastrada.
    - `Fonte:` /src/TryBets.Odds
    - `Porta`: 5504
    - `Rotas`:
        - PATCH /odd/{matchId}/{TeamId}/{BetValue}
</details>

## ©️ DISCLAIMER

<div align="justify">
Com exceção das alterações destacadas acima no tópico "implementações realizadas", <b>TODOS OS DEMAIS ARQUIVOS</b> foram desenvolvidos e estão sob responsabilidade da TRYBE, incluindo, mas não se limitando ao: código inicial pré-implementado, models, services, database, diagrama entidade-relacionamento do sistema e demais diretórios necessários para o funcionamento da aplicação.
</div>
