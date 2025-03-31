using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.API.Domain.Models;

public class QuizAttempt
{
    public int Id { get; private set; }
    public int QuizId { get; private set; }
    public int UserId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int Score { get; private set; }
    private readonly List<QuestionResponse> _responses;
    public IReadOnlyCollection<QuestionResponse> Responses => _responses.AsReadOnly();

    private QuizAttempt() { }

    public QuizAttempt(int quizId, int userId)
    {
        QuizId = quizId;
        UserId = userId;
        StartedAt = DateTime.UtcNow;
        _responses = new List<QuestionResponse>();
    }

    public void SubmitResponses(List<QuestionResponse> responses, Quiz quiz)
    {
        if (responses == null || !responses.Any())
            throw new ArgumentException("Responses cannot be empty", nameof(responses));

        if (CompletedAt.HasValue)
            throw new InvalidOperationException("Quiz attempt has already been completed");

        _responses.Clear();
        _responses.AddRange(responses);

        // Calculate score
        Score = responses.Count(r => quiz.Questions
            .FirstOrDefault(q => q.Id == r.QuestionId)
            ?.IsCorrectAnswer(r.SelectedAnswerId) ?? false);

        CompletedAt = DateTime.UtcNow;
    }

    public bool IsCompleted() => CompletedAt.HasValue;
}