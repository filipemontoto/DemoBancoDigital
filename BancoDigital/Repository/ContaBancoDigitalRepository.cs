using BancoDigital.Repository.MongoDBModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BancoDigital.Repository
{
    public class ContaBancoDigitalRepository
    {
        private readonly IMongoCollection<Contas> _contaBancoDigitalCollection;

        public ContaBancoDigitalRepository(IOptions<MongoContaBancoDigitalSettings> ContaBancoDigitalSettings)
        {
            // Aplicar configurações de credenciais para acesso ao banco
            var client = new MongoClient(ContaBancoDigitalSettings.Value.ConnectionString);
            var database = client.GetDatabase(ContaBancoDigitalSettings.Value.DatabaseName);
            _contaBancoDigitalCollection = database.GetCollection<Contas>(ContaBancoDigitalSettings.Value.ContaBancoDigitalCollectionName);
        }

        public Contas? GetContaBancoDigital(int conta)
        {
            // Realizar tentativa de busca, a partir da conta
            var isConta = _contaBancoDigitalCollection.Find(x => x.Conta == conta).Any();

            // Caso algum registro seja encontrado, retorna o primeiro registro
            if (isConta)
                return _contaBancoDigitalCollection.Find(x => x.Conta == conta).First();

            // Caso não seja encontrado, retorna nulo
            return null;
        }

        public Contas InsertMongoDbContaBancoDigital(CreateContaInput contaInput)
        {
            // Instanciar a classe que representa a tabela de contas no MongoDB
            // a partir dos dados enviados pela API
            Contas conta = new Contas()
            {
                Conta = contaInput.Conta,
                Saldo = contaInput.Saldo,
            };

            // Insere conta no banco
            _contaBancoDigitalCollection.InsertOne(conta);

            // Se chegou aqui, deu sucesso e podemos afirmar que a conta solicitada, foi criada
            return conta;
        }

        public void DeleteMongoDbContaBancoDigital(Guid Id)
        {
            // A partir do Id enviado pela API, cria um filtro para deleção
            var filter = Builders<Contas>.Filter.Eq("Id", $"{Id}");

            // Como a lógica aplicada na API nos permite
            // podemos confiar que o Id enviado existe na tabela e podemos apenas solicitar a deleção
            _ = _contaBancoDigitalCollection.DeleteOne(filter);
        }

        public void UpdateMongoDbContaBancoDigital(Contas conta)
        {
            // A partir do Id enviado pela API, cria um filtro para atualização
            var filter = Builders<Contas>.Filter.Eq("Id", $"{conta.Id}");

            // Definir qual dado deve ser atualizado e por qual valor
            var update = Builders<Contas>.Update.Set("Saldo", conta.Saldo);

            // Solicitar update do registro
            _ = _contaBancoDigitalCollection.UpdateOne(filter, update);
        }
    }
}
