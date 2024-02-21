using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<INotificationSenderService, NotificationSenderService>();
builder.Services.AddSingleton<IMessageStatisticService>(new MessageStatisticService(10));
builder.Services.AddTransient<IClientService, ClientService>();

var app = builder.Build();
app.Run();
