using System;

namespace QuizApp.API.Domain.Models;

public class QuestionResponse
{
    public int Id { get; private set; }
    public int QuizAttemptId { get; private set; }
    public int QuestionId { get; private set; }
    public int SelectedAnswerId { get; private set; }
    public bool IsCorrect { get; private set; }

    private QuestionResponse() { }

    public QuestionResponse(int quizAttemptId, int questionId, int selectedAnswerId, bool isCorrect)
    {
        QuizAttemptId = quizAttemptId;
        QuestionId = questionId;
        SelectedAnswerId = selectedAnswerId;
        IsCorrect = isCorrect;
    }
}