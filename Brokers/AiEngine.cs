﻿using Microsoft.Extensions.AI;
namespace Blazor.AI.Chat.Brokers;
public class AiEngine(IChatClient ChatClient)
{
    static List<ChatMessage> Conversation => [new(ChatRole.System, "You are a helpful AI assistant, Do not talk a lot, keep it short and simple")];
    public IAsyncEnumerable<StreamingChatCompletionUpdate> StreamResponseAsync(string message)
    {
        Conversation.Add(new(ChatRole.User, message));
        return ChatClient.CompleteStreamingAsync(Conversation); 
    }
    public async ValueTask<string?> GetResponseAsync(string message)
    {
        Conversation.Add(new(ChatRole.User, message));
        return (await ChatClient.CompleteAsync(Conversation)).Message.Text;
    }
}