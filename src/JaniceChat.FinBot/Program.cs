using JaniceChat.FinBot;
using JaniceChat.FinBot.Consumers;
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
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ConfigureEndpoints(context);
        });
    });

var app = builder.Build();

app.Run();