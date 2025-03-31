using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.API.Domain.Models;

public class Quiz
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int TimeLimit { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public int CreatedById { get; private set; }
    private readonly List<Question> _questions;
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    private Quiz() { }

    public Quiz(string title, string description, int timeLimit, int createdById)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        TimeLimit = timeLimit;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
        _questions = new List<Question>();
    }

    public void AddQuestion(string text, List<Answer> answers)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Question text cannot be empty", nameof(text));

        if (answers == null || !answers.Any())
            throw new ArgumentException("Question must have at least one answer", nameof(answers));

        if (!answers.Any(a => a.IsCorrect))
            throw new ArgumentException("Question must have at least one correct answer", nameof(answers));

        var question = new Question(text, answers);
        _questions.Add(question);
    }

    public void SetExpirationDate(DateTime expirationDate)
    {
        if (expirationDate <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future", nameof(expirationDate));

        ExpiresAt = expirationDate;
    }

    public bool IsExpired() => ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow;
}