using System;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Domain.Models.Quiz;

public class QuestionResponse
{
    public int Id { get; private set; }

    public int QuizAttemptId { get; private set; }

    public virtual QuizAttempt QuizAttempt { get; private set; }

    public int QuestionId { get; private set; }

    public virtual Question Question { get; private set; }

    public int SelectedAnswerId { get; private set; }

    public virtual Answer SelectedAnswer { get; private set; }

    public bool IsCorrect { get; private set; }

    private QuestionResponse() { }

    public QuestionResponse(Question question, Answer selectedAnswer)
    {
        Question = question ?? throw new ArgumentNullException(nameof(question));
        QuestionId = question.Id;
        SelectedAnswer = selectedAnswer ?? throw new ArgumentNullException(nameof(selectedAnswer));
        SelectedAnswerId = selectedAnswer.Id;
        IsCorrect = selectedAnswer.IsCorrect;
    }

    public void SetQuizAttempt(QuizAttempt quizAttempt)
    {
        QuizAttempt = quizAttempt ?? throw new ArgumentNullException(nameof(quizAttempt));
        QuizAttemptId = quizAttempt.Id;
    }
}