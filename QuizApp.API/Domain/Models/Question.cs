using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.API.Domain.Models;

public class Question
{
    public int Id { get; private set; }
    public string Text { get; private set; }
    private readonly List<Answer> _answers;
    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    private Question() { }

    public Question(string text, List<Answer> answers)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        _answers = answers ?? throw new ArgumentNullException(nameof(answers));

        if (!_answers.Any())
            throw new ArgumentException("Question must have at least one answer", nameof(answers));

        if (!_answers.Any(a => a.IsCorrect))
            throw new ArgumentException("Question must have at least one correct answer", nameof(answers));
    }

    public bool IsCorrectAnswer(int answerId)
    {
        var answer = _answers.FirstOrDefault(a => a.Id == answerId);
        return answer?.IsCorrect ?? false;
    }
}