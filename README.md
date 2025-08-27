# api-blog
API de blog criada para desafio de programação

## Executando o projeto
**Para executar esse projeto é necessário ter o .NET8 e SQL Server instalados.**

* Após ter clonado o projeto, abra o arquivo Tables.sql e execute o script, isso irá criar as tabelas necessárias;
* Abra o projeto no seu VS e altere as informações do banco de dados na connectionString dentro do arquivo appsettings.json;
* Após isso você pode pressionar F5 dentro do VS e isso irá executar o projeto ou se preferir pode executar via linha de comando com os passos abaixo: 
    * Abra o terminal e vá até a pasta que está o projeto (NÃO é a pasta do .sln);
    * Execute o comando "dotnet run", ele irá compilar e executar o projeto;
    * Abra o seu navegador e acesse http://localhost:5136/swagger/index.html isso deve abrir a página do Swagger, você pode testar a API por ela.


## Considerações caso eu tivesse mais tempo
* Iria criar uma parte de usuários para que cada post e comentário fosse atribuido a um usuário;
* Endpoints para deletar, desativar ou ocultar um post. Já deixei tanto o Post e o comentário com uma coluna status para que fosse implementado esse controle. 
* Organizar melhor o código criando interfaces para que seja feito a injeção de dependências.
