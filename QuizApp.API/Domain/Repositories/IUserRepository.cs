using System.Threading.Tasks;
using QuizApp.API.Domain.Models;

namespace QuizApp.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}