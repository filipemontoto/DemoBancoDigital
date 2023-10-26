namespace BancoDigital.Repository.DbModels
{
    [Node(
    IdField = nameof(Id),
    NodeResolverType = typeof(ContaBancoDigitalResolver),
    NodeResolver = nameof(ContaBancoDigitalResolver.ResolveAsync))]
    public class Contas
    {
        // Atributos da conta
        // De acordo com a necessidade do Cliente,
        // apenas precisamos do identificador "conta"
        // e o saldo 
        public Guid Id { get; init; }
        public int Conta { get; set; }
        public int Saldo { get; set; }

        // Se o construtor é invocado de forma padrão,
        // instancia a conta de identificador 0 e saldo 0
        public Contas()
        {
            Conta = 0;
            Saldo = 0;
        }

        // Se o construtor é invocado com parâmetros,
        // instancia a conta a partir dos parâmetros.
        public Contas(int pConta, int pSaldo)
        {
            Conta = pConta;
            Saldo = pSaldo;
        }
    }
}
