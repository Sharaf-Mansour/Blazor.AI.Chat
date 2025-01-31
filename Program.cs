using Blazor.AI.Chat.Brokers;
using Blazor.AI.Chat.Components;
using Microsoft.Extensions.AI;
using Toolbelt.Blazor.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().Services.AddSingleton<AiEngine>().AddSpeechSynthesis()
    .AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434/"), "deepseek-r1:14b"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error", createScopeForErrors: true).UseHsts();

app.UseHttpsRedirection().UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
