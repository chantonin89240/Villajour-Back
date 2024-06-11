using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot;

namespace Villajour.External.Chatbot;

internal class SemanticKernelService : ISemanticKernelService
{
    //Semantic Kernel
    private readonly Kernel kernel;
    private readonly IChatSessionRepository chatSessionRepository;
    private readonly IChatParticipantRepository chatParticipantRepository;
    private readonly IChatMessageRepository chatMessageRepository;

    /// <summary>
    /// System prompt to send with user prompts to instruct the model for chat session
    /// </summary>
    private readonly string _systemPrompt = @"
        You are an AI assistant that helps people find information.
        Provide concise answers that are polite and professional.";

    /// <summary>    
    /// System prompt to send with user prompts to instruct the model for summarization
    /// </summary>
    private readonly string _summarizePrompt = @"
        Summarize this text. One to three words maximum length. 
        Plain text only. No punctuation, markup or tags.";


    /// <summary>
    /// Creates a new instance of the Semantic Kernel.
    /// </summary>
    /// <param name="endpoint">Endpoint URI.</param>
    /// <param name="key">Account key.</param>
    /// <param name="completionDeploymentName">Name of the deployed Azure OpenAI completion model.</param>
    /// <param name="embeddingDeploymentName">Name of the deployed Azure OpenAI embedding model.</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoint, key, or modelName is either null or empty.</exception>
    /// <remarks>
    /// This constructor will validate credentials and create a Semantic Kernel instance.
    /// </remarks>
    public SemanticKernelService(Kernel kernel, IChatSessionRepository chatSessionRepository, IChatParticipantRepository chatParticipantRepository, IChatMessageRepository chatMessageRepository)
    {
        this.kernel = kernel;
        this.chatSessionRepository = chatSessionRepository;
        this.chatParticipantRepository = chatParticipantRepository;
        this.chatMessageRepository = chatMessageRepository;
    }

    /// <summary>
    /// Sends the existing conversation to the Semantic Kernel and returns a two word summary.
    /// </summary>
    /// <param name="sessionId">Chat session identifier for the current conversation.</param>
    /// <param name="conversationText">conversation history to send to Semantic Kernel.</param>
    /// <returns>Summarization response from the OpenAI completion model deployment.</returns>
    public async Task<string> SummarizeConversationAsync(string conversation)
    {
        //return await summarizePlugin.SummarizeConversationAsync(conversation, kernel);

        var skChatHistory = new ChatHistory();
        skChatHistory.AddSystemMessage(_summarizePrompt);
        skChatHistory.AddUserMessage(conversation);

        PromptExecutionSettings settings = new()
        {
            ExtensionData = new Dictionary<string, object>()
                    {
                        { "Temperature", 0.0 },
                        { "TopP", 1.0 },
                        { "MaxTokens", 100 }
                    }
        };

        var result = await kernel.GetRequiredService<IChatCompletionService>().GetChatMessageContentAsync(skChatHistory, settings);

        string completion = result.Items[0].ToString()!;

        return completion;
    }

    public async IAsyncEnumerable<string?> ChatAsync(Guid userId, Guid chatId, string message)
    {
        var chatIdString = chatId.ToString();
        var userIdString = userId.ToString();

        ChatSession? chatSession = null;
        if (!await chatSessionRepository.TryFindByIdAsync(chatIdString, callback: c => chatSession = c))
        {
            yield break;
        }

        if (!await chatParticipantRepository.IsUserInChatAsync(userIdString, chatIdString))
        {
            yield break;
        }

        var chatHistory = new ChatHistory();

        chatHistory.AddSystemMessage(_systemPrompt);

        var chatMessages = await chatMessageRepository.FindByChatIdAsync(chatIdString);

        //if (chatMessages.Count() == 1)
        //{
        //    if (chatSession != null)
        //    {
        //        chatSession.Title = await this.SummarizeConversationAsync(message);
        //        await chatSessionRepository.UpsertAsync(chatSession);
        //    }
        //}

        foreach (var chatMessage in chatMessages)
        {
            if (chatMessage.AuthorRole == ChatMessage.AuthorRoles.User)
            {
                chatHistory.AddUserMessage(chatMessage.Content);
            }
            else
            {
                chatHistory.AddAssistantMessage(chatMessage.Content);
            }
        }

        chatHistory.AddUserMessage(message);

        var chat = kernel.GetRequiredService<IChatCompletionService>();

        var response = new List<string>();

        await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            if (item.Content != null)
            {
                yield return item.Content;
                response.Add(item.Content);
            }
        }

        await chatMessageRepository.CreateAsync(new ChatMessage(userIdString, "", chatIdString, message));
        await chatMessageRepository.CreateAsync(new ChatMessage(userIdString, "", chatIdString, string.Concat(response), authorRole: ChatMessage.AuthorRoles.Bot));
    }

    public async Task<Guid> CreateChatSessionAsync(Guid userId)
    {
        var userIdString = userId.ToString();

        var newChat = new ChatSession("Session", "Test");
        await chatSessionRepository.CreateAsync(newChat);

        var chatMessage = ChatMessage.CreateBotResponseMessage(
            newChat.Id,
            string.Empty,
            string.Empty,
            null,
            null);
        await chatMessageRepository.CreateAsync(chatMessage);

        await chatParticipantRepository.CreateAsync(new ChatParticipant(userIdString, newChat.Id));

        return new Guid(newChat.Id);
    }


    public async Task<List<ChatSession>> GetAllChatSessionsAsync(Guid userId)
    {
        var chats = new List<ChatSession>();

        if (!string.IsNullOrWhiteSpace(userId.ToString()))
        {
            var chatParticipants = await chatParticipantRepository.FindByUserIdAsync(userId.ToString());

            foreach (var chatParticipant in chatParticipants)
            {
                ChatSession? chat = null;
                if (await chatSessionRepository.TryFindByIdAsync(chatParticipant.ChatId, callback: v => chat = v))
                {
                    chats.Add(chat!);
                }
            }
        }

        return chats;
    }

    public async Task<List<ChatMessage>?> GetChatMessagesAsync(Guid chatId)
    {
        var chatMessages = await chatMessageRepository.FindByChatIdAsync(chatId.ToString());

        if (!chatMessages.Any())
        {
            return null;
        }

        chatMessages = chatMessages.OrderBy(m => m.Timestamp).Skip(1).ToList();

        return chatMessages.ToList();
    }
}
