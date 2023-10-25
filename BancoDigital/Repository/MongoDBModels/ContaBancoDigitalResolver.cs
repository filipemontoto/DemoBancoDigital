using MongoDB.Driver;
using System;

namespace BancoDigital.Repository.MongoDBModels
{
    public class ContaBancoDigitalResolver
    {
        public Task<MongoDbContaBancoDigital> ResolveAsync(
        [Service] IMongoCollection<MongoDbContaBancoDigital> collection,
        Guid id)
        {
            return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
