using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Domain.Models;
using QuizApp.API.Domain.Repositories;
using QuizApp.API.Infrastructure.Data;

namespace QuizApp.API.Infrastructure.Repositories;

public class QuizAttemptRepository : IQuizAttemptRepository
{
    private readonly ApplicationDbContext _context;

    public QuizAttemptRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<QuizAttempt> GetByIdAsync(int id)
    {
        return await _context.QuizAttempts
            .Include(a => a.Responses)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId)
    {
        return await _context.QuizAttempts
            .Include(a => a.Responses)
            .Where(a => a.QuizId == quizId)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId)
    {
        return await _context.QuizAttempts
            .Include(a => a.Responses)
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<QuizAttempt> CreateAsync(QuizAttempt attempt)
    {
        await _context.QuizAttempts.AddAsync(attempt);
        await _context.SaveChangesAsync();
        return attempt;
    }

    public async Task UpdateAsync(QuizAttempt attempt)
    {
        _context.Entry(attempt).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasUserAttemptedQuizAsync(int userId, int quizId)
    {
        return await _context.QuizAttempts
            .AnyAsync(a => a.UserId == userId && a.QuizId == quizId);
    }
}