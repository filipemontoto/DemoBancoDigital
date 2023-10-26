using MongoDB.Driver;

namespace BancoDigital.Repository.DbModels
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
