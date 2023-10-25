using BancoDigital.Interfaces;
using BancoDigital.Repository.MongoDBModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BancoDigital.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<MongoDbContaBancoDigital> _contaBancoDigitalCollection;

        public MongoDbService(IOptions<MongoContaBancoDigitalSettings> ContaBancoDigitalSettings)
        {
            var client = new MongoClient(ContaBancoDigitalSettings.Value.ConnectionString);
            var database = client.GetDatabase(ContaBancoDigitalSettings.Value.DatabaseName);
            _contaBancoDigitalCollection = database.GetCollection<MongoDbContaBancoDigital>(ContaBancoDigitalSettings.Value.ContaBancoDigitalCollectionName);
        }

        public MongoDbContaBancoDigital? GetMongoDbContaBancoDigital(int conta)
        {
            var isContaMongoDb = _contaBancoDigitalCollection.Find(x => x.Conta == conta).Any();
            if (isContaMongoDb)
                return _contaBancoDigitalCollection.Find(x => x.Conta == conta).First();
            return null;
        }

        public MongoDbContaBancoDigital? InsertMongoDbContaBancoDigital(CreateContaInput contaInput)
        {

            MongoDbContaBancoDigital conta = new MongoDbContaBancoDigital()
            {
                Conta = contaInput.Conta,
                Saldo = contaInput.Saldo,
            };

            _contaBancoDigitalCollection.InsertOne(conta);

            return conta;
        }

        public void DeleteMongoDbContaBancoDigital(Guid Id)
        {
            var filter = Builders<MongoDbContaBancoDigital>.Filter.Eq("Id", $"{Id}");
            _ = _contaBancoDigitalCollection.DeleteOne(filter);
        }

        public void UpdateMongoDbContaBancoDigital(MongoDbContaBancoDigital conta)
        {
            var filter = Builders<MongoDbContaBancoDigital>.Filter.Eq("Id", $"{conta.Id}");
            var update = Builders<MongoDbContaBancoDigital>.Update.Set("Saldo", conta.Saldo);

            _ = _contaBancoDigitalCollection.UpdateOne(filter, update);
        }
    }
}
