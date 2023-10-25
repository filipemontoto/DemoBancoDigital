using BancoDigital.Classes;
using BancoDigital.Interfaces;
using BancoDigital.Repository.MongoDBModels;
using MongoDB.Driver;

namespace BancoDigital.Services
{
    public class ContaBancoDigitalService : IContaBancoDigital
    {
        private MongoDbService _MongoDbContaBancoDigitalService;

        public ContaBancoDigitalService(MongoDbService contaBancoDigitalService)
        {
            _MongoDbContaBancoDigitalService = contaBancoDigitalService;
        }

        public SaldoPayload GetContaBancoDigital(
            int conta)
        {

            var contaMongoDb = _MongoDbContaBancoDigitalService.GetMongoDbContaBancoDigital(conta);

            // Caso não exista, devolve erro
            if (contaMongoDb == null)
            {
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Consulta inválida - Conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            // Caso já exista uma conta com esse código, vamos devolver a conta já criada
            // e ignorar o insert. Não queremos contas com números duplicados
            return new SaldoPayload(contaMongoDb.Saldo);
        }

        public ContaBancoDigitalPayload CreateContaBancoDigital(
            CreateContaInput input)
        {

            var contaMongoDb = _MongoDbContaBancoDigitalService.GetMongoDbContaBancoDigital(input.Conta);

            // Caso não exista, cria e devolve os dados da nova conta
            if (contaMongoDb == null)
            {
                return new ContaBancoDigitalPayload(_MongoDbContaBancoDigitalService.InsertMongoDbContaBancoDigital(input));
            }

            // Caso já exista uma conta com esse código, vamos devolver a conta já criada
            // e ignorar o insert. Não queremos contas com números duplicados
            return new ContaBancoDigitalPayload(contaMongoDb);
        }

        public ContaBancoDigitalPayload DeletarContaBancoDigital(
            CreateContaInput input)
        {
            var contaMongoDb = _MongoDbContaBancoDigitalService.GetMongoDbContaBancoDigital(input.Conta);

            // Caso não exista, devolve erro
            if (contaMongoDb == null)
            {
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação de deleção de uma conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            var conta = new MongoDbContaBancoDigital()
            {
                Id = contaMongoDb.Id,
                Conta = contaMongoDb.Conta,
                Saldo = contaMongoDb.Saldo,
            };

            _MongoDbContaBancoDigitalService.DeleteMongoDbContaBancoDigital(contaMongoDb.Id);

            return new ContaBancoDigitalPayload(conta);
        }

        public ContaBancoDigitalPayload DepositarContaBancoDigital(ValorContaInput input)
        {
            var contaMongoDb = _MongoDbContaBancoDigitalService.GetMongoDbContaBancoDigital(input.Conta);

            var conta = new MongoDbContaBancoDigital()
            {
                Id = contaMongoDb.Id,
                Conta = contaMongoDb.Conta,
                Saldo = contaMongoDb.Saldo,
            };

            conta.Saldo += input.Valor;

            if (conta.Saldo < 0)
            {
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Saldo insuficiente para esse valor de saque.")
                        .SetCode("-1")
                        .Build());
            }

            _MongoDbContaBancoDigitalService.UpdateMongoDbContaBancoDigital(conta);

            return new ContaBancoDigitalPayload(conta);
        }

        public ContaBancoDigitalPayload SacarContaBancoDigital(ValorContaInput input)
        {
            var contaMongoDb = _MongoDbContaBancoDigitalService.GetMongoDbContaBancoDigital(input.Conta);

            var conta = new MongoDbContaBancoDigital()
            {
                Id = contaMongoDb.Id,
                Conta = contaMongoDb.Conta,
                Saldo = contaMongoDb.Saldo,
            };

            conta.Saldo -= input.Valor;

            if (conta.Saldo < 0)
            {
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Saldo insuficiente para esse valor de saque.")
                        .SetCode("-1")
                        .Build());
            }

            _MongoDbContaBancoDigitalService.UpdateMongoDbContaBancoDigital(conta);

            return new ContaBancoDigitalPayload(conta);
        }
    }
}
