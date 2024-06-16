# Classificador de Entidades Nomeadas em Bulas Farmacêuticas

## ☕ Funcionalidades Implementadas

## 🐱‍🏍Práticas Utilizadas:

## 💻 Pré-requisitos

## 🎬 Executando o Projeto

### 👉 Executando Migrações
* Para executar as migrações necessárias no postgres, altere o [appsettings](./src/Classificador.Api.Presentation/appsettings.json) inserindo a sua String de Conexão no local indicado por .
```json
    "PostgreSQL": '[REPLACE TO YOUR POSTGRES CONNECTION]'
```

* No terminal de comandos da sua IDE, navegue até o diretório 'Classificador.Api.Presentation>'.

```console
    cd .\src\Classificador.Api.Presentation
```

* Digite o comando:
```console
    dotnet ef database update
```

* Verifique se o banco foi criado corretamente


### 👉 Gerando Novas Migrações
Migrações podem ser geradas para alterar o esquema presente no banco de dados, para isso será necessário conhecimento do sistema de ORM Entity Framework, para aprender como alterar o ORM você pode verificar o artigo presente [aqui](https://learn.microsoft.com/pt-br/ef/core/modeling/relationships).

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

