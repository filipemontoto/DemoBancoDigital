namespace BancoDigital.Classes
{
    public class ContaBancoDigital
    {
        public int Conta { get; set; }
        public int Saldo { get; set; }

        // Se o construtor é invocado de forma padrão,
        // instancia a conta de identificador 0 e saldo 0
        public ContaBancoDigital()
        {
            Conta = 0;
            Saldo = 0;
        }

        // Se o construtor é invocado com parâmetros,
        // instancia a conta a partir dos parâmetros.
        public ContaBancoDigital(int pConta, int pSaldo)
        {
            Conta = pConta;
            Saldo = pSaldo;
        }
    }
}
