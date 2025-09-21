using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.CommentServices;
using TaskTracker.Application.Model.CommentModels;

namespace TaskTracker.WebAPI.Controllers;

[Route("comments")]
public class CommentController(ICommentService commentService) : ApiBaseController
{
    private readonly ICommentService _commentService = commentService;

    [HttpGet("get/{taskId}")]
    public async Task<IActionResult> GetComments(long taskId)
    {
        var comments = await _commentService.GetComments(taskId);
        return Ok(comments);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto)
    {
        bool isCreated = await _commentService.AddComment(commentDto);
        if (!isCreated) return BadRequest();

        return Ok(isCreated);  
    }

    // [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
    {
        bool isUpdated = await _commentService.UpdateComment(updateCommentDto);
        if (!isUpdated) return BadRequest();

        return Ok(isUpdated);
    }
}
