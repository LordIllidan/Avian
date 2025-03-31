using System;

namespace QuizApp.API.Domain.Models;

public class Answer
{
    public int Id { get; private set; }
    public string Text { get; private set; }
    public bool IsCorrect { get; private set; }

    private Answer() { }

    public Answer(string text, bool isCorrect)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        IsCorrect = isCorrect;
    }
}