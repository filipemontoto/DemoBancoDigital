using System;
using System.Net;

namespace BancoDigital.Repository.MongoDBModels
{
    public record ContaBancoDigitalPayload(Contas Conta);
    public record SaldoPayload(int Saldo);
}
