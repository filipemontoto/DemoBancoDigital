# BancoDigital
## O Projeto:
Esse projeto foi desenvolvido em C# com .NET Core 7, para fazer uma API GraphQL onde também foi utilizado o [Hot Chocolate V13](https://chillicream.com/docs/hotchocolate/v13) como GraphQL Server.

## Para rodar a aplicação:
1) Devem ser executados os comandos docker-compose up, sendo que o DockerFile e docker-compose precisam estar no mesmo diretório, sendo ele o mesmo diretório do arquivo sln.
2) Após a aplicação subir, basta acessar o http://localhost:32775/graphql/
3) Criar um novo documento para interação com a API
![image](https://github.com/filipemontoto/DemoBancoDigital/assets/53829736/0b00a1ed-1282-40a2-9e03-f16c4797cae1)
4) Utilizar o espaço operation para escrever a ação que deseja realizar (Query ou Mutation)
![image](https://github.com/filipemontoto/DemoBancoDigital/assets/53829736/37cbf8a9-5058-4d1a-ae80-45269d0314f5)
5) Para testar, baseando-se nos casos de teste exemplo do challenge podem ser utilizadas as seguintes chamadas (variando ou não as entradas):

```
mutation{
    createConta(input: {conta: 123 saldo: 123}){
        conta{
            id
            conta
            saldo
        }
    }
}
```

```
mutation{
    deletarConta(input: {conta: 123 saldo: 123}){
        conta{
            id
            conta
            saldo
        }
    }
}
```

 ```
query{
  saldo(conta: 0){
    saldo
  }
}
 ```

 ```
mutation{
  sacarConta(input: {conta: 0 valor:10}  ){
    conta{
      conta
      saldo
    }
  }
}
 ```
 ```
mutation{
  depositarConta(input: {conta: 0 valor:10}  ){
    conta{
      conta
      saldo
    }
  }
}
 ```

## OBS: 
Baseando-se no modelo proposto, os tipos de dado dos atributos conta e valor foram definidos como Int 32, logo a API limita-se ao uso e tamanho desse tipo de dado.
