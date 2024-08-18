# Classificador de Entidades Nomeadas em Bulas Farmacêuticas

## ☕ Funcionalidades Implementadas

## 🐱‍🏍Práticas Utilizadas:

## 💻 Pré-requisitos
Antes de iniciar a configuração e execução da aplicação MedTagger, certifique-se de que os seguintes pré-requisitos estão atendidos:

* [.NET 8.0.303 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* [PostgreSQL](https://www.postgresql.org/download/)

## 🎬 Executando o Projeto
Para executar o projeto você deve se certificar que ter todos os pré requisitos instalados e devidamente configurados. Para que o medtagger seja executado sem problemas, precisamos fazer configurações adicionais no arquivo de configuração padrão de aplicações [asp.net mvc](https://dotnet.microsoft.com/pt-br/apps/aspnet/mvc), o [appsettings](./src/Classificador.Api.Presentation/appsettings.json).

### Configurações obrigatórias e opcionais
Para começar precisamos definir a String de Conexão padrão com o postgres, você pode encontrar exemplos de *connectionStrings* [aqui](https://www.connectionstrings.com/npgsql/). Altere o campo presente no appsettings.

O nome do banco de dados é definido direto na String de Conexão, isso só é possível por que este projeto utiliza a estratégia de Migrações, mais a frente veremos mais sobre elas.
Certifique-se que o [appsettings](./src/Classificador.Api.Presentation/appsettings.json) esteja correto. 

```json
    "ConnectionStrings": {
      "PostgreSQL": "Server=localhost;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;"
  },
```

Após fazer a configuração da String de Conexão com o banco de dados precisamos fazer as configurações do serviço de email, sem ela as opções de contato não iram funcionar.

```json
  "EmailOptions": {
    "EmailPassword": "[REPLACE TO YOUR PASSWORD]"
  }
```

E essa é toda a configuração obrigatoria no AppSettings. Logo após nós vamos instalar os pacotes necessários para a aplicação. Em um terminal certifique-se de estar dentro do diretório [src](./src/) do projeto:

```console
cd .\src
```

Logo após entre com os seguintes comandos:

```console
dotnet clean
```

```console
dotnet restore
```
Este projeto por padrão utiliza da técnica de migrações presentes no pacote Entity Framework core, que faz parte do .Net, então o banco de dados será automaticamente criado no seu postgres. Note que o nome do banco sempre será o que foi colocado na String de Conexão.
E isso é tudo que você precisa para iniciar o projeto, se tudo ocorrer bem a aplicação poderá ser iniciada pressionando a tecla **F5** para iniciar de forma padrão ou **Ctrl + F5** para inicar em modo debugger.

## 👉 Migrações
Este projeto utiliza migrações, uma migração, no contexto de desenvolvimento de software, é o processo de alteração da estrutura do banco de dados de forma controlada e rastreável. Utilizando do Entity Framework, as migrações são usadas para manter o esquema do banco de dados sincronizado com o modelo de dados da aplicação. As migrações permitem que as mudanças no modelo de dados sejam propagadas para o banco de dados de forma incremental. Para saber mais, [clique aqui](https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

Este projeto usa migrações autômaticas, isto é, ao executar o projeto com a conexão correta, o banco de dados sera criado autômaticamente na versão atual, para **desativar** está função, mude a opção "IsMigrationActive" para "false" dentro do [appsettings](./src/Classificador.Api.Presentation/appsettings.json).

Para desabilitar as migrações dentro do sistema altere:

```json
    "DatabaseSeedOptions": {
        "IsMigrationActive": true
    }
```

para:

```json
    "DatabaseSeedOptions": {
        "IsMigrationActive": false
    }
```

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

### 🌱 Data Seed
Data seeding é o processo de pré-carregar dados iniciais no banco de dados. Esses dados podem ser úteis para testar a aplicação ou fornecer um conjunto inicial de dados para o usuário final, para saber mais, [clique aqui](https://learn.microsoft.com/pt-br/ef/core/modeling/data-seeding). Você pode adicionar um novo *seeder* nas sessão "DatabaseSeedOptions" do [appsettings](./src/Classificador.Api.Presentation/appsettings.json) da aplicação. Lembre-se que. para cada seeder existe uma opção 'booleana' que controla se ele será executado ou não, por padrão essas opções vem desligadas **exceto** para as tabelas 'categorias' e 'especialidades'.


#### 👨‍🦰 Data Seed de usuários
Caso deseje que o projeto seja iniciado com usuários já cadastrados(para criar o banco já com um registro de Admin do sistema, por exemplo), você pode adicionar um novo valor ao campo json correspondede a 'Users' dessa aplicação. É altamente recomendado que você inicie este projeto com um usuário cadastrado como admin, uma vez que sem um admin, funções do sistema estarão indisponíveis. Para iniciar o sistema com usuários previamente cadastrados, altere:

```json
"DatabaseSeedOptions": {
    "IsUserSeedingActive": false,
    "Users": "[REPLACE TO YOUR USERS LIST]"
}
```

para:

```json
"DatabaseSeedOptions": {
    "IsUserSeedingActive": true,
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

#### 🔖 Data Seed de categorias
Semelhante aos usuários também disponibilizei um seeder para as categorias, por padrão esse seeder vem ligado e com seis categorias pré-definidas, são elas: Pessoa, Medicamento, Princípio Ativo, Doença, Sintoma e Outros. Uma vez que o sistema estja funcionando somente um Admin terá permissão para criar ou alterar essas categorias, portanto cuidado ao manusear esse seeder. Para manipular esse seeder por favor altere:

```json
"DatabaseSeedOptions": {
    "IsCategorySeedingActive": false,
    "Categories": "[REPLACE TO YOUT CATEGORIES LIST]"
},
```

para:

```json
"DatabaseSeedOptions": {
    "IsCategorySeedingActive": true,
    "Categories": [
        {
            "Name": "Categoria",
            "Description": "Descreva aqui a sua categoria. Lembre-se que este campo é opcional."
        }
    ]
}
```
 
#### 🦄 Data Seed de especialidade
Este data seeder inicializa a tabela especialidades com valores definidos,
assim como o seeder de categorias após a criação do banco o único usuário que podera adicionar valores a essa tabela é o admin. Este dataseeder por padrão contem os valores: Enfermeiro, Estudante, Médico e Outros. Para configurar esse seeder por favor altere:

```json
"DatabaseSeedOptions": {
    "IsSpecialtySeedingActive": false,
    "Specialties": "[REPLACE TO YOUR SPECIALTIES LIST]"
}
```

para:

```json
"DatabaseSeedOptions": {
    "IsSpecialtySeedingActive": true,
    "Specialties": "Specialties": [
        {
        "Name": "Estudante",
        "Description": "Registro inserido automaticamente pelo 'seeding' de especialidades."
        },
        {
        "Name": "Enfermeiro",
        "Description": "Registro inserido automaticamente pelo 'seeding' de especialidades."
        },
        {
        "Name": "Médico",
        "Description": "Registro inserido automaticamente pelo 'seeding' de especialidades."
        },
        {
        "Name": "Outros",
        "Description": "Registro inserido automaticamente pelo 'seeding' de especialidades."
        }
    ]
}
```

## 📩 Emails

Para que a aplicação MedTagger possa enviar e receber e-mails, é necessário configurar as opções de e-mail no arquivo [appsettings](./src/Classificador.Api.Presentation/appsettings.json). Essas configurações permitem que a aplicação utilize um servidor SMTP para enviar e-mails e também receber contatos de usuários. Está é a sessão que deve ser alterada:

```json
    "EmailOptions": {
    "SmtpServer": "smtp.gmail.com",
    "Port": "587",
    "EmailAddress": "medtagger.contato@gmail.com",
    "EmailPassword": "[REPLACE TO YOUR PASSWORD]"
  }
```

Note que é necessário inserir uma senha. Como esse projeto utiliza o servidor de emails da google é necessario que essa seja uma senha de aplicativo que é fornecida pelo próprio google, para saber mais [click aqui](https://support.google.com/accounts/answer/185833?hl=pt-BR).

