# Projeto Comrade (comrade-intl-net6)

## Run
Altere o arquivo appsettings.Development.json em Comrade.Api de acordo com o seu ambiente:	
- MsSqlServer (true | false): Ativar acesso ao banco MsSqlServer
- PostgresSql (true | false): Ativar acesso ao banco PostgresSql
- InjectInitialData (true | false): Inserir dados b�sicos no banco

## Migrations
### Criando base local
Com o appsettings.Development.json no projeto Comrade.Api devidamente configurado. Para subir sua base localmente voc� deve seguir os seguintes passos:
- No NuGet Package Manager selecionando o projeto Comrade.Persistence. E execute o comando:
  ```
  Update-Database
  ```
- Caso ocorra algum erro referente a tipos de colunas durante a opera��o anterior:
  - Apague todos os arquivos da pasta Migrations no projeto Comrade.Persistence
  - No NuGet Package Manager selecionando o projeto Comrade.Persistence. E execute o comando:
    ```
    Add-Migration "InitialMigration"
    Update-Database
    ```