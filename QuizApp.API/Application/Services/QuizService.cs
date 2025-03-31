using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Domain.Models;
using QuizApp.API.Domain.Models.Quiz;
using QuizApp.API.Domain.Repositories;

namespace QuizApp.API.Application.Services;

public interface IQuizService
{
    Task<Quiz> CreateQuizAsync(string title, string description, int timeLimit, int createdById);
    Task<Quiz> GetQuizAsync(int id);
    Task<IEnumerable<Quiz>> GetAvailableQuizzesAsync(int userId);
    Task<QuizAttempt> StartQuizAttemptAsync(int quizId, int userId);
    Task<QuizAttempt> SubmitQuizAttemptAsync(int quizId, int attemptId, int userId, List<QuestionResponse> responses);
    Task DeleteQuizAsync(int id);
}

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizAttemptRepository _attemptRepository;

    public QuizService(IQuizRepository quizRepository, IQuizAttemptRepository attemptRepository)
    {
        _quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        _attemptRepository = attemptRepository ?? throw new ArgumentNullException(nameof(attemptRepository));
    }

    public async Task<Quiz> CreateQuizAsync(string title, string description, int timeLimit, int createdById)
    {
        var quiz = new Quiz(title, description, timeLimit, createdById);
        return await _quizRepository.CreateAsync(quiz);
    }

    public async Task<Quiz> GetQuizAsync(int id)
    {
        return await _quizRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Quiz>> GetAvailableQuizzesAsync(int userId)
    {
        return await _quizRepository.GetAvailableForUserAsync(userId);
    }

    public async Task<QuizAttempt> StartQuizAttemptAsync(int quizId, int userId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null)
            throw new ArgumentException("Quiz not found", nameof(quizId));

        if (quiz.IsExpired())
            throw new InvalidOperationException("Quiz has expired");

        var hasAttempted = await _attemptRepository.HasUserAttemptedQuizAsync(userId, quizId);
        if (hasAttempted)
            throw new InvalidOperationException("User has already attempted this quiz");

        var attempt = new QuizAttempt(quizId, userId);
        return await _attemptRepository.CreateAsync(attempt);
    }

    public async Task<QuizAttempt> SubmitQuizAttemptAsync(int quizId, int attemptId, int userId, List<QuestionResponse> responses)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null)
            throw new ArgumentException("Quiz not found", nameof(quizId));

        var attempt = await _attemptRepository.GetByIdAsync(attemptId);
        if (attempt == null)
            throw new ArgumentException("Quiz attempt not found", nameof(attemptId));

        if (attempt.UserId != userId)
            throw new InvalidOperationException("User is not authorized to submit this attempt");

        if (attempt.IsCompleted())
            throw new InvalidOperationException("Quiz attempt has already been completed");

        attempt.SubmitResponses(responses, quiz);
        await _attemptRepository.UpdateAsync(attempt);
        return attempt;
    }

    public async Task DeleteQuizAsync(int id)
    {
        await _quizRepository.DeleteAsync(id);
    }
}