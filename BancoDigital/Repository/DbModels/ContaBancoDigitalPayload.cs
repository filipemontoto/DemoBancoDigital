using System;
using System.Net;

namespace BancoDigital.Repository.DbModels
{
    public record ContaBancoDigitalPayload(Contas Conta);
    public record SaldoPayload(int Saldo);
}
