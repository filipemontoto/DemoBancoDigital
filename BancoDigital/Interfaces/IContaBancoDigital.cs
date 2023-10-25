﻿using BancoDigital.Classes;
using BancoDigital.Repository.MongoDBModels;
using MongoDB.Driver;

namespace BancoDigital.Interfaces
{
    public interface IContaBancoDigital
    {
        SaldoPayload GetContaBancoDigital(int conta);
        ContaBancoDigitalPayload CreateContaBancoDigital(CreateContaInput input);
        ContaBancoDigitalPayload DepositarContaBancoDigital(ValorContaInput input);
        ContaBancoDigitalPayload SacarContaBancoDigital(ValorContaInput input);
        ContaBancoDigitalPayload DeletarContaBancoDigital(CreateContaInput input);

    }
}