using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Domain.Models.Quiz;

public class Question
{
    private readonly List<Answer> _answers;
    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    public int Id { get; private set; }

    [Required]
    public string Text { get; private set; }

    public int QuizId { get; private set; }

    public virtual Quiz Quiz { get; private set; }

    private Question()
    {
        _answers = new List<Answer>();
    }

    public Question(string text, List<Answer> answers)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        _answers = answers ?? throw new ArgumentNullException(nameof(answers));
    }

    public void SetQuiz(Quiz quiz)
    {
        Quiz = quiz ?? throw new ArgumentNullException(nameof(quiz));
        QuizId = quiz.Id;
    }

    public bool IsCorrectAnswer(int selectedAnswerId)
    {
        return _answers.Any(a => a.Id == selectedAnswerId && a.IsCorrect);
    }
}