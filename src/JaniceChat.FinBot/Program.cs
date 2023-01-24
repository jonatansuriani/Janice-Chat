using JaniceChat.FinBot;
using JaniceChat.FinBot.Consumers;
using JaniceChat.MessageBroker.Abstraction;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<IStockGateway, StockGateway>()
    .AddScoped<IStockParser, StockParser>()
    .AddHttpClient()
    .AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumer<MessageCreatedToSendCommandConsumer>();
        x.AddConsumer<StockCommandConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            var brokerConfig = builder.Configuration.GetMessageBrokerConfiguration();
            cfg.Host(brokerConfig.Host, "/", h =>
            {
                h.Username(brokerConfig.User);
                h.Password(brokerConfig.Password);
            });

            cfg.ConfigureEndpoints(context);
        });
    });

var app = builder.Build();

app.Run();