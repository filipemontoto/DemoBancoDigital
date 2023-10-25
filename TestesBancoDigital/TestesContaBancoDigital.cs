using BancoDigital.Services;
using BancoDigital.Repository.MongoDBModels;
using Microsoft.Extensions.Options;

namespace TestesBancoDigital
{
    public class TestesContaBancoDigital
    {
        private MongoDbService _contaService;

        private ContaBancoDigitalService _service;

        public TestesContaBancoDigital()
        {
            var mongoContaBancoDigitalSettings = new MongoContaBancoDigitalSettings
            {
                ConnectionString = "mongodb+srv://DemoFuncional:DemoFuncional@demofuncional.lukthum.mongodb.net/?retryWrites=true&w=majority",
                DatabaseName = "BancoDigital",
                ContaBancoDigitalCollectionName = "Contas"
            };

            var options = Options.Create(mongoContaBancoDigitalSettings);

            _contaService = new MongoDbService(options);
            _service = new ContaBancoDigitalService(_contaService);
        }

        [Fact]
        public async Task Get_ContaExistente()
        {
            #region Criação de conta de teste
            int numeroConta = 999999999;
            int valorInicial = 1024;
            CreateContaInput input = new(numeroConta, valorInicial);

            var conta = _service.CreateContaBancoDigital(input);
            #endregion

            #region Teste
            var retornoGetConta = _service.GetContaBancoDigital(numeroConta);

            Assert.NotNull(retornoGetConta);
            #endregion

            #region Deleção de conta de teste
            _ = _service.DeletarContaBancoDigital(input);
            #endregion
        }

        [Fact]
        public async Task Saca_ContaValida_ValorValido()
        {
            #region Criação de conta de teste
            int numeroConta = 999999999;
            int valorInicial = 1024;
            CreateContaInput input = new(numeroConta, valorInicial);

            var conta = _service.CreateContaBancoDigital(input);
            #endregion

            #region Inicialização dados do teste
            int valorParaSacar = 10;
            ValorContaInput inputValor = new(numeroConta, valorParaSacar);
            #endregion

            #region Teste
            var retornoGetConta = _service.GetContaBancoDigital(numeroConta);
            var saldoInicial = retornoGetConta.Saldo;

            var retornoSacarConta = _service.SacarContaBancoDigital(inputValor);
            var saldoFinal = retornoSacarConta.Conta.Saldo;

            Assert.True(saldoFinal == saldoInicial-valorParaSacar);
            #endregion

            #region Deleção de conta de teste
            _ = _service.DeletarContaBancoDigital(input);
            #endregion
        }

        [Fact]
        public async Task Deposita_ContaValida_ValorValido()
        {
            #region Criação de conta de teste
            int numeroConta = 999999999;
            int valorInicial = 1024;
            CreateContaInput input = new(numeroConta, valorInicial);

            var conta = _service.CreateContaBancoDigital(input);
            #endregion

            #region Inicialização dados do teste
            int valorParaDepositar = 256;
            ValorContaInput inputValor = new(numeroConta, valorParaDepositar);
            #endregion

            #region Teste
            var retornoGetConta = _service.GetContaBancoDigital(numeroConta);
            var saldoInicial = retornoGetConta.Saldo;

            var retornoSacarConta = _service.DepositarContaBancoDigital(inputValor);
            var saldoFinal = retornoSacarConta.Conta.Saldo;

            Assert.True(saldoFinal == saldoInicial + valorParaDepositar);
            #endregion

            #region Deleção de conta de teste
            _ = _service.DeletarContaBancoDigital(input);
            #endregion
        }

        [Fact]
        public async Task Saca_ContaValida_ValorInValido()
        {
            #region Criação de conta de teste
            int numeroConta = 999999999;
            int valorInicial = 256;
            CreateContaInput input = new(numeroConta, valorInicial);

            var conta = _service.CreateContaBancoDigital(input);
            #endregion

            #region Inicialização dados do teste
            int valorParaSacar = 300;
            ValorContaInput inputValor = new(numeroConta, valorParaSacar);
            var mensagemException = string.Empty;
            #endregion

            #region Teste
            try
            {
                _ = _service.SacarContaBancoDigital(inputValor);
            }
            catch (Exception e)
            {
                mensagemException = e.Message;
            }

            Assert.True(mensagemException == "Saldo insuficiente para esse valor de saque.");
            #endregion

            #region Deleção de conta de teste
            _ = _service.DeletarContaBancoDigital(input);
            #endregion
        }
    }
}