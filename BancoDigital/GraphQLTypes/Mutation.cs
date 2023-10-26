using BancoDigital.Interfaces;
using BancoDigital.Repository.MongoDBModels;

namespace BancoDigital.GraphQLTypes
{
    public class Mutation
    {
        private IContaBancoDigital _contaBancoDigital;

        public Mutation(IContaBancoDigital contaBancoDigital)
        {
            _contaBancoDigital = contaBancoDigital;
        }

        public ContaBancoDigitalPayload CreateConta(
        CreateContaInput input)
        {
            return _contaBancoDigital.CreateContaBancoDigital(input);
        }

        public ContaBancoDigitalPayload DepositarConta(
        ValorContaInput input)
        {
            return _contaBancoDigital.DepositarContaBancoDigital(input);
        }

        public ContaBancoDigitalPayload SacarConta(
        ValorContaInput input)
        {
            return _contaBancoDigital.SacarContaBancoDigital(input);
        }

        public ContaBancoDigitalPayload DeletarConta(
        CreateContaInput input)
        {
            return _contaBancoDigital.DeletarContaBancoDigital(input);
        }
    }
}
