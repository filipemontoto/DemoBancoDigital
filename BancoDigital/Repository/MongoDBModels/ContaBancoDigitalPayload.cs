using System;
using System.Net;

namespace BancoDigital.Repository.MongoDBModels
{
    public record ContaBancoDigitalPayload(MongoDbContaBancoDigital Conta);
    public record SaldoPayload(int Saldo);
}
