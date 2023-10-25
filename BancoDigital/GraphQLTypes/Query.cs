using BancoDigital.Interfaces;
using BancoDigital.Repository.MongoDBModels;
using BancoDigital.Services;
using HotChocolate.Data;
using MongoDB.Driver;
using System;

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
