﻿using BancoDigital.Interfaces;
using BancoDigital.Repository;
using BancoDigital.Repository.DbModels;

namespace BancoDigital.Services
{
    public class ContaBancoDigitalService : IContaBancoDigital
    {
        private ContaBancoDigitalRepository _ContaBancoDigitalRepository;

        public ContaBancoDigitalService(ContaBancoDigitalRepository contaBancoDigitalService)
        {
            _ContaBancoDigitalRepository = contaBancoDigitalService;
        }

        public SaldoPayload GetContaBancoDigital(
            int conta)
        {
            // Busca se conta existe em banco de dados, a partir de input da API
            var contaDb = _ContaBancoDigitalRepository.GetContaBancoDigital(conta);

            // Caso não exista, devolve erro
            if (contaDb == null)
            {
                // Está sendo utilizado uma forma nativa de devolver erros para o usuário
                // Apenas é necessário escolher um Código e uma Mensagem de retorno
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação inválida - Conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            // Caso já exista uma conta com esse código, vamos devolver a conta já criada
            // e ignorar o insert. Não queremos contas com números duplicados
            return new SaldoPayload(contaDb.Saldo);
        }

        public ContaBancoDigitalPayload CreateContaBancoDigital(
            CreateContaInput input)
        {
            // Busca se conta existe em banco de dados, a partir de input da API
            var contaDb = _ContaBancoDigitalRepository.GetContaBancoDigital(input.Conta);

            // Caso não exista, cria e devolve os dados da nova conta
            if (contaDb == null)
            {
                return new ContaBancoDigitalPayload(_ContaBancoDigitalRepository.InsertContaBancoDigital(input));
            }

            // Caso já exista uma conta com esse código, vamos devolver a conta já criada
            // e ignorar o insert. Não queremos contas com números duplicados
            return new ContaBancoDigitalPayload(contaDb);
        }

        public ContaBancoDigitalPayload DeletarContaBancoDigital(
            CreateContaInput input)
        {
            // Busca se conta existe em banco de dados, a partir de input da API
            var contaDb = _ContaBancoDigitalRepository.GetContaBancoDigital(input.Conta);

            // Caso não exista, devolve erro
            if (contaDb == null)
            {
                // Está sendo utilizado uma forma nativa de devolver erros para o usuário
                // Apenas é necessário escolher um Código e uma Mensagem de retorno
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação inválida - Conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            // Caso sucesso até aqui, só precisamos iniciar a deleção da conta, que não precisa retornar nada
            _ContaBancoDigitalRepository.DeleteContaBancoDigital(contaDb.Id);

            return new ContaBancoDigitalPayload(contaDb);
        }

        public ContaBancoDigitalPayload DepositarContaBancoDigital(ValorContaInput input)
        {
            // Busca se conta existe em banco de dados, a partir de input da API
            var contaDb = _ContaBancoDigitalRepository.GetContaBancoDigital(input.Conta);

            // Caso não exista, devolve erro
            if (contaDb == null)
            {
                // Está sendo utilizado uma forma nativa de devolver erros para o usuário
                // Apenas é necessário escolher um Código e uma Mensagem de retorno
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação inválida - Conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            // Caso sucesso até aqui, vamos adicionar o valor para depósito sobre o saldo atual
            contaDb.Saldo += input.Valor;

            // Por fim, vamos atualizar no a conta para o novo saldo
            _ContaBancoDigitalRepository.UpdateContaBancoDigital(contaDb);

            return new ContaBancoDigitalPayload(contaDb);
        }

        public ContaBancoDigitalPayload SacarContaBancoDigital(ValorContaInput input)
        {
            // Busca se conta existe em banco de dados, a partir de input da API
            var contaDb = _ContaBancoDigitalRepository.GetContaBancoDigital(input.Conta);

            // Caso não exista, devolve erro
            if (contaDb == null)
            {
                // Está sendo utilizado uma forma nativa de devolver erros para o usuário
                // Apenas é necessário escolher um Código e uma Mensagem de retorno
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação inválida - Conta inexistente.")
                        .SetCode("-1")
                        .Build());
            }

            contaDb.Saldo -= input.Valor;

            // Se resultado final for negativo, o usuário não tem saldo suficiente para realizar esse saque
            if (contaDb.Saldo < 0)
            {
                // Está sendo utilizado uma forma nativa de devolver erros para o usuário
                // Apenas é necessário escolher um Código e uma Mensagem de retorno
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage("Solicitação inválida - Saldo insuficiente para esse valor de saque.")
                        .SetCode("-2")
                        .Build());
            }

            // Por fim, vamos atualizar no a conta para o novo saldo
            _ContaBancoDigitalRepository.UpdateContaBancoDigital(contaDb);

            return new ContaBancoDigitalPayload(contaDb);
        }
    }
}
