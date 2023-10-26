using BancoDigital.GraphQLTypes;
using BancoDigital.Interfaces;
using BancoDigital.Repository;
using BancoDigital.Repository.DbModels;
using BancoDigital.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .Configure<ContaBancoDigitalRepositorySettings>(builder.Configuration.GetSection("MongoContaBancoDigital"))
    .AddSingleton<ContaBancoDigitalRepository>()
    .AddTransient<IContaBancoDigital, ContaBancoDigitalService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());
app.MapGet("/", () => "Hello World!");

app.Run();
