using BancoDigital.Interfaces;
using BancoDigital.Repository.DbModels;
using HotChocolate.Data;

namespace BancoDigital.GraphQLTypes
{
    public class Query
    {
        private IContaBancoDigital _contaBancoDigital;

        public Query(IContaBancoDigital contaBancoDigital)
        {
            _contaBancoDigital = contaBancoDigital;
        }

        [UseFirstOrDefault]
        public SaldoPayload GetSaldo(
        int conta)
        {
            return _contaBancoDigital.GetContaBancoDigital(conta);
        }
    }
}
