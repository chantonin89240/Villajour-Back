using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

    [HttpGet("session/{userId}/getlastsession")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChatSessionsAsync(Guid userId)
    {
        return Ok(await semanticKernelService.GetChatSessionsAsync(userId));
    }

    [HttpPost("session")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSession([FromBody] Guid userId)
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
        var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await foreach (var response in semanticKernelService.ChatAsync(userId, chatId, message))
        {
            yield return response;
        }
    }
}
