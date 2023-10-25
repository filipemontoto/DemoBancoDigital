namespace BancoDigital.Repository.MongoDBModels
{
    public class MongoContaBancoDigitalSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ContaBancoDigitalCollectionName { get; set; } = null!;
    }
}
