using BancoDigital.GraphQLTypes;
using BancoDigital.Interfaces;
using BancoDigital.Repository.MongoDBModels;
using BancoDigital.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .Configure<MongoContaBancoDigitalSettings>(builder.Configuration.GetSection("MongoContaBancoDigital"))
    .AddSingleton<MongoDbService>()
    .AddTransient<IContaBancoDigital, ContaBancoDigitalService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());
app.MapGet("/", () => "Hello World!");

app.Run();
