using BancoDigital.GraphQLTypes;
using BancoDigital.Interfaces;
using BancoDigital.Repository;
using BancoDigital.Repository.MongoDBModels;
using BancoDigital.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .Configure<MongoContaBancoDigitalSettings>(builder.Configuration.GetSection("MongoContaBancoDigital"))
    .AddSingleton<ContaBancoDigitalRepository>()
    .AddTransient<IContaBancoDigital, ContaBancoDigitalService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());
app.MapGet("/", () => "Hello World!");

app.Run();
