using System;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Domain.Models.Quiz;

public class Answer
{
    public int Id { get; private set; }

    [Required]
    public string Text { get; private set; }

    public bool IsCorrect { get; private set; }

    public int QuestionId { get; private set; }

    public virtual Question Question { get; private set; }

    private Answer() { }

    public Answer(string text, bool isCorrect)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        IsCorrect = isCorrect;
    }

    public void SetQuestion(Question question)
    {
        Question = question ?? throw new ArgumentNullException(nameof(question));
        QuestionId = question.Id;
    }
}