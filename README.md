# CycleLab - Backend

CycleLab é uma aplicação ASP.NET/Angular que fornece um sistema laboratorial interno para gerenciar usuários, amostras e exames. Foi desenvolvida com base no padrão DDD (Domain-Driven Design) e visa fornecer uma arquitetura modular, segura e escalável. O frontend da aplicação foi desenvolvido em Angular e pode ser encontrado no repositório [CycleLab-Frontend-Angular](https://github.com/Vitto-Mazeto/CycleLab-Frontend-Angular).

## Funcionalidades Principais

- Autenticação de usuários utilizando o Identity.
- Sistema de permissões usando Roles
- Gerenciamento de usuários: criação, edição e exclusão de contas de usuário.
- Gerenciamento de amostras: criação, edição e exclusão de amostras.
- Gerenciamento de exames: criação, edição e exclusão de exames.

## Estrutura do Projeto

A estrutura do projeto CycleLab segue o padrão DDD e é organizada da seguinte forma:

- `Authentication`: Contém a implementação da autenticação utilizando o Identity.
- `DTOs`: Contém os objetos de transferência de dados utilizados pela aplicação.
- `Database`: Responsável pela configuração do banco de dados e migrações.
- `Domain`: Contém as entidades de domínio.
- `ExercicioWebAPI`: Projeto principal da API do ASP.NET, responsável por fornecer endpoints para as operações da aplicação.
- `Repository`: Interfaces e implementações concretas dos repositórios.
- `Services`: Interfaces e implementações concretas dos serviços utilizados pela aplicação.

## Instalação e Execução

Para executar a aplicação CycleLab localmente, siga as etapas abaixo:

1. Certifique-se de ter o [.NET Core SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) instalado em sua máquina.
2. Clone o repositório do CycleLab:

```shell
git clone https://github.com/Vitto-Mazeto/CycleLab.git
```

3. Acesse a pasta do projeto:
```shell
cd CycleLab/ExercicioWebAPI
```

4. Execute o seguinte comando para restaurar as dependências do projeto:
```shell
dotnet restore
```


5. Configure a string de conexão com o banco de dados em `appsettings.json`.
6. Execute as migrações do Entity Framework para criar o banco de dados:
```shell
dotnet ef database update
```


7. Inicie a aplicação:
```shell
dotnet run
```


A API estará disponível em `https://localhost:7237`.

## Contribuição

Contribuições são bem-vindas! Se você deseja contribuir para o projeto, siga as etapas abaixo:

1. Faça um fork do repositório.

2. Crie uma nova branch para a sua contribuição:
```shell
git checkout -b minha-contribuicao
```

3. Faça o commit das suas alterações:
```shell
git commit -m "Minha contribuição"
```


4. Faça o push para o seu fork:
```shell
git push origin minha-contribuicao
```


5. Abra um Pull Request para a branch principal deste repositório.

## Suporte

Se você tiver alguma dúvida ou encontrar algum problema, sinta-se à vontade para abrir uma [issue](https://github.com/seu-usuario/CycleLab/issues) no repositório. 

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).