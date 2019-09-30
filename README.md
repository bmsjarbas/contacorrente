
# backend-test

[![CircleCI](https://circleci.com/gh/randomjs/contacorrente/tree/master.svg?style=svg&circle-token=f27ef86fb055bd6820cf1472db421f070e7d3436)](https://circleci.com/gh/randomjs/contacorrente/tree/master)

## requisitos
1. dotnet core versão 3.0 - Teste foi apenas validado nesta versão
2. dotnet cli 

## comandos úteis
 Os comandos abaixo devem ser executados na raiz do projeto

#### executar testes
```console
dotnet test
```
#### para executar a aplicação 
```console
dotnet run movimentacao.log --project app/
```
ou execute o script run.sh, caso utilize um terminal como bash.

#### para buildar a aplicação
```console
dotnet build app/
```

#### para gerar um artefato executável
```console
dotnet publish app/
```

## como a aplicação foi construída

Esta aplicação foi construída baseado nos tópicos de engenharia de sotware abaixo:

1. Test driven Development
2. Domain Driven Design
    1. Linguagem ubíqua.
    2. Agregação raíz.
    3. Repositório

### sobre a aplicação

A aplicação é do tipo console e tem um menu auto explicativo conforme abaixo:

```console
1 - exibir o log de movimentações de forma ordenada
2 - informar o total de gastos por categoria
3 - informar qual categoria cliente gastou mais
4 - informar qual foi o mês que cliente mais gastou
5 - quanto de dinheiro o cliente gastou
6 - quanto de dinheiro o cliente recebeu
7 - saldo total de movimentações do cliente
0 - Sair
```
