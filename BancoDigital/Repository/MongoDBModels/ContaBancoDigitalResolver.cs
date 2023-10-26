using MongoDB.Driver;

namespace BancoDigital.Repository.MongoDBModels
{
    public class ContaBancoDigitalResolver
    {
        public Task<Contas> ResolveAsync(
        [Service] IMongoCollection<Contas> collection,
        Guid id)
        {
            return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
