using Microsoft.AspNetCore.Mvc;
using Villajour.Application.Chatbot.Interfaces;

namespace Villajour.API.Controllers;

public class ChatBotController : ApiControllerBase
{
    private ISemanticKernelService semanticKernelService;

    public ChatBotController(ISemanticKernelService semanticKernel)
    {
        this.semanticKernelService = semanticKernel;
    }

    [HttpGet("session/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllChatSessionsAsync(Guid userId)
    {
        return Ok(await semanticKernelService.GetAllChatSessionsAsync(userId));
    }

    [HttpPost("session/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSession(Guid userId)
    {
        return Ok(await semanticKernelService.CreateChatSessionAsync(userId));
    }

    [HttpGet("{chatId}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChatMessagesAsync(Guid chatId)
    {
        var chatMessages = await semanticKernelService.GetChatMessagesAsync(chatId);

        return chatMessages != null ? Ok(chatMessages) : NotFound();
    }

    [HttpPost("{chatId}/messages")]
    public async IAsyncEnumerable<string?> ChatAsync([FromBody] string message,[FromRoute] Guid chatId)
    {
        var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        await foreach (var response in semanticKernelService.ChatAsync(userId, chatId, message))
        {
            yield return response;
        }
    }
}
