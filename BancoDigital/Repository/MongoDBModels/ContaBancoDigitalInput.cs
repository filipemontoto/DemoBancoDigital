using System.Net;

namespace BancoDigital.Repository.MongoDBModels
{
    public record CreateContaInput(
    int Conta,
    int Saldo);

    public record ValorContaInput(
    int Conta,
    int Valor);
}
