namespace BancoDigital.Repository.DbModels
{

    public record CreateContaInput(
    int Conta,
    int Saldo);

    public record ValorContaInput(
    int Conta,
    int Valor);
}
