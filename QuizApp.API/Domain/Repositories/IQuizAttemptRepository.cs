using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Domain.Models;

namespace QuizApp.API.Domain.Repositories;

public interface IQuizAttemptRepository
{
    Task<QuizAttempt> GetByIdAsync(int id);
    Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId);
    Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId);
    Task<QuizAttempt> CreateAsync(QuizAttempt attempt);
    Task UpdateAsync(QuizAttempt attempt);
    Task<bool> HasUserAttemptedQuizAsync(int userId, int quizId);
}