using Microsoft.Extensions.AI;
namespace Blazor.AI.Chat.Brokers;
public class AiEngine(IChatClient ChatClient)
{
    public List<ChatMessage> Conversation { get; private set; } = [new(ChatRole.System, "You are a helpful AI assistant, Do not talk a lot, keep it short and simple")];
    public IAsyncEnumerable<ChatResponseUpdate> StreamResponseAsync(string message)
    {
        Conversation.Add(new(ChatRole.User, message));
        return ChatClient.GetStreamingResponseAsync(Conversation);
    }
    public async ValueTask<string?> GetResponseAsync(string message)
    {
        Conversation.Add(new(ChatRole.User, message));
        return (await ChatClient.GetResponseAsync(Conversation)).Text?.Trim();
    }
}