using BancoDigital.Services;
using BancoDigital.Repository.DbModels;
using Microsoft.Extensions.Options;
using BancoDigital.Repository;
using Microsoft.Extensions.Configuration;

namespace TestesBancoDigital
{
    public class TestesContaBancoDigital
    {
        private ContaBancoDigitalRepository _contaService;

        private ContaBancoDigitalService _service;

        public TestesContaBancoDigital()
        {
            var config = InitConfiguration();
            var repositorySettings = config.GetSection("MongoContaBancoDigital");

            var contaBancoDigitalSettings = new ContaBancoDigitalRepositorySettings
            {
                ConnectionString = repositorySettings.GetSection("ConnectionString").Value,
                DatabaseName = repositorySettings.GetSection("DatabaseName").Value,
                ContaBancoDigitalCollectionName = repositorySettings.GetSection("ContaBancoDigitalCollectionName").Value
            };

            var options = Options.Create(contaBancoDigitalSettings);

            _contaService = new ContaBancoDigitalRepository(options);
            _service = new ContaBancoDigitalService(_contaService);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
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

            Assert.True(mensagemException == "Solicitação inválida - Saldo insuficiente para esse valor de saque.");
            #endregion

            #region Deleção de conta de teste
            _ = _service.DeletarContaBancoDigital(input);
            #endregion
        }
    }
}