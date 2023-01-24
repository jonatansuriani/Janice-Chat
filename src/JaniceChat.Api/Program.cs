using JaniceChat.Api.Models;
using JaniceChat.Domain;
using JaniceChat.MessageBroker;
using JaniceChat.MessageBroker.Abstraction;
using JaniceChat.MessageBroker.Handlers;
using JaniceChat.Repository;
using JaniceChat.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors()
    .AddDatabaseProvider(builder.Configuration)
    .AddRepositories(builder.Configuration)
    .AddApplicationServices()
    .AddSignalRServices()
    .AddAutoMapper(x=> 
    {
        x.CreateMap<ChatRoom, ChatRoomModel>();
        x.CreateMap<ChatMessage, ChatMessageModel>();
        x.AddServiceMapperConfiguration();
    })
    .AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();
        
        x.AddConsumer<SendMessageConsumer>();
        x.AddConsumer<MessageCreatedToSignalRConsumer>();

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
    })
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.MapHub<ChatHub>("/Chat");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChatContext>();
    db.ChatRooms.Add(new ChatRoom() { Name = "Default" });
    db.Users.Add(new User() { UserName = "FinBot" });
    db.SaveChanges();
}

app.Run();
