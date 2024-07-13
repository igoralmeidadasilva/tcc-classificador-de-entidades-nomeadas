# Classificador de Entidades Nomeadas em Bulas Farmacêuticas

## ☕ Funcionalidades Implementadas

## 🐱‍🏍Práticas Utilizadas:

## 💻 Pré-requisitos

## 🎬 Executando o Projeto

### 🌱 Data Seed de usuários
Data seeding é o processo de pré-carregar dados iniciais no banco de dados. Esses dados podem ser úteis para testar a aplicação ou fornecer um conjunto inicial de dados para o usuário final, para saber mais, [clique aqui](https://learn.microsoft.com/pt-br/ef/core/modeling/data-seeding).

Caso deseje que o projeto seja iniciado com usuários já cadastrados(para criar o banco já com um registro de Admin do sistema por exemplo), você pode adicionar um novo *seeder* nas sessão "DatabaseSeedOptions" do [appsettings](./src/Classificador.Api.Presentation/appsettings.json) da aplicação, por exemplo, altere:

```json
    "DatabaseSeedOptions": {
        "IsSeedingActive": true,
        "IsMigrationActive": true,
        "Users": "[REPLACE TO YOUR USERS LIST]"
    }
```

para:
```json
    "DatabaseSeedOptions": {
        "IsSeedingActive": true,
        "IsMigrationActive": true,
        "Users": [
            {
                "Email": "john.email@email.com",
                "HashedPassword": "@Admin123",
                "Name": "John Doe",
                "Role": "Admin",
                "Contact": "(11)99988-7766"
            },
            {
                "Email": "jane.email@email.com",
                "HashedPassword": "@Padrao123",
                "Name": "Jane Doe",
                "Role": "Padrao",
                "Contact": "(11)99955-4433"
            }
        ]
    }
```

* Cuidado com o campo Role, ele é um Enumerate e só permite valores pré definidos ou seus respectivos identificadores: "Padrao" ou "0" e "Admin" ou "2", você pode verificar esse Enumerate [clicando aqui](./src/Classificador.Api.Domain/Enums/UserRole.cs) .
* Note que o atributo "Users" é uma lista de usuários, por tanto não esqueça dos colchetes 😉. 

### 👉 Migrações
Este projeto utiliza migrações, uma migração, no contexto de desenvolvimento de software, é o processo de alteração da estrutura do banco de dados de forma controlada e rastreável. Utilizando do Entity Framework, as migrações são usadas para manter o esquema do banco de dados sincronizado com o modelo de dados da aplicação. As migrações permitem que as mudanças no modelo de dados sejam propagadas para o banco de dados de forma incremental. Para saber mais, [clique aqui](https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

Este projeto usa migrações autômaticas, isto é, ao executar o projeto com a conexão correta, o banco de dados sera criado autômaticamente na versão atual, para **desativar** está função, mude a opção "IsMigrationActive" para "false" dentro do [appsettings](./src/Classificador.Api.Presentation/appsettings.json).


### 🧳 Executando Migrações por linhas de comando
Executar migrações por linha de comando é uma forma eficaz de controlar melhor as alterações no banco de dados. A seguir, apresento um passo a passo para executar migrações no Entity Framework:

* Certifique-se que o [appsettings](./src/Classificador.Api.Presentation/appsettings.json) esteja correto. Para ver exemplos de *connectionStrings* do postgres, [clique aqui](https://www.connectionstrings.com/npgsql/).
```json
    "PostgreSQL": '[REPLACE TO YOUR POSTGRES CONNECTION]'
```

* Certifique-se que a ferramenta [Entity Framework CLI](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet) está instalada.

```console
    dotnet tool install --global dotnet-ef
```

* No terminal de comandos da sua IDE, navegue até o diretório 'Classificador.Api.Presentation>'.

```console
    cd .\src\Classificador.Api.Presentation
```

* Digite o comando.
```console
    dotnet ef database update
```

* Verifique se o banco foi criado corretamente.


### 🔌 Gerando Novas Migrações
Migrações podem ser geradas para alterar o esquema presente no banco de dados, para isso será necessário conhecimento do sistema de ORM Entity Framework, para aprender como alterar o ORM, recomendo a você ler este [artigo](https://learn.microsoft.com/pt-br/ef/core/modeling/relationships).

* Se desejar alterar o esquema do banco de dados, modifique o conteúdo dos [arquivos de ORM](./src/Classificador.Api.Infrastructure/Context/Configurations/) presentes em '\src\Classificador.Api.Infrastructure\Context\Configurations'.

* No terminal, certifique-se de estar dentro de '\src\Classificador.Api.Presentation'
```console
    cd .\src\Classificador.Api.Presentation
```

* Gere uma nova migração com o comando (substitua NovaMigracao pelo nome especifico da sua migração):
```console
    dotnet ef migrations add NovaMigracao --project ..\Classificador.Api.Infrastructure\Classificador.Api.Infrastructure.csproj
```

* Digite o comando 'dotnet ef database update' para aplicar as alterações.
```console
    dotnet ef database update
```

