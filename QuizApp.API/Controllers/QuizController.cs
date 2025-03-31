using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.API.Application.Services;
using QuizApp.API.Domain.Models;
using System.Security.Claims;

namespace QuizApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Quiz>> CreateQuiz([FromBody] Quiz quiz)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var createdQuiz = await _quizService.CreateQuizAsync(
            quiz.Title,
            quiz.Description,
            quiz.TimeLimit,
            userId
        );

        return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.Id }, createdQuiz);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var quizzes = await _quizService.GetAvailableQuizzesAsync(userId);
        return Ok(quizzes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int id)
    {
        var quiz = await _quizService.GetQuizAsync(id);
        if (quiz == null)
        {
            return NotFound();
        }

        return Ok(quiz);
    }

    [HttpPost("{id}/attempt")]
    [Authorize(Roles = "Developer")]
    public async Task<ActionResult<QuizAttempt>> StartQuizAttempt(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        try
        {
            var attempt = await _quizService.StartQuizAttemptAsync(id, userId);
            return Ok(attempt);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/attempt/{attemptId}/submit")]
    [Authorize(Roles = "Developer")]
    public async Task<ActionResult<QuizAttempt>> SubmitQuizAttempt(int id, int attemptId, [FromBody] List<QuestionResponse> responses)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        try
        {
            var attempt = await _quizService.SubmitQuizAttemptAsync(id, attemptId, userId, responses);
            return Ok(attempt);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        await _quizService.DeleteQuizAsync(id);
        return NoContent();
    }
}