using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models;

public class Quiz
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiresAt { get; set; }

    public int CreatedById { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
}

public class Question
{
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    public int QuizId { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}

public class Answer
{
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}

public class QuizAttempt
{
    public int Id { get; set; }

    public int QuizId { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public int Score { get; set; }

    public virtual ICollection<QuestionResponse> Responses { get; set; } = new List<QuestionResponse>();
}

public class QuestionResponse
{
    public int Id { get; set; }

    public int QuizAttemptId { get; set; }

    public virtual QuizAttempt QuizAttempt { get; set; } = null!;

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public int SelectedAnswerId { get; set; }

    public virtual Answer SelectedAnswer { get; set; } = null!;

    public bool IsCorrect { get; set; }
}