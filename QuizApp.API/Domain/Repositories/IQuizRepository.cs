using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Domain.Models;

namespace QuizApp.API.Domain.Repositories;

public interface IQuizRepository
{
    Task<Quiz> GetByIdAsync(int id);
    Task<IEnumerable<Quiz>> GetAllAsync();
    Task<IEnumerable<Quiz>> GetAvailableForUserAsync(int userId);
    Task<Quiz> CreateAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
}